namespace Tests;

using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

public class StartAddressesTests
{
    public Integra7StartAddresses _a = new Integra7StartAddresses();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSynthTonePartials()
    {
        byte[] data = _a.Lookup("Offset/PCM Synth Tone/Partial 1").Address;
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x20, 0x00]));
        byte[] data2 = _a.Lookup("Offset/PCM Synth Tone/Partial 3").Address;
        Assert.That(data2, Is.EquivalentTo((byte[])[0x00, 0x24, 0x00]));
        byte[] data3 = _a.Lookup("Offset/PCM Synth Tone/Partial 4").Address;
        Assert.That(data3, Is.EquivalentTo((byte[])[0x00, 0x26, 0x00]));
        Assert.That(_a.Exists("Offset/PCM Synth Tone/Partial 5"), Is.EqualTo(false));

        byte[] data4 = _a.Lookup("Offset/SN Synth Tone/Partial 1").Address;
        Assert.That(data4, Is.EquivalentTo((byte[])[0x00, 0x20, 0x00]));
        byte[] data5 = _a.Lookup("Offset/SN Synth Tone/Partial 3").Address;
        Assert.That(data5, Is.EquivalentTo((byte[])[0x00, 0x22, 0x00]));
        Assert.That(_a.Exists("Offset/SN Synth Tone/Partial 4"), Is.EqualTo(false));
    }

    [Test]
    public void TestPcmDrumKitPartials()
    {
        byte[] data = _a.Lookup("Offset/PCM Drum Kit/Partial 21").Address;
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x10, 0x00]));
        byte[] data2 = _a.Lookup("Offset/PCM Drum Kit/Partial 77").Address;
        Assert.That(data2, Is.EquivalentTo((byte[])[0x01, 0x00, 0x00]));
        byte[] data3 = _a.Lookup("Offset/PCM Drum Kit/Partial 108").Address;
        Assert.That(data3, Is.EquivalentTo((byte[])[0x01, 0x3e, 0x00]));
        Assert.That(_a.Exists("Offset/PCM Drum Kit/Partial 109"), Is.EqualTo(false));
    }

    [Test]
    public void TestSnDrumKitPartials()
    {
        byte[] data = _a.Lookup("Offset/SN Drum Kit/Partial 27").Address;
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x10, 0x00]));
        byte[] data2 = _a.Lookup("Offset/SN Drum Kit/Partial 88").Address;
        Assert.That(data2, Is.EquivalentTo((byte[])[0x00, 0x4d, 0x00]));
        Assert.That(_a.Exists("Offset/SN Drum Kit/Partial 89"), Is.EqualTo(false));
    }
}