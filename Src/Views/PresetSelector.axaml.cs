using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Views;

public partial class PresetSelector : UserControl
{
    // add an argument "SearchText" to the user control
    public static readonly StyledProperty<string> SearchTextProperty =
        AvaloniaProperty.Register<PresetSelector, string>(nameof(SearchText));

    public string SearchText
    {
        get => GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    // add an argument "Presets" to the user control
    public static readonly StyledProperty<ReadOnlyObservableCollection<Integra7Preset>> PresetsProperty =
        AvaloniaProperty.Register<PresetSelector, ReadOnlyObservableCollection<Integra7Preset>>(nameof(Presets));

    public ReadOnlyObservableCollection<Integra7Preset> Presets
    {
        get => GetValue(PresetsProperty);
        set => SetValue(PresetsProperty, value);
    }

    // add an argument "SelectedPreset" to the user control
    public static readonly StyledProperty<Integra7Preset> SelectedPresetProperty =
        AvaloniaProperty.Register<PresetSelector, Integra7Preset>(nameof(SelectedPreset));

    public Integra7Preset SelectedPreset
    {
        get => (Integra7Preset)GetValue(SelectedPresetProperty);
        set
        {
            if (GetValue(SelectedPresetProperty) != value) // argh
            {
                SetValue(SelectedPresetProperty, value);
            }
        }
    }
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == SelectedPresetProperty)
        {
            if (change.OldValue is not null && change.NewValue is not null && change.OldValue != change.NewValue)
            {
                PresetDataGrid.ScrollIntoView(change.NewValue, null);
            }
        }
        base.OnPropertyChanged(change);
    }

    public PresetSelector()
    {
        InitializeComponent();
    }
}