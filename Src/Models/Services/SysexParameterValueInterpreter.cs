using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Models.Services;

public class SysexParameterValueInterpreter
{
    public static void Interpret(byte[] parResult, Integra7ParameterSpec parspec, out long rawNumericValue, out string stringValue)
    {
        rawNumericValue = 0;

        if (parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC)
        {
            if (parspec.PerNibble)
            {
                rawNumericValue = ByteUtils.NibbledToInt(parResult);
            }
            else
            {
                rawNumericValue = ByteUtils.Bytes7ToInt(parResult);
            }

            if (parspec.IMin != parspec.OMin || parspec.IMax != parspec.OMax)
            {
                stringValue = $"{(long)Mapping.linlin(rawNumericValue, parspec.IMin, parspec.IMax, parspec.OMin, parspec.OMax)}";
            }
            else
            {
                stringValue = $"{rawNumericValue}";
            }

            if (parspec.Repr != null)
            {
                bool mappedNibbledValue = parspec.PerNibble && (parspec.IMin != parspec.OMin || parspec.IMax != parspec.OMax);
                if (mappedNibbledValue)
                {
                    stringValue = parspec.Repr[int.Parse(stringValue)];
                }
                else
                {
                    stringValue = parspec.Repr[(int)rawNumericValue];
                }
            }
        }
        else
        {
            stringValue = System.Text.Encoding.ASCII.GetString(parResult);
        }
    }
}