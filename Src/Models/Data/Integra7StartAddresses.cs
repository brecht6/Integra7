using System.Collections.Generic;
using System.Diagnostics;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Data;
public class Integra7StartAddresses
{
    private IDictionary<string, Integra7StartAddressSpec> _startAddresses;

    public Integra7StartAddressSpec Lookup(string path)
    {
        return _startAddresses[path];
    }

    public bool Exists(string path)
    {
        return _startAddresses.ContainsKey(path);
    }

    public Integra7StartAddresses()
    {
        _startAddresses = new Dictionary<string, Integra7StartAddressSpec>
        {
            ["Setup"] = new(addr: [0x01, 0x00, 0x00, 0x00]),
            ["System"] = new(addr: [0x02, 0x00, 0x00, 0x00]),
            ["Temporary Studio Set"] = new(addr: [0x18, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 1"] = new(addr: [0x19, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 2"] = new(addr: [0x19, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 3"] = new(addr: [0x19, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 4"] = new(addr: [0x19, 0x60, 0x00, 0x00]),
            ["Temporary Tone Part 5"] = new(addr: [0x1A, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 6"] = new(addr: [0x1A, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 7"] = new(addr: [0x1A, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 8"] = new(addr: [0x1A, 0x60, 0x00, 0x00]),
            ["Temporary Tone Part 9"] = new(addr: [0x1B, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 10"] = new(addr: [0x1B, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 11"] = new(addr: [0x1B, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 12"] = new(addr: [0x1B, 0x60, 0x00, 0x00]),
            ["Temporary Tone Part 13"] = new(addr: [0x1C, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 14"] = new(addr: [0x1C, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 15"] = new(addr: [0x1C, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 16"] = new(addr: [0x1C, 0x60, 0x00, 0x00]),
            
            //
            
            ["Offset/Not Used"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset/Temporary PCM Synth Tone"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset/Temporary SuperNATURAL Synth Tone"] = new(addr: [0x01, 0x00, 0x00]),
            ["Offset/Temporary SuperNATURAL Acoustic Tone"] = new(addr: [0x02, 0x00, 0x00]),
            ["Offset/Temporary SuperNATURAL Drum Kit"] = new(addr: [0x03, 0x00, 0x00]),
            ["Offset/Temporary PCM Drum Kit"] = new([0x10, 0x00, 0x00]),
            
            //
            ["Offset2/System Common"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset2/Setup Sound Mode"] = new(addr: [0x00, 0x00]),
            
            ["Offset2/Studio Set Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/Studio Set Common Chorus"] = new([0x00, 0x04, 0x00]),
            ["Offset2/Studio Set Common Reverb"] = new([0x00, 0x06, 0x00]),
            ["Offset2/Studio Set Common Motional Surround"] = new(addr: [0x00, 0x08, 0x00]),
            ["Offset2/Studio Set Master EQ"] = new(addr: [0x00, 0x09, 0x00]),
            
            ["Offset2/PCM Synth Tone Common"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset2/PCM Synth Tone Common MFX"] = new(addr: [0x00, 0x02, 0x00]),
            ["Offset2/PCM Synth Tone Partial Mix Table"] = new(addr: [0x00, 0x10, 0x00]),
            ["Offset2/PCM Synth Tone Common 2"] = new(addr: [0x00, 0x30, 0x00]),
            
            ["Offset2/PCM Drum Kit Common"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset2/PCM Drum Kit Common MFX"] = new(addr: [0x00, 0x02, 0x00]),
            ["Offset2/PCM Drum Kit Common COMP-EQ"] = new(addr: [0x00, 0x08, 0x00]),

            ["Offset2/SuperNATURAL Synth Tone Common"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset2/SuperNATURAL Synth Tone MFX"] = new(addr: [0x00, 0x02, 0x00]),

            ["Offset2/SuperNATURAL Acoustic Tone Common"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset2/SuperNATURAL Acoustic Tone MFX"] = new(addr: [0x00, 0x02, 0x00]),

            ["Offset2/SuperNATURAL Drum Kit Common"] = new(addr: [0x00, 0x00, 0x00]),
            ["Offset2/SuperNATURAL Drum Kit MFX"] = new(addr: [0x00, 0x02, 0x00]),
            ["Offset2/SuperNATURAL Drum Kit Common COMP-EQ"] = new(addr: [0x00, 0x08, 0x00]),
        };

        for (byte i = 0; i < 16; i++)
        {
            _startAddresses[$"Offset2/Studio Set MIDI Channel {i + 1}"] = new(addr: Offset_StudioSet_MIDI_Ch(i));
            _startAddresses[$"Offset2/Studio Set Part {i + 1}"] = new(addr: Offset_StudioSet_Part(i));
            _startAddresses[$"Offset2/Studio Set Part EQ {i + 1}"] = new(addr: Offset_StudioSet_Part_EQ(i));
        }

        for (byte i = 0; i < 4; i++)
        {
            _startAddresses[$"Offset2/PCM Synth Tone Partial {i + 1}"] = new(addr: Offset_PCM_SynthTone_Partial(i));
        }

        for (byte i = 21; i < 109; i++)
        {
            _startAddresses[$"Offset2/PCM Drum Kit Partial {i}"] = new(addr: Offset_PCM_DrumKit_Partial_Key(i));
        }

        for (byte i = 0; i < 3; i++)
        {
            _startAddresses[$"Offset2/SuperNATURAL Synth Tone Partial {i + 1}"] = new(addr: Offset_SN_SynthTone_Partial(i));
        }

        for (byte i = 27; i < 89; i++)
        {
            _startAddresses[$"Offset2/SuperNATURAL Drum Kit Partial {i}"] = new(addr: Offset_SN_DrumKit_Partial_Key(i));
        }
    }

    private static byte[] Offset_StudioSet_MIDI_Ch(byte ZeroBasedChannel)
    {
        byte[] final = [0x00, 0x10, 0x00];
        for (byte b = 0; b < ZeroBasedChannel; b++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        }
        return final;
    }

    private static byte[] Offset_StudioSet_Part(byte ZeroBasedPart)
    {
        byte[] final = [0x00, 0x20, 00];
        for (byte b = 0; b < ZeroBasedPart; b++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        }
        return final;
    }
    private static byte[] Offset_StudioSet_Part_EQ(byte ZeroBasedPart)
    {
        byte[] final = [0x00, 0x50, 0x00];
        for (byte b = 0; b < ZeroBasedPart; b++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        }
        return final;
    }

    private static byte[] Offset_PCM_SynthTone_Partial(int ZeroBasedPartial)
    {
        Debug.Assert(ZeroBasedPartial >= 0);
        Debug.Assert(ZeroBasedPartial <= 3);
        byte[] final = [0x00, 0x20, 0x00];
        for (int i = 0; i < ZeroBasedPartial; i++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
        }
        return final;
    }

    private static byte[] Offset_PCM_DrumKit_Partial_Key(int KeyNumber)
    {
        Debug.Assert(KeyNumber >= 21);
        Debug.Assert(KeyNumber <= 108);
        if (KeyNumber >= 21 && KeyNumber <= 76)
        {
            byte[] final = [0x00, 0x10, 0x00];
            for (int i = 0; i < (KeyNumber - 21); i++)
            {
                final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
            }
            return final;
        }

        if (KeyNumber >= 77 && KeyNumber <= 108)
        {
            byte[] final = [0x01, 0x00, 0x00];
            for (int i = 0; i < (KeyNumber - 77); i++)
            {
                final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
            }
            return final;
        }
        Debug.Assert(false); // invalid key number specified...
        return [];
    }

    private static byte[] Offset_SN_DrumKit_Partial_Key(int KeyNumber)
    {
        Debug.Assert(KeyNumber >= 27);
        Debug.Assert(KeyNumber <= 88);
        byte[] final = [0x00, 0x10, 0x00];
        for (int i = 27; i < KeyNumber; i++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x00, 0x01, 0x00]);
        }
        return final;
    }

    private static byte[] Offset_SN_SynthTone_Partial(int ZeroBasedPartial)
    {
        Debug.Assert(ZeroBasedPartial >= 0);
        Debug.Assert(ZeroBasedPartial <= 2);
        byte[] final = [0x00, 0x20, 0x00];
        for (int i = 0; i < ZeroBasedPartial; i++)
        {
            final = ByteUtils.AddressWithOffset(final, [0x00, 0x01, 0x00]);
        }
        return final;
    }

}