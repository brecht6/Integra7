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
        _tcs?.TrySetResult(e.Data);
    }

    public async Task<byte[]> WaitForMidiMessageAsync()
    {
        _tcs = new TaskCompletionSource<byte[]>();
        // Start listening for MIDI messages
        byte[] message = await _tcs.Task;
        _midiInput.ConfigureDefaultHandler();
        return message;
    }

    public void CleanupAfterTimeOut()
    {
        _midiInput.RestoreAutomaticHandling();
    }
}