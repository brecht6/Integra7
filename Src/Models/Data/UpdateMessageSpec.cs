namespace Integra7AuralAlchemist.Models.Data;

public class UpdateMessageSpec
{
    private readonly FullyQualifiedParameter _par;
    public FullyQualifiedParameter Par => _par;
    private readonly string _displayvalue;
    public string DisplayValue => _displayvalue;

    public UpdateMessageSpec(FullyQualifiedParameter par, string displayvalue)
    {
        _par = par;
        _displayvalue = displayvalue;
    }
}

public class UpdateFromSysexSpec(byte[] sysexMsg)
{
    private readonly byte[] _sysexMsg = sysexMsg;
    public byte[] SysexMsg => _sysexMsg;
}

public class UpdateResyncPart(byte partNo)
{
    private readonly byte _partNo = partNo;
    public byte PartNo => _partNo;
}
