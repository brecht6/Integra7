using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainPCMSynthTonePartial : DomainBase
{
    public DomainPCMSynthTonePartial(int zeroBasedPart, int zeroBasedPartial, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, SemaphoreSlim semaphore)
    : base(integra7Api, startAddresses, parameters, 
        startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}", 
        offsetAddressName:"Offset/Temporary PCM Synth Tone",
        offset2AddressName:$"Offset2/PCM Synth Tone Partial {zeroBasedPartial + 1}", 
        parameterNamePrefix:"PCM Synth Tone Partial/",
        semaphore)
    {
    }
}

