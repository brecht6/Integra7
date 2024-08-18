using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Views;

public partial class ParameterCollection : UserControl
{
    // add an argument "Parameters" to the user control
    public static readonly StyledProperty<ReadOnlyObservableCollection<FullyQualifiedParameter>> ParametersProperty =
        AvaloniaProperty.Register<PresetSelector, ReadOnlyObservableCollection<FullyQualifiedParameter>>(nameof(Parameters));
    public ReadOnlyObservableCollection<FullyQualifiedParameter> Parameters
    {
        get => (ReadOnlyObservableCollection<FullyQualifiedParameter>)GetValue(ParametersProperty);
        set => SetValue(ParametersProperty, value);
    }

    public ParameterCollection()
    {
        InitializeComponent();
    }
}