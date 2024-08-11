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
        FullyQualifiedParameterRange r = new FullyQualifiedParameterRange(_domainParameters[0].Start,
                                                                          _domainParameters[0].Offset,
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
        FullyQualifiedParameterRange r = new FullyQualifiedParameterRange(_domainParameters[0].Start,
                                                                          _domainParameters[0].Offset,
                                                                          _domainParameters[0].ParSpec,
                                                                          _domainParameters.Last().ParSpec);
        r.Initialize(_domainParameters);
        r.WriteToIntegra(_integra7Api, _startAddresses, _parameters);
    }

    public FullyQualifiedParameter? ReadFromIntegra(string parameterName)
    {
        bool found = false;
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_domainParameters);

        for (int i = 0; i < _domainParameters.Count && !found; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ValidInContext(ctx) && p.ParSpec.Path == parameterName)
            {
                found = true;
                p.RetrieveFromIntegra(_integra7Api, _startAddresses, _parameters);
                p.DebugLog();
                return p;
            }
        }
        if (!found)
        {
            Debug.WriteLine($"parameter {parameterName} does not exist, or is not valid in the current context.");
        }
        return null;
    }

    public void WriteToIntegra(string parameterName)
    {
        bool found = false;
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_domainParameters);
        for (int i = 0; i < _domainParameters.Count && !found; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ValidInContext(ctx) && p.ParSpec.Path == parameterName)
            {
                found = true;
                p.WriteToIntegra(_integra7Api, _startAddresses, _parameters);
                p.DebugLog();
            }
        }
        if (!found)
        {
            Debug.WriteLine($"parameter {parameterName} does not exist, or is not valid in the current context.");
        }
    }

    public void WriteToIntegra(string parameterName, string displayedValue)
    {
        ModifySingleParameterDisplayedValue(parameterName, displayedValue);
        WriteToIntegra(parameterName);
    }

    public string LookupSingleParameterDisplayedValue(string parameterName)
    {
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_domainParameters);

        for (int i = 0; i < _domainParameters.Count; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ValidInContext(ctx) && p.ParSpec.Path == parameterName)
            {
                return p.StringValue;
            }
        }
        return "";
    }

    public void ModifySingleParameterDisplayedValue(string parameterName, string displayedValue)
    {
        bool found = false;
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_domainParameters);

        for (int i = 0; i < _domainParameters.Count && !found; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (p.ValidInContext(ctx) && p.ParSpec.Path == parameterName)
            {
                found = true;
                DisplayValueToRawValueConverter.UpdateFromDisplayedValue(displayedValue, p);
                p.DebugLog();
            }
        }
        if (!found)
        {
            // did you try to update a parameter that simply does not exist?
            // or did you try to update a data dependent parameter while the master parameter was set to a
            // value that makes this parameter inaccessible?
            Debug.Assert(false, $"Parameter {parameterName} does not exist or is not valid in the current context.");
        }
    }

    List<string> GetParameterNames(bool IncludeReserved = false, bool IncludeInvalidIncontext = false)
    {
        List<string> names = [];
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_domainParameters);

        for (int i = 0; i < _domainParameters.Count; i++)
        {
            Integra7ParameterSpec p = _domainParameters[i].ParSpec;
            if (_domainParameters[i].ValidInContext(ctx) || IncludeInvalidIncontext)
            {
                if (p.Reserved && IncludeReserved || !p.Reserved)
                {
                    names.Add(p.Path);
                }
            }
        }
        return names;
    }

}
