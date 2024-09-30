using System.Threading;
using System.Threading.Channels;
using Commons.Music.Midi;

namespace Integra7AuralAlchemist.Models.Services;

using System;
using System.Threading.Tasks;

public class AsyncMidiInputWrapper
{
    private const double inactivityTimespan = 0.5;
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
        byte[] localCopy = new byte[e.Length];
        Buffer.BlockCopy(e.Data, 0, localCopy, 0, e.Length);
        ByteStreamDisplay.Display($"Received {localCopy.Length} bytes (async): ", localCopy);
        //ByteStreamDisplay.Display($"Writing {localCopy.Length} bytes into channel: ", localCopy);
        //Log.Debug($"Writing {localCopy.Length} bytes into channel");
        _channel.Writer.TryWrite(localCopy);
    }

    public async Task<byte[]> WaitForMidiMessageAsync()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<bool> waitForData = _channel.Reader.WaitToReadAsync(cts.Token).AsTask();
        Task waitForTimeout = Task.Delay(TimeSpan.FromSeconds(inactivityTimespan), cts.Token);

        if (await Task.WhenAny(waitForTimeout, waitForData) == waitForData)
        {
            byte[] message = await _channel.Reader.ReadAsync(cts.Token);
            await cts.CancelAsync();
            _midiInput.ConfigureDefaultHandler();
            return message;
        }

        await cts.CancelAsync();
        _midiInput.ConfigureDefaultHandler();
        return [];
    }

    public async Task<byte[]> WaitForMidiMessageAsyncExpectingMultipleInARow()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<bool> waitForData = _channel.Reader.WaitToReadAsync(cts.Token).AsTask();
        Task waitForTimeout = Task.Delay(TimeSpan.FromSeconds(inactivityTimespan), cts.Token);

        if (await Task.WhenAny(waitForTimeout, waitForData) == waitForData)
        {
            byte[] message = await _channel.Reader.ReadAsync(cts.Token);
            await cts.CancelAsync();
            return message;
        }

        await cts.CancelAsync();
        return [];
    }

    public void CleanupAfterTimeOut()
    {
        _midiInput.ConfigureDefaultHandler();
        _midiInput.RestoreAutomaticHandling();
        try
        {
            _channel.Writer.Complete();   
        }
        catch (System.Threading.Channels.ChannelClosedException e)
        {
            // ignore
        }
    }
}