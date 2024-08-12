using System.Linq;

namespace Integra7AuralAlchemist.Models.Services;

public class Integra7SysexHelpers
{
    private static byte[] EXCLUSIVE_STATUS = [0xF0];
    private static byte[] UNIVERSAL_NON_RT = [0x7e];
    private static byte[] SYSEX_GLOBAL_CH = [0x7f];
    private static byte[] IDENTITY_GEN_INFO = [0x06];
    private static byte[] IDENTITY_ID_REQ = [0x01];
    private static byte[] IDENTITY_ID_REP = [0x02];
    private static byte[] ROLAND_ID = [0x41];
    private static byte[] ROLAND_DEVICE_FAMILY_CODE = [0x64, 0x02];
    private static byte[] ROLAND_DEVICE_FAMILY_NUMBER_CODE = [0x00, 0x00];
    private static byte[] ROLAND_DEVICE_FAMILY_SW_REV = [0x00, 0x00, 0x00, 0x00];
    private static byte[] MODEL_ID = [0x00, 0x00, 0x64];
    private static byte[] END_OF_SYSEX = [0xF7];
    private static byte[] DEVICE_ID = [0x10];
    private static byte[] COMMAND_DATAREQ = [0x11];
    private static byte[] COMMAND_DATASET = [0x12];

    public static byte[] IDENTITY_REQUEST = ByteUtils.Flatten([ EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, SYSEX_GLOBAL_CH,
                                       IDENTITY_GEN_INFO, IDENTITY_ID_REQ, END_OF_SYSEX ]);
    public static byte[] IDENTITY_REPLY = ByteUtils.Flatten([EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, DEVICE_ID,
                                    IDENTITY_GEN_INFO, IDENTITY_ID_REP, ROLAND_ID,
                                    ROLAND_DEVICE_FAMILY_CODE, ROLAND_DEVICE_FAMILY_NUMBER_CODE, ROLAND_DEVICE_FAMILY_SW_REV,
                                    END_OF_SYSEX]);



    public static bool CheckIdentityReply(byte[] reply, out byte deviceId)
    {
        if (reply.Length > 2)
        {
            deviceId = reply[2];
            IDENTITY_REPLY[2] = deviceId;
            return reply.SequenceEqual(IDENTITY_REPLY);
        }
        deviceId = 0;
        return false;
    }

    public static byte[] MakeDataRequest(byte deviceId, byte[] address, long size)
    {
        byte[] payload = ByteUtils.Flatten([address, ByteUtils.IntToBytes7_4(size)]);
        byte[] data = ByteUtils.Flatten([EXCLUSIVE_STATUS, ROLAND_ID, [deviceId], MODEL_ID,
                                         COMMAND_DATAREQ, payload, [ByteUtils.CheckSum(payload)], END_OF_SYSEX]);
        return data;
    }

    public static byte[] MakeDataSet(byte deviceId, byte[] address, byte[] data)
    {
        byte[] payload = ByteUtils.Flatten([address, data]);
        byte[] msg = ByteUtils.Flatten([EXCLUSIVE_STATUS, ROLAND_ID, [deviceId], MODEL_ID,
                                        COMMAND_DATASET, payload, [ByteUtils.CheckSum(payload)], END_OF_SYSEX]);
        return msg;
    }
}

