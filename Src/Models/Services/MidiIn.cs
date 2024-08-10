using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

using Commons.Music.Midi;

namespace Integra7AuralAlchemist.Models.Services;
public interface IMidiIn
{
    public byte[] GetReply();
    public void DiscardUnhandledPreviousReply();
}


public class MidiIn : IMidiIn
{
    private readonly IMidiAccess2? _midiAccessManager = null;
    private IMidiInput? _access = null;
    private IMidiPortDetails? _midiPortDetails = null;
    private event EventHandler<MidiReceivedEventArgs> _lastEventHandler;
    private static ManualResetEvent _replyReady = new ManualResetEvent(false);
    private byte[] _replyData = [];
#if DEBUG    
    public bool Verbose { get; set; } = true;
#else
    public bool Verbose { get; set; } = false;
#endif

    public MidiIn(string Name)
    {
        _midiAccessManager = MidiAccessManager.Default as IMidiAccess2;
        _lastEventHandler = DefaultHandler;
        try
        {
            _midiPortDetails = _midiAccessManager?.Inputs.Where(x => x.Name.Contains(Name)).Last();       
            _access = _midiAccessManager?.OpenInputAsync(_midiPortDetails?.Id).Result;
            if (_access != null)
            {
                _access.MessageReceived += _lastEventHandler;
            }
        }
        catch (System.InvalidOperationException)
        {
            _midiPortDetails = null;
        }
    }

    private void DefaultHandler(object? sender, MidiReceivedEventArgs e)
    {
        _replyReady.Reset();
        _replyData = new byte[e.Length];
        Debug.WriteLineIf(e.Length == 0, "WHY?! BROKEN");
        Array.Copy(e.Data, _replyData, e.Length);
        _replyReady.Set();
        if (Verbose)
        {
            StringBuilder hex = new StringBuilder(e.Length * 2);
            for (int i = 0; i < e.Length; i++)
            {
                hex.AppendFormat("{0:x2} ", e.Data[i]);
            }
            Debug.WriteLine(hex.ToString());
        }
    }

    public byte[] GetReply()
    {
        if (_replyReady.WaitOne(5000))
        {
            _replyReady.Reset();
            return _replyData;
        }
        else
        {
            // if no reply after 5 seconds, most likely no reply will come anymore...
            _replyReady.Reset();
            return [];
        }
    }

    public void DiscardUnhandledPreviousReply()
    {
        _replyReady.Reset();
    }
}