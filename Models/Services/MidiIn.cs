using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Commons.Music.Midi;
using ReactiveUI;

public interface IMidiIn
{
    public void SetCustomReplyHandler(EventHandler<MidiReceivedEventArgs> eventHandler);
    public void RemoveCustomReplyHandler(bool reinstateDefaultHandler);
}


public class MidiIn : IMidiIn
{
    private readonly IMidiAccess2? _midiAccessManager = null;
    private IMidiInput? _access = null;
    private IMidiPortDetails? _midiPortDetails = null;
    private byte[] _data = [];
    private event EventHandler<MidiReceivedEventArgs> _lastEventHandler;

    public MidiIn(string Name) 
    {
        _midiAccessManager = MidiAccessManager.Default as IMidiAccess2;
        try {
            _midiPortDetails = _midiAccessManager?.Inputs.Where(x => x.Name.Contains(Name)).Last();
            _access = _midiAccessManager?.OpenInputAsync(_midiPortDetails?.Id).Result;
            if (_access != null)
            {
                _lastEventHandler = DefaultHandler;
                _access.MessageReceived += _lastEventHandler;
            }
        } catch(System.InvalidOperationException) {
            _midiPortDetails = null;
        }
    }

    public void SetCustomReplyHandler(EventHandler<MidiReceivedEventArgs> eventHandler)
    {
        if (_access != null)
        {
            if (_lastEventHandler != null) {
                _access.MessageReceived -= _lastEventHandler;
            }
            _lastEventHandler = eventHandler;
            _access.MessageReceived += _lastEventHandler;
        }
    }

    public void RemoveCustomReplyHandler(bool reinstateDefaultHandler)
    {
        if (_access != null) 
        {
            if (_lastEventHandler != null) {
                _access.MessageReceived -= _lastEventHandler;
                if (reinstateDefaultHandler) {
                    _lastEventHandler = DefaultHandler;
                    _access.MessageReceived += _lastEventHandler;
                }
            }
        }
    }

    private void DefaultHandler(object? sender, MidiReceivedEventArgs e)
    {
        StringBuilder hex = new StringBuilder(e.Length * 2);
        for (int i = 0; i < e.Length; i++)
        {
            hex.AppendFormat("{0:x2} ", e.Data[i]);
        }
        Debug.WriteLine(hex.ToString());
    }
}