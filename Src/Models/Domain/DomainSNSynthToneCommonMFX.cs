using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSNSynthToneCommonMFX : DomainBase
{
    public DomainSNSynthToneCommonMFX(int zeroBasedPart, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    : base(integra7Api, startAddresses, parameters, 
        startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}", 
        offsetAddressName:"Offset/Temporary SuperNATURAL Synth Tone",
        offset2AddressName:"Offset2/SuperNATURAL Synth Tone Common MFX", 
        parameterNamePrefix:"SuperNATURAL Synth Tone Common MFX/")
    {
    }
}

