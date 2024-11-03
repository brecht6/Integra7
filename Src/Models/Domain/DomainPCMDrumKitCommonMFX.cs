using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainPCMDrumKitCommonMFX : DomainBase
{
    public DomainPCMDrumKitCommonMFX(int zeroBasedPart, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses,
        Integra7Parameters parameters, SemaphoreSlim semaphore)
        : base(integra7Api, startAddresses, parameters,
            $"Temporary Tone Part {zeroBasedPart + 1}",
            "Offset/Temporary PCM Drum Kit",
            "Offset2/PCM Drum Kit Common MFX",
            "PCM Drum Kit Common MFX/",
            semaphore)
    {
    }
}