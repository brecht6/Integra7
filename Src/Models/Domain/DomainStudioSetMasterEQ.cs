using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainStudioSetMasterEQ : DomainBase
{
    public DomainStudioSetMasterEQ(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    : base(integra7Api, startAddresses, parameters, 
        startAddressName:"Temporary Studio Set", 
        offsetAddressName:"Offset/Not Used", 
        offset2AddressName: "Offset2/Studio Set Master EQ", 
        parameterNamePrefix:"Studio Set Master EQ/")
    {
    }
}
