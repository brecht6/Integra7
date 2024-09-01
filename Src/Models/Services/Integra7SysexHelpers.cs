using System;
using System.Diagnostics;
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

    public static byte[] IDENTITY_REQUEST = ByteUtils.Flatten(EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, SYSEX_GLOBAL_CH, IDENTITY_GEN_INFO, IDENTITY_ID_REQ, END_OF_SYSEX);
    public static byte[] IDENTITY_REPLY = ByteUtils.Flatten(EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, DEVICE_ID, IDENTITY_GEN_INFO, IDENTITY_ID_REP, ROLAND_ID, ROLAND_DEVICE_FAMILY_CODE, ROLAND_DEVICE_FAMILY_NUMBER_CODE, ROLAND_DEVICE_FAMILY_SW_REV, END_OF_SYSEX);


    public static byte[] MakeStopPreviewPhraseMsg(byte devicId)
    {
        return ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [devicId], MODEL_ID, COMMAND_DATASET,
            [0x0f, 00, 0x20, 00, 0x0, 0x51], END_OF_SYSEX);
    }
    public static byte[] MakePlayPreviewPhraseMsg(byte channel, byte deviceId)
    {
        return ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [deviceId], MODEL_ID, COMMAND_DATASET,
            [0x0f, 00, 0x20, 00, (byte)(channel + 1), (byte)(0x50 - channel)], END_OF_SYSEX);
    }

    public static bool CheckIdentityReply(byte[] reply, out byte deviceId)
    {
        if (reply.Length > 2)
        {
            byte [] trimmedReply = TrimAfterEndOfSysex(reply);
            deviceId = trimmedReply[2];
            IDENTITY_REPLY[2] = deviceId;
            if (!trimmedReply.SequenceEqual(IDENTITY_REPLY))
            {
                Debug.WriteLine("Identity check failed.");
                ByteStreamDisplay.Display("Expected: ", IDENTITY_REPLY);
                ByteStreamDisplay.Display("Actual: ", trimmedReply);
                return false;
            }

            return true;
        }
        deviceId = 0;
        return false;
    }

    public static bool CheckIsDataSetMsg(byte[] reply)
    {
        // device id will be ignored
        byte[] expectedHeader = ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [0x10], MODEL_ID, COMMAND_DATASET);
        int len = expectedHeader.Length;
        byte[] header = ByteUtils.Slice(reply, 0, len);
        return header[0] == EXCLUSIVE_STATUS[0] && header[1] == ROLAND_ID[0] && header[3..6].SequenceEqual(MODEL_ID) && header[6] == COMMAND_DATASET[0];
    }

    public static byte[] ExtractPayload(byte[] reply)
    {
        // device id will be ignored
        byte[] expectedHeader = ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [0x10], MODEL_ID, COMMAND_DATASET);
        int len = expectedHeader.Length;
        int trimIdx = Array.IndexOf(reply, END_OF_SYSEX[0]);
        byte[] trimmedSysexReply = ByteUtils.Slice(reply, 0, trimIdx); // this already removes teh END_OF_SYSEX byte
        byte[] payload = ByteUtils.Slice(trimmedSysexReply, len, trimmedSysexReply.Length - len - 1); // -1 to remove the checksum
        return payload;
    }

    public static byte[] TrimAfterEndOfSysex(byte[] reply)
    {
        byte[] expectedHeader = ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [0x10], MODEL_ID, COMMAND_DATASET);
        int len = expectedHeader.Length;
        int trimIdx = Array.IndexOf(reply, END_OF_SYSEX[0]) + 1;
        byte[] trimmedSysexReply = ByteUtils.Slice(reply, 0, trimIdx);
        return trimmedSysexReply;
    }

    public static byte[] MakeDataRequest(byte deviceId, byte[] address, long size)
    {
        byte[] payload = ByteUtils.Flatten(address, ByteUtils.IntToBytes7_4(size));
        byte[] data = ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [deviceId], MODEL_ID, COMMAND_DATAREQ, payload, [ByteUtils.CheckSum(payload)], END_OF_SYSEX);
        return data;
    }

    public static byte[] MakeDataSet(byte deviceId, byte[] address, byte[] data)
    {
        byte[] payload = ByteUtils.Flatten(address, data);
        byte[] msg = ByteUtils.Flatten(EXCLUSIVE_STATUS, ROLAND_ID, [deviceId], MODEL_ID, COMMAND_DATASET, payload, [ByteUtils.CheckSum(payload)], END_OF_SYSEX);
        return msg;
    }
}

