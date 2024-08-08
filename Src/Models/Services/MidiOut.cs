using System.Linq;
using Commons.Music.Midi;


namespace Integra7AuralAlchemist.Models.Services;

public interface IMidiOut
{
    public bool ConnectionOk();
    public void SafeSend(byte[] data);
}

public class MidiOut : IMidiOut
{
    private readonly IMidiAccess2? _midiAccessManager = null;
    private IMidiOutput? _access = null;
    private IMidiPortDetails? _midiPortDetails = null;
    public MidiOut(string Name)
    {
        _midiAccessManager = MidiAccessManager.Default as IMidiAccess2;
        try
        {
            _midiPortDetails = _midiAccessManager?.Outputs.Where(x => x.Name.Contains(Name)).Last();
        }
        catch (System.InvalidOperationException)
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
                _access = _midiAccessManager?.OpenOutputAsync(_midiPortDetails?.Id).Result;
            }
            _access?.Send(data, 0, data.Length, 0);
        }
        catch (System.ArgumentException)
        {
            _midiPortDetails = null;
            _access = null;
        }
        catch (System.NullReferenceException)
        {
            _midiPortDetails = null;
            _access = null;
        }
    }
}
