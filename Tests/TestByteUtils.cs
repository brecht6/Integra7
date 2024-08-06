namespace Tests;

using Integra7AuralAlchemist.Models.Services;

public class ByteUtilsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestFlatten()
    {
        byte[] data = ByteUtils.Flatten([[0x00], [0x00, 0x00]]);
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x00, 0x00]));
    }

    [Test]
    public void TestAddressWithOffset()
    {
        byte[] addr = [0x00, 0x20, 0x00];
        byte[] offs = [0x02, 0x00];
        byte[] comb = ByteUtils.AddressWithOffset(addr, offs);
        Assert.That(comb, Is.EquivalentTo((byte[])[0x00, 0x22, 0x00]));
    }

    [Test]
    public void TestCheckSum()
    {
        byte[] payload = ByteUtils.AddressWithOffset(
            ByteUtils.AddressWithOffset(
                ByteUtils.AddressWithOffset([0x18, 0x00, 0x00, 0x00] /*temp studio set start address*/, [0x06, 0x00] /*studio set reverb offset*/),
                [0x00, 0x00] /*reverb address*/),
            [0x02] /*reverb value*/);
        Assert.That(payload, Is.EquivalentTo((byte[])[0x18, 0x00, 0x06, 0x02]));
        byte cs = ByteUtils.CheckSum(payload);
        Assert.That(cs, Is.EqualTo(0x60));
    }

    [Test]
    public void TestIntToBytes7_2()
    {
        int value = 0x7f;
        byte[] data = ByteUtils.IntToBytes7_2(value);
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x7f]));

        int value2 = 0x80;
        byte[] data2 = ByteUtils.IntToBytes7_2(value2);
        Assert.That(data2, Is.EquivalentTo((byte[])[0x01, 0x00]));
    }

    [Test]
    public void TestBytes7ToInt()
    {
        byte[] data = [0x00, 0x7f];
        long value = ByteUtils.Bytes7ToInt(data);
        Assert.That(value, Is.EqualTo(0x7f));

        byte[] data2 = [0x01, 0x00];
        long value2 = ByteUtils.Bytes7ToInt(data2);
        Assert.That(value2, Is.EqualTo(0x80));
    }
}