using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Controls.Embedding.Offscreen;
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
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presets;
    public ReadOnlyObservableCollection<Integra7Preset> Presets => _presets;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters => _studioSetMidiParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters => _studioSetPartParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters => _StudioSetPartEQParameters;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters => _PCMSynthToneCommonParameters;
    private byte _part;

    public bool SelectedPresetIsSynthTone
    {
        get => _selectedPreset is null ? false : _selectedPreset.ToneTypeStr == "PCMS";
    }

    [Reactive]
    private string _searchTextStudioSetMidi;
    [Reactive]
    private string _searchTextStudioSetPart;
    [Reactive]
    public string _refreshStudioSetPart;
    [Reactive]
    private string _searchTextStudioSetPartEQ;
    [Reactive]
    public string _refreshStudioSetPartEQ;
    [Reactive]
    private string _searchTextPCMSynthToneCommon;
    [Reactive]
    public string _refreshPCMSynthToneCommon;

    public string Header { get => $"Part {_part + 1}"; }

    IDisposable _cleanupPresets;
    IDisposable _cleanupMidiParams;
    IDisposable _cleanupStudioSetPartParams;
    IDisposable _cleanupStudioSetPartEQParams;
    IDisposable _cleanupPCMSynthToneCommonParams;

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


    public PartViewModel(ViewModelBase parent, byte zeroBasedPartNo, Integra7StartAddresses i7startAddr, Integra7Parameters i7par, IIntegra7Api i7, Integra7Domain i7dom, List<Integra7Preset> i7presets)
    {
        _parent = parent;
        _part = zeroBasedPartNo;
        _i7startAddresses = i7startAddr;
        _i7parameters = i7par;
        _i7Api = i7;
        _i7domain = i7dom;
        _i7presets = i7presets;

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
                                    .Throttle(TimeSpan.FromMilliseconds(250))
                                    .DistinctUntilChanged()
                                    .Select(FilterProvider.ParameterFilter);
        var parFilterStudioSetPartParameters = this.WhenAnyValue(x => x.SearchTextStudioSetPart)
                                            .Throttle(TimeSpan.FromMilliseconds(250))
                                            .DistinctUntilChanged()
                                            .Select(FilterProvider.ParameterFilter);
        var refreshFilterStudioSetPart = this.WhenAnyValue(x => x.RefreshStudioSetPart)
                                            .Select(FilterProvider.ParameterFilter);
        var parFilterStudioSetPartEQParameters = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ)
                                            .Throttle(TimeSpan.FromMilliseconds(250))
                                            .DistinctUntilChanged()
                                            .Select(FilterProvider.ParameterFilter);
        var refreshFilterStudioSetPartEQ = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ)
                                            .Select(FilterProvider.ParameterFilter);
        var parFilterPCMSynthToneCommonParameters = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon)
                                            .Throttle(TimeSpan.FromMilliseconds(250))
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
                                    .Throttle(TimeSpan.FromMilliseconds(250))
                                    .Filter(parFilterStudioSetMidiParameters)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanupStudioSetPartParams = _sourceCacheStudioSetPartParameters.Connect()
                                    .Filter(refreshFilterStudioSetPart)
                                    .Throttle(TimeSpan.FromMilliseconds(250))
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
                                            .Throttle(TimeSpan.FromMilliseconds(250))
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
                                    .Throttle(TimeSpan.FromMilliseconds(250))
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

    public static TabType SelectedTabType
    {
        get => TabType.Part;
    }
    public void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string ParPath)
    {
        if (OffsetAddressName == $"Offset/Studio Set Part {_part + 1}")
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

        if (ParPath.StartsWith("Tone Bank Select") || ParPath.StartsWith("Tone Bank Program Number"))
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
            this.RaisePropertyChanged(nameof(SelectedPresetIsSynthTone));
        }
    }
}