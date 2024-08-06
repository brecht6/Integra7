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

    public static byte[] IDENTITY_REQUEST = ByteUtils.Flatten([ EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, SYSEX_GLOBAL_CH,
                                       IDENTITY_GEN_INFO, IDENTITY_ID_REQ, END_OF_SYSEX ]);
    public static byte[] IDENTITY_REPLY = ByteUtils.Flatten([EXCLUSIVE_STATUS, UNIVERSAL_NON_RT, DEVICE_ID,
                                    IDENTITY_GEN_INFO, IDENTITY_ID_REP, ROLAND_ID,
                                    ROLAND_DEVICE_FAMILY_CODE, ROLAND_DEVICE_FAMILY_NUMBER_CODE, ROLAND_DEVICE_FAMILY_SW_REV,
                                    END_OF_SYSEX]);

    public static byte[] StartAddress_Setup = [0x01, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_System = [0x02, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_TempStudioSet = [0x18, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part1 = [0x19, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part2 = [0x19, 0x20, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part3 = [0x19, 0x40, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part4 = [0x19, 0x60, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part5 = [0x1A, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part6 = [0x1A, 0x20, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part7 = [0x1A, 0x40, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part8 = [0x1A, 0x60, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part9 = [0x1B, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part10 = [0x1B, 0x20, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part11 = [0x1B, 0x40, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part12 = [0x1B, 0x60, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part13 = [0x1C, 0x00, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part14 = [0x1C, 0x20, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part15 = [0x1C, 0x40, 0x00, 0x00];
    public static byte[] StartAddress_TemporaryTone_Part16 = [0x1C, 0x60, 0x00, 0x00];
    public static byte[] Offset_System_Common = [0x00, 0x00, 0x00];
    public static byte[] Offset_TemporaryTone_PCM_Synth_Tone = [0x00, 0x00, 0x00];
    public static byte[] Offset_TemporaryTone_SN_Synth_Tone = [0x01, 0x00, 0x00];
    public static byte[] Offset_TemporaryTone_SN_Acoustic_Tone = [0x02, 0x00, 0x00];
    public static byte[] Offset_TemporaryTone_SN_Drum_Kit = [0x03, 0x00, 0x00];
    public static byte[] Offset_TemporaryTone_PCM_Drum_Kit = [0x10, 0x00, 0x00];
    public static byte[] Offset_StudioSet_Common = [0x00, 0x00, 0x00];
    public static byte[] Offset_StudioSet_Common_Chorus = [0x00, 0x04, 0x00];
    public static byte[] Offset_StudioSet_Common_Reverb = [0x00, 0x06, 0x00];
    public static byte[] Offset_StudioSet_Common_Motional_Surround = [0x00, 0x08, 0x00];
    public static byte[] Offset_StudioSet_Master_EQ = [0x00, 0x09, 0x00];
    public static byte[] Offset_StudioSet_MIDI_Ch(byte ZeroBasedChannel)
    {
        byte[] final = [0x00, 0x10, 0x00];
        for (byte b = 0; b < ZeroBasedChannel; b++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        }
        return final;
    }
    public static byte[] Offset_StudioSet_Part(byte ZeroBasedPart)
    {
        byte[] final = [0x00, 0x20, 00];
        for (byte b = 0; b < ZeroBasedPart; b++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        }
        return final;
    }
    public static byte[] Offset_StudioSet_Part_EQ(byte ZeroBasedPart)
    {
        byte[] final = [0x00, 0x50, 0x00];
        for (byte b = 0; b < ZeroBasedPart; b++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        }
        return final;
    }

    public static byte[] Offset_PCM_SynthTone_Common = [0x00, 0x00, 0x00];
    public static byte[] Offset_PCM_SynthTone_Common_MFX = [0x00, 0x02, 0x00];
    public static byte[] Offset_PCM_SynthTone_PartialMixTable = [0x00, 0x10, 0x00];
    public static byte[] Offset_PCM_SynthTone_Partial(int ZeroBasedPartial)
    {
        Debug.Assert(ZeroBasedPartial >= 0);
        Debug.Assert(ZeroBasedPartial <= 4);
        byte[] final = [0x00, 0x20, 0x00];
        for (int i = 0; i < ZeroBasedPartial; i++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
        }
        return final;
    }
    public static byte[] Offset_PCM_SynthTone_Common_2 = [0x00, 0x30, 0x00];

    public static byte[] Offset_PCM_DrumKit_Common = [0x00, 0x00, 0x00];
    public static byte[] Offset_PCM_DrumKit_Common_MFX = [0x00, 0x02, 0x00];
    public static byte[] Offset_PCM_DrumKit_Common_COMP_EQ = [0x00, 0x08, 0x00];

    public static byte[] Offset_PCM_DrumKit_Partial_Key(int KeyNumber)
    {
        Debug.Assert(KeyNumber >= 21);
        Debug.Assert(KeyNumber <= 108);
        if (KeyNumber >= 21 && KeyNumber <= 76)
        {
            byte[] final = [0x00, 0x10, 0x00];
            for (int i = 0; i < (KeyNumber - 21); i++)
            {
                final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
            }
            return final;
        }
        else if (KeyNumber >= 77 && KeyNumber <= 108)
        {
            byte[] final = [0x01, 0x00, 0x00];
            for (int i = 0; i < (KeyNumber - 77); i++)
            {
                final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
            }
            return final;
        }
        Debug.Assert(false); // invalid key number specified...
        return [];
    }

    public static byte[] Offset_SN_SynthTone_Common = [0x00, 0x00, 0x00];
    public static byte[] Offset_SN_SynthTone_MFX = [0x00, 0x02, 0x00];
    public static byte[] Offset_SN_SynthTone_Partial(int ZeroBasedPartial)
    {
        Debug.Assert(ZeroBasedPartial >= 0);
        Debug.Assert(ZeroBasedPartial <= 2);
        byte[] final = [0x00, 0x20, 0x00];
        for (int i =0; i < ZeroBasedPartial; i++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x00, 0x01, 0x00]);
        }
        return final;
    }

    public static byte[] Offset_Setup_SoundMode = [0x00, 0x00]; 

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

