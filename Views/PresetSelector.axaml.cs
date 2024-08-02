using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Views;

public partial class PresetSelector : UserControl
{
    public static readonly StyledProperty<ReadOnlyObservableCollection<Integra7Preset>> PresetsProperty = 
        AvaloniaProperty.Register<PresetSelector, ReadOnlyObservableCollection<Integra7Preset>>(nameof(Presets));
    public ReadOnlyObservableCollection<Integra7Preset> Presets {
        get => (ReadOnlyObservableCollection<Integra7Preset>)GetValue(PresetsProperty);
        set => SetValue(PresetsProperty, value);
    }
    public PresetSelector()
    {
        InitializeComponent();
    }
}