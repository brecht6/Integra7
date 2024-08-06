namespace Tests;

using Integra7AuralAlchemist.Models.Services;

public class Tests
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
}