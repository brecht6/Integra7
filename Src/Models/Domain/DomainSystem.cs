using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSystem : DomainBase
{
    public DomainSystem(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters) 
    : base(integra7Api, startAddresses, parameters, "System", "Offset/System Common", "System Common/")
    {
    }
}
