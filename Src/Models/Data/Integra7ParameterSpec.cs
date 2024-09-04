using System;
using System.Collections.Generic;
using Avalonia.Collections;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Data;

public class Integra7ParameterSpec
{
    public enum SpecType { NUMERIC, ASCII }
    private SpecType _type;
    public SpecType Type => _type;
    private string _path;
    public string Path => _path;
    private byte[] _address;
    public byte[] Address => _address;
    private int _imin;
    public int IMin => _imin;
    private int _imax;
    public int IMax => _imax;
    private float _omin;
    public float OMin => _omin;
    private float _omax;
    public float OMax => _omax;
    private int _bytes;
    public int Bytes => _bytes;
    private bool _reserved;
    public bool Reserved => _reserved;
    private bool _perNibble;
    public bool PerNibble => _perNibble;
    private string _unit = "";
    public string Unit => _unit;
    private IDictionary<int, string>? _repr;
    public IDictionary<int, string>? Repr => _repr;
    private string _parentCtrlPath = "";
    public string ParentCtrl { get => _parentCtrlPath; set => _parentCtrlPath = value; }
    private string _parentCtrlDispValue = "";
    public string ParentCtrlDispValue { get => _parentCtrlDispValue; set => _parentCtrlDispValue = value; }
    private bool _isParentCtrl;
    public bool IsParent { get => _isParentCtrl; set => _isParentCtrl = value; }
    private string _parentCtrlPath2 = "";
    public string ParentCtrl2 { get => _parentCtrlPath2; set => _parentCtrlPath2 = value; }
    private string _parentCtrlDispValue2 = "";
    public string ParentCtrlDispValue2 { get => _parentCtrlDispValue2; set => _parentCtrlDispValue2 = value; }
    private float _imin2 = float.NaN;
    public float IMin2 => _imin2;
    private float _imax2 = float.NaN;
    public float IMax2 => _imax2;
    private float _omin2 = float.NaN;
    public float OMin2 => _omin2;
    private float _omax2 = float.NaN;
    public float OMax2 => _omax2;
    public string Name => Path.Split('/')[^1];

    public AvaloniaList<double> Ticks
    {
        get
        {
            if (Type == SpecType.ASCII)
            {
                return [];
            }

            if (!float.IsNaN(_imin2) && !float.IsNaN(_imax2) && !float.IsNaN(_omin2) && !float.IsNaN(_omax2))
            {
                AvaloniaList<double> ticks = [];
                for (long i = (long)_imin2; i < (long)(_imax2 + 1); i++)
                {
                    ticks.Add(Math.Round(Mapping.linlin(i, _imin2, _imax2, _omin2, _omax2), 2));
                }
                return ticks;
            }

            if (_omin2 == -20000 && _omax2 == 20000)
            {
                AvaloniaList<double> ticks = [];
                for (long i = 0; i < 127 + 1; i++)
                {
                    ticks.Add(i);
                }
                return ticks;
            }
            else
            {
                AvaloniaList<double> ticks = [];
                for (long i = (long)_imin; i <= (long)(_imax); i++)
                {
                    ticks.Add(Mapping.linlin(i, _imin, _imax, _omin, _omax));
                }
                return ticks;
            }
        }
    }

    public Integra7ParameterSpec(SpecType type, string path, byte[] offs, int imin, int imax, float omin, float omax, int bytes, bool res, bool nib, string unit, IDictionary<int, string>? repr, string par = "", string parval = "", bool isparent = false, string par2 = "", string parval2 = "", float imin2 = float.NaN, float imax2 = float.NaN, float omin2 = float.NaN, float omax2 = float.NaN)
    {
        _type = type; // numeric or ascii?
        _path = path; // name of parameter
        _address = offs; // parameter address in sysex byte stream
        _imin = imin; // min possible (raw) value for this parameter
        _imax = imax; // max possible (raw) value for this parameter
        _omin = omin; // min possible mapped value for this parameter
        _omax = omax; // max possible mapped value for this parameter
        _bytes = bytes; // no of bytes used by value in sysex data stream
        _reserved = res; // boolean to indicate if this parameter is documented as "reserved"
        _perNibble = nib; // boolean to indicate if this parameter value is transmitted as a series of nibbles
        _unit = unit; // string to indicate a unit
        _repr = repr; // lookup table string -> int for discrete raw values mapping 
        _parentCtrlPath = par; // a path of a parent control who's value determines if this spec is valid
        _parentCtrlDispValue = parval; // the displayed value that the parent control must have for this spec to be valid
        _isParentCtrl = isparent; // a boolean to indicate that the displayed value of this parameter should be recorded in a context table during parsing (used for parent controls)
        _parentCtrlPath2 = par2; // second level of depencency
        _parentCtrlDispValue2 = parval2; // second level of dependency
        _imin2 = imin2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        _imax2 = imax2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        _omin2 = omin2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
        _omax2 = omax2; // second level of output mapping, used in combination with generic chorus, reverb, mfx parameters
    }

    public bool IsSameAs(Integra7ParameterSpec other)
    {
        return _path == other._path;
        // no need to check repr, par, parval, par2, parval2
        // just path should be enough since we are not supposed to specify duplicate paths
    }

}
