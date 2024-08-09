namespace Tests;

using System.Text;
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
    public void TestAddressWithOffsetCarryOver()
    {
        byte[] addr = [0x00, 0x20, 0x00];
        byte[] offs = [0x00, 0x70, 0x00];
        byte[] comb = ByteUtils.AddressWithOffset(addr, offs);
        Assert.That(comb, Is.EquivalentTo((byte[])[0x01, 0x10, 0x00]));
    }

    [Test]
    public void TestCheckSum()
    {
        byte[] payload = ByteUtils.Concat(
            ByteUtils.AddressWithOffset(
                ByteUtils.AddressWithOffset(
                    [0x18, 0x00, 0x00, 0x00] /*temp studio set start address*/,
                    [0x06, 0x00] /*studio set reverb offset*/),
                [0x00, 0x00] /*reverb type address*/),
            [0x02] /*reverb value*/);
        Assert.That(payload, Is.EquivalentTo((byte[])[0x18, 0x00, 0x06, 0x00, 0x02]));
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

    [Test]
    public void TestIntToNibbled()
    {
        long value = 0xab;
        byte[] nibbled = ByteUtils.IntToNibbled(value, 2);
        Assert.That(nibbled, Is.EquivalentTo((byte[])[0x0a, 0x0b]));

        long value2 = 0xfbea;
        byte[] nibbled2 = ByteUtils.IntToNibbled(value2, 4);
        Assert.That(nibbled2, Is.EquivalentTo((byte[])[0xf, 0xb, 0x0e, 0x0a]));
    }

    [Test]
    public void TestNibbledToInt()
    {
        byte[] data = [0x0a, 0x0b];
        Assert.That(ByteUtils.NibbledToInt(data), Is.EqualTo(0xab));

        byte[] data2 = [0x0f, 0x0b, 0x0e, 0x0a];
        Assert.That(ByteUtils.NibbledToInt(data2), Is.EqualTo(0xfbea));

    }

    [Test]
    public void TestConcat1()
    {
        byte[] data1 = [0x01, 0x02, 0x03];
        byte[] data2 = [0x04, 0x05];
        byte[] conc = ByteUtils.Concat(data1, data2);
        Assert.That(conc, Is.EquivalentTo((byte[])[0x01, 0x02, 0x03, 0x04, 0x05]));
    }

    [Test]
    public void TestConcat2()
    {
        byte[] data1 = [];
        byte[] data2 = [0x04, 0x05];
        byte[] conc = ByteUtils.Concat(data1, data2);
        Assert.That(conc, Is.EquivalentTo((byte[])[0x04, 0x05]));
    }

    [Test]
    public void TestConcat3()
    {
        byte[] data1 = [0x01, 0x02, 0x03];
        byte[] data2 = [];
        byte[] conc = ByteUtils.Concat(data1, data2);
        Assert.That(conc, Is.EquivalentTo((byte[])[0x01, 0x02, 0x03]));
    }

    [Test]
    public void TestConcat4()
    {
        byte[] data1 = [];
        byte[] data2 = [];
        byte[] conc = ByteUtils.Concat(data1, data2);
        Assert.That(conc, Is.EquivalentTo((byte[])[]));
    }

    [Test]
    public void TestSlice()
    {
        byte[] data1 = [0x0, 0x1, 0x2, 0x3];

        byte[] slice = ByteUtils.Slice(data1, 0, 1);
        Assert.That(slice, Is.EquivalentTo((byte[])[0x0]));

        byte[] slice2 = ByteUtils.Slice(data1, 1, 2);
        Assert.That(slice2, Is.EquivalentTo((byte[])[0x1, 0x2]));
    }

    [Test]
    public void TestZeros()
    {
        int noOfZeros = 0;
        byte[] zeros = ByteUtils.Zeros(noOfZeros);
        Assert.That(zeros, Is.EquivalentTo((byte[])[]));

        int noOfZeros2 = 4;
        byte[] zeros2 = ByteUtils.Zeros(noOfZeros2);
        Assert.That(zeros2, Is.EquivalentTo((byte[])[0x00, 0x00, 0x00, 0x00]));
    }

    [Test]
    public void TestPadString()
    {
        byte[] shortd = Encoding.ASCII.GetBytes("biebel");
        byte[] longd = ByteUtils.PadString(shortd, 12);
        Assert.That(longd, Is.EquivalentTo(ByteUtils.Flatten(shortd, [0x20, 0x20, 0x20, 0x20, 0x20, 0x20])));
    }
}