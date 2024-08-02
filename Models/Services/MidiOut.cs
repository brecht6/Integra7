using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Commons.Music.Midi;
using CoreMidi;


namespace Integra7AuralAlchemist.Models.Services;

interface IMidiOut 
{
    bool ConnectionOk();    
    void NoteOn(byte Channel, byte Note, byte Velocity);
    void NoteOff(byte Channel, byte Note);
    void ChangePreset(byte Channel, int Msb, int Lsb, int Pc);
}

internal class MidiOut : IMidiOut 
{
    private readonly IMidiAccess2? _midiAccessManager = null;
    private IMidiOutput? _access = null;
    private IMidiPortDetails? _midiPortDetails = null;
    public MidiOut(string Name) 
    {
        _midiAccessManager = MidiAccessManager.Default as IMidiAccess2;
        try {
            _midiPortDetails = _midiAccessManager?.Outputs.Where(x => x.Name.Contains(Name)).Last();
        } catch(System.InvalidOperationException) {
            _midiPortDetails = null;
        }
    }
    
    public bool ConnectionOk() => _midiPortDetails != null;

    private void SafeSend(byte[] data) 
    {
        try { 
            if (_access is null) {
                _access = _midiAccessManager?.OpenOutputAsync(_midiPortDetails?.Id).Result;
            }
            _access?.Send(data, 0, data.Length, 0);
        } catch (System.ArgumentException) {
            _midiPortDetails = null;
            _access = null;
        } catch (System.NullReferenceException){
            _midiPortDetails = null;
            _access = null;
        }
    }

    public void NoteOn(byte Channel, byte Note, byte Velocity)
    {
        byte[] data = [(byte)(MidiEvent.NoteOn + Channel), Note, Velocity];
        SafeSend(data);
    }

    public void NoteOff(byte Channel, byte Note)
    {
        byte[] data = [(byte)(MidiEvent.NoteOff + Channel), Note, 0];
        SafeSend(data);
    }

    private void BankSelectMsb(byte Channel, int BankNumberMsb) {
        ISet<int> PossibleBankMsb = new HashSet<int>{ 85, 86, 87, 88, 89, 92, 93, 95, 96, 97, 120, 121};
        if (PossibleBankMsb.Contains(BankNumberMsb)) {
            byte[] data = [(byte)(MidiEvent.CC + Channel), 0, (byte)BankNumberMsb];
            SafeSend(data);
        } else {
            throw new MidiException("Trying to select impossible MSB Banknumber: " + BankNumberMsb);
        }
    }

    private void BankSelectLsb(byte Channel, int BankNumberLsb) {
        if (0 <= BankNumberLsb && BankNumberLsb <= 127) {
            byte[] data = [(byte)(MidiEvent.CC + Channel), 0x20, (byte)BankNumberLsb];
            SafeSend(data);
        } else {
            throw new MidiException("Trying to select impossible LSB BankNumber: " + BankNumberLsb);
        }
    }

    private void ProgramChange(byte Channel, int ProgramNumber) {
        byte[] data = [(byte)(MidiEvent.Program + Channel), (byte)ProgramNumber];
        SafeSend(data);
    }

    public void ChangePreset(byte Channel, int Msb, int Lsb, int Pc)
    {
        BankSelectMsb(0, Msb);
        BankSelectLsb(0, Lsb);
        ProgramChange(0, Pc -1);
    }
}
