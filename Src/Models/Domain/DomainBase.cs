using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;


namespace Integra7AuralAlchemist.Models.Domain;

public class DomainBase
{
    private readonly IIntegra7Api _integra7Api;
    private readonly Integra7StartAddresses _startAddresses;
    private readonly Integra7Parameters _parameters;
    private readonly List<FullyQualifiedParameter> _domainParameters = [];

    public DomainBase(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, string startAddressName, string offsetAddressName, string parameterNamePrefix)
    {
        _integra7Api = integra7Api;
        _startAddresses = startAddresses;
        _parameters = parameters;
        List<Integra7ParameterSpec> relevant = parameters.GetParametersWithPrefix(parameterNamePrefix);
        for (int i = 0; i < relevant.Count; i++)
        {
            _domainParameters.Add(new FullyQualifiedParameter(startAddressName, offsetAddressName, relevant[i]));
        }
    }

    public void ReadFromIntegra()
    {
        FullyQualifiedParameterRange r = new FullyQualifiedParameterRange("Setup", "Offset/Setup Sound Mode",
                                                                          _domainParameters[0].ParSpec,
                                                                          _domainParameters.Last().ParSpec);
        r.RetrieveFromIntegra(_integra7Api, _startAddresses, _parameters);
        for (int i = 0; i < r.Range.Count; i++)
        {
            _domainParameters[i].CopyParsedDataFrom(r.Range[i]);
        }
    }

    public void WriteToIntegra()
    {
        FullyQualifiedParameterRange r = new FullyQualifiedParameterRange("Setup", "Offset/Setup Sound Mode",
                                                                          _domainParameters[0].ParSpec,
                                                                          _domainParameters.Last().ParSpec);
        r.Initialize(_domainParameters);
        r.WriteToIntegra(_integra7Api, _startAddresses, _parameters);
    }

    public void ReadFromIntegra(Integra7ParameterSpec singleParameter)
    {
        bool found = false;
        for (int i = 0; i < _domainParameters.Count && !found; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ParSpec.IsSameAs(singleParameter))
            {
                found = true;
                p.RetrieveFromIntegra(_integra7Api, _startAddresses, _parameters);
                p.DebugLog();
            }
        }
    }

    public void WriteToIntegra(Integra7ParameterSpec singleParameter)
    {
        bool found = false;
        for (int i = 0; i < _domainParameters.Count && !found; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ParSpec.IsSameAs(singleParameter))
            {
                found = true;
                p.WriteToIntegra(_integra7Api, _startAddresses, _parameters);
                p.DebugLog();
            }
        }
    }

    public void ModifySingleParameterDisplayedValue(string parameterName, string displayedValue)
    {
        bool found = false;
        for (int i = 0; i < _domainParameters.Count && !found; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ParSpec.Path  == parameterName)
            {
                found = true;
                if (p.IsNumeric)
                {
                    if (p.ParSpec.Repr != null)
                    {
                        var key = p.ParSpec.Repr
                            .Where(keyvaluepair => keyvaluepair.Value == displayedValue)
                            .Select(keyvaluepair => keyvaluepair.Key)
                            .ToList();
                        if (key.Count == 0)
                        {
                            Debug.Assert(false);
                        }
                        p.RawNumericValue = key.First();
                        p.StringValue = displayedValue;
                    }
                    else if (p.ParSpec.IMin != p.ParSpec.OMin || p.ParSpec.IMax != p.ParSpec.OMax)
                    {
                        // need to unmap mapped value to raw value
                        p.RawNumericValue = (long)Mapping.linlin(long.Parse(displayedValue), p.ParSpec.OMin, p.ParSpec.OMax, p.ParSpec.IMin, p.ParSpec.IMax, true);
                        p.StringValue = displayedValue;
                    }
                    else
                    {
                        p.RawNumericValue = long.Parse(displayedValue);
                        p.StringValue = displayedValue;
                    }
                }
                else
                {
                    p.StringValue = displayedValue[..p.ParSpec.Bytes]; // clip string to max length
                }

                p.DebugLog();
            }
        }
    }

}
