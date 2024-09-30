using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.ViewModels;

public class FilterProvider
{
    public static HashSet<string> SrxToStr(int srx)
    {
        switch (srx)
        {
            case 0:
                return ["Empty"];
            case 1:
                return ["SRX01"];
            case 2:
                return ["SRX02"];
            case 3:
                return ["SRX03"];
            case 4:
                return ["SRX04"];
            case 5:
                return ["SRX05"];
            case 6:
                return ["SRX06"];
            case 7:
                return ["SRX07"];
            case 8:
                return ["SRX08"];
            case 9:
                return ["SRX09"];
            case 10:
                return ["SRX10"];
            case 11:
                return ["SRX11"];
            case 12:
                return ["SRX12"];
            case 13:
                return ["ExSN1"];
            case 14:
                return ["ExSN2"];
            case 15:
                return ["ExSN3"];
            case 16:
                return ["ExSN4"];
            case 17:
                return ["ExSn5"];
            case 18:
                return ["ExSN6"];
            case 19:
                return ["ExPCM", "GM2/GM2#"];
            default:
                return ["Empty"];
        }
    }

    public static Func<FullyQualifiedParameter, bool> ParameterFilter(string text) => par =>
    {
        IEnumerable<string> textParts = Regex.Split(text, @"\s+").Where(s => s != string.Empty);
        bool ReturnValue = !par.ParSpec.Reserved && (string.IsNullOrEmpty(text) ||
                                                     textParts.All(txt => par.ParSpec.Name.Contains(txt.Trim(),
                                                         StringComparison.CurrentCultureIgnoreCase)));
        return ReturnValue;
    };

    public static Func<Integra7Preset, bool> PresetFilter(string text, int srx01, int srx02, int srx03, int srx04) =>
        preset =>
        {
            IEnumerable<string> textParts = Regex.Split(text, @"\s+").Where(s => s != string.Empty);
            return (preset.ToneBankStr == "PRST" ||
                    SrxToStr(srx01).Contains(preset.ToneBankStr) ||
                    SrxToStr(srx02).Contains(preset.ToneBankStr) ||
                    SrxToStr(srx03).Contains(preset.ToneBankStr) ||
                    SrxToStr(srx04).Contains(preset.ToneBankStr))
                   &&
                   (string.IsNullOrEmpty(text)
                    ||
                    textParts.All(txt => (preset.Name.Contains(txt.Trim(), StringComparison.CurrentCultureIgnoreCase)
                                          || preset.ToneBankStr.Contains(txt.Trim(),
                                              StringComparison.CurrentCultureIgnoreCase)
                                          || preset.ToneTypeStr.Contains(txt.Trim(),
                                              StringComparison.CurrentCultureIgnoreCase)
                                          || preset.CategoryStr.Contains(txt.Trim(),
                                              StringComparison.CurrentCultureIgnoreCase)
                                          || preset.InternalUserDefinedStr.Contains(txt.Trim(),
                                              StringComparison.CurrentCultureIgnoreCase))));
        };
}