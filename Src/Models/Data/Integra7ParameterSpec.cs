using System;
using System.Collections.Generic;
using Avalonia.Collections;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Data;

public class Integra7ParameterSpec
{
    public enum SpecType
    {
        NUMERIC,
        ASCII,
        DISCRETE
    }

    public Integra7ParameterSpec(SpecType type, string path, byte[] offs, int imin, int imax, float omin, float omax,
        int bytes, bool res, bool nib, string unit, IDictionary<int, string>? repr, string par = "", string parval = "",
        bool isparent = false, string par2 = "", string parval2 = "", float imin2 = float.NaN, float imax2 = float.NaN,
        float omin2 = float.NaN, float omax2 = float.NaN, List<Tuple<int, string>>? discrete = null)
    {
        Type = type; // numeric or ascii?
        Path = path; // name of parameter
        Address = offs; // parameter address in sysex byte stream
        IMin = imin; // min possible (raw) value for this parameter
        IMax = imax; // max possible (raw) value for this parameter
        OMin = omin; // min possible mapped value for this parameter
        OMax = omax; // max possible mapped value for this parameter
        Bytes = bytes; // no of bytes used by value in sysex data stream
        Reserved = res; // boolean to indicate if this parameter is documented as "reserved"
        PerNibble = nib; // boolean to indicate if this parameter value is transmitted as a series of nibbles
        Unit = unit; // string to indicate a unit
        Repr = repr; // lookup table string -> int for discrete raw values mapping 
        ParentCtrl = par; // a path of a parent control who's value determines if this spec is valid
        ParentCtrlDispValue = parval; // the displayed value that the parent control must have for this spec to be valid
        IsParent = isparent; // a boolean to indicate that the displayed value of this parameter should be recorded in a context table during parsing (used for parent controls)
        ParentCtrl2 = par2; // second level of depencency
        ParentCtrlDispValue2 = parval2; // second level of dependency
        IMin2 = imin2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        IMax2 = imax2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        OMin2 = omin2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        OMax2 = omax2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        Discrete = discrete; // for a discrete list of items (typically with gaps)
    }

    public SpecType Type { get; }

    public string Path { get; }

    public byte[] Address { get; }

    public int IMin { get; }

    public int IMax { get; }

    public float OMin { get; }

    public float OMax { get; }

    public int Bytes { get; }

    public bool Reserved { get; }

    public bool PerNibble { get; }

    public string Unit { get; } = "";

    public IDictionary<int, string>? Repr { get; }

    public List<Tuple<int, string>> Discrete { get; }

    public string ParentCtrl { get; set; } = "";
    public string ParentCtrlDispValue { get; set; } = "";
    public bool IsParent { get; set; }

    public string ParentCtrl2 { get; set; } = "";
    public string ParentCtrlDispValue2 { get; set; } = "";
    public float IMin2 { get; } = float.NaN;

    public float IMax2 { get; } = float.NaN;

    public float OMin2 { get; } = float.NaN;

    public float OMax2 { get; } = float.NaN;

    public string Name => Path.Split('/')[^1];

    public AvaloniaList<double> Ticks
    {
        get
        {
            if (Type == SpecType.ASCII || Type == SpecType.DISCRETE) return [];

            if (!float.IsNaN(IMin2) && !float.IsNaN(IMax2) && !float.IsNaN(OMin2) && !float.IsNaN(OMax2))
            {
                AvaloniaList<double> ticks = [];
                for (var i = (long)IMin2; i < (long)(IMax2 + 1); i++)
                    ticks.Add(Math.Round(Mapping.linlin(i, IMin2, IMax2, OMin2, OMax2), 2));
                return ticks;
            }

            if (OMin2 == -20000 && OMax2 == 20000)
            {
                AvaloniaList<double> ticks = [];
                for (long i = 0; i < 127 + 1; i++) ticks.Add(i);
                return ticks;
            }
            else
            {
                AvaloniaList<double> ticks = [];
                for (var i = (long)IMin; i <= IMax; i++) ticks.Add(Mapping.linlin(i, IMin, IMax, OMin, OMax));
                return ticks;
            }
        }
    }

    public bool IsSameAs(Integra7ParameterSpec other)
    {
        return Path == other.Path;
        // no need to check repr, par, parval, par2, parval2
        // just path should be enough since we are not supposed to specify duplicate paths
    }
}