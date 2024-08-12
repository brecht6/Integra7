using System.Collections.Generic;

namespace Integra7AuralAlchemist.Models.Data;

public class ParserContext
{
    private IDictionary<string, string> _parameterNameToparameterValue = new Dictionary<string, string>();

    public void Clear()
    {
        _parameterNameToparameterValue.Clear();
    }
    public void Register(string parameterName, string parameterValue)
    {
        _parameterNameToparameterValue[parameterName] = parameterValue;
    }
    public string Lookup(string parameterName)
    {
        if (Contains(parameterName))
        {
            return _parameterNameToparameterValue[parameterName];
        }
        return "";
    }
    public bool Contains(string parameterName)
    {
        return _parameterNameToparameterValue.ContainsKey(parameterName);
    }
    public void InitializeFromExistingData(List<FullyQualifiedParameter> pars)
    {
        Clear();
        foreach(FullyQualifiedParameter p in pars)
        {
            if (p.ParSpec.Store)
            {
                Register(p.ParSpec.Path, p.StringValue);
            }
        }
    }
}