using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSNDrumKitPartial : DomainBase
{
    public DomainSNDrumKitPartial(int zeroBasedPart, int zeroBasedPartial, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, SemaphoreSlim semaphore)
        : base(integra7Api, startAddresses, parameters, 
            startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}",
            offsetAddressName:"Offset/Temporary SuperNATURAL Drum Kit",
            offset2AddressName:$"Offset2/SuperNATURAL Drum Kit Partial {zeroBasedPartial + 1}", 
            parameterNamePrefix:"SuperNATURAL Drum Kit Partial/",
            semaphore)
    {
    }
}