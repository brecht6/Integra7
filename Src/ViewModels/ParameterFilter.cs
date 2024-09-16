using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.ViewModels;

public class FilterProvider
{
    public static Func<FullyQualifiedParameter, bool> ParameterFilter(string text) => par =>
    {
        IEnumerable<string> textParts = Regex.Split(text, @"\s+").Where(s => s != string.Empty);
        bool ReturnValue = !par.ParSpec.Reserved && (string.IsNullOrEmpty(text) || 
                           textParts.All(txt => par.ParSpec.Name.Contains(txt.Trim(), StringComparison.CurrentCultureIgnoreCase)));
        return ReturnValue;
    };

    public static Func<Integra7Preset, bool> PresetFilter(string text) => preset =>
    {
        IEnumerable<string> textParts = Regex.Split(text, @"\s+").Where(s => s != string.Empty);
        return string.IsNullOrEmpty(text) || 
               textParts.All(txt => preset.Name.Contains(txt.Trim(), StringComparison.CurrentCultureIgnoreCase)
                                    || preset.ToneBankStr.Contains(txt.Trim(), StringComparison.CurrentCultureIgnoreCase)
                                    || preset.ToneTypeStr.Contains(txt.Trim(), StringComparison.CurrentCultureIgnoreCase)
                                    || preset.CategoryStr.Contains(txt.Trim(), StringComparison.CurrentCultureIgnoreCase));
    };
}
