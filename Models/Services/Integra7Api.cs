using System.Collections.Generic;
using Commons.Music.Midi;
using CoreMidi;

namespace Integra7AuralAlchemist.Models.Services;

public interface IIntegra7Api 
{
    bool ConnectionOk();    
    void NoteOn(byte Channel, byte Note, byte Velocity);
    void NoteOff(byte Channel, byte Note);
    void ChangePreset(byte Channel, int Msb, int Lsb, int Pc);
}

public class Integra7Api : IIntegra7Api
{
    private IMidiOut? _midiOut;
    public Integra7Api(IMidiOut midiOut)
    {
        _midiOut = midiOut;
    }

    public bool ConnectionOk(){
        if (_midiOut != null)
            return _midiOut.ConnectionOk();
        return false;
    }

    public void NoteOn(byte Channel, byte Note, byte Velocity)
    {
        byte[] data = [(byte)(MidiEvent.NoteOn + Channel), Note, Velocity];
        _midiOut?.SafeSend(data);
    }

    public void NoteOff(byte Channel, byte Note)
    {
        byte[] data = [(byte)(MidiEvent.NoteOff + Channel), Note, 0];
        _midiOut?.SafeSend(data);
    }

    private void BankSelectMsb(byte Channel, int BankNumberMsb) {
        ISet<int> PossibleBankMsb = new HashSet<int>{ 85, 86, 87, 88, 89, 92, 93, 95, 96, 97, 120, 121};
        if (PossibleBankMsb.Contains(BankNumberMsb)) {
            byte[] data = [(byte)(MidiEvent.CC + Channel), 0, (byte)BankNumberMsb];
            _midiOut?.SafeSend(data);
        } else {
            throw new MidiException("Trying to select impossible MSB Banknumber: " + BankNumberMsb);
        }
    }

    private void BankSelectLsb(byte Channel, int BankNumberLsb) {
        if (0 <= BankNumberLsb && BankNumberLsb <= 127) {
            byte[] data = [(byte)(MidiEvent.CC + Channel), 0x20, (byte)BankNumberLsb];
            _midiOut?.SafeSend(data);
        } else {
            throw new MidiException("Trying to select impossible LSB BankNumber: " + BankNumberLsb);
        }
    }

    private void ProgramChange(byte Channel, int ProgramNumber) {
        byte[] data = [(byte)(MidiEvent.Program + Channel), (byte)ProgramNumber];
        _midiOut.SafeSend(data);
    }

    public void ChangePreset(byte Channel, int Msb, int Lsb, int Pc)
    {
        BankSelectMsb(Channel, Msb);
        BankSelectLsb(Channel, Lsb);
        ProgramChange(Channel, Pc -1);
    }
}