using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSNAcousticToneCommonMFX : DomainBase
{
    public DomainSNAcousticToneCommonMFX(int zeroBasedPart, IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    : base(integra7Api, startAddresses, parameters, 
        startAddressName:$"Temporary Tone Part {zeroBasedPart + 1}", 
        offsetAddressName:"Offset/Temporary SuperNATURAL Acoustic Tone",
        offset2AddressName:"Offset2/SuperNATURAL Acoustic Tone Common MFX", 
        parameterNamePrefix:"SuperNATURAL Acoustic Tone Common MFX/")
    {
    }
}

