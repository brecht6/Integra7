namespace Integra7AuralAlchemist.Models.Data;

public class UpdateMessageSpec
{
    private readonly FullyQualifiedParameter _par;
    public FullyQualifiedParameter Par { get => _par; }
    private readonly string _displayvalue;
    public string DisplayValue { get => _displayvalue; }

    public UpdateMessageSpec(FullyQualifiedParameter par, string displayvalue)
    {
        _par = par;
        _displayvalue = displayvalue;
    }
}

public class UpdateFromSysexSpec(byte[] sysexMsg)
{
    private readonly byte[] _sysexMsg = sysexMsg;
    public byte[] SysexMsg { get => _sysexMsg; }
}

public class UpdateResyncPart(byte partNo)
{
    private readonly byte _partNo = partNo;
    public byte PartNo { get => _partNo; }
}
