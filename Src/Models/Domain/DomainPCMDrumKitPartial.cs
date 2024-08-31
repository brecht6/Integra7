using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainPCMDrumKitPartial : DomainBase
{
    public DomainPCMDrumKitPartial(int zeroBasedPart, int zeroBasedPartial, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
        : base(integra7Api, startAddresses, parameters, 
            startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}", 
            offsetAddressName:"Offset/Temporary PCM Drum Kit",
            offset2AddressName:$"Offset2/PCM Drum Kit Partial {zeroBasedPartial + 1}", 
            parameterNamePrefix:"PCM Drum Kit Partial/")
    {
    }
}