namespace Tests;

using Integra7AuralAlchemist.Models.Services;

public class MappingTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(0, 40, 70, 40)]
    [TestCase(50, 40, 70, 50)]
    [TestCase(100, 40, 70, 70)]
    [TestCase(40, 40, 70, 40)]
    [TestCase(70, 40, 70, 70)]
    [TestCase(0, -70, -40, -40)]
    [TestCase(0, 70, -40, 0)]
    public void TestClip(double val, double bound1, double bound2, double expected)
    {
        Assert.That(Mapping.clip(val, bound1, bound2), Is.EqualTo(expected));
    }

    [TestCase(1, 1, 127, -63, 63, -63)]
    [TestCase(25, 24, 2024, -100, 100, -99.9)]
    [TestCase(26, 24, 2024, -100, 100, -99.8)]
    [TestCase(2023, 24, 2024, -100, 100, 99.9)]
    public void TestLinlin(double val, double imin, double imax, double omin, double omax, double expected)
    {
        Assert.That(Mapping.linlin(val, imin, imax, omin, omax), Is.EqualTo(expected));
    }
}