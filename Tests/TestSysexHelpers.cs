namespace Tests;

using Integra7AuralAlchemist.Models.Services;

public class SysexHelpersTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSynthTonePartials()
    {
        byte[] data = Integra7SysexHelpers.Offset_PCM_SynthTone_Partial(3);
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x26, 0x00]));
    }

    [Test]
    public void TestPcmDrumKitPartials()
    {
        byte[] data = Integra7SysexHelpers.Offset_PCM_DrumKit_Partial_Key(21);
        Assert.That(data, Is.EquivalentTo((byte[])[0x00, 0x10, 0x00]));
        byte[] data2 = Integra7SysexHelpers.Offset_PCM_DrumKit_Partial_Key(77);
        Assert.That(data2, Is.EquivalentTo((byte[])[0x01, 0x00, 0x00]));
        byte[] data3 = Integra7SysexHelpers.Offset_PCM_DrumKit_Partial_Key(108);
        Assert.That(data3, Is.EquivalentTo((byte[])[0x01, 0x3e, 0x00]));
    }
}