using System.Diagnostics;
namespace Integra7AuralAlchemist.Models.Services;
public enum Integra7SoundMode { STUDIO=1, GM1=2, GM2=3, GS=4 }
public class SystemCommon_Setup_SoundMode
{
    private Integra7SoundMode _integra7SoundMode;
    public SystemCommon_Setup_SoundMode(Integra7SoundMode integra7SoundMode)
    {
        _integra7SoundMode = integra7SoundMode;
    }
    
    public byte[] StartAddress()
    {
        return Integra7SysexHelpers.StartAddress_Setup;
    }
    public byte[] OffsetAddress()
    {
        return Integra7SysexHelpers.Offset_Setup_SoundMode;
    }
    public byte[] GenerateValue()
    {
        return ByteUtils.IntToBytes7_2((int)_integra7SoundMode);
    }

    public void ParseValue(byte[] data)
    {
        Debug.Assert(data.Length == 2);
        _integra7SoundMode = (Integra7SoundMode)data[1];
    }

}