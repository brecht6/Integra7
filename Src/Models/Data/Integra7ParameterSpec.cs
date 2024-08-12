using System.Collections.Generic;

namespace Integra7AuralAlchemist.Models.Data;

public class Integra7ParameterSpec
{
    public enum SpecType { NUMERIC, ASCII };
    private SpecType _type;
    public SpecType Type { get => _type; }
    private string _path;
    public string Path { get => _path; }
    private byte[] _address;
    public byte[] Address { get => _address; }
    private int _imin;
    public int IMin { get => _imin; }
    private int _imax;
    public int IMax { get => _imax; }
    private float _omin;
    public float OMin { get => _omin; }
    private float _omax;
    public float OMax { get => _omax; }
    private int _bytes;
    public int Bytes { get => _bytes; }
    private bool _reserved = false;
    public bool Reserved { get => _reserved; }
    private bool _perNibble = false;
    public bool PerNibble { get => _perNibble; }
    private string _unit = "";
    public string Unit { get => _unit; }
    private IDictionary<int, string>? _repr;
    public IDictionary<int, string>? Repr { get => _repr; }
    private string _masterCtrlPath = "";
    public string MasterCtrl { get => _masterCtrlPath; set => _masterCtrlPath = value; }
    private string _masterCtrlDispValue  = "";
    public string MasterCtrlDispValue { get => _masterCtrlDispValue; set => _masterCtrlDispValue = value; }
    private bool _store = false;
    public bool Store { get => _store; set => _store = value; }
    private string _masterCtrlPath2 = "";
    public string MasterCtrl2 { get => _masterCtrlPath2; set => _masterCtrlPath2 = value; }
    private string _masterCtrlDispValue2  = "";
    public string MasterCtrlDispValue2 { get => _masterCtrlDispValue2; set => _masterCtrlDispValue2 = value; }


    public Integra7ParameterSpec(SpecType type, string path, byte[] offs, int imin, int imax, float omin, float omax, int bytes, bool res, bool nib, string unit, IDictionary<int, string>? repr, string mst = "", string mstval = "", bool store = false, string mst2 = "", string mstval2 = "")
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
        _masterCtrlPath = mst; // a path of a master control who's value determines if this spec is valid
        _masterCtrlDispValue = mstval; // the displayed value that the master control must have for this spec to be valid
        _store = store; // a boolean to indicate that the displayed value of this parameter should be recorded in a context table during parsing (used for master controls)
        _masterCtrlPath2 = mst2; // second level of depencency
        _masterCtrlDispValue2 = mstval2; // second level of dependency
    }

    public bool IsSameAs(Integra7ParameterSpec other)
    {
        return _type == other._type &&
            _path == other._path && 
            _address == other._address &&
            _imin == other._imin && 
            _imax == other._imax && 
            _omin == other._omin &&
            _omax == other._omax &&
            _bytes == other._bytes && 
            _reserved == other._reserved && 
            _perNibble == other._perNibble;
            // no real need to check repr, mst, mstval, mst2, mstval2 (in fact, just path should be enough since we are not supposed to specify duplicate paths)
    }

}
