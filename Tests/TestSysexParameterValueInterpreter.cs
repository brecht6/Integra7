namespace Tests;

using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

public class TestSysexParameterValueInterpreter
{
    const Integra7ParameterSpec.SpecType NUM = Integra7ParameterSpec.SpecType.NUMERIC;
    public const bool USED = false;
    public const bool RESERVED = true;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_NotNibbled_NotMapped_NoRepr()
    {
        byte[] parData = [0x00, 0x64];
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 0, omax: 127, bytes: 1, res: USED, nib: false, unit: "", repr: null);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(0x64));
        Assert.That(s, Is.EqualTo("100"));
    }

    [Test]
    public void Test_NotNibbled_NotMapped_WithRepr()
    {
        IDictionary<int, string> LUT = new Dictionary<int, string>
        {
            [0x64] = "YIPPEE"
        };
        byte[] parData = [0x00, 0x64];
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 0, omax: 127, bytes: 1, res: USED, nib: false, unit: "", repr: LUT);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(0x64));
        Assert.That(s, Is.EqualTo(LUT[0x64]));
    }

    [Test]
    public void Test_NotNibbled_WithMapped_NoRepr()
    {
        byte[] parData = [0x00, 0x64];
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: -64, omax: 63, bytes: 1, res: USED, nib: false, unit: "", repr: null);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(0x64));
        Assert.That(s, Is.EqualTo("36"));
    }

    [Test]
    public void Test_NotNibbled_WithMapped_WithRepr()
    {
        // not used in practice; for now look up raw value instead of mapped value (which effectively ignores the mapping)
        IDictionary<int, string> LUT = new Dictionary<int, string>
        {
            [0x64] = "YIPPEE"
        };

        byte[] parData = [0x00, 0x64];
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: -64, omax: 63, bytes: 1, res: USED, nib: false, unit: "", repr: LUT);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(0x64));
        Assert.That(s, Is.EqualTo(LUT[0x064]));
    }

    [Test]
    public void Test_Nibbled_NotMapped_NoRepr()
    {
        byte[] parData = ByteUtils.IntToNibbled(325, 4);
        Assert.That(parData, Is.EquivalentTo((byte[])[0x0, 0x01, 0x04, 0x05]));

        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 0, omax: 127,
                                             bytes: 2, res: USED, nib: true, unit: "", repr: null);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(325));
        Assert.That(s, Is.EqualTo("325"));
    }

    [Test]
    public void Test_Nibbled_NotMapped_WithRepr()
    {
        IDictionary<int, string> LUT = new Dictionary<int, string>
        {
            [325] = "MEH"
        };
        byte[] parData = ByteUtils.IntToNibbled(325, 4);
        Assert.That(parData, Is.EquivalentTo((byte[])[0x0, 0x01, 0x04, 0x05]));

        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 0, omax: 127,
                                             bytes: 2, res: USED, nib: true, unit: "", repr: LUT);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(325));
        Assert.That(s, Is.EqualTo(LUT[325]));
    }

    [Test]
    public void Test_Nibbled_WithMapped_NoRepr()
    {
        byte[] parData = ByteUtils.IntToNibbled(325, 4);
        Assert.That(parData, Is.EquivalentTo((byte[])[0x0, 0x01, 0x04, 0x05]));

        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 1, omax: 128,
                                             bytes: 2, res: USED, nib: true, unit: "", repr: null);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(325)); // raw value is always unmapped
        Assert.That(s, Is.EqualTo("326")); // mapped value
    }

    [Test]
    public void Test_Nibbled_WithMapped_WithRepr()
    {
        IDictionary<int, string> LUT = new Dictionary<int, string>
        {
            [326] = "MEH"
        };
        byte[] parData = ByteUtils.IntToNibbled(325, 4);
        Assert.That(parData, Is.EquivalentTo((byte[])[0x0, 0x01, 0x04, 0x05]));

        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 1, omax: 128,
                                             bytes: 2, res: USED, nib: true, unit: "", repr: LUT);
        long n;
        string s;
        SysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(325)); // raw value is always unmapped
        Assert.That(s, Is.EqualTo(LUT[326])); // nibbled+mapped looks up mapped value in repr table
    }

    [Test]
    public void Test_Ascii()
    {
        byte[] parData = Encoding.ASCII.GetBytes("Integra Preview ");
        Integra7ParameterSpec testspec = new(type: ASC, path: "Studio Set Common/Studio Set Name", offs: [0x00, 0x00], imin: 32, imax: 127, omin: 32, omax: 127, bytes: 16, res: USED, nib: false, unit: "", repr: null);
        long n;
        string s;
        TestSysexParameterValueInterpreter.Interpret(parData, testspec, out n, out s);
        Assert.That(n, Is.EqualTo(0)); // not used
        Assert.That(s, Is.EqualTo("Integra Preview "));
    }
}
