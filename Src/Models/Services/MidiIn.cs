using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Commons.Music.Midi;
using Integra7AuralAlchemist.Models.Data;
using ReactiveUI;

namespace Integra7AuralAlchemist.Models.Services;
public interface IMidiIn
{
    public byte[] GetReply();
    public void AnnounceIntentionToManuallyHandleReply();
}


public class MidiIn : IMidiIn
{
    private readonly IMidiAccess2? _midiAccessManager;
    private IMidiInput? _access;
    private IMidiPortDetails? _midiPortDetails;
    private bool _manualReplyHandling;
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
        catch (InvalidOperationException)
        {
            _midiPortDetails = null;
        }
    }

    private void DefaultHandler(object? sender, MidiReceivedEventArgs e)
    {
        _replyReady.Reset();
        _replyData = new byte[e.Length];
        Debug.Assert(e.Length != 0);
        Array.Copy(e.Data, _replyData, e.Length);
        _replyReady.Set();
        if (Verbose)
        {
            ByteStreamDisplay.Display("Received: ", e.Data);
        }
        if (!_manualReplyHandling)
        {
            if (Integra7SysexHelpers.CheckIsDataSetMsg(e.Data))
                MessageBus.Current.SendMessage(new UpdateFromSysexSpec(e.Data), "hw2ui");
            else if (Integra7Api.CheckIsPartOfPresetChange(e.Data, out byte midiChannel)) {
                MessageBus.Current.SendMessage(new UpdateResyncPart(midiChannel));
            }
        }
        _manualReplyHandling = false;
    }

    public byte[] GetReply()
    {
        if (_replyReady.WaitOne(5000))
        {
            _replyReady.Reset();
            return _replyData;
        }

        // if no reply after 5 seconds, most likely no reply will come anymore...
        _replyReady.Reset();
        return [];
    }

    public void AnnounceIntentionToManuallyHandleReply()
    {
        _replyReady.Reset();
        _manualReplyHandling = true;
    }
}