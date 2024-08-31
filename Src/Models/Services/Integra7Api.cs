using System.Collections.Generic;
using Commons.Music.Midi;
using CoreMidi;
using Integra7AuralAlchemist.Models.Data;
using ReactiveUI;

namespace Integra7AuralAlchemist.Models.Services;

public interface IIntegra7Api
{
    byte DeviceId();
    bool CheckIdentity();
    bool ConnectionOk();
    void NoteOn(byte Channel, byte Note, byte Velocity);
    void NoteOff(byte Channel, byte Note);
    void AllNotesOff();
    void ChangePreset(byte Channel, int Msb, int Lsb, int Pc);

    byte[] MakeDataRequest(byte[] address, long size);
    void MakeDataTransmission(byte[] address, byte[] data);
}

public class Integra7Api : IIntegra7Api
{
    private IMidiOut? _midiOut;
    private IMidiIn? _midiIn;
    private byte _deviceId;

    public byte DeviceId()
    {
        return _deviceId;
    }

    public Integra7Api(IMidiOut midiOut, IMidiIn midiIn)
    {
        _midiOut = midiOut;
        _midiIn = midiIn;
        if (!CheckIdentity())
        {
            _midiOut = null;
            _midiIn = null;
            _deviceId = 0;
        }
    }


    public bool CheckIdentity()
    {
        byte[] data = Integra7SysexHelpers.IDENTITY_REQUEST;
        _midiIn?.AnnounceIntentionToManuallyHandleReply();
        _midiOut?.SafeSend(data);
        byte[] reply = _midiIn?.GetReply() ?? [];
        byte[] usefulreply = Integra7SysexHelpers.TrimAfterEndOfSysex(reply);
        return Integra7SysexHelpers.CheckIdentityReply(usefulreply, out _deviceId);
    }

    public byte[] MakeDataRequest(byte[] address, long size)
    {
        byte[] data = Integra7SysexHelpers.MakeDataRequest(DeviceId(), address, size);
        _midiIn?.AnnounceIntentionToManuallyHandleReply();
        _midiOut?.SafeSend(data);
        byte[] reply = _midiIn?.GetReply() ?? [];
        return reply;
    }

    public void MakeDataTransmission(byte[] address, byte[] data)
    {
        byte[] transmission = Integra7SysexHelpers.MakeDataSet(DeviceId(), address, data);
        _midiOut?.SafeSend(transmission);
    }

    public bool ConnectionOk()
    {
        return _midiOut?.ConnectionOk() ?? false;
    }

    public void NoteOn(byte Channel, byte Note, byte Velocity)
    {
        byte[] data = [(byte)(Integra7MidiControlNos.NoteOn + Channel), Note, Velocity];
        _midiOut?.SafeSend(data);
    }

    public void NoteOff(byte Channel, byte Note)
    {
        byte[] data = [(byte)(Integra7MidiControlNos.NoteOff + Channel), Note, 0];
        _midiOut?.SafeSend(data);
    }

    public void AllNotesOff()
    {
        for (int i = 0; i < Constants.NO_OF_PARTS; i++)
        {
            byte[] data = [(byte)(Integra7MidiControlNos.AllNotesOff + i), 0x7C, 0x00];
            _midiOut?.SafeSend(data);
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

    public void ChangePreset(byte Channel, int Msb, int Lsb, int Pc)
    {
        BankSelectMsb(Channel, Msb);
        BankSelectLsb(Channel, Lsb);
        ProgramChange(Channel, Pc - 1);
        MessageBus.Current.SendMessage(new UpdateResyncPart(Channel));
    }
}