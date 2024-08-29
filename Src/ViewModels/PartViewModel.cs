using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Domain;
using Integra7AuralAlchemist.Models.Services;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Integra7AuralAlchemist.ViewModels;
public partial class PartViewModel : ViewModelBase
{
    private ViewModelBase _parent;
    private Integra7StartAddresses _i7startAddresses;
    private Integra7Parameters _i7parameters;
    private IIntegra7Api _i7Api;
    private Integra7Domain? _i7domain;
    public Integra7Domain I7Domain
    {
        get => _i7domain;
        set
        {
            _i7domain = value;
            if (_partialViewModels != null)
            {
                foreach (PartialViewModel pvm in _partialViewModels)
                {
                    pvm.I7Domain = value;
                }
            }
        } 
    }
    private List<Integra7Preset> _i7presets;
    private Integra7Preset _selectedPreset;
    
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presets = new([]);
    private SourceCache<Integra7Preset, int> _sourceCachePresets = new SourceCache<Integra7Preset, int>(x => x.Id);
    public ReadOnlyObservableCollection<Integra7Preset> Presets => _presets;
    
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters => _studioSetMidiParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters => _studioSetPartParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters => _StudioSetPartEQParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters => _PCMSynthToneCommonParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommon2Parameters => _PCMSynthToneCommon2Parameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommon2Parameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommon2Parameters = new([]);
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonMFXParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonMFXParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonMFXParameters => _PCMSynthToneCommonMFXParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthTonePMTParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthTonePMTParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthTonePMTParameters => _PCMSynthTonePMTParameters;
    
    private ReadOnlyObservableCollection<PartialViewModel>? _partialViewModels;
    public ReadOnlyObservableCollection<PartialViewModel>? PartialViewModels => _partialViewModels;
    
    private byte _part;

    public bool SelectedPresetIsSynthTone => _selectedPreset is null ? false : _selectedPreset.ToneTypeStr == "PCMS";

    [Reactive] private string _searchTextPreset = "";
    [Reactive] private string _searchTextStudioSetMidi = "";
    [Reactive] private string _searchTextStudioSetPart = "";
    [Reactive] private string _refreshStudioSetPart = "";
    [Reactive] private string _searchTextStudioSetPartEQ = "";
    [Reactive] private string _refreshStudioSetPartEQ = "";
    [Reactive] private string _searchTextPCMSynthToneCommon = "";
    [Reactive] private string _refreshPCMSynthToneCommon = "";
    [Reactive] private string _searchTextPCMSynthToneCommon2 = "";
    [Reactive] private string _refreshPCMSynthToneCommon2 = "";
    [Reactive] private string _searchTextPCMSynthToneCommonMFX = "";
    [Reactive] private string _refreshPCMSynthToneCommonMFX = "";
    [Reactive] private string _searchTextPCMSynthTonePMT = "";
    [Reactive] private string _refreshPCMSynthTonePMT = "";

    public string Header => _commonTab ? "Common" : $"Part {_part + 1}";

    IDisposable? _cleanupPresets;
    IDisposable? _cleanupMidiParams;
    IDisposable? _cleanupStudioSetPartParams;
    IDisposable? _cleanupStudioSetPartEQParams;
    IDisposable? _cleanupPCMSynthToneCommonParams;
    IDisposable? _cleanupPCMSynthToneCommon2Params;
    IDisposable? _cleanupPCMSynthToneCommonMFXParams;
    IDisposable? _cleanupPCMSynthTonePMTParametersParams;

    [Reactive]
    private string _searchTextSetup = "";
    [Reactive]
    private string _searchSystem = "";
    [Reactive]
    private string _searchTextStudioSetCommon = "";
    [Reactive]
    private string _searchTextStudioSetCommonChorus = "";
    [Reactive]
    private string _refreshCommonChorusNeeded = "";
    [Reactive]
    private string _searchTextStudioSetCommonReverb = "";
    [Reactive]
    private string _refreshCommonReverbNeeded = "";
    [Reactive]
    private string _searchTextStudioSetCommonMotionalSurround = "";
    [Reactive]
    private string _refreshCommonMotionalSurroundNeeded = "";
    [Reactive]
    private string _searchTextStudioSetCommonMasterEQ = "";
    [Reactive]
    public string _refreshCommonMasterEQNeeded = "";


    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheSetupParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _setupParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> SetupParameters => _setupParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheSystem = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _systemParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> SystemParameters => _systemParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonParameters => _studioSetCommonParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonChorusParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonChorusParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonChorusParameters => _studioSetCommonChorusParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonReverbParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonReverbParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonReverbParameters => _studioSetCommonReverbParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonMotionalSurroundParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonMotionalSurroundParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonMotionalSurroundParameters => _studioSetCommonMotionalSurroundParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonMasterEQParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonMasterEQParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonMasterEQParameters => _studioSetCommonMasterEQParameters;

    IDisposable? _cleanupSetup;
    IDisposable? _cleanupSystem;
    IDisposable? _cleanupStudioSetCommon;
    IDisposable? _cleanupStudioSetChorus;
    IDisposable? _cleanupStudioSetReverb;
    IDisposable? _cleanupMotionalSurround;
    IDisposable? _cleanupStudioSetMasterEQ;

    public void PreSelectConfiguredPreset(DomainBase b)
    {
        string msbstr = b.LookupSingleParameterDisplayedValue("Studio Set Part/Tone Bank Select MSB");
        string lsbstr = b.LookupSingleParameterDisplayedValue("Studio Set Part/Tone Bank Select LSB");
        string pcstr = b.LookupSingleParameterDisplayedValue("Studio Set Part/Tone Bank Program Number (PC)");
        foreach (Integra7Preset p in _i7presets)
        {
            if (msbstr == $"{p.Msb}" && lsbstr == $"{p.Lsb}" && pcstr == $"{p.Pc - 1}") // note: seems like integra-7 sends back a one-based program change (PC)??
            {
                _selectedPreset = p;
                this.RaisePropertyChanged(nameof(SelectedPreset));
            }
        }
    }


    public PartViewModel(ViewModelBase parent, byte zeroBasedPartNo, Integra7StartAddresses i7startAddr, Integra7Parameters i7par, IIntegra7Api i7, Integra7Domain i7dom, List<Integra7Preset> i7presets, bool commonTab = false)
    {
        const int THROTTLE = 250;
        _parent = parent;
        _part = zeroBasedPartNo;
        _i7startAddresses = i7startAddr;
        _i7parameters = i7par;
        _i7Api = i7;
        _i7domain = i7dom;
        _i7presets = i7presets;
        _commonTab = commonTab;
        _selectedPreset = _i7presets[0];

        _sourceCachePresets.AddOrUpdate(i7presets);
        InitializeParameterSourceCaches();

        if (!commonTab)
        {
            var parFilterPreset = this.WhenAnyValue(x => x.SearchTextPreset)
                                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                        .DistinctUntilChanged()
                                                        .Select(FilterProvider.PresetFilter);
            var parFilterStudioSetMidiParameters = this.WhenAnyValue(x => x.SearchTextStudioSetMidi)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .DistinctUntilChanged()
                                        .Select(FilterProvider.ParameterFilter);
            var parFilterStudioSetPartParameters = this.WhenAnyValue(x => x.SearchTextStudioSetPart)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);
            var refreshFilterStudioSetPart = this.WhenAnyValue(x => x.RefreshStudioSetPart)
                                                .Select(FilterProvider.ParameterFilter);
            var parFilterStudioSetPartEQParameters = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);
            var refreshFilterStudioSetPartEQ = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ)
                                                .Select(FilterProvider.ParameterFilter);
            var parFilterPCMSynthToneCommonParameters = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);
            var refreshFilterPCMSynthToneCommon = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon)
                                                .Select(FilterProvider.ParameterFilter);
            var parFilterPCMSynthToneCommon2Parameters = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon2)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);
            var refreshFilterPCMSynthToneCommon2 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon2)
                                                .Select(FilterProvider.ParameterFilter);
            var parFilterPCMSynthToneCommonMFXParameters = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommonMFX)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);
            var refreshFilterPCMSynthToneCommonMFX = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommonMFX)
                                                .Select(FilterProvider.ParameterFilter);
            var parFilterPCMSynthTonePMTParameters = this.WhenAnyValue(x => x.SearchTextPCMSynthTonePMT)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);
            var refreshFilterPCMSynthTonePMTParameters = this.WhenAnyValue(x => x.RefreshPCMSynthTonePMT)
                                                .Select(FilterProvider.ParameterFilter);
            _cleanupPresets = _sourceCachePresets.Connect()
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterPreset)
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _presets,
                                            SortExpressionComparer<Integra7Preset>.Ascending(t => t.Id))
                                        .DisposeMany()
                                        .Subscribe();
            _cleanupMidiParams = _sourceCacheStudioSetMidiParameters.Connect()
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterStudioSetMidiParameters)
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetMidiParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupStudioSetPartParams = _sourceCacheStudioSetPartParameters.Connect()
                                        .Filter(refreshFilterStudioSetPart)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterStudioSetPartParameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCacheStudioSetPartParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCacheStudioSetPartParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetPartParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();
            _cleanupStudioSetPartEQParams = _sourceCacheStudioSetPartEQParameters.Connect()
                                                .Filter(refreshFilterStudioSetPartEQ)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .Filter(parFilterStudioSetPartEQParameters)
                                                .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                        ? _sourceCacheStudioSetPartEQParameters
                                                            .Watch(parentId)
                                                            .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                        : Observable.Return(true))
                                                .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                        ? _sourceCacheStudioSetPartEQParameters
                                                            .Watch(parentId2)
                                                            .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                        : Observable.Return(true))
                                                .ObserveOn(RxApp.MainThreadScheduler)
                                                .SortAndBind(
                                                    out _StudioSetPartEQParameters,
                                                    SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                                .DisposeMany()
                                                .Subscribe();
            _cleanupPCMSynthToneCommonParams = _sourceCachePCMSynthToneCommonParameters.Connect()
                                        .Filter(refreshFilterPCMSynthToneCommon)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterPCMSynthToneCommonParameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCachePCMSynthToneCommonParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCachePCMSynthToneCommonParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _PCMSynthToneCommonParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();
            _cleanupPCMSynthToneCommon2Params = _sourceCachePCMSynthToneCommon2Parameters.Connect()
                                        .Filter(refreshFilterPCMSynthToneCommon2)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterPCMSynthToneCommon2Parameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCachePCMSynthToneCommon2Parameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCachePCMSynthToneCommon2Parameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _PCMSynthToneCommon2Parameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();
            _cleanupPCMSynthToneCommonMFXParams = _sourceCachePCMSynthToneCommonMFXParameters.Connect()
                                        .Filter(refreshFilterPCMSynthToneCommonMFX)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterPCMSynthToneCommonMFXParameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCachePCMSynthToneCommonMFXParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCachePCMSynthToneCommonMFXParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _PCMSynthToneCommonMFXParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();
            _cleanupPCMSynthTonePMTParametersParams = _sourceCachePCMSynthTonePMTParameters.Connect()
                                        .Filter(refreshFilterPCMSynthTonePMTParameters)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterPCMSynthTonePMTParameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCachePCMSynthTonePMTParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCachePCMSynthTonePMTParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _PCMSynthTonePMTParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();
        }
        else
        {
            var parFilterSetup = this.WhenAnyValue(x => x.SearchTextSetup)
                                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                        .DistinctUntilChanged()
                                                        .Select(FilterProvider.ParameterFilter);

            var parFilterSystem = this.WhenAnyValue(x => x.SearchSystem)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);

            var parFilterStudioSetCommon = this.WhenAnyValue(x => x.SearchTextStudioSetCommon)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);

            var parFilterStudioSetCommonChorus = this.WhenAnyValue(x => x.SearchTextStudioSetCommonChorus)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);

            var refreshCommonChorus = this.WhenAnyValue(x => x.RefreshCommonChorusNeeded)
                                                .Select(FilterProvider.ParameterFilter);

            var parFilterStudioSetCommonReverb = this.WhenAnyValue(x => x.SearchTextStudioSetCommonReverb)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);

            var refreshCommonReverb = this.WhenAnyValue(x => x.RefreshCommonReverbNeeded)
                                                .Select(FilterProvider.ParameterFilter);

            var parFilterStudioSetCommonMotionalSurroundParameters = this.WhenAnyValue(x => x.SearchTextStudioSetCommonMotionalSurround)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);

            var refreshCommonMotionalSurround = this.WhenAnyValue(x => x.RefreshCommonMotionalSurroundNeeded)
                                                .Select(FilterProvider.ParameterFilter);

            var parFilterStudioSetCommonMasterEQParameters = this.WhenAnyValue(x => x.SearchTextStudioSetCommonMasterEQ)
                                                .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                                .DistinctUntilChanged()
                                                .Select(FilterProvider.ParameterFilter);

            var refreshCommonMasterEQ = this.WhenAnyValue(x => x.RefreshCommonMasterEQNeeded)
                                                .Select(FilterProvider.ParameterFilter);

            _cleanupSetup = _sourceCacheSetupParameters.Connect()
                                        .Filter(parFilterSetup)
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _setupParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupSystem = _sourceCacheSystem.Connect()
                                        .Filter(parFilterSystem)
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _systemParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupStudioSetCommon = _sourceCacheStudioSetCommonParameters.Connect()
                                        .Filter(parFilterStudioSetCommon)
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetCommonParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupStudioSetChorus = _sourceCacheStudioSetCommonChorusParameters.Connect()
                                        .Filter(refreshCommonChorus)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterStudioSetCommonChorus)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCacheStudioSetCommonChorusParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCacheStudioSetCommonChorusParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetCommonChorusParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupStudioSetReverb = _sourceCacheStudioSetCommonReverbParameters.Connect()
                                        .Filter(refreshCommonReverb)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterStudioSetCommonReverb)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCacheStudioSetCommonReverbParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCacheStudioSetCommonReverbParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetCommonReverbParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupMotionalSurround = _sourceCacheStudioSetCommonMotionalSurroundParameters.Connect()
                                        .Filter(refreshCommonMotionalSurround)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterStudioSetCommonMotionalSurroundParameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCacheStudioSetCommonMotionalSurroundParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCacheStudioSetCommonMotionalSurroundParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetCommonMotionalSurroundParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();

            _cleanupStudioSetMasterEQ = _sourceCacheStudioSetCommonMasterEQParameters.Connect()
                                        .Filter(refreshCommonMasterEQ)
                                        .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                        .Filter(parFilterStudioSetCommonMasterEQParameters)
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                                ? _sourceCacheStudioSetCommonMasterEQParameters
                                                    .Watch(parentId)
                                                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                                : Observable.Return(true))
                                        .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                                ? _sourceCacheStudioSetCommonMasterEQParameters
                                                    .Watch(parentId2)
                                                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                                : Observable.Return(true))
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .SortAndBind(
                                            out _studioSetCommonMasterEQParameters,
                                            SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                        .DisposeMany()
                                        .Subscribe();
        }
    }
    private bool _commonTab;
    public bool IsCommonTab => _commonTab;
    public bool IsPartTab => !_commonTab;

    public void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string Offset2AddressName, string ParPath, bool ResyncNeeded)
    {
        if (!ResyncNeeded)
        {
            if (IsCommonTab)
            {
                if (Offset2AddressName == "Offset2/Studio Set Common Chorus")
                {
                    // force re-evaluation of the dynamic data filters after the parameters were read from integra-7
                    // this feels like a very ugly hack, but i currently do not know how to do it properly
                    // i tried tons of other stuff (like: "this.RaisePropertyChanged(nameof(RefreshCommonChorusNeeded))"), but nothing seems to work
                    // ... shiver ...
                    RefreshCommonChorusNeeded = "."; // RefreshCommonChorusNeeded must not have any .Throttle clauses
                    RefreshCommonChorusNeeded = SearchTextStudioSetCommonChorus;
                }
                else if (Offset2AddressName == "Offset2/Studio Set Common Reverb")
                {
                    RefreshCommonReverbNeeded = ".";
                    RefreshCommonReverbNeeded = SearchTextStudioSetCommonReverb;
                }
            }
            else if (IsPartTab)
            {
                if (Offset2AddressName == $"Offset2/Studio Set Part {_part + 1}")
                {
                    RefreshStudioSetPart = ".";
                    RefreshStudioSetPart = SearchTextStudioSetPart;
                }
                else if (Offset2AddressName == $"Offset2/Studio Set Part EQ {_part + 1}")
                {
                    RefreshStudioSetPart = ".";
                    RefreshStudioSetPart = SearchTextStudioSetPart;
                }
                else if (StartAddressName == $"Temporary Tone Part {_part + 1}" && Offset2AddressName == "Offset2/PCM Synth Tone Common")
                {
                    RefreshPCMSynthToneCommon = ".";
                    RefreshPCMSynthToneCommon = SearchTextPCMSynthToneCommon;
                }
                else if (StartAddressName == $"Temporary Tone Part {_part + 1}" && Offset2AddressName == "Offset2/PCM Synth Tone Common 2")
                {
                    RefreshPCMSynthToneCommon2 = ".";
                    RefreshPCMSynthToneCommon2 = SearchTextPCMSynthToneCommon2;
                }
                else if (StartAddressName == $"Temporary Tone Part {_part + 1}" && Offset2AddressName == "Offset2/PCM Synth Tone Common MFX")
                {
                    RefreshPCMSynthToneCommonMFX = ".";
                    RefreshPCMSynthToneCommonMFX = SearchTextPCMSynthToneCommonMFX;
                }
                else if (StartAddressName == $"Temporary Tone Part {_part + 1}" && Offset2AddressName == "Offset2/PCM Synth Tone Partial Mix Table")
                {
                    RefreshPCMSynthTonePMT = ".";
                    RefreshPCMSynthTonePMT = SearchTextPCMSynthTonePMT;
                }
                
                if (IsPartTab && ParPath.Contains("Tone Bank Select") || ParPath.Contains("Tone Bank Program Number"))
                {
                    if (Offset2AddressName == $"Offset2/Studio Set Part {_part + 1}")
                    {
                        // using MessageBus instead of direct call because it is automatically throttled
                        MessageBus.Current.SendMessage(new UpdateResyncPart(_part));
                    }
                }
            }

            if (_partialViewModels != null)
            {
                foreach (PartialViewModel pvm in _partialViewModels)
                {
                    pvm.ForceUiRefresh(StartAddressName, OffsetAddressName, Offset2AddressName, ParPath, ResyncNeeded);
                }
            }
        }
        else
        {
            if (IsPartTab && (OffsetAddressName.Contains($"Part {_part + 1}") || StartAddressName.Contains($"Part {_part + 1}")))
                MessageBus.Current.SendMessage(new UpdateResyncPart(_part));
        }
    }

    public void InitializeParameterSourceCaches()
    {
        if (_i7domain is null)
            return;
        
        if (!_commonTab)
        {
            _i7domain.StudioSetMidi(_part).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid = _i7domain.StudioSetMidi(_part).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters.AddOrUpdate(p_mid);

            _i7domain.StudioSetPart(_part).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part = _i7domain.StudioSetPart(_part).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters.AddOrUpdate(p_part);
            PreSelectConfiguredPreset(_i7domain.StudioSetPart(_part));

            _i7domain.StudioSetPartEQ(_part).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq = _i7domain.StudioSetPartEQ(_part).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters.AddOrUpdate(p_parteq);

            if (_selectedPreset?.ToneTypeStr == "PCMS")
            {
                _i7domain.PCMSynthToneCommon(_part).ReadFromIntegra();
                _i7domain.PCMSynthToneCommon2(_part).ReadFromIntegra();
                _i7domain.PCMSynthToneCommonMFX(_part).ReadFromIntegra();
                _i7domain.PCMSynthTonePMT(_part).ReadFromIntegra();
            }
            ObservableCollection<PartialViewModel> pvm = [];
            for (byte i = 0; i < 4; i++)
            {
                pvm.Add(new PartialViewModel(this, _part, i,
                    _selectedPreset?.ToneTypeStr,
                    _i7startAddresses, _i7parameters, _i7Api,
                    _i7domain));
            }
            _partialViewModels = new ReadOnlyObservableCollection<PartialViewModel>(pvm);
            foreach (PartialViewModel p in _partialViewModels)
            {
                p.InitializeParameterSourceCaches();
            }            
            List<FullyQualifiedParameter> p_pcmstc = _i7domain.PCMSynthToneCommon(_part).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters.AddOrUpdate(p_pcmstc);
            List<FullyQualifiedParameter> p_pcmstc2 = _i7domain.PCMSynthToneCommon2(_part).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommon2Parameters.AddOrUpdate(p_pcmstc2);
            List<FullyQualifiedParameter> p_pcmmfx = _i7domain.PCMSynthToneCommonMFX(_part).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonMFXParameters.AddOrUpdate(p_pcmmfx);
            List<FullyQualifiedParameter> p_pcmpmt = _i7domain.PCMSynthTonePMT(_part).GetRelevantParameters(true, true);
            _sourceCachePCMSynthTonePMTParameters.AddOrUpdate(p_pcmpmt);
        }
        else
        {
            _i7domain?.Setup.ReadFromIntegra();
            List<FullyQualifiedParameter> p_s = _i7domain?.Setup.GetRelevantParameters();
            _sourceCacheSetupParameters.AddOrUpdate(p_s);

            _i7domain?.System.ReadFromIntegra();
            List<FullyQualifiedParameter> s_s = _i7domain?.System.GetRelevantParameters();
            _sourceCacheSystem.AddOrUpdate(s_s);

            _i7domain?.StudioSetCommon.ReadFromIntegra();
            List<FullyQualifiedParameter> p_ssc = _i7domain?.StudioSetCommon.GetRelevantParameters();
            _sourceCacheStudioSetCommonParameters.AddOrUpdate(p_ssc);

            _i7domain?.StudioSetCommonChorus.ReadFromIntegra();
            List<FullyQualifiedParameter> p_sscc = _i7domain?.StudioSetCommonChorus.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonChorusParameters.AddOrUpdate(p_sscc);

            _i7domain?.StudioSetCommonReverb.ReadFromIntegra();
            List<FullyQualifiedParameter> p_sscr = _i7domain?.StudioSetCommonReverb.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonReverbParameters.AddOrUpdate(p_sscr);

            _i7domain?.StudioSetCommonMotionalSurround.ReadFromIntegra();
            List<FullyQualifiedParameter> p_ssms = _i7domain?.StudioSetCommonMotionalSurround.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonMotionalSurroundParameters.AddOrUpdate(p_ssms);

            _i7domain?.StudioSetCommonMasterEQ.ReadFromIntegra();
            List<FullyQualifiedParameter> p_meq = _i7domain?.StudioSetCommonMasterEQ.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonMasterEQParameters.AddOrUpdate(p_meq);
        }
    }

    public Integra7Preset SelectedPreset
    {
        get => _selectedPreset;
        set
        {
            if (_selectedPreset != value && value is not null)
            {
                _selectedPreset = value;
                ChangePreset();
                this.RaisePropertyChanged();
            }
        }
    }

    [ReactiveCommand]
    public void ChangePreset()
    {
        Integra7Preset CurrentSelection = _selectedPreset;
        if (CurrentSelection != null)
        {
            _i7Api.ChangePreset(_part, CurrentSelection.Msb, CurrentSelection.Lsb, CurrentSelection.Pc);
        }
    }

    public void ResyncPart(byte part)
    {
        if (_i7domain is null)
            return;
        
        if (part != _part)
            return;

        if (_commonTab)
            return;

        DomainBase midiPart = _i7domain?.StudioSetMidi(part);
        midiPart.ReadFromIntegra();
        ForceUiRefresh(midiPart.StartAddressName, midiPart.OffsetAddressName, midiPart.Offset2AddressName, "", false /* don't cause inf loop */);
        DomainBase setPart = _i7domain?.StudioSetPart(part);
        setPart.ReadFromIntegra();
        ForceUiRefresh(setPart.StartAddressName, setPart.OffsetAddressName, setPart.Offset2AddressName, "", false /* don't cause inf loop */);
        if (_selectedPreset.ToneTypeStr == "PCMS")
        {
            DomainBase setPCMSTone = _i7domain?.PCMSynthToneCommon(part);
            setPCMSTone.ReadFromIntegra();
            ForceUiRefresh(setPCMSTone.StartAddressName, setPCMSTone.OffsetAddressName, setPCMSTone.Offset2AddressName, "", false /* don't cause inf loop */);
            DomainBase setPCMSTone2 = _i7domain?.PCMSynthToneCommon2(part);
            setPCMSTone2.ReadFromIntegra();
            ForceUiRefresh(setPCMSTone2.StartAddressName, setPCMSTone2.OffsetAddressName, setPCMSTone2.Offset2AddressName, "", false /* don't cause inf loop */);
            DomainBase setPCMSToneMFX = _i7domain?.PCMSynthToneCommonMFX(part);
            setPCMSToneMFX.ReadFromIntegra();
            ForceUiRefresh(setPCMSToneMFX.StartAddressName, setPCMSToneMFX.OffsetAddressName, setPCMSToneMFX.Offset2AddressName, "", false /* don't cause inf loop */);
            DomainBase setPCMSTonePMT = _i7domain?.PCMSynthTonePMT(part);
            setPCMSTonePMT.ReadFromIntegra();
            ForceUiRefresh(setPCMSTonePMT.StartAddressName, setPCMSTonePMT.OffsetAddressName, setPCMSTonePMT.Offset2AddressName, "", false /* don't cause inf loop */);
            foreach (PartialViewModel p in _partialViewModels)
            {
                p.ResyncPart(part);
            }
        }
        PreSelectConfiguredPreset(setPart);
        this.RaisePropertyChanged(nameof(SelectedPresetIsSynthTone));
    }
}