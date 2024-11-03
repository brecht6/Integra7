using System.Collections.Generic;
using System.Diagnostics;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Data;

public class Integra7StartAddresses
{
    private readonly IDictionary<string, Integra7StartAddressSpec> _startAddresses;

    public Integra7StartAddresses()
    {
        _startAddresses = new Dictionary<string, Integra7StartAddressSpec>
        {
            ["Setup"] = new([0x01, 0x00, 0x00, 0x00]),
            ["System"] = new([0x02, 0x00, 0x00, 0x00]),
            ["Temporary Studio Set"] = new([0x18, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 1"] = new([0x19, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 2"] = new([0x19, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 3"] = new([0x19, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 4"] = new([0x19, 0x60, 0x00, 0x00]),
            ["Temporary Tone Part 5"] = new([0x1A, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 6"] = new([0x1A, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 7"] = new([0x1A, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 8"] = new([0x1A, 0x60, 0x00, 0x00]),
            ["Temporary Tone Part 9"] = new([0x1B, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 10"] = new([0x1B, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 11"] = new([0x1B, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 12"] = new([0x1B, 0x60, 0x00, 0x00]),
            ["Temporary Tone Part 13"] = new([0x1C, 0x00, 0x00, 0x00]),
            ["Temporary Tone Part 14"] = new([0x1C, 0x20, 0x00, 0x00]),
            ["Temporary Tone Part 15"] = new([0x1C, 0x40, 0x00, 0x00]),
            ["Temporary Tone Part 16"] = new([0x1C, 0x60, 0x00, 0x00]),

            //

            ["Offset/Not Used"] = new([0x00, 0x00, 0x00]),
            ["Offset/Temporary PCM Synth Tone"] = new([0x00, 0x00, 0x00]),
            ["Offset/Temporary SuperNATURAL Synth Tone"] = new([0x01, 0x00, 0x00]),
            ["Offset/Temporary SuperNATURAL Acoustic Tone"] = new([0x02, 0x00, 0x00]),
            ["Offset/Temporary SuperNATURAL Drum Kit"] = new([0x03, 0x00, 0x00]),
            ["Offset/Temporary PCM Drum Kit"] = new([0x10, 0x00, 0x00]),

            //
            ["Offset2/System Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/Setup Sound Mode"] = new([0x00, 0x00]),

            ["Offset2/Studio Set Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/Studio Set Common Chorus"] = new([0x00, 0x04, 0x00]),
            ["Offset2/Studio Set Common Reverb"] = new([0x00, 0x06, 0x00]),
            ["Offset2/Studio Set Common Motional Surround"] = new([0x00, 0x08, 0x00]),
            ["Offset2/Studio Set Master EQ"] = new([0x00, 0x09, 0x00]),

            ["Offset2/PCM Synth Tone Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/PCM Synth Tone Common MFX"] = new([0x00, 0x02, 0x00]),
            ["Offset2/PCM Synth Tone Partial Mix Table"] = new([0x00, 0x10, 0x00]),
            ["Offset2/PCM Synth Tone Common 2"] = new([0x00, 0x30, 0x00]),

            ["Offset2/PCM Drum Kit Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/PCM Drum Kit Common MFX"] = new([0x00, 0x02, 0x00]),
            ["Offset2/PCM Drum Kit Common Comp-EQ"] = new([0x00, 0x08, 0x00]),
            ["Offset2/PCM Drum Kit Common 2"] = new([0x02, 0x00, 0x00]),

            ["Offset2/SuperNATURAL Synth Tone Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/SuperNATURAL Synth Tone Common MFX"] = new([0x00, 0x02, 0x00]),

            ["Offset2/SuperNATURAL Acoustic Tone Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/SuperNATURAL Acoustic Tone Common MFX"] = new([0x00, 0x02, 0x00]),

            ["Offset2/SuperNATURAL Drum Kit Common"] = new([0x00, 0x00, 0x00]),
            ["Offset2/SuperNATURAL Drum Kit Common MFX"] = new([0x00, 0x02, 0x00]),
            ["Offset2/SuperNATURAL Drum Kit Common Comp-EQ"] = new([0x00, 0x08, 0x00])
        };

        for (byte i = 0; i < Constants.NO_OF_PARTS; i++)
        {
            _startAddresses[$"Offset2/Studio Set MIDI Channel {i + 1}"] =
                new Integra7StartAddressSpec(Offset_StudioSet_MIDI_Ch(i));
            _startAddresses[$"Offset2/Studio Set Part {i + 1}"] =
                new Integra7StartAddressSpec(Offset_StudioSet_Part(i));
            _startAddresses[$"Offset2/Studio Set Part EQ {i + 1}"] =
                new Integra7StartAddressSpec(Offset_StudioSet_Part_EQ(i));
        }

        for (byte i = 0; i < Constants.NO_OF_PARTIALS_PCM_SYNTH_TONE; i++)
            _startAddresses[$"Offset2/PCM Synth Tone Partial {i + 1}"] =
                new Integra7StartAddressSpec(Offset_PCM_SynthTone_Partial(i));

        for (byte i = 0; i < Constants.NO_OF_PARTIALS_PCM_DRUM; i++)
            _startAddresses[$"Offset2/PCM Drum Kit Partial {i + 1}"] =
                new Integra7StartAddressSpec(Offset_PCM_DrumKit_Partial_Key(i));

        for (byte i = 0; i < Constants.NO_OF_PARTIALS_SN_SYNTH_TONE; i++)
            _startAddresses[$"Offset2/SuperNATURAL Synth Tone Partial {i + 1}"] =
                new Integra7StartAddressSpec(Offset_SN_SynthTone_Partial(i));

        for (byte i = 0; i < Constants.NO_OF_PARTIALS_SN_DRUM; i++)
            _startAddresses[$"Offset2/SuperNATURAL Drum Kit Partial {i + 1}"] =
                new Integra7StartAddressSpec(Offset_SN_DrumKit_Partial_Key(i));
    }

    public Integra7StartAddressSpec Lookup(string path)
    {
        return _startAddresses[path];
    }

    public bool Exists(string path)
    {
        return _startAddresses.ContainsKey(path);
    }

    private static byte[] Offset_StudioSet_MIDI_Ch(byte zeroBasedChannel)
    {
        byte[] final = [0x00, 0x10, 0x00];
        for (byte b = 0; b < zeroBasedChannel; b++) final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);
        return final;
    }

    private static byte[] Offset_StudioSet_Part(byte zeroBasedPart)
    {
        byte[] final = [0x00, 0x20, 00];
        for (byte b = 0; b < zeroBasedPart; b++) final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);

        return final;
    }

    private static byte[] Offset_StudioSet_Part_EQ(byte zeroBasedPart)
    {
        byte[] final = [0x00, 0x50, 0x00];
        for (byte b = 0; b < zeroBasedPart; b++) final = ByteUtils.AddressWithOffset(final, [0x01, 0x00]);

        return final;
    }

    private static byte[] Offset_PCM_SynthTone_Partial(int zeroBasedPartial)
    {
        Debug.Assert(zeroBasedPartial >= 0);
        Debug.Assert(zeroBasedPartial < Constants.NO_OF_PARTIALS_PCM_SYNTH_TONE);
        byte[] final = [0x00, 0x20, 0x00];
        for (var i = 0; i < zeroBasedPartial; i++) final = ByteUtils.AddressWithOffset(final, [0x02, 0x00]);
        return final;
    }

    private static byte[] Offset_PCM_DrumKit_Partial_Key(int zeroBasedPartial)
    {
        Debug.Assert(zeroBasedPartial >= 0);
        Debug.Assert(zeroBasedPartial < Constants.NO_OF_PARTIALS_PCM_DRUM);
        var final = ByteUtils.Bytes7ToInt([0x00, 0x10, 0x00]);
        for (var i = 0; i < zeroBasedPartial; i++) final += ByteUtils.Bytes7ToInt([0x02, 0x00]);
        return ByteUtils.IntToBytes7_3(final);
    }

    private static byte[] Offset_SN_DrumKit_Partial_Key(int zeroBasedPartial)
    {
        Debug.Assert(zeroBasedPartial >= 0);
        Debug.Assert(zeroBasedPartial < Constants.NO_OF_PARTIALS_SN_DRUM);
        byte[] final = [0x00, 0x10, 0x00];
        for (var i = 0; i < zeroBasedPartial; i++) final = ByteUtils.AddressWithOffset(final, [0x00, 0x01, 0x00]);

        return final;
    }

    private static byte[] Offset_SN_SynthTone_Partial(int zeroBasedPartial)
    {
        Debug.Assert(zeroBasedPartial >= 0);
        Debug.Assert(zeroBasedPartial < Constants.NO_OF_PARTIALS_SN_SYNTH_TONE);
        byte[] final = [0x00, 0x20, 0x00];
        for (var i = 0; i < zeroBasedPartial; i++) final = ByteUtils.AddressWithOffset(final, [0x00, 0x01, 0x00]);
        return final;
    }
}