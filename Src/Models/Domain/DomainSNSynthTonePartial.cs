using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSNSynthTonePartial : DomainBase
{
    public DomainSNSynthTonePartial(int zeroBasedPart, int zeroBasedPartial, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, SemaphoreSlim semaphore)
    : base(integra7Api, startAddresses, parameters, 
        startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}", 
        offsetAddressName:"Offset/Temporary SuperNATURAL Synth Tone",
        offset2AddressName:$"Offset2/SuperNATURAL Synth Tone Partial {zeroBasedPartial + 1}", 
        parameterNamePrefix:"SuperNATURAL Synth Tone Partial/",
        semaphore)
    {
    }
}

