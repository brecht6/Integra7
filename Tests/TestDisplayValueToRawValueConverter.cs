namespace Tests;

using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;


public class TestDisplayValueToRawValueConverter
{
    const Integra7ParameterSpec.SpecType NUM = Integra7ParameterSpec.SpecType.NUMERIC;
    const Integra7ParameterSpec.SpecType ASC = Integra7ParameterSpec.SpecType.ASCII;
    public const bool USED = false;
    public const bool RESERVED = true;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_NotNibbled_NotMapped_NoRepr()
    {
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 0, omax: 127, bytes: 1, res: USED, nib: false, unit: "", repr: null);
        FullyQualifiedParameter p = new("blah", "blob", "foobar", testspec, 0, "0");
        DisplayValueToRawValueConverter.UpdateFromDisplayedValue("100", p);
        Assert.That(p.RawNumericValue, Is.EqualTo(100));
        Assert.That(p.StringValue, Is.EqualTo("100"));
    }

    [Test]
    public void Test_NotNibbled_NotMapped_WithRepr()
    {
        IDictionary<int, string> LUT = new Dictionary<int, string>
        {
            [0x64] = "YIPPEE",
            [0x34] = "WHY?"
        };
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: 0, omax: 127, bytes: 1, res: USED, nib: false, unit: "", repr: LUT);
        FullyQualifiedParameter p = new("blah", "blob", "foobar", testspec, 0x64, "YIPPEE");
        DisplayValueToRawValueConverter.UpdateFromDisplayedValue("WHY?", p);
        Assert.That(p.RawNumericValue, Is.EqualTo(0x34));
        Assert.That(p.StringValue, Is.EqualTo(LUT[0x34]));
    }

    [Test]
    public void Test_NotNibbled_WithMapped_NoRepr()
    {
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: -64, omax: 63, bytes: 1, res: USED, nib: false, unit: "", repr: null);
        FullyQualifiedParameter p = new("blah", "blob", "foobar", testspec, 0, "-64");
        DisplayValueToRawValueConverter.UpdateFromDisplayedValue("0", p);
        Assert.That(p.RawNumericValue, Is.EqualTo(64));
        Assert.That(p.StringValue, Is.EqualTo("0"));
    }

    [Test]
    public void Test_NotNibbled_WithMapped_WithRepr()
    {
        // not used in practice; look up mapped value in repr table
        IDictionary<int, string> LUT = new Dictionary<int, string>
        {
            [0] = "YIPPEE",
            [63] = "MEH?"
        };
        Integra7ParameterSpec testspec = new(type: NUM, path: "System Common/Master Level", offs: [0x00, 0x05], imin: 0, imax: 127, omin: -64, omax: 63, bytes: 1, res: USED, nib: false, unit: "", repr: LUT);
        FullyQualifiedParameter p = new("blah", "blob","foobar", testspec, 127, LUT[63]);
        DisplayValueToRawValueConverter.UpdateFromDisplayedValue("YIPPEE", p);
        Assert.That(p.RawNumericValue, Is.EqualTo(64)); // raw value is unmapped value
        Assert.That(p.StringValue, Is.EqualTo(LUT[0]));
    }

    // note nibbled values behave exactly the same as non-nibbled values for this purpose
}
