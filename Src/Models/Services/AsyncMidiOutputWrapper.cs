using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;

public class AsyncMidiOutputWrapper
{
    private readonly IMidiOut _midiOutput;
    private readonly SemaphoreSlim? _semaphore;

    public AsyncMidiOutputWrapper(IMidiOut midiOut, SemaphoreSlim semaphore)
    {
        _midiOutput = midiOut;
        _semaphore = semaphore;
    }

    public async Task SafeSendAsync(byte[] data)
    {
        await Task.Run(async () =>
        {
            try
            {
                if (_semaphore != null)
                {
                    await _semaphore.WaitAsync();
                    Log.Debug("Send midi acquired lock");
                }

                _midiOutput.SafeSend(data);
            }
            catch (Exception ex)
            {
                Log.Debug($"Exception in AsyncMidiOutputWrapper: {ex.Message}");
                throw;
            }
            finally
            {
                if (_semaphore != null)
                {
                    _semaphore.Release();
                    Log.Debug("Send midi released lock");
                }
            }
        });
    }
}