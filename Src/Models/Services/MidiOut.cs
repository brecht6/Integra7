using System;
using System.Linq;
using Commons.Music.Midi;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;

public interface IMidiOut
{
    public bool ConnectionOk();
    public void SafeSend(byte[] data);
}

public class MidiOut : IMidiOut
{
    private readonly IMidiAccess2? _midiAccessManager;
    private IMidiOutput? _access;
    private IMidiPortDetails? _midiPortDetails;
#if DEBUG
    public bool Verbose { get; set; } = true;
#else
    public bool Verbose { get; set; } = false;
#endif
    public MidiOut(string Name)
    {
        _midiAccessManager = MidiAccessManager.Default as IMidiAccess2;
        try
        {
            var outputs = _midiAccessManager?.Outputs.Where(x => x.Name.Contains(Name));
            if (!outputs.Any())
                _midiPortDetails = null;
            else
                _midiPortDetails = outputs.Last();
        }
        catch (InvalidOperationException)
        {
            _midiPortDetails = null;
        }
    }

    public bool ConnectionOk() => _midiPortDetails != null;

    public void SafeSend(byte[] data)
    {
        try
        {
            if (_access is null)
            {
                if (_midiPortDetails is null)
                {
                    Log.Error("No MIDI message sent because no Integra-7 hardware found.");
                    return;
                }
                _access = _midiAccessManager?.OpenOutputAsync(_midiPortDetails?.Id).Result;
            }
            _access?.Send(data, 0, data.Length, 0);
            if (Verbose)
            {
                if (_access is null)
                {
                    Log.Debug("_access is null... cannot complete the following: ");
                }
                ByteStreamDisplay.Display("Sent: ", data);
            }
        }
        catch (ArgumentException)
        {
            _midiPortDetails = null;
            _access = null;
        }
        catch (NullReferenceException)
        {
            _midiPortDetails = null;
            _access = null;
        }
    }
}
