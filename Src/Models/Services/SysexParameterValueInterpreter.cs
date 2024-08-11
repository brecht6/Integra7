using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Models.Services;

public class SysexParameterValueInterpreter
{
    public static void Interpret(byte[] parResult, Integra7ParameterSpec _parspec, out long _rawNumericValue, out string _stringValue)
    {
        _rawNumericValue = 0;

        if (_parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC)
        {
            if (_parspec.PerNibble)
            {
                _rawNumericValue = ByteUtils.NibbledToInt(parResult);
            }
            else
            {
                _rawNumericValue = ByteUtils.Bytes7ToInt(parResult);
            }

            if (_parspec.IMin != _parspec.OMin || _parspec.IMax != _parspec.OMax)
            {
                _stringValue = $"{(long)Mapping.linlin(_rawNumericValue, _parspec.IMin, _parspec.IMax, _parspec.OMin, _parspec.OMax)}";
            }
            else
            {
                _stringValue = $"{_rawNumericValue}";
            }

            if (_parspec.Repr != null)
            {
                bool mappedNibbledValue = _parspec.PerNibble && (_parspec.IMin != _parspec.OMin || _parspec.IMax != _parspec.OMax);
                if (mappedNibbledValue)
                {
                    _stringValue = _parspec.Repr[int.Parse(_stringValue)];
                }
                else
                {
                    _stringValue = _parspec.Repr[(int)_rawNumericValue];
                }
            }
        }
        else
        {
            _stringValue = System.Text.Encoding.ASCII.GetString(parResult);
        }
    }
}