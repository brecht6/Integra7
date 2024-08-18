using System.IO;
using Integra7AuralAlchemist.Models.Data;
using ReactiveUI;

public class UpdateMessageSpec
{
    private readonly FullyQualifiedParameter _par;
    public FullyQualifiedParameter Par { get => _par; }
    private readonly string _displayvalue;
    public string DisplayValue { get => _displayvalue; }

    public UpdateMessageSpec(FullyQualifiedParameter par, string displayvalue)
    {
        _par = par;
        _displayvalue = displayvalue;
    }
}
