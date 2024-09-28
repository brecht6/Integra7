using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Commons.Music.Midi;
using Integra7AuralAlchemist.Models.Data;
using ReactiveUI;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;
public interface IMidiIn
{
    public void ConfigureHandler(EventHandler<MidiReceivedEventArgs> handler);
    public void ConfigureDefaultHandler();
    public byte[] GetReply();
    public void AnnounceIntentionToManuallyHandleReply();
    public void RestoreAutomaticHandling();    
}


public class MidiIn : IMidiIn
{
    private readonly IMidiAccess2? _midiAccessManager;
    private IMidiInput? _access;
    private IMidiPortDetails? _midiPortDetails;
    private bool _manualReplyHandling;
    public bool ManualReplyHandling { 
        get => _manualReplyHandling;
        set
        {
            Log.Debug($"Set manual MIDI reply handling to {value}.");
            _manualReplyHandling = value;
        }
    }
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
        _manualReplyHandling = false;
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

    public void ConfigureDefaultHandler()
    {
        if (_access == null)
            return; 
        
        _access.MessageReceived -= _lastEventHandler;
        _lastEventHandler = DefaultHandler;
        _access.MessageReceived += _lastEventHandler;
        _manualReplyHandling = false;
    }

    public void ConfigureHandler(EventHandler<MidiReceivedEventArgs> handler)
    {
        _access.MessageReceived -= _lastEventHandler;
        _lastEventHandler = handler;
        _access.MessageReceived += _lastEventHandler;
        _manualReplyHandling = false;
    }

    private void DefaultHandler(object? sender, MidiReceivedEventArgs e)
    {
        _replyReady.Reset();
        _replyData = new byte[e.Length];
        var localCopy = new byte[e.Length];
        Debug.Assert(e.Length != 0);
        Array.Copy(e.Data, localCopy, e.Length);
        Array.Copy(localCopy, _replyData, e.Length);
        if (Verbose)
        {
            ByteStreamDisplay.Display("Received (default handler): ", localCopy);
        }
        if (!_manualReplyHandling)
        {
            if (Integra7SysexHelpers.CheckIsDataSetMsg(localCopy))
            {
                Log.Debug("Request UpdateSysexSpec");
                MessageBus.Current.SendMessage(new UpdateFromSysexSpec((localCopy)), "hw2ui");
            }
            else if (Integra7Api.CheckIsPartOfPresetChange(localCopy, out byte midiChannel)) {
                Log.Debug($"Request UpdateSetPresetandResyncPart for channel {midiChannel}");
                MessageBus.Current.SendMessage(new UpdateSetPresetAndResyncPart(midiChannel));
            }
            else
            {
                Log.Debug($"Received MIDI msg that will not be dispatched for ui update.");
                ByteStreamDisplay.Display("The message was: ", localCopy);
            }
        }
        else
        {
            Log.Debug($"Received MIDI msg that will not be dispatched for ui update because manual reply handling is active.");
        }
        _manualReplyHandling = false;
        _replyReady.Set();
    }

    public byte[] GetReply()
    {
        if (_replyReady.WaitOne(500))
        {
            _replyReady.Reset();
            return _replyData;
        }

        // if no reply after 5 seconds, most likely no reply will come anymore...
        _replyReady.Reset();
        _manualReplyHandling = false;
        return [];
    }

    public void AnnounceIntentionToManuallyHandleReply()
    {
        _replyReady.Reset();
        _manualReplyHandling = true;
    }

    public void RestoreAutomaticHandling()
    {
        _replyReady.Set();
        _manualReplyHandling = false;
    }
}