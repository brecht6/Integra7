using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSNDrumKitCommonCompEQ : DomainBase
{
    public DomainSNDrumKitCommonCompEQ(int zeroBasedPart, IIntegra7Api integra7Api,
        Integra7StartAddresses startAddresses, Integra7Parameters parameters, SemaphoreSlim semaphore)
        : base(integra7Api, startAddresses, parameters,
            $"Temporary Tone Part {zeroBasedPart + 1}",
            "Offset/Temporary SuperNATURAL Drum Kit",
            "Offset2/SuperNATURAL Drum Kit Common Comp-EQ",
            "SuperNATURAL Drum Kit Common Comp-EQ/",
            semaphore)
    {
    }
}