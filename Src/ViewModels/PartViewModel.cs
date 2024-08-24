using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private Integra7Domain _i7domain;
    private List<Integra7Preset> _i7presets;
    private Integra7Preset _selectedPreset;
    private SourceCache<Integra7Preset, int> _sourceCachePresets = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presets = new([]);
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
    private byte _part = 0;

    public bool SelectedPresetIsSynthTone
    {
        get => _selectedPreset is null ? false : _selectedPreset.ToneTypeStr == "PCMS";
    }

    [Reactive]
    private string _searchTextStudioSetMidi = "";
    [Reactive]
    private string _searchTextStudioSetPart = "";
    [Reactive]
    public string _refreshStudioSetPart = "";
    [Reactive]
    private string _searchTextStudioSetPartEQ = "";
    [Reactive]
    public string _refreshStudioSetPartEQ = "";
    [Reactive]
    private string _searchTextPCMSynthToneCommon = "";
    [Reactive]
    public string _refreshPCMSynthToneCommon = "";

    public string Header { get => _commonTab ? "Common" : $"Part {_part + 1}"; }

    IDisposable? _cleanupPresets = null;
    IDisposable? _cleanupMidiParams = null;
    IDisposable? _cleanupStudioSetPartParams = null;
    IDisposable? _cleanupStudioSetPartEQParams = null;
    IDisposable? _cleanupPCMSynthToneCommonParams = null;

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

    IDisposable? _cleanupSetup = null;
    IDisposable? _cleanupSystem = null;
    IDisposable? _cleanupStudioSetCommon = null;
    IDisposable? _cleanupStudioSetChorus = null;
    IDisposable? _cleanupStudioSetReverb = null;
    IDisposable? _cleanupMotionalSurround = null;
    IDisposable? _cleanupStudioSetMasterEQ = null;

    public void PreSelectConfiguredPreset(DomainBase b)
    {
        string msbstr = b.LookupSingleParameterDisplayedValue("Studio Set Part/Tone Bank Select MSB");
        string lsbstr = b.LookupSingleParameterDisplayedValue("Studio Set Part/Tone Bank Select LSB");
        string pcstr = b.LookupSingleParameterDisplayedValue("Studio Set Part/Tone Bank Program Number (PC)");
        foreach (Integra7Preset p in _i7presets)
        {
            if (msbstr == $"{p.Msb}" && lsbstr == $"{p.Lsb}" && pcstr == $"{p.Pc}")
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

        if (!commonTab)
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
            }
            List<FullyQualifiedParameter> p_pcmstc = _i7domain.PCMSynthToneCommon(_part).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters.AddOrUpdate(p_pcmstc);

            _sourceCachePresets.AddOrUpdate(i7presets);
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

            _cleanupPresets = _sourceCachePresets.Connect()
                                        .ObserveOn(RxApp.MainThreadScheduler)
                                        .Bind(out _presets)
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
    private bool _commonTab = false;
    public bool IsCommonTab { get => _commonTab; }
    public bool IsPartTab { get => !_commonTab; }
    public void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string ParPath)
    {
        if (OffsetAddressName == "Offset/Studio Set Common Chorus")
        {
            // force re-evaluation of the dynamic data filters after the parameters were read from integra-7
            // this feels like a very ugly hack, but i currently do not know how to do it properly
            // i tried tons of other stuff (like: "this.RaisePropertyChanged(nameof(RefreshCommonChorusNeeded))"), but nothing seems to work
            // ... shiver ...
            RefreshCommonChorusNeeded = "."; // RefreshCommonChorusNeeded must not have any .Throttle clauses
            RefreshCommonChorusNeeded = SearchTextStudioSetCommonChorus;
        }
        else if (OffsetAddressName == "Offset/Studio Set Common Reverb")
        {
            RefreshCommonReverbNeeded = ".";
            RefreshCommonReverbNeeded = SearchTextStudioSetCommonReverb;
        }
        else if (OffsetAddressName == $"Offset/Studio Set Part {_part + 1}")
        {
            RefreshStudioSetPart = ".";
            RefreshStudioSetPart = SearchTextStudioSetPart;
        }
        else if (OffsetAddressName == $"Offset/Studio Set Part EQ {_part + 1}")
        {
            RefreshStudioSetPart = ".";
            RefreshStudioSetPart = SearchTextStudioSetPart;
        }
        else if (StartAddressName == $"Temporary Tone Part {_part + 1}" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon = ".";
            RefreshPCMSynthToneCommon = SearchTextPCMSynthToneCommon;
        }

        if (ParPath.Contains("Tone Bank Select") || ParPath.Contains("Tone Bank Program Number"))
        {
            if (OffsetAddressName == $"Offset/Studio Set Part {_part + 1}")
            {
                // using MessageBus instead of direct call because it is automatically throttled
                MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(_part));
            }
        }
    }

    public void UpdateConnected()
    {
        if (!_commonTab)
        {
            _i7domain.StudioSetMidi(_part).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid = _i7domain.StudioSetMidi(_part).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters.AddOrUpdate(p_mid);

            _i7domain.StudioSetPart(_part).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part = _i7domain.StudioSetPart(_part).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters.AddOrUpdate(p_part);

            _i7domain.StudioSetPartEQ(_part).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq = _i7domain.StudioSetPartEQ(_part).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters.AddOrUpdate(p_parteq);

            if (_selectedPreset?.ToneTypeStr == "PCMS")
            {
                _i7domain.PCMSynthToneCommon(_part).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc = _i7domain.PCMSynthToneCommon(_part).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters.AddOrUpdate(p_pcmstc);
        }
        else
        {
            _i7domain.Setup.ReadFromIntegra();
            List<FullyQualifiedParameter> p_s = _i7domain.Setup.GetRelevantParameters(false, false);
            _sourceCacheSetupParameters.AddOrUpdate(p_s);

            _i7domain.System.ReadFromIntegra();
            List<FullyQualifiedParameter> s_s = _i7domain.System.GetRelevantParameters(false, false);
            _sourceCacheSystem.AddOrUpdate(s_s);

            _i7domain.StudioSetCommon.ReadFromIntegra();
            List<FullyQualifiedParameter> p_ssc = _i7domain.StudioSetCommon.GetRelevantParameters(false, false);
            _sourceCacheStudioSetCommonParameters.AddOrUpdate(p_ssc);

            _i7domain.StudioSetCommonChorus.ReadFromIntegra();
            List<FullyQualifiedParameter> p_sscc = _i7domain.StudioSetCommonChorus.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonChorusParameters.AddOrUpdate(p_sscc);

            _i7domain.StudioSetCommonReverb.ReadFromIntegra();
            List<FullyQualifiedParameter> p_sscr = _i7domain.StudioSetCommonReverb.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonReverbParameters.AddOrUpdate(p_sscr);

            _i7domain.StudioSetCommonMotionalSurround.ReadFromIntegra();
            List<FullyQualifiedParameter> p_ssms = _i7domain.StudioSetCommonMotionalSurround.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonMotionalSurroundParameters.AddOrUpdate(p_ssms);

            _i7domain.StudioSetCommonMasterEQ.ReadFromIntegra();
            List<FullyQualifiedParameter> p_meq = _i7domain.StudioSetCommonMasterEQ.GetRelevantParameters(true, true);
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
                this.RaisePropertyChanged(nameof(SelectedPreset));
            }
        }
    }
    public void SetSelectedPreset(Integra7Preset selectedPreset)
    {
        SelectedPreset = selectedPreset;
        ChangePreset();
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
        if (part != _part)
            return;

        if (_commonTab)
            return;

        DomainBase midiPart = _i7domain.StudioSetMidi(part);
        midiPart.ReadFromIntegra();
        ForceUiRefresh(midiPart.StartAddressName, midiPart.OffsetAddressName, "");
        DomainBase setPart = _i7domain.StudioSetPart(part);
        setPart.ReadFromIntegra();
        ForceUiRefresh(setPart.StartAddressName, setPart.OffsetAddressName, "");

        if (_selectedPreset.ToneTypeStr == "PCMS")
        {
            DomainBase setPCMSTone = _i7domain.PCMSynthToneCommon(part);
            setPCMSTone.ReadFromIntegra();
            ForceUiRefresh(setPCMSTone.StartAddressName, setPCMSTone.OffsetAddressName, "");
        }
        this.RaisePropertyChanged(nameof(SelectedPresetIsSynthTone));
    }
}