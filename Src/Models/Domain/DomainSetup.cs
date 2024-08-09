using System.Collections.Generic;
using System.Diagnostics;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;


namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSetup
{
    private readonly IIntegra7Api _integra7Api;
    private readonly Integra7StartAddresses _startAddresses;
    private readonly Integra7Parameters _parameters;
    private readonly List<FullyQualifiedParameter> _domainParameters = [];

    public DomainSetup(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        _integra7Api = integra7Api;
        _startAddresses = startAddresses;
        _parameters = parameters;
        List<Integra7ParameterSpec> relevant = parameters.GetParametersWithPrefix("Setup/");
        for (int i=0; i < relevant.Count; i++)
        {
            _domainParameters.Add(new FullyQualifiedParameter("Setup", "Offset/Setup Sound Mode", relevant[i]));
        }
    }

    public void ReadFromIntegra()
    {
        for (int i=0; i < _domainParameters.Count; i++)
        {
            FullyQualifiedParameter p = _domainParameters[i];
            if (!p.ParSpec.Reserved)
            {
                p.RetrieveFromIntegra(_integra7Api, _startAddresses, _parameters);  
                if (p.IsNumeric)      
                {
                    Debug.WriteLine($"Read parameter {p.ParSpec.Path} and found value raw {p.RawNumericValue} (mapped: {p.StringValue})");
                }
                else
                {
                    Debug.WriteLine($"Read parameter {p.ParSpec.Path} and found value {p.StringValue}");
                }
            }
        }
    }

}