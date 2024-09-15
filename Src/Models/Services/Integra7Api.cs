using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commons.Music.Midi;
using CoreMidi;
using Integra7AuralAlchemist.Models.Data;
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
}

public class Integra7Api : IIntegra7Api
{
    private IMidiOut? _midiOut;
    private IMidiIn? _midiIn;
    private byte _deviceId;
    private SemaphoreSlim _semaphore;

    public byte DeviceId()
    {
        return _deviceId;
    }

    public Integra7Api(IMidiOut midiOut, IMidiIn midiIn, SemaphoreSlim semaphore)
    {
        _midiOut = midiOut;
        _midiIn = midiIn;
        _semaphore = semaphore;
    }
    
    public async Task CheckIdentityAsync()
    {
        byte[] data = Integra7SysexHelpers.IDENTITY_REQUEST;
        AsyncMidiInputWrapper mi = new AsyncMidiInputWrapper(_midiIn);
        _midiOut?.SafeSend(data);
        byte[] reply = [];
        try
        {
            reply = await mi.WaitForMidiMessageAsync().WaitAsync(TimeSpan.FromSeconds(2));
        }
        catch (TimeoutException ex)
        {
            mi.CleanupAfterTimeOut();
            Log.Error(ex, $"Timeout waiting for MIDI reply while connecting to Integra-7 {ex.Message}");
            _midiOut = null;
            _midiIn = null;
            _deviceId = 0;
        }
        
        byte[] usefulreply = Integra7SysexHelpers.TrimAfterEndOfSysex(reply);
        if (!Integra7SysexHelpers.CheckIdentityReply(usefulreply, out _deviceId))
        {
            _midiOut = null;
            _midiIn = null;
            _deviceId = 0;
        }
    }

    public async Task<byte[]> MakeDataRequestAsync(byte[] address, long size)
    {
        await _semaphore.WaitAsync();
        try
        {
            Log.Debug($"DataRequest Lock acquired");
            byte[] data = Integra7SysexHelpers.MakeDataRequest(DeviceId(), address, size);
            AsyncMidiInputWrapper mi = new AsyncMidiInputWrapper(_midiIn);
            _midiOut?.SafeSend(data);
            byte[] reply = [];
            try
            {
                reply = await mi.WaitForMidiMessageAsync().WaitAsync(TimeSpan.FromSeconds(2));
            }
            catch (TimeoutException ex)
            {
                mi.CleanupAfterTimeOut();
                Log.Error(ex, $"Timeout waiting for MIDI reply after data request {ex.Message}");
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
        byte[] transmission = Integra7SysexHelpers.MakeDataSet(DeviceId(), address, data);
        AsyncMidiOutputWrapper w = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await w.SafeSendAsync(transmission);
    }

    public bool ConnectionOk()
    {
        return _midiOut?.ConnectionOk() ?? false;
    }

    public async Task NoteOnAsync(byte Channel, byte Note, byte Velocity)
    {
        byte[] data = [(byte)(Integra7MidiControlNos.NoteOn + Channel), Note, Velocity];
        AsyncMidiOutputWrapper mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(data);
    }

    public async Task NoteOffAsync(byte Channel, byte Note)
    {
        byte[] data = [(byte)(Integra7MidiControlNos.NoteOff + Channel), Note, 0];
        AsyncMidiOutputWrapper mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(data);
    }

    public async Task AllNotesOffAsync()
    {
        for (int i = 0; i < Constants.NO_OF_PARTS; i++)
        {
            byte[] data = [(byte)(Integra7MidiControlNos.AllNotesOff + i), 0x7C, 0x00];
            AsyncMidiOutputWrapper mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
            await mo.SafeSendAsync(data);
        }
    }

    public async Task SendStopPreviewPhraseMsgAsync()
    {
        byte[] stop = Integra7SysexHelpers.MakeStopPreviewPhraseMsg(_deviceId);
        AsyncMidiOutputWrapper mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(stop);
    }

    public async Task SendPlayPreviewPhraseMsgAsync(byte channel)
    {
        byte[] start = Integra7SysexHelpers.MakePlayPreviewPhraseMsg(channel, _deviceId);
        AsyncMidiOutputWrapper mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(start);
    }

    public async Task SendLoadSrxAsync(byte srx_slot1, byte srx_slot2, byte srx_slot3, byte srx_slot4)
    {
        byte[] msg = Integra7SysexHelpers.MakeLoadSrxMsg(srx_slot1, srx_slot2, srx_slot3, srx_slot4, _deviceId);
        AsyncMidiOutputWrapper mo = new AsyncMidiOutputWrapper(_midiOut, _semaphore);
        await mo.SafeSendAsync(msg);
    }

    public async Task<(byte /*slot1*/, byte/* slot2*/, byte /*slot3*/, byte /*slot4*/)> GetLoadedSrxAsync()
    {
        byte[] msg = Integra7SysexHelpers.MakeAskLoadedSrxMsg(_deviceId);
        try
        {
            await _semaphore.WaitAsync();
            AsyncMidiInputWrapper mi = new AsyncMidiInputWrapper(_midiIn);
            _midiOut?.SafeSend(msg);
            byte[] reply = [];
            try
            {
                reply = await mi.WaitForMidiMessageAsync().WaitAsync(TimeSpan.FromSeconds(2));
            }
            catch (TimeoutException ex)
            {
                mi.CleanupAfterTimeOut();
                Log.Error(ex, $"Timeout waiting for MIDI reply while requesting loaded SRX: {ex.Message}");
            }
            if (reply.Length > 15)
            {
                return (reply[11], reply[12], reply[13], reply[14]);
            }
            return (0, 0, 0, 0);
        }
        finally
        {
            _semaphore.Release();
        }
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
        if (reply.Length > 2 && reply[0] >= MidiEvent.CC && reply[0] <= (MidiEvent.CC + 15) && reply[1] == 0x00)
        {
            midiChannel = (byte)(reply[0] - MidiEvent.CC);
            return true;
        }

        // ckeck for bank select lsb
        if (reply.Length > 2 && reply[0] >= MidiEvent.CC && reply[0] <= (MidiEvent.CC + 15) && reply[1] == 0x20)
        {
            midiChannel = (byte)(reply[0] - MidiEvent.CC);
            return true;
        }

        // check for program change
        if (reply.Length > 1 && reply[0] >= MidiEvent.Program && reply[0] <= (MidiEvent.Program + 15))
        {
            midiChannel = (byte)(reply[0] - MidiEvent.Program);
            return true;
        }

        return false;
    }

    public async Task ChangePresetAsync(byte Channel, int Msb, int Lsb, int Pc)
    {
        BankSelectMsb(Channel, Msb);
        BankSelectLsb(Channel, Lsb);
        ProgramChange(Channel, Pc - 1);
        MessageBus.Current.SendMessage(new UpdateResyncPart(Channel));
    }
}