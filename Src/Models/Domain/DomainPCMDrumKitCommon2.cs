using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainPCMDrumKitCommon2 : DomainBase
{
    public DomainPCMDrumKitCommon2(int zeroBasedPart, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, SemaphoreSlim semaphore)
        : base(integra7Api, startAddresses, parameters, 
            startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}",
            offsetAddressName:"Offset/Temporary PCM Drum Kit",  
            offset2AddressName:"Offset2/PCM Drum Kit Common 2", 
            parameterNamePrefix:"PCM Drum Kit Common 2/",
            semaphore)
    {
    }
}