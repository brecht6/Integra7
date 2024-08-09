using System.Collections.Generic;
using System.Linq;
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
        for (int i = 0; i < relevant.Count; i++)
        {
            _domainParameters.Add(new FullyQualifiedParameter("Setup", "Offset/Setup Sound Mode", relevant[i]));
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

}