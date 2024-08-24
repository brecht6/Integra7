using System;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.ViewModels;

public class FilterProvider
{
    public static Func<FullyQualifiedParameter, bool> ParameterFilter(string text) => par =>
    {
        bool ReturnValue = !par.ParSpec.Reserved && (string.IsNullOrEmpty(text) || par.ParSpec.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase));
        return ReturnValue;
    };
}
