using Commons.Music.Midi;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;
using System;
using System.Threading.Tasks;

public class AsyncMidiInputWrapper
{
    private IMidiIn _midiInput;
    private TaskCompletionSource<byte[]> _tcs;

    public AsyncMidiInputWrapper(IMidiIn midiIn)
    {
        _midiInput = midiIn;
        _midiInput.AnnounceIntentionToManuallyHandleReply();
        _midiInput.ConfigureHandler(OnMidiMessageReceived);
    }

    private void OnMidiMessageReceived(object? sender, MidiReceivedEventArgs e)
    {
        // Set the result of the TaskCompletionSource
        ByteStreamDisplay.Display("Received (async): ", e.Data);
        byte[] localCopy = new byte[e.Data.Length];
        Buffer.BlockCopy(e.Data, 0, localCopy, 0, e.Data.Length);
        _tcs?.TrySetResult(localCopy);
    }

    public async Task<byte[]> WaitForMidiMessageAsync()
    {
        _tcs = new TaskCompletionSource<byte[]>();
        // Start listening for MIDI messages
        byte[] message = await _tcs.Task;
        _midiInput.ConfigureDefaultHandler();
        return message;
    }
    
    public async Task<byte[]> WaitForMidiMessageAsyncExpectingMultipleInARow()
    {
        _tcs = new TaskCompletionSource<byte[]>();
        // Start listening for MIDI messages
        byte[] message = await _tcs.Task;
        _midiInput.AnnounceIntentionToManuallyHandleReply();
        return message;
    }

    public void CleanupAfterTimeOut()
    {
        _midiInput.ConfigureDefaultHandler();
        _midiInput.RestoreAutomaticHandling();
    }
}