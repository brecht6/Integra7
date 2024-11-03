namespace Integra7AuralAlchemist.Models.Data;

public class UpdateMessageSpec
{
    public UpdateMessageSpec(FullyQualifiedParameter par, string displayvalue)
    {
        Par = par;
        DisplayValue = displayvalue;
    }

    public FullyQualifiedParameter Par { get; }

    public string DisplayValue { get; }
}

public class UpdateFromSysexSpec(byte[] sysexMsg)
{
    public byte[] SysexMsg { get; } = sysexMsg;
}

public class UpdateResyncPart(byte partNo)
{
    public byte PartNo { get; } = partNo;
}

public class UpdateSetPresetAndResyncPart(byte partNo)
{
    public byte PartNo { get; } = partNo;
}