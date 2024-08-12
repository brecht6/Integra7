using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Views;

public partial class PresetSelector : UserControl
{
    // add an argument "Presets" to the user control
    public static readonly StyledProperty<ReadOnlyObservableCollection<Integra7Preset>> PresetsProperty =
        AvaloniaProperty.Register<PresetSelector, ReadOnlyObservableCollection<Integra7Preset>>(nameof(Presets));
    public ReadOnlyObservableCollection<Integra7Preset> Presets
    {
        get => (ReadOnlyObservableCollection<Integra7Preset>)GetValue(PresetsProperty);
        set => SetValue(PresetsProperty, value);
    }

    // add an argument "SelectedPreset" to the user control
    public static readonly StyledProperty<Integra7Preset> SelectedPresetProperty =
        AvaloniaProperty.Register<PresetSelector, Integra7Preset>(nameof(SelectedPreset));
    public Integra7Preset SelectedPreset
    {
        get => (Integra7Preset)GetValue(SelectedPresetProperty);
        set => SetValue(SelectedPresetProperty, value);
    }

    public PresetSelector()
    {
        InitializeComponent();
    }
}