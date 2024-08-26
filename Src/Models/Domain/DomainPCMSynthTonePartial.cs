using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainPCMSynthTonePartial : DomainBase
{
    public DomainPCMSynthTonePartial(int zeroBasedPart, int zeroBasedPartial, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    : base(integra7Api, startAddresses, parameters, $"Temporary Tone Part {zeroBasedPart + 1}", $"Offset/PCM Synth Tone Partial {zeroBasedPartial + 1}", "PCM Synth Tone Partial/")
    {
    }
}

