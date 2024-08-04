using Avalonia.Controls;
using ReactiveUI;

public class Integra7SysexHelpers
{
    private static byte EXCLUSIVE_STATUS = 0xF0;
    private static byte UNIVERSAL_NON_RT = 0x7e;
    private static byte SYSEX_GLOBAL_CH = 0x7f;
    private static byte IDENTITY_GEN_INFO = 0x06;
    private static byte IDENTITY_ID_REQ = 0x01;
    private static byte END_OF_SYSEX = 0xF7;
    public byte[] IDENTITY_REQUEST = [ EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, SYSEX_GLOBAL_CH, 
                                       IDENTITY_GEN_INFO, IDENTITY_ID_REQ, END_OF_SYSEX ];

}