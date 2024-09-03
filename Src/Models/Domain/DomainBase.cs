using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;
using Serilog;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainBase
{
    private readonly IIntegra7Api _integra7Api;
    private readonly Integra7StartAddresses _startAddresses;
    private readonly Integra7Parameters _parameters;
    private readonly string _startAddressName;
    public string StartAddressName => _startAddressName;
    private readonly string _offsetAddressName;
    public string OffsetAddressName => _offsetAddressName;
    private readonly string _offset2AddressName;
    public string Offset2AddressName => _offset2AddressName;

    private readonly List<FullyQualifiedParameter> _domainParameters = [];

    public DomainBase(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, 
        string startAddressName, string offsetAddressName, string offset2AddressName, string parameterNamePrefix)
    {
        _integra7Api = integra7Api;
        _startAddresses = startAddresses;
        _parameters = parameters;
        _startAddressName = startAddressName;
        _offsetAddressName = offsetAddressName;
        _offset2AddressName = offset2AddressName;
        

        List<Integra7ParameterSpec> relevant = parameters.GetParametersWithPrefix(parameterNamePrefix);
        for (int i = 0; i < relevant.Count; i++)
        {
            _domainParameters.Add(new FullyQualifiedParameter(startAddressName, offsetAddressName, offset2AddressName, relevant[i]));
        }
    }

    public void ReadFromIntegra()
    {
        Log.Debug($"Reading range of parameters (start address:{_domainParameters[0].Start}, offset address: {_domainParameters[0].Offset}, offset2 address: {_domainParameters[0].Offset2}) between {_domainParameters[0].ParSpec.Path} and {_domainParameters.Last().ParSpec.Path} from integra.");
        FullyQualifiedParameterRange r = new FullyQualifiedParameterRange(_domainParameters[0].Start,
                                                                          _domainParameters[0].Offset,
                                                                          _domainParameters[0].Offset2,
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
        Log.Debug($"Writing range of parameters (start address:{_domainParameters[0].Start}, offset address: {_domainParameters[0].Offset}), offset2 address: {_domainParameters[0].Offset2} between {_domainParameters[0].ParSpec.Path} and {_domainParameters.Last().ParSpec.Path} to integra.");
        FullyQualifiedParameterRange r = new FullyQualifiedParameterRange(_domainParameters[0].Start,
                                                                          _domainParameters[0].Offset,
                                                                          _domainParameters[0].Offset2,
                                                                          _domainParameters[0].ParSpec,
                                                                          _domainParameters.Last().ParSpec);
        r.Initialize(_domainParameters);
        r.WriteToIntegra(_integra7Api, _startAddresses, _parameters);
    }

    public FullyQualifiedParameter? ReadFromIntegra(string parameterName)
    {
        Log.Debug($"Reading single parameter {parameterName}, (start address:{_domainParameters[0].Start}, offset address: {_domainParameters[0].Offset}), offset2 address: {_domainParameters[0].Offset2}) from integra.");
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
            Log.Error($"parameter {parameterName} does not exist, or is not valid in the current context.");
        }
        return null;
    }

    public void WriteToIntegra(string parameterName)
    {
        Log.Debug($"Writing single parameter {parameterName}, (start address:{_domainParameters[0].Start}, offset address: {_domainParameters[0].Offset}), offset2 address: {_domainParameters[0].Offset2}) to integra.");
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
            Log.Error($"parameter {parameterName} does not exist, or is not valid in the current context.");
        }
    }

    public void WriteToIntegra(string parameterName, string displayedValue)
    {
        ModifySingleParameterDisplayedValue(parameterName, displayedValue);
        WriteToIntegra(parameterName);
    }

    public string LookupSingleParameterDisplayedValue(string parameterName)
    {
        Log.Debug($"Look up value of parameter {parameterName}");
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
        Log.Error($"Could not find the value of parameter {parameterName}");
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
            // or did you try to update a data dependent parameter while the parent parameter was set to a
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

    public List<FullyQualifiedParameter> GetRelevantParameters(bool IncludeReserved = false, bool IncludeInvalidIncontext = false)
    {
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_domainParameters);
        List<FullyQualifiedParameter> pars = [];
        for (int i = 0; i < _domainParameters.Count; i++)
        {
            Integra7ParameterSpec p = _domainParameters[i].ParSpec;
            if (_domainParameters[i].ValidInContext(ctx) || IncludeInvalidIncontext)
            {
                if (p.Reserved && IncludeReserved || !p.Reserved)
                {
                    pars.Add(_domainParameters[i]);
                }
            }
        }
        return pars;
    }

}
