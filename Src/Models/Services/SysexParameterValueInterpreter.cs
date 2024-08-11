using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
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
                stringValue = $"{(long)Math.Round(Mapping.linlin(rawNumericValue, parspec.IMin, parspec.IMax, parspec.OMin, parspec.OMax))}";
            }
            else
            {
                stringValue = $"{rawNumericValue}";
            }

            if (parspec.Repr != null)
            {
                int key = int.Parse(stringValue);
                if (parspec.Repr.ContainsKey(key)) 
                {
                    stringValue = parspec.Repr[key];
                } else
                {
                    //Debug.Assert(false, $"mapped value {key} for par {parspec.Path} not found in {parspec.Repr.Keys}");
                    Debug.WriteLine($"ERROR: mapped value {key} for par {parspec.Path} not found in {parspec.Repr.Keys}");
                }

            }
        }
        else
        {
            stringValue = System.Text.Encoding.ASCII.GetString(parResult);
        }
    }
}