using System;
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
    private IDictionary<int, string>? _repr;
    public IDictionary<int, string>? Repr { get => _repr; }

    public Integra7ParameterSpec(SpecType type, string path, byte[] offs, int imin, int imax, float omin, float omax, int bytes, bool res, bool nib, IDictionary<int, string>? repr)
    {
        _type = type;
        _path = path;
        _address = offs;
        _imin = imin;
        _imax = imax;
        _omin = omin;
        _omax = omax;
        _bytes = bytes;
        _reserved = res;
        _perNibble = nib;
        _repr = repr;
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
            // no need to check repr
    }

}
