using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainPCMSynthToneCommon : DomainBase
{
    public DomainPCMSynthToneCommon(int zeroBasedPart, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    : base(integra7Api, startAddresses, parameters, $"Temporary Tone Part {zeroBasedPart + 1}", "Offset/PCM Synth Tone Common", "PCM Synth Tone Common/")
    {
    }
}

