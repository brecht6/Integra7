using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Views;

public partial class ParameterCollection : UserControl
{
    // add an argument "Parameters" to the user control
    public static readonly StyledProperty<ReadOnlyObservableCollection<FullyQualifiedParameter>> ParametersProperty =
        AvaloniaProperty.Register<ParameterCollection, ReadOnlyObservableCollection<FullyQualifiedParameter>>(nameof(Parameters));
    public ReadOnlyObservableCollection<FullyQualifiedParameter> Parameters
    {
        get => GetValue(ParametersProperty);
        set => SetValue(ParametersProperty, value);
    }

    public static readonly StyledProperty<string> SearchTextProperty = AvaloniaProperty.Register<ParameterCollection, string>(nameof(SearchText));
    public string SearchText
    {
        get => GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }
    public ParameterCollection()
    {
        InitializeComponent();
    }
}