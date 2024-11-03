using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Tests;

public class TestDisplayValueToRawValueConverter
{
    private const Integra7ParameterSpec.SpecType NUM = Integra7ParameterSpec.SpecType.NUMERIC;
    private const Integra7ParameterSpec.SpecType ASC = Integra7ParameterSpec.SpecType.ASCII;
    public const bool USED = false;
    public const bool RESERVED = true;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_NotNibbled_NotMapped_NoRepr()
    {
        Integra7ParameterSpec testspec = new(NUM, "System Common/Master Level", [0x00, 0x05], 0, 127, 0, 127, 1, USED,
            false, "", null);
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
        Integra7ParameterSpec testspec = new(NUM, "System Common/Master Level", [0x00, 0x05], 0, 127, 0, 127, 1, USED,
            false, "", LUT);
        FullyQualifiedParameter p = new("blah", "blob", "foobar", testspec, 0x64, "YIPPEE");
        DisplayValueToRawValueConverter.UpdateFromDisplayedValue("WHY?", p);
        Assert.That(p.RawNumericValue, Is.EqualTo(0x34));
        Assert.That(p.StringValue, Is.EqualTo(LUT[0x34]));
    }

    [Test]
    public void Test_NotNibbled_WithMapped_NoRepr()
    {
        Integra7ParameterSpec testspec = new(NUM, "System Common/Master Level", [0x00, 0x05], 0, 127, -64, 63, 1, USED,
            false, "", null);
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
        Integra7ParameterSpec testspec = new(NUM, "System Common/Master Level", [0x00, 0x05], 0, 127, -64, 63, 1, USED,
            false, "", LUT);
        FullyQualifiedParameter p = new("blah", "blob", "foobar", testspec, 127, LUT[63]);
        DisplayValueToRawValueConverter.UpdateFromDisplayedValue("YIPPEE", p);
        Assert.That(p.RawNumericValue, Is.EqualTo(64)); // raw value is unmapped value
        Assert.That(p.StringValue, Is.EqualTo(LUT[0]));
    }

    // note nibbled values behave exactly the same as non-nibbled values for this purpose
}