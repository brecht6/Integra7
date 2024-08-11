using System.Diagnostics;
using System.Linq;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Models.Services;

public class DisplayValueToRawValueConverter
{
    public static void UpdateFromDisplayedValue(string displayValue, FullyQualifiedParameter p)
    {
        if (p.IsNumeric)
        {
            if (p.ParSpec.Repr != null)
            {
                var key = p.ParSpec.Repr
                    .Where(keyvaluepair => keyvaluepair.Value == displayValue)
                    .Select(keyvaluepair => keyvaluepair.Key)
                    .ToList();
                if (key.Count == 0)
                {
                    Debug.Assert(false, $"cannot find {displayValue} in {p.ParSpec.Repr}");
                }
                p.RawNumericValue = key.First(); // if a repr is present, and a mapping is present this is still a "mapped value"
            }

            if (p.ParSpec.IMin != p.ParSpec.OMin || p.ParSpec.IMax != p.ParSpec.OMax)
            {
                // need to unmap mapped value to raw value
                if (p.ParSpec.Repr != null)
                {
                    p.RawNumericValue = (long)Mapping.linlin(p.RawNumericValue, p.ParSpec.OMin, p.ParSpec.OMax, p.ParSpec.IMin, p.ParSpec.IMax, true);
                }
                else
                {
                    p.RawNumericValue = (long)Mapping.linlin(long.Parse(displayValue), p.ParSpec.OMin, p.ParSpec.OMax, p.ParSpec.IMin, p.ParSpec.IMax, true);
                }
            }
            else
            {
                if (p.ParSpec.Repr == null) // otherwise p.RawNumericValue is already found in the previous paragraph
                {
                    p.RawNumericValue = long.Parse(displayValue);
                }
            }
            p.StringValue = displayValue;
        }
        else
        {
            if (displayValue.Length > p.ParSpec.Bytes)
                p.StringValue = displayValue[..p.ParSpec.Bytes]; // clip string to max length
        }

        p.DebugLog();
    }
}