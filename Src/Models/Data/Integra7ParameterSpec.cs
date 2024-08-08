using System.Collections.Generic;

namespace Integra7AuralAlchemist.Models.Data;

class Integra7ParameterSpec
{
    public enum SpecType { NUMERIC, ASCII };
    private SpecType _type;
    public SpecType Type { get => _type; }
    private string _path;
    public string Path { get => _path; }
    private byte[] _offsetAddress;
    public byte[] OffsetAddress { get => _offsetAddress; }
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
    private bool _exposed = false;
    public bool Exposed { get => _exposed; }
    private bool _perNibble = false;
    public bool PerNibble { get => _perNibble; }
    private IDictionary<int, string>? _repr;
    public IDictionary<int, string>? Repr { get => _repr; }

    public Integra7ParameterSpec(SpecType type, string path, byte[] offs, int imin, int imax, float omin, float omax, int bytes, bool vis, bool nib, IDictionary<int, string>? repr)
    {
        _type = type;
        _path = path;
        _offsetAddress = offs;
        _imin = imin;
        _imax = imax;
        _omin = omin;
        _omax = omax;
        _bytes = bytes;
        _exposed = vis;
        _perNibble = nib;
        _repr = repr;
    }

}


/*

*/