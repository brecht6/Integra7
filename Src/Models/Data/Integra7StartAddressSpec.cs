namespace Integra7AuralAlchemist.Models.Data;

public class Integra7StartAddressSpec
{
    public Integra7StartAddressSpec(byte[] addr)
    {
        Address = addr;
    }

    public byte[] Address { get; }
}