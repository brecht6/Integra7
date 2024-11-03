using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;

namespace Tests;

public class TestIntegra7ParameterDatabaseAnalyzer
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
    public void Test_MarkParentControls()
    {
        IList<Integra7ParameterSpec> db =
        [
            new(NUM, "a", [0x00, 0x00], 1, 4, 1, 4, 1, USED, false, "", null),
            new(NUM, "b", [0x00, 0x00], 1, 4, 1, 4, 1, USED, false, "", null,
                "a", "23"),
            new(NUM, "c", [0x00, 0x00], 1, 4, 1, 4, 1, USED, false, "", null,
                "b", "1")
        ];
        Integra7ParameterDatabaseAnalyzer.MarkAllParentParametersAsIsParentTrue(db);
        Assert.That(db[0].IsParent, Is.EqualTo(true)); // a is parent parameter for b
        Assert.That(db[1].IsParent, Is.EqualTo(true)); // b is parent parameter for c
        Assert.That(db[2].IsParent, Is.EqualTo(false)); // c is not a parent parameter
    }

    [Test]
    public void Test_SecondaryDeps()
    {
        IList<Integra7ParameterSpec> db =
        [
            new(NUM, "a", [0x00, 0x00], 1, 4, 1, 4, 1, USED, false, "", null),
            new(NUM, "b", [0x00, 0x00], 1, 4, 1, 4, 1, USED, false, "", null,
                "a", "23"),
            new(NUM, "c", [0x00, 0x00], 1, 4, 1, 4, 1, USED, false, "", null,
                "b", "1")
        ];
        Integra7ParameterDatabaseAnalyzer.FillInSecondaryDependencies(db);
        Assert.That(db[0].ParentCtrl, Is.EqualTo(""));
        Assert.That(db[0].ParentCtrlDispValue, Is.EqualTo(""));
        Assert.That(db[1].ParentCtrl, Is.EqualTo("a"));
        Assert.That(db[1].ParentCtrlDispValue, Is.EqualTo("23"));
        Assert.That(db[1].ParentCtrl2, Is.EqualTo(""));
        Assert.That(db[1].ParentCtrlDispValue2, Is.EqualTo(""));
        Assert.That(db[2].ParentCtrl, Is.EqualTo("a"));
        Assert.That(db[2].ParentCtrlDispValue, Is.EqualTo("23"));
        Assert.That(db[2].ParentCtrl2, Is.EqualTo("b"));
        Assert.That(db[2].ParentCtrlDispValue2, Is.EqualTo("1"));
    }
}