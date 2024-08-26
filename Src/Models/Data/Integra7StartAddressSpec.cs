
namespace Integra7AuralAlchemist.Models.Data;

public class Integra7StartAddressSpec 
{
    private byte[] _addr;
    public byte[] Address => _addr;

    public Integra7StartAddressSpec(byte[] addr)
    {
        _addr = addr;
    }
}