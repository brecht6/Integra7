namespace Tests;

using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;


public class TestIntegra7ParameterDatabaseAnalyzer
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
    public void Test_MarkMasterControls()
    {
        IList<Integra7ParameterSpec> db = [
            new(type:NUM, path:"a", offs:[0x00, 0x00], imin:1, imax:4, omin:1, omax:4, bytes:1, res:USED, nib: false, unit:"", repr:null),
            new(type:NUM, path:"b", offs:[0x00, 0x00], imin:1, imax:4, omin:1, omax:4, bytes:1, res:USED, nib: false, unit:"", repr:null,
                    mst:"a", mstval:"23"),
            new(type:NUM, path:"c", offs:[0x00, 0x00], imin:1, imax:4, omin:1, omax:4, bytes:1, res:USED, nib: false, unit:"", repr:null,
                    mst:"b", mstval:"1"),
        ];
        Integra7ParameterDatabaseAnalyzer.MarkAllMasterParametersAsStoreTrue(db);
        Assert.That(db[0].Store, Is.EqualTo(true)); // a is master parameter for b
        Assert.That(db[1].Store, Is.EqualTo(true)); // b is master parameter for c
        Assert.That(db[2].Store, Is.EqualTo(false)); // c is not a master parameter
    }

    [Test]
    public void Test_SecondaryDeps()
    {
        IList<Integra7ParameterSpec> db = [
            new(type:NUM, path:"a", offs:[0x00, 0x00], imin:1, imax:4, omin:1, omax:4, bytes:1, res:USED, nib: false, unit:"", repr:null),
            new(type:NUM, path:"b", offs:[0x00, 0x00], imin:1, imax:4, omin:1, omax:4, bytes:1, res:USED, nib: false, unit:"", repr:null,
                    mst:"a", mstval:"23"),
            new(type:NUM, path:"c", offs:[0x00, 0x00], imin:1, imax:4, omin:1, omax:4, bytes:1, res:USED, nib: false, unit:"", repr:null,
                    mst:"b", mstval:"1"),
        ];
        Integra7ParameterDatabaseAnalyzer.FillInSecondaryDependencies(db);
        Assert.That(db[0].MasterCtrl, Is.EqualTo(""));
        Assert.That(db[0].MasterCtrlDispValue, Is.EqualTo(""));
        Assert.That(db[1].MasterCtrl, Is.EqualTo("a"));
        Assert.That(db[1].MasterCtrlDispValue, Is.EqualTo("23"));
        Assert.That(db[1].MasterCtrl2, Is.EqualTo(""));
        Assert.That(db[1].MasterCtrlDispValue2, Is.EqualTo(""));
        Assert.That(db[2].MasterCtrl, Is.EqualTo("a"));
        Assert.That(db[2].MasterCtrlDispValue, Is.EqualTo("23"));
        Assert.That(db[2].MasterCtrl2, Is.EqualTo("b"));
        Assert.That(db[2].MasterCtrlDispValue2, Is.EqualTo("1"));
    }

}