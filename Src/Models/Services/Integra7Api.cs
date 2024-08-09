using System.Collections.Generic;
using Commons.Music.Midi;
using CoreMidi;

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
    private byte _deviceId = 0;
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
        _midiOut?.SafeSend(data);
        byte[] reply = _midiIn?.GetReply() ?? [];
        return Integra7SysexHelpers.CheckIdentityReply(reply, out _deviceId);
    }

    public byte[] MakeDataRequest(byte[] address, long size)
    {
        byte[] data = Integra7SysexHelpers.MakeDataRequest(DeviceId(), address, size);
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
        for (int i = 0; i < 16; i++)
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

    public void ChangePreset(byte Channel, int Msb, int Lsb, int Pc)
    {
        BankSelectMsb(Channel, Msb);
        BankSelectLsb(Channel, Lsb);
        ProgramChange(Channel, Pc - 1);
    }
}