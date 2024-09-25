using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Channels;
using Commons.Music.Midi;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;
using System;
using System.Threading.Tasks;

public class AsyncMidiInputWrapper
{
    private IMidiIn _midiInput;
    private readonly Channel<byte[]> _channel = Channel.CreateUnbounded<byte[]>();

    public AsyncMidiInputWrapper(IMidiIn midiIn)
    {
        _midiInput = midiIn;
        _midiInput.ConfigureHandler(OnMidiMessageReceived);
    }

    private void OnMidiMessageReceived(object? sender, MidiReceivedEventArgs e)
    {
        // Set the result of the TaskCompletionSource
        ByteStreamDisplay.Display($"Received (async): ", e.Data);
        byte[] localCopy = new byte[e.Data.Length];
        Buffer.BlockCopy(e.Data, 0, localCopy, 0, e.Data.Length);
        _channel.Writer.TryWrite(localCopy);
    }

    public async Task<byte[]> WaitForMidiMessageAsync()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<bool> waitForData = _channel.Reader.WaitToReadAsync(cts.Token).AsTask();
        Task waitForTimeout = Task.Delay(TimeSpan.FromSeconds(1), cts.Token);

        if (await Task.WhenAny(waitForTimeout, waitForData) == waitForData)
        {
            byte[] message = await _channel.Reader.ReadAsync(cts.Token);
            return message;
        }

        cts.Cancel();
        _midiInput.ConfigureDefaultHandler();
        return [];
    }
    
    public async Task<byte[]> WaitForMidiMessageAsyncExpectingMultipleInARow()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<bool> waitForData = _channel.Reader.WaitToReadAsync(cts.Token).AsTask();
        Task waitForTimeout = Task.Delay(TimeSpan.FromSeconds(2), cts.Token);

        if (await Task.WhenAny(waitForTimeout, waitForData) == waitForData)
        {
            byte[] message = await _channel.Reader.ReadAsync(cts.Token);
            return message;
        }

        cts.Cancel();
        return [];
    }

    public void CleanupAfterTimeOut()
    {
        _midiInput.ConfigureDefaultHandler();
        _midiInput.RestoreAutomaticHandling();
    }
}