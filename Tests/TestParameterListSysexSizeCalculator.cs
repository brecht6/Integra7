namespace Tests;

using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

public class ParameterListSysexSizeCalculatorTests
{
    public readonly IDictionary<int, string> CHORUS_TYPE = new Dictionary<int, string> {
            [0] = "Off", [1] = "Chorus", [2] ="Delay", [3] = "GM2 Chorus"
    };
    public readonly IDictionary<int, string> OFF_LPF_HPF = new Dictionary<int, string> {
        [0] = "Off", [1] = "Low Pass Filter", [2] = "High Pass Filter"
    };
    public readonly IDictionary<int, string> DELAY_MSEC_NOTE = new Dictionary<int, string> {
        [0] = "msec", [1] = "Note"
    };
    public readonly IDictionary<int, string> MAIN_REV = new Dictionary<int, string> {
        [0] = "MAIN", [1] = "REV", [2] = "MAIN+REV"
    };

    const Integra7ParameterSpec.SpecType NUM = Integra7ParameterSpec.SpecType.NUMERIC;
    public const bool USED = false;
    public const bool RESERVED = true;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSizeWithoutDataDep()
    {
        List<Integra7ParameterSpec> l = new List<Integra7ParameterSpec>() {
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Type", offs:[0x00, 0x00], imin:0, imax:3, omin:0, omax:3, bytes:1, res:USED, nib:false, unit:"", repr:CHORUS_TYPE, store:true),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Level", offs:[0x00, 0x01], imin:0, imax:127, omin:0, omax:127, bytes:1, res:USED, nib:false, unit:"", repr:null),
            new(type:NUM, path:"Studio Set Common Chorus/Reserved", offs:[0x00, 0x02], imin:0, imax:3, omin:0, omax:3, bytes:1, res:RESERVED, nib:false, unit:"", repr:null),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Output Select", offs:[0x00, 0x03], imin:0, imax:2, omin:0, omax:2, bytes:1, res:USED, nib:false, unit:"", repr:MAIN_REV),
        };

        int size = ParameterListSysexSizeCalculator.CalculateSysexSize(l);
        Assert.That(size, Is.EqualTo(4));
        int gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(l);
        Assert.That(gap, Is.EqualTo(3));
    }

    [Test]
    public void TestSizeWithDataDepInMiddle()
    {
        List<Integra7ParameterSpec> l = new List<Integra7ParameterSpec>() {
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Type", offs:[0x00, 0x00], imin:0, imax:3, omin:0, omax:3, bytes:1, res:USED, nib:false, unit:"", repr:CHORUS_TYPE, store:true),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Level", offs:[0x00, 0x01], imin:0, imax:127, omin:0, omax:127, bytes:1, res:USED, nib:false, unit:"", repr:null),
            new(type:NUM, path:"Studio Set Common Chorus/Reserved", offs:[0x00, 0x02], imin:0, imax:3, omin:0, omax:3, bytes:1, res:RESERVED, nib:false, unit:"", repr:null),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Output Select", offs:[0x00, 0x03], imin:0, imax:2, omin:0, omax:2, bytes:1, res:USED, nib:false, unit:"", repr:MAIN_REV),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Reserved", offs:[0x00, 0x04], imin:12768, imax:52768, omin:-20000, omax:20000, bytes:4, res:RESERVED, nib:true, unit:"", repr:null, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[0]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Filter Type", offs:[0x00, 0x04], imin:0, imax:2, omin:0, omax:2, bytes:4, res:USED, nib:true, unit:"", repr:OFF_LPF_HPF, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[1]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Delay Left", offs:[0x00, 0x04], imin:0, imax:1, omin:0, omax:1, bytes:4, res:USED, nib:true, unit:"", repr:DELAY_MSEC_NOTE, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[2]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Pre-LPF", offs:[0x00, 0x04], imin:0, imax:7, omin:0, omax:7, bytes:4, res:USED, nib:true, unit:"", repr:null, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[3]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 2/Reserved", offs:[0x00, 0x04], imin:12768, imax:52768, omin:-20000, omax:20000, bytes:4, res:RESERVED, nib:true, unit:"", repr:null),
        };
        
        int size = ParameterListSysexSizeCalculator.CalculateSysexSize(l);
        Assert.That(size, Is.EqualTo(12));
        int gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(l);
        Assert.That(gap, Is.EqualTo(8));
    }

    [Test]
    public void TestSizeWithDataDepAtEnd()
    {
        List<Integra7ParameterSpec> l = new List<Integra7ParameterSpec>() {
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Type", offs:[0x00, 0x00], imin:0, imax:3, omin:0, omax:3, bytes:1, res:USED, nib:false, unit:"", repr:CHORUS_TYPE, store:true),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Level", offs:[0x00, 0x01], imin:0, imax:127, omin:0, omax:127, bytes:1, res:USED, nib:false, unit:"", repr:null),
            new(type:NUM, path:"Studio Set Common Chorus/Reserved", offs:[0x00, 0x02], imin:0, imax:3, omin:0, omax:3, bytes:1, res:RESERVED, nib:false, unit:"", repr:null),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Output Select", offs:[0x00, 0x03], imin:0, imax:2, omin:0, omax:2, bytes:1, res:USED, nib:false, unit:"", repr:MAIN_REV),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Reserved", offs:[0x00, 0x04], imin:12768, imax:52768, omin:-20000, omax:20000, bytes:4, res:RESERVED, nib:true, unit:"", repr:null, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[0]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Filter Type", offs:[0x00, 0x04], imin:0, imax:2, omin:0, omax:2, bytes:4, res:USED, nib:true, unit:"", repr:OFF_LPF_HPF, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[1]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Delay Left", offs:[0x00, 0x04], imin:0, imax:1, omin:0, omax:1, bytes:4, res:USED, nib:true, unit:"", repr:DELAY_MSEC_NOTE, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[2]),
            new(type:NUM, path:"Studio Set Common Chorus/Chorus Parameter 1/Pre-LPF", offs:[0x00, 0x04], imin:0, imax:7, omin:0, omax:7, bytes:4, res:USED, nib:true, unit:"", repr:null, mst:"Studio Set Common Chorus/Chorus Type", mstval:CHORUS_TYPE[3]),
        };

        int size = ParameterListSysexSizeCalculator.CalculateSysexSize(l);
        Assert.That(size, Is.EqualTo(8));
        int gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(l);
        Assert.That(gap, Is.EqualTo(4));
    }
}