using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Tests;

public class ParameterListSysexSizeCalculatorTests
{
    private const Integra7ParameterSpec.SpecType NUM = Integra7ParameterSpec.SpecType.NUMERIC;
    public const bool USED = false;
    public const bool RESERVED = true;

    public readonly IDictionary<int, string> CHORUS_TYPE = new Dictionary<int, string>
    {
        [0] = "Off",
        [1] = "Chorus",
        [2] = "Delay",
        [3] = "GM2 Chorus"
    };

    public readonly IDictionary<int, string> DELAY_MSEC_NOTE = new Dictionary<int, string>
    {
        [0] = "msec",
        [1] = "Note"
    };

    public readonly IDictionary<int, string> MAIN_REV = new Dictionary<int, string>
    {
        [0] = "MAIN",
        [1] = "REV",
        [2] = "MAIN+REV"
    };

    public readonly IDictionary<int, string> OFF_LPF_HPF = new Dictionary<int, string>
    {
        [0] = "Off",
        [1] = "Low Pass Filter",
        [2] = "High Pass Filter"
    };

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSizeWithoutDataDep()
    {
        List<Integra7ParameterSpec> l = new()
        {
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Type", [0x00, 0x00], 0, 3, 0, 3, 1, USED,
                false, "", CHORUS_TYPE, isparent: true),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Level", [0x00, 0x01], 0, 127, 0, 127, 1,
                USED, false, "", null),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Reserved", [0x00, 0x02], 0, 3, 0, 3, 1, RESERVED,
                false, "", null),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Output Select", [0x00, 0x03], 0, 2, 0, 2, 1,
                USED, false, "", MAIN_REV)
        };

        var size = ParameterListSysexSizeCalculator.CalculateSysexSize(l);
        Assert.That(size, Is.EqualTo(4));
        var gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(l);
        Assert.That(gap, Is.EqualTo(3));
    }

    [Test]
    public void TestSizeWithDataDepInMiddle()
    {
        List<Integra7ParameterSpec> l = new()
        {
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Type", [0x00, 0x00], 0, 3, 0, 3, 1, USED,
                false, "", CHORUS_TYPE, isparent: true),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Level", [0x00, 0x01], 0, 127, 0, 127, 1,
                USED, false, "", null),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Reserved", [0x00, 0x02], 0, 3, 0, 3, 1, RESERVED,
                false, "", null),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Output Select", [0x00, 0x03], 0, 2, 0, 2, 1,
                USED, false, "", MAIN_REV),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Reserved", [0x00, 0x04], 12768,
                52768, -20000, 20000, 4, RESERVED, true, "", null, "Studio Set Common Chorus/Chorus Type",
                CHORUS_TYPE[0]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Filter Type", [0x00, 0x04], 0,
                2, 0, 2, 4, USED, true, "", OFF_LPF_HPF, "Studio Set Common Chorus/Chorus Type", CHORUS_TYPE[1]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Delay Left", [0x00, 0x04], 0, 1,
                0, 1, 4, USED, true, "", DELAY_MSEC_NOTE, "Studio Set Common Chorus/Chorus Type", CHORUS_TYPE[2]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Pre-LPF", [0x00, 0x04], 0, 7, 0,
                7, 4, USED, true, "", null, "Studio Set Common Chorus/Chorus Type", CHORUS_TYPE[3]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 2/Reserved", [0x00, 0x04], 12768,
                52768, -20000, 20000, 4, RESERVED, true, "", null)
        };

        var size = ParameterListSysexSizeCalculator.CalculateSysexSize(l);
        Assert.That(size, Is.EqualTo(12));
        var gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(l);
        Assert.That(gap, Is.EqualTo(8));
    }

    [Test]
    public void TestSizeWithDataDepAtEnd()
    {
        List<Integra7ParameterSpec> l = new()
        {
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Type", [0x00, 0x00], 0, 3, 0, 3, 1, USED,
                false, "", CHORUS_TYPE, isparent: true),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Level", [0x00, 0x01], 0, 127, 0, 127, 1,
                USED, false, "", null),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Reserved", [0x00, 0x02], 0, 3, 0, 3, 1, RESERVED,
                false, "", null),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Output Select", [0x00, 0x03], 0, 2, 0, 2, 1,
                USED, false, "", MAIN_REV),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Reserved", [0x00, 0x04], 12768,
                52768, -20000, 20000, 4, RESERVED, true, "", null, "Studio Set Common Chorus/Chorus Type",
                CHORUS_TYPE[0]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Filter Type", [0x00, 0x04], 0,
                2, 0, 2, 4, USED, true, "", OFF_LPF_HPF, "Studio Set Common Chorus/Chorus Type", CHORUS_TYPE[1]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Delay Left", [0x00, 0x04], 0, 1,
                0, 1, 4, USED, true, "", DELAY_MSEC_NOTE, "Studio Set Common Chorus/Chorus Type", CHORUS_TYPE[2]),
            new Integra7ParameterSpec(NUM, "Studio Set Common Chorus/Chorus Parameter 1/Pre-LPF", [0x00, 0x04], 0, 7, 0,
                7, 4, USED, true, "", null, "Studio Set Common Chorus/Chorus Type", CHORUS_TYPE[3])
        };

        var size = ParameterListSysexSizeCalculator.CalculateSysexSize(l);
        Assert.That(size, Is.EqualTo(8));
        var gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(l);
        Assert.That(gap, Is.EqualTo(4));
    }
}