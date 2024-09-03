using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSNDrumKitCommonCompEQ : DomainBase
{
    public DomainSNDrumKitCommonCompEQ(int zeroBasedPart, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
        : base(integra7Api, startAddresses, parameters, 
            startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}", 
            offsetAddressName:"Offset/Temporary SuperNATURAL Drum Kit",
            offset2AddressName:"Offset2/SuperNATURAL Drum Kit Common Comp-EQ", 
            parameterNamePrefix:"SuperNATURAL Drum Kit Common Comp-EQ/")
    {
    }
}