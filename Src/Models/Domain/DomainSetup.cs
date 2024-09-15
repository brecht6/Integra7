using System.Threading;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Domain;

public class DomainSetup : DomainBase
{
    public DomainSetup(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters, SemaphoreSlim semaphore) 
    : base(integra7Api, startAddresses, parameters, 
        startAddressName: "Setup", 
        offsetAddressName:"Offset/Not Used",
        offset2AddressName:"Offset2/Setup Sound Mode",  
        "Setup/",
        semaphore)
    {
    }
}
