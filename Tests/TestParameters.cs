namespace Tests;

using Integra7AuralAlchemist.Models.Data;

public class ParametersTests
{
    public Integra7Parameters _p = new Integra7Parameters(true);

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestFromTo()
    {
        List<Integra7ParameterSpec> l = _p.GetParametersFromTo("Studio Set Part/Keyboard Range Lower", "Studio Set Part/Velocity Range Upper");
        Assert.That(l.Count, Is.EqualTo(6));
        Assert.That(l[0].Path, Is.EqualTo("Studio Set Part/Keyboard Range Lower"));
        Assert.That(l[1].Path, Is.EqualTo("Studio Set Part/Keyboard Range Upper"));
        Assert.That(l[2].Path, Is.EqualTo("Studio Set Part/Keyboard Fade Width Lower"));
        Assert.That(l[3].Path, Is.EqualTo("Studio Set Part/Keyboard Fade Width Upper"));
        Assert.That(l[4].Path, Is.EqualTo("Studio Set Part/Velocity Range Lower"));
        Assert.That(l[5].Path, Is.EqualTo("Studio Set Part/Velocity Range Upper"));
    }
}