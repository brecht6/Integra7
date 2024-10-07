using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using Integra7AuralAlchemist.Models.Data;
using ReactiveUI.SourceGenerators;

namespace Integra7AuralAlchemist.ViewModels;

public partial class SaveUserToneViewModel : ViewModelBase
{
    [Reactive] private string _searchTextPreset;
    private Integra7Preset? _selectedPreset;
    private List<Integra7Preset> i7presets = [];
    public Integra7Preset? SelectedPreset => _selectedPreset;
    private SourceCache<Integra7Preset, int> _sourceCachePresets = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presets = new([]);
    public ReadOnlyObservableCollection<Integra7Preset> Presets => _presets;

    public SaveUserToneViewModel(List<Integra7Preset> presets)
    {
        i7presets.AddRange(presets);
    }
}