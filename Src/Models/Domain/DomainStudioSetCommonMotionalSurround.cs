using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainStudioSetCommonMotionalSurround : DomainBase
{
    public DomainStudioSetCommonMotionalSurround(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    : base(integra7Api, startAddresses, parameters, 
        startAddressName:"Temporary Studio Set", 
        offsetAddressName:"Offset/Not Used", 
        offset2AddressName: "Offset2/Studio Set Common Motional Surround", 
        parameterNamePrefix:"Studio Set Common Motional Surround/")
    {
    }
}
