using Integra7AuralAlchemist.Models.Data;

namespace Tests;

public class StartAddressesTests
{
    public Integra7StartAddresses _a = new();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSynthTonePartials()
    {
        var data = _a.Lookup("Offset2/PCM Synth Tone Partial 1").Address;
        Assert.That(data, Is.EquivalentTo((byte[]) [0x00, 0x20, 0x00]));
        var data2 = _a.Lookup("Offset2/PCM Synth Tone Partial 3").Address;
        Assert.That(data2, Is.EquivalentTo((byte[]) [0x00, 0x24, 0x00]));
        var data3 = _a.Lookup("Offset2/PCM Synth Tone Partial 4").Address;
        Assert.That(data3, Is.EquivalentTo((byte[]) [0x00, 0x26, 0x00]));
        Assert.That(_a.Exists("Offset2/PCM Synth Tone Partial 5"), Is.EqualTo(false));

        var data4 = _a.Lookup("Offset2/SuperNATURAL Synth Tone Partial 1").Address;
        Assert.That(data4, Is.EquivalentTo((byte[]) [0x00, 0x20, 0x00]));
        var data5 = _a.Lookup("Offset2/SuperNATURAL Synth Tone Partial 3").Address;
        Assert.That(data5, Is.EquivalentTo((byte[]) [0x00, 0x22, 0x00]));
        Assert.That(_a.Exists("Offset2/SuperNATURAL Synth Tone Partial 4"), Is.EqualTo(false));
    }

    [Test]
    public void TestPcmDrumKitPartials()
    {
        var data = _a.Lookup("Offset2/PCM Drum Kit Partial 1").Address;
        Assert.That(data, Is.EquivalentTo((byte[]) [0x00, 0x10, 0x00]));
        var data2 = _a.Lookup("Offset2/PCM Drum Kit Partial 57").Address;
        Assert.That(data2, Is.EquivalentTo((byte[]) [0x01, 0x00, 0x00]));
        var data3 = _a.Lookup("Offset2/PCM Drum Kit Partial 88").Address;
        Assert.That(data3, Is.EquivalentTo((byte[]) [0x01, 0x3e, 0x00]));
        Assert.That(_a.Exists("Offset2/PCM Drum Kit Partial 89"), Is.EqualTo(false));
    }

    [Test]
    public void TestSnDrumKitPartials()
    {
        var data = _a.Lookup("Offset2/SuperNATURAL Drum Kit Partial 1").Address;
        Assert.That(data, Is.EquivalentTo((byte[]) [0x00, 0x10, 0x00]));
        var data2 = _a.Lookup("Offset2/SuperNATURAL Drum Kit Partial 62").Address;
        Assert.That(data2, Is.EquivalentTo((byte[]) [0x00, 0x4d, 0x00]));
        Assert.That(_a.Exists("Offset2/SuperNATURAL Drum Kit Partial 63"), Is.EqualTo(false));
    }
}