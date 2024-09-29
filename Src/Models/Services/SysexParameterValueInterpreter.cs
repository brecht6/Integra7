using System;
using System.Text;
using Integra7AuralAlchemist.Models.Data;
using Serilog;

namespace Integra7AuralAlchemist.Models.Services;

public class SysexParameterValueInterpreter
{
    public static void Interpret(byte[] parResult, Integra7ParameterSpec? parspec, out long rawNumericValue, out string stringValue)
    {
        rawNumericValue = 0;
        stringValue = "";

        if (parspec is null)
        {
            stringValue = "";
            return ;
        }

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

            if (parspec.IMin != parspec.OMin || parspec.IMax != parspec.OMax || parspec.IMin2 != parspec.OMin2 || parspec.IMax2 != parspec.OMax2)
            {
                double mapped = Mapping.linlin(rawNumericValue, parspec.IMin, parspec.IMax, parspec.OMin, parspec.OMax);
                if (!float.IsNaN(parspec.IMin2) && !float.IsNaN(parspec.IMax2) && !float.IsNaN(parspec.OMin2) && !float.IsNaN(parspec.OMax2))
                {
                    mapped = Mapping.linlin(mapped, parspec.IMin2, parspec.IMax2, parspec.OMin2, parspec.OMax2);
                }
                stringValue = $"{Math.Round(mapped, 2)}";
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
                }
                else
                {
                    //Debug.Assert(false, $"mapped value {key} for par {parspec.Path} not found in {parspec.Repr.Keys}");
                    Log.Debug($"ERROR: mapped value {key} for par {parspec.Path} not found in {parspec.Repr.Keys}");
                }

            }
        }
        else if (parspec.Type == Integra7ParameterSpec.SpecType.DISCRETE)
        {
            bool found = false;
            long val = (parResult[0] << 8) + parResult[1];
            foreach (Tuple<int, string> entry in parspec.Discrete)
            {
                if (entry.Item1 == val)
                {
                    found = true;
                    rawNumericValue = val;
                    stringValue = entry.Item2;
                }
            }
            if (!found)
            {
                Log.Error($"Discrete value {val} has not known value for parameter {parspec.Path}. Choosing something else instead.");
                rawNumericValue = parspec.Discrete[0].Item1;
                stringValue = parspec.Discrete[0].Item2;
            }
        }
        else
        {
            stringValue = Encoding.ASCII.GetString(parResult);
        }
    }
}