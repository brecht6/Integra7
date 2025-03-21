using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Commons.Music.Midi;
using CoreMidi;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Domain;
using ReactiveUI;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;

public interface IIntegra7Api
{
    byte DeviceId();
    Task CheckIdentityAsync();
    bool ConnectionOk();
    Task NoteOnAsync(byte Channel, byte Note, byte Velocity);
    Task NoteOffAsync(byte Channel, byte Note);
    Task AllNotesOffAsync();
    Task ChangePresetAsync(byte Channel, int Msb, int Lsb, int Pc);

    Task<byte[]> MakeDataRequestAsync(byte[] address, long size);
    Task MakeDataTransmissionAsync(byte[] address, byte[] data);
    Task SendStopPreviewPhraseMsgAsync();
    Task SendLoadSrxAsync(byte srx_slot1, byte srx_slot2, byte srx_slot3, byte srx_slot4);
    Task<(byte, byte, byte, byte)> GetLoadedSrxAsync();
    Task SendPlayPreviewPhraseMsgAsync(byte channel);

    Task WriteToneToUserMemory(Integra7Domain i7domain, string toneTypeStr, byte zeroBasedPartNo, string name,
        int zeroBasedUserMemoryId);

    Task<List<string>> GetStudioSetNames0to63();
    Task<List<string>> GetPCMDrumKitUserNames0to31();
    Task<List<string>> GetPCMToneUserNames0to63();
    Task<List<string>> GetPCMToneUserNames64to127();
    Task<List<string>> GetPCMToneUserNames128to191();
    Task<List<string>> GetPCMToneUserNames192to255();
    Task<List<string>> GetSuperNATURALDrumKitUserNames0to63();
    Task<List<string>> GetSuperNATURALAcousticToneUserNames0to63();
    Task<List<string>> GetSuperNATURALAcousticToneUserNames64to127();
    Task<List<string>> GetSuperNATURALAcousticToneUserNames128to191();
    Task<List<string>> GetSuperNATURALAcousticToneUserNames192to255();
    Task<List<string>> GetSuperNATURALSynthToneUserNames0to63();
    Task<List<string>> GetSuperNATURALSynthToneUserNames64to127();
    Task<List<string>> GetSuperNATURALSynthToneUserNames128to191();
    Task<List<string>> GetSuperNATURALSynthToneUserNames192to255();
    Task<List<string>> GetSuperNATURALSynthToneUserNames256to319();
    Task<List<string>> GetSuperNATURALSynthToneUserNames320to383();
    Task<List<string>> GetSuperNATURALSynthToneUserNames384to447();
    Task<List<string>> GetSuperNATURALSynthToneUserNames448to511();
}

public class Integra7Api : IIntegra7Api
{
    private readonly SemaphoreSlim _semaphore;
    private byte _deviceId;
    private IMidiIn? _midiIn;
    private IMidiOut? _midiOut;

    public Integra7Api(IMidiOut midiOut, IMidiIn midiIn, SemaphoreSlim semaphore)
    {
        _midiOut = midiOut;
        _midiIn = midiIn;
        _semaphore = semaphore;
    }

    public byte DeviceId()
    {
        return _deviceId;
    }

    public async Task CheckIdentityAsync()
    {
        var data = Integra7SysexHelpers.IDENTITY_REQUEST;
        var mi = new AsyncMidiInputWrapper(_midiIn);
        _midiOut?.SafeSend(data);
        byte[] reply = [];
        reply = await mi.WaitForMidiMessageAsync();
        if (reply.Length == 0)
        {
            mi.CleanupAfterTimeOut();
            Log.Error("Timeout waiting for MIDI reply while connecting to Integra-7.");
            _midiOut = null;
            _midiIn = null;
            _deviceId = 0;
        }
        else
        {
            var usefulreply = Integra7SysexHelpers.TrimAfterEndOfSysex(reply);
            if (!Integra7SysexHelpers.CheckIdentityReply(usefulreply, out _deviceId))
            {
                _midiOut = null;
                _midiIn = null;
                _deviceId = 0;
            }
        }
    }

    public async Task<byte[]> MakeDataRequestAsync(byte[] address, long size)
    {
        await _semaphore.WaitAsync();
        try
        {
            Log.Debug("DataRequest Lock acquired");
            var data = Integra7SysexHelpers.MakeDataRequest(DeviceId(), address, size);
            var mi = new AsyncMidiInputWrapper(_midiIn);
            _midiOut?.SafeSend(data);
            var reply = await mi.WaitForMidiMessageAsync();
            if (reply.Length == 0)
            {
                mi.CleanupAfterTimeOut();
                Log.Error("Timeout waiting for MIDI reply after data request.");
                return [];
            }

            return reply;
        }
        finally
        {
            Log.Debug("DataRequest Lock released");
            _semaphore.Release();
        }
    }

    public async Task MakeDataTransmissionAsync(byte[] address, byte[] data)
    {
        var transmission = Integra7SysexHelpers.MakeDataSet(DeviceId(), address, data);
        var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await w.SafeSendAsync(transmission);
    }

    public bool ConnectionOk()
    {
        return _midiOut?.ConnectionOk() ?? false;
    }

    public async Task NoteOnAsync(byte Channel, byte Note, byte Velocity)
    {
        byte[] data = [(byte)(Integra7MidiControlNos.NoteOn + Channel), Note, Velocity];
        var mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(data);
    }

    public async Task NoteOffAsync(byte Channel, byte Note)
    {
        byte[] data = [(byte)(Integra7MidiControlNos.NoteOff + Channel), Note, 0];
        var mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(data);
    }

    public async Task AllNotesOffAsync()
    {
        for (var i = 0; i < Constants.NO_OF_PARTS; i++)
        {
            byte[] data = [(byte)(Integra7MidiControlNos.AllNotesOff + i), 0x7C, 0x00];
            var mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
            await mo.SafeSendAsync(data);
        }
    }

    public async Task SendStopPreviewPhraseMsgAsync()
    {
        var stop = Integra7SysexHelpers.MakeStopPreviewPhraseMsg(_deviceId);
        var mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(stop);
    }

    public async Task SendPlayPreviewPhraseMsgAsync(byte channel)
    {
        var start = Integra7SysexHelpers.MakePlayPreviewPhraseMsg(channel, _deviceId);
        var mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(start);
    }

    public async Task SendLoadSrxAsync(byte srx_slot1, byte srx_slot2, byte srx_slot3, byte srx_slot4)
    {
        var msg = Integra7SysexHelpers.MakeLoadSrxMsg(srx_slot1, srx_slot2, srx_slot3, srx_slot4, _deviceId);
        var mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(msg);
    }

    public async Task<(byte /*slot1*/, byte /* slot2*/, byte /*slot3*/, byte /*slot4*/)> GetLoadedSrxAsync()
    {
        var msg = Integra7SysexHelpers.MakeAskLoadedSrxMsg(_deviceId);
        try
        {
            await _semaphore.WaitAsync();
            var mi = new AsyncMidiInputWrapper(_midiIn);
            _midiOut?.SafeSend(msg);
            var reply = await mi.WaitForMidiMessageAsync();
            if (reply.Length == 0)
            {
                mi.CleanupAfterTimeOut();
                Log.Error("Timeout waiting for MIDI reply while requesting loaded SRX.");
            }

            if (reply.Length > 15) return (reply[11], reply[12], reply[13], reply[14]);

            return (0, 0, 0, 0);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<string>> GetStudioSetNames0to63()
    {
        var msg = Integra7SysexHelpers.MakeRequestStudioSetNames0to63Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetPCMDrumKitUserNames0to31()
    {
        var msg = Integra7SysexHelpers.MakeRequestPCMDrumKitUserNames0to31Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetPCMToneUserNames0to63()
    {
        var msg = Integra7SysexHelpers.MakeRequestPCMToneUserNames0to63Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetPCMToneUserNames64to127()
    {
        var msg = Integra7SysexHelpers.MakeRequestPCMToneUserNames64to127Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetPCMToneUserNames128to191()
    {
        var msg = Integra7SysexHelpers.MakeRequestPCMToneUserNames128to191Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetPCMToneUserNames192to255()
    {
        var msg = Integra7SysexHelpers.MakeRequestPCMToneUserNames192to255Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALDrumKitUserNames0to63()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALDrumKitUserNames0to63Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALAcousticToneUserNames0to63()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALAcousticToneUserNames0to63Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALAcousticToneUserNames64to127()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALAcousticToneUserNames64to127Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALAcousticToneUserNames128to191()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALAcousticToneUserNames128to191Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALAcousticToneUserNames192to255()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALAcousticToneUserNames192to255Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames0to63()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames0to63Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames64to127()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames64to127Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames128to191()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames128to191Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames192to255()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames192to255Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames256to319()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames256to319Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames320to383()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames320to383Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames384to447()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames384to447Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task<List<string>> GetSuperNATURALSynthToneUserNames448to511()
    {
        var msg = Integra7SysexHelpers.MakeRequestSuperNATURALSynthToneUserNames448to511Msg(_deviceId);
        return await GetListOfNamesHelper(msg);
    }

    public async Task WriteToneToUserMemory(Integra7Domain i7domain, string toneTypeStr, byte zeroBasedPartNo,
        string name,
        int zeroBasedUserMemoryId)
    {
        // 3-steps needed
        //
        // 1. write the current patch to the user memory -> this has the side effect of also selecting the new patch
        // 2. write the name to the user memory
        // 3. write the current patch (i.e. the selected user patch) to the user memory again to cement the new name
        var msb = 0;
        var lsb = 0;

        // step 1
        switch (toneTypeStr)
        {
            case "SN-A":
            {
                msb = 89;
                lsb = zeroBasedUserMemoryId >> 7;
                var msg =
                    Integra7SysexHelpers.MakeWriteSuperNATURALAcousticToneMsg(_deviceId, zeroBasedPartNo,
                        zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "SN-S":
            {
                msb = 95;
                lsb = zeroBasedUserMemoryId >> 7;
                var msg =
                    Integra7SysexHelpers.MakeWriteSuperNATURALSynthToneMsg(_deviceId, zeroBasedPartNo,
                        zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "SN-D":
            {
                msb = 88;
                lsb = zeroBasedUserMemoryId >> 7;
                var msg =
                    Integra7SysexHelpers.MakeWriteSuperNATURALDrumKitMsg(_deviceId, zeroBasedPartNo,
                        zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "PCMS":
            {
                msb = 87;
                lsb = zeroBasedUserMemoryId >> 7;
                var msg =
                    Integra7SysexHelpers.MakeWritePCMSynthToneMsg(_deviceId, zeroBasedPartNo, zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "PCMD":
            {
                msb = 86;
                lsb = 0;
                var msg =
                    Integra7SysexHelpers.MakeWritePCMDrumKitMsg(_deviceId, zeroBasedPartNo, zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
        }

        // step 2
        await ChangePresetNameAsync(i7domain, zeroBasedPartNo, toneTypeStr, name);

        // step 3: todo cleanup duplication with step 1
        switch (toneTypeStr)
        {
            case "SN-A":
            {
                var msg =
                    Integra7SysexHelpers.MakeWriteSuperNATURALAcousticToneMsg(_deviceId, zeroBasedPartNo,
                        zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "SN-S":
            {
                var msg =
                    Integra7SysexHelpers.MakeWriteSuperNATURALSynthToneMsg(_deviceId, zeroBasedPartNo,
                        zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "SN-D":
            {
                var msg =
                    Integra7SysexHelpers.MakeWriteSuperNATURALDrumKitMsg(_deviceId, zeroBasedPartNo,
                        zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "PCMS":
            {
                var msg =
                    Integra7SysexHelpers.MakeWritePCMSynthToneMsg(_deviceId, zeroBasedPartNo, zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
            case "PCMD":
            {
                var msg =
                    Integra7SysexHelpers.MakeWritePCMDrumKitMsg(_deviceId, zeroBasedPartNo, zeroBasedUserMemoryId);
                var w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
                await w.SafeSendAsync(msg);
            }
                break;
        }
    }

    public async Task ChangePresetAsync(byte Channel, int Msb, int Lsb, int Pc)
    {
        BankSelectMsb(Channel, Msb);
        BankSelectLsb(Channel, Lsb);
        ProgramChange(Channel, Pc - 1);
        MessageBus.Current.SendMessage(new UpdateResyncPart(Channel));
    }

    private void BankSelectMsb(byte Channel, int BankNumberMsb)
    {
        ISet<int> PossibleBankMsb = new HashSet<int> { 85, 86, 87, 88, 89, 92, 93, 95, 96, 97, 120, 121 };
        if (PossibleBankMsb.Contains(BankNumberMsb))
        {
            byte[] data = [(byte)(MidiEvent.CC + Channel), 0, (byte)BankNumberMsb];
            _midiOut?.SafeSend(data);
        }
        else
        {
            throw new MidiException("Trying to select impossible MSB Banknumber: " + BankNumberMsb);
        }
    }

    private void BankSelectLsb(byte Channel, int BankNumberLsb)
    {
        if (0 <= BankNumberLsb && BankNumberLsb <= 127)
        {
            byte[] data = [(byte)(MidiEvent.CC + Channel), 0x20, (byte)BankNumberLsb];
            _midiOut?.SafeSend(data);
        }
        else
        {
            throw new MidiException("Trying to select impossible LSB BankNumber: " + BankNumberLsb);
        }
    }

    private void ProgramChange(byte Channel, int ProgramNumber)
    {
        byte[] data = [(byte)(MidiEvent.Program + Channel), (byte)ProgramNumber];
        _midiOut?.SafeSend(data);
    }


    public static bool CheckIsPartOfPresetChange(byte[] reply, out byte midiChannel)
    {
        midiChannel = 0;
        // check for bank select msb
        if (reply.Length > 2 && reply[0] >= MidiEvent.CC && reply[0] <= MidiEvent.CC + 15 && reply[1] == 0x00)
        {
            midiChannel = (byte)(reply[0] - MidiEvent.CC);
            return true;
        }

        // ckeck for bank select lsb
        if (reply.Length > 2 && reply[0] >= MidiEvent.CC && reply[0] <= MidiEvent.CC + 15 && reply[1] == 0x20)
        {
            midiChannel = (byte)(reply[0] - MidiEvent.CC);
            return true;
        }

        // check for program change
        if (reply.Length > 1 && reply[0] >= MidiEvent.Program && reply[0] <= MidiEvent.Program + 15)
        {
            midiChannel = (byte)(reply[0] - MidiEvent.Program);
            return true;
        }

        return false;
    }

    private async Task<List<string>> GetListOfNamesHelper(byte[] msg)
    {
        await _semaphore.WaitAsync();
        var mi = new AsyncMidiInputWrapper(_midiIn);
        try
        {
            Log.Debug("DataRequest Lock acquired");
            List<byte[]> allReplies = [];
            var totalRepliesReceived = 0;
            _midiOut?.SafeSend(msg);
            var continueReading = true;
            while (continueReading) // concatenate multiple incoming replies 
            {
                var localReply = await mi.WaitForMidiMessageAsyncExpectingMultipleInARow();
                if (localReply.Length != 0)
                {
                    //Debug.WriteLine($"len: {localReply.Length}");
                    byte[][] multiplereplies = ByteUtils.SplitAfterF7(localReply);
                    foreach (var r in multiplereplies)
                        if (r is [0xf0, 0x41, ..])
                        {
                            allReplies.Add(r);
                            totalRepliesReceived += 1;
                            //ByteStreamDisplay.Display($"partial reply #{totalRepliesReceived}:", r); 
                        }
                }
                else
                {
                    continueReading = false;
                    mi.CleanupAfterTimeOut();
                    if (totalRepliesReceived == 0)
                    {
                        Log.Error("Timeout waiting for MIDI reply after data request.");
                        return [];
                    }
                }
            }

            List<string> names = [];
            foreach (var reply in allReplies)
            {
                var name = ByteUtils.Slice(reply, 16, 16);
                if (name[0] != 0x00) // last returned message contains all 00's
                    names.Add(Encoding.ASCII.GetString(name));
            }

            var idx = 0;
            foreach (var n in names)
            {
                idx++;
                Log.Debug($"{idx}: {n}");
            }

            return names;
        }
        finally
        {
            mi.CleanupAfterTimeOut();
            Log.Debug("DataRequest Lock released");
            _semaphore.Release();
        }
    }

    private async Task ChangePresetNameAsync(Integra7Domain i7domain, byte zeroBasedPartNo, string toneType,
        string name)
    {
        switch (toneType)
        {
            case "PCMD":
            {
                var d = i7domain.PCMDrumKitCommon(zeroBasedPartNo);
                d.ModifySingleParameterDisplayedValue("PCM Drum Kit Common/Kit Name", name);
                await d.WriteToIntegraAsync("PCM Drum Kit Common/Kit Name");
            }
                break;
            case "PCMS":
            {
                var d = i7domain.PCMSynthToneCommon(zeroBasedPartNo);
                d.ModifySingleParameterDisplayedValue("PCM Synth Tone Common/PCM Synth Tone Name", name);
                await d.WriteToIntegraAsync("PCM Synth Tone Common/PCM Synth Tone Name");
            }
                break;
            case "SN-A":
            {
                var d = i7domain.SNAcousticToneCommon(zeroBasedPartNo);
                d.ModifySingleParameterDisplayedValue("SuperNATURAL Acoustic Tone Common/Tone Name", name);
                await d.WriteToIntegraAsync("SuperNATURAL Acoustic Tone Common/Tone Name");
            }
                break;
            case "SN-S":
            {
                var d = i7domain.SNSynthToneCommon(zeroBasedPartNo);
                d.ModifySingleParameterDisplayedValue("SuperNATURAL Synth Tone Common/Tone Name", name);
                await d.WriteToIntegraAsync("SuperNATURAL Synth Tone Common/Tone Name");
            }
                break;
            case "SN-D":
            {
                var d = i7domain.SNDrumKitCommon(zeroBasedPartNo);
                d.ModifySingleParameterDisplayedValue("SuperNATURAL Drum Kit Common/Kit Name", name);
                await d.WriteToIntegraAsync("SuperNATURAL Drum Kit Common/Kit Name");
            }
                break;
        }
    }
}