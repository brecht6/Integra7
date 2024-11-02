using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Integra7AuralAlchemist.ViewModels;

public partial class SaveUserToneViewModel : ViewModelBase
{
    [Reactive] private string _searchTextPreset = "";
    
    private Integra7Preset? _selectedPreset;
    private List<Integra7Preset> i7presets = [];
    private string _toneTypeStr;
    public Integra7Preset? SelectedPreset => _selectedPreset;
    private SourceCache<Integra7Preset, int> _sourceCachePresets = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presets = new([]);
    public ReadOnlyObservableCollection<Integra7Preset> Presets => _presets;
    private UserToneToSave? _userToneToSave = null;
    

    public ReactiveCommand<Unit, UserToneToSave?> CancelCommand { get; }
    
    private IDisposable? _cleanupPresets;

    public SaveUserToneViewModel(List<Integra7Preset> presets, string toneTypeStr)
    {
        _toneTypeStr = toneTypeStr;
        i7presets.AddRange(presets);
        _sourceCachePresets.AddOrUpdate(i7presets);

        CancelCommand = ReactiveCommand.Create(() =>
        {
            _userToneToSave = null;
            return _userToneToSave;
        });
        
        var parFilterPreset = this.WhenAnyValue(x => x.SearchTextPreset)
            .Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .DistinctUntilChanged()
            .Select(text => FilterProvider.SaveTonePresetFilter(_toneTypeStr, text));
        
        _cleanupPresets = _sourceCachePresets.Connect()
            .Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .Filter(parFilterPreset)
            .ObserveOn(RxApp.MainThreadScheduler)
            .SortAndBind(
                out _presets,
                SortExpressionComparer<Integra7Preset>.Ascending(t => t.Id))
            .DisposeMany()
            .Subscribe();
    }
}