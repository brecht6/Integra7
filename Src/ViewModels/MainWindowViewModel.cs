using Avalonia;
using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.IO;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Platform;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;
using Integra7AuralAlchemist.Models.Domain;
using DynamicData.Binding;

namespace Integra7AuralAlchemist.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS8618 // Non-nullable field 'xxx' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the field as nullable.
    private TabType _tabType;
    public TabType SelectedTabType
    {
        get => _tabType;
        set => this.RaiseAndSetIfChanged(ref _tabType, value);
    }
    public TabType[] AvailableTabTypes { get; } = Enum.GetValues<TabType>();

    private Integra7StartAddresses _i7startAddresses = new();
    private Integra7Parameters _i7parameters = new();
    [Reactive]
    private string _searchTextSetup;
    [Reactive]
    private string _searchSystem;
    [Reactive]
    private string _searchTextStudioSetCommon;
    [Reactive]
    private string _searchTextStudioSetCommonChorus;
    [Reactive]
    private string _refreshCommonChorusNeeded;
    [Reactive]
    private string _searchTextStudioSetCommonReverb;
    [Reactive]
    private string _refreshCommonReverbNeeded;
    [Reactive]
    private string _searchTextStudioSetCommonMotionalSurround;
    [Reactive]
    private string _refreshCommonMotionalSurroundNeeded;
    [Reactive]
    private string _searchTextStudioSetCommonMasterEQ;
    [Reactive]
    public string _refreshCommonMasterEQNeeded;

    [Reactive]
    private bool _rescanButtonEnabled = true;
    private Integra7Domain? _integra7Communicator = null;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheSetupParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _setupParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> SetupParameters => _setupParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheSystem = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _systemParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> SystemParameters => _systemParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonParameters => _studioSetCommonParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonChorusParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonChorusParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonChorusParameters => _studioSetCommonChorusParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonReverbParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonReverbParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonReverbParameters => _studioSetCommonReverbParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonMotionalSurroundParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonMotionalSurroundParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonMotionalSurroundParameters => _studioSetCommonMotionalSurroundParameters;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetCommonMasterEQParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetCommonMasterEQParameters;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetCommonMasterEQParameters => _studioSetCommonMasterEQParameters;
    private readonly ReadOnlyObservableCollection<PartViewModel> _partViewModels;
    public ReadOnlyObservableCollection<PartViewModel> PartViewModels => _partViewModels;
    private readonly Dictionary<int, IDisposable> _cleanUp = new Dictionary<int, IDisposable>();

    private const string INTEGRA_CONNECTION_STRING = "INTEGRA-7";
    private IIntegra7Api? Integra7 { get; set; } = null;

    [Reactive]
    private bool _connected = false;

    [Reactive]
    private string _midiDevices = "No Midi Devices Detected";

    [ReactiveCommand]
    public void PlayNote()
    {
        byte zeroBasedMidiChannel = 0;
        if (CurrentPartSelection > 0 && CurrentPartSelection < 17)
        {
            zeroBasedMidiChannel = (byte)(CurrentPartSelection - 1);
        }
        Integra7?.NoteOn(zeroBasedMidiChannel, 65, 100);
        Thread.Sleep(1000);
        Integra7?.NoteOff(zeroBasedMidiChannel, 65);
    }

    [ReactiveCommand]
    public void Panic()
    {
        Integra7?.AllNotesOff();
    }

    [ReactiveCommand]
    public void RescanMidiDevices()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING));
        UpdateConnected(Integra7);
    }

    private void UpdateConnected(IIntegra7Api integra7Api)
    {
        Connected = integra7Api.ConnectionOk();
        if (_connected)
        {
            MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING + " with device id " + integra7Api.DeviceId().ToString("x2");
            _integra7Communicator = new Integra7Domain(integra7Api, _i7startAddresses, _i7parameters);

            _integra7Communicator.Setup.ReadFromIntegra();
            List<FullyQualifiedParameter> p_s = _integra7Communicator.Setup.GetRelevantParameters(false, false);
            _sourceCacheSetupParameters.AddOrUpdate(p_s);

            _integra7Communicator.System.ReadFromIntegra();
            List<FullyQualifiedParameter> s_s = _integra7Communicator.System.GetRelevantParameters(false, false);
            _sourceCacheSystem.AddOrUpdate(s_s);

            _integra7Communicator.StudioSetCommon.ReadFromIntegra();
            List<FullyQualifiedParameter> p_ssc = _integra7Communicator.StudioSetCommon.GetRelevantParameters(false, false);
            _sourceCacheStudioSetCommonParameters.AddOrUpdate(p_ssc);

            _integra7Communicator.StudioSetCommonChorus.ReadFromIntegra();
            List<FullyQualifiedParameter> p_sscc = _integra7Communicator.StudioSetCommonChorus.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonChorusParameters.AddOrUpdate(p_sscc);

            _integra7Communicator.StudioSetCommonReverb.ReadFromIntegra();
            List<FullyQualifiedParameter> p_sscr = _integra7Communicator.StudioSetCommonReverb.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonReverbParameters.AddOrUpdate(p_sscr);

            _integra7Communicator.StudioSetCommonMotionalSurround.ReadFromIntegra();
            List<FullyQualifiedParameter> p_ssms = _integra7Communicator.StudioSetCommonMotionalSurround.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonMotionalSurroundParameters.AddOrUpdate(p_ssms);

            _integra7Communicator.StudioSetCommonMasterEQ.ReadFromIntegra();
            List<FullyQualifiedParameter> p_meq = _integra7Communicator.StudioSetCommonMasterEQ.GetRelevantParameters(true, true);
            _sourceCacheStudioSetCommonMasterEQParameters.AddOrUpdate(p_meq);

            if (_partViewModels is not null)
            {
                foreach (PartViewModel pvm in _partViewModels)
                {
                    pvm.UpdateConnected();
                }
            }
        }
        else
        {
            MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
        }
        RescanButtonEnabled = !_connected;
    }

    [Reactive]
    private int _currentPartSelection = 0;

    [ReactiveCommand]
    public void DebugCode()
    {
        /*
        DomainSetup dse = new DomainSetup(Integra7, _i7startAddresses, _i7parameters);
        dse.ReadFromIntegra();
        dse.ModifySingleParameterDisplayedValue("Setup/Studio Set BS MSB", "85");
        dse.WriteToIntegra();

        DomainSystem dsy = new DomainSystem(Integra7, _i7startAddresses, _i7parameters);
        FullyQualifiedParameter? q = dsy.ReadFromIntegra("System Common/Master Tune");
        q?.DebugLog();
        dsy.ReadFromIntegra();
        string CurrentValue = dsy.LookupSingleParameterDisplayedValue("System Common/Master Tune");
        dsy.ModifySingleParameterDisplayedValue("System Common/Master Tune", "-50");
        dsy.WriteToIntegra();
        FullyQualifiedParameter? p = dsy.ReadFromIntegra("System Common/Master Tune");
        p?.DebugLog();
        string originalVal = dsy.LookupSingleParameterDisplayedValue("System Common/Master Level");
        dsy.WriteToIntegra("System Common/Master Level", "120");
        FullyQualifiedParameter? r = dsy.ReadFromIntegra("System Common/Master Level");
        r?.DebugLog();
        dsy.WriteToIntegra("System Common/Master Level", "127");
        r = dsy.ReadFromIntegra("System Common/Master Level");
        r?.DebugLog();

        DomainStudioSetCommon dssc = new DomainStudioSetCommon(Integra7, _i7startAddresses, _i7parameters);
        dssc.ReadFromIntegra();
        dssc.WriteToIntegra("Studio Set Common/Studio Set Name", "Integra Preview");
        FullyQualifiedParameter? s = dssc.ReadFromIntegra("Studio Set Common/Studio Set Name");
        s?.DebugLog();

        DomainStudioSetCommonChorus dsscc = new DomainStudioSetCommonChorus(Integra7, _i7startAddresses, _i7parameters);
        dsscc.ReadFromIntegra();

        DomainStudioSetCommonReverb dsscr = new DomainStudioSetCommonReverb(Integra7, _i7startAddresses, _i7parameters);
        dsscr.ReadFromIntegra();

        DomainStudioSetCommonMotionalSurround dsscms = new DomainStudioSetCommonMotionalSurround(Integra7, _i7startAddresses, _i7parameters);
        dsscms.ReadFromIntegra();

        DomainStudioSetMasterEQ dssme = new DomainStudioSetMasterEQ(Integra7, _i7startAddresses, _i7parameters);
        dssme.ReadFromIntegra();
   
        DomainStudioSetMIDI dssmi0 = new DomainStudioSetMIDI(0, Integra7, _i7startAddresses, _i7parameters);DomainStudioSetPart
        dssmi0.ReadFromIntegra();
        DomainStudioSetMIDI dssmi1 = new DomainStudioSetMIDI(1, Integra7, _i7startAddresses, _i7parameters);
        dssmi1.ReadFromIntegra();

        DomainStudioSetPart dsspa0 = new DomainStudioSetPart(0, Integra7, _i7startAddresses, _i7parameters);
        dsspa0.ReadFromIntegra();

        DomainStudioSetPartEQ dsspeq0 = new DomainStudioSetPartEQ(0, Integra7, _i7startAddresses, _i7parameters);
        dsspeq0.ReadFromIntegra();

        DomainPCMSynthToneCommon dpcmsynthtonecommon0 = new DomainPCMSynthToneCommon(0, Integra7, _i7startAddresses, _i7parameters);
        dpcmsynthtonecommon0.ReadFromIntegra();

        DomainPCMSynthToneCommonMFX dpcmsynthtonecommonmfx0 = new DomainPCMSynthToneCommonMFX(0, Integra7, _i7startAddresses, _i7parameters);
        dpcmsynthtonecommonmfx0.ReadFromIntegra();
        */

    }

    private List<Integra7Preset> LoadPresets()
    {
        var uri = @"avares://" + "Integra7AuralAlchemist/" + "Assets/Presets.csv";
        var file = new StreamReader(AssetLoader.Open(new Uri(uri)));
        var data = file.ReadLine();
        char[] separators = { ',' };
        List<Integra7Preset> Presets = [];
        int id = 0;
        while ((data = file.ReadLine()) != null)
        {
            string[] read = data.Split(separators, StringSplitOptions.None);
            string tonetype = read[0].Trim('"');
            string tonebank = read[1].Trim('"');
            int number = int.Parse(read[2]);
            string name = read[3].Trim('"');
            int msb = int.Parse(read[4]);
            int lsb = int.Parse(read[5]);
            int pc = int.Parse(read[6]);
            string category = read[7].Trim('"');
            for (byte ch = 0; ch < 16; ch++)
            {
                Presets.Add(new Integra7Preset(id, tonetype, tonebank, number, name, msb, lsb, pc, category));
            }
            id++;
        }
        return Presets;
    }

    public MainWindowViewModel()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING));
        List<Integra7Preset> presets = LoadPresets();
        UpdateConnected(Integra7);
        ObservableCollection<PartViewModel> pvm = [];
        for (byte i = 0; i < 16; i++)
        {
            pvm.Add(new PartViewModel(this, i, _i7startAddresses, _i7parameters, Integra7, _integra7Communicator, presets));
        }
        _partViewModels = new ReadOnlyObservableCollection<PartViewModel>(pvm);
        foreach (PartViewModel pa in _partViewModels)
        {
            pa.UpdateConnected();
        }

        const int THROTTLE = 250;
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

        _cleanUp[16] = _sourceCacheSetupParameters.Connect()
                                    .Filter(parFilterSetup)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _setupParameters,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[17] = _sourceCacheSystem.Connect()
                                    .Filter(parFilterSystem)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _systemParameters,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[18] = _sourceCacheStudioSetCommonParameters.Connect()
                                    .Filter(parFilterStudioSetCommon)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetCommonParameters,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[19] = _sourceCacheStudioSetCommonChorusParameters.Connect()
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

        _cleanUp[20] = _sourceCacheStudioSetCommonReverbParameters.Connect()
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

        _cleanUp[21] = _sourceCacheStudioSetCommonMotionalSurroundParameters.Connect()
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

        _cleanUp[21] = _sourceCacheStudioSetCommonMasterEQParameters.Connect()
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

        MessageBus.Current.Listen<UpdateMessageSpec>("ui2hw").Throttle(TimeSpan.FromMilliseconds(250)).Subscribe(m => UpdateIntegraFromUi(m));
        MessageBus.Current.Listen<UpdateFromSysexSpec>("hw2ui").Throttle(TimeSpan.FromMilliseconds(250)).Subscribe(m => UpdateUiFromIntegra(m));
        MessageBus.Current.Listen<UpdateResyncPart>().Throttle(TimeSpan.FromMilliseconds(250)).Subscribe(m => ResyncPart(m.PartNo));
    }

    public void UpdateIntegraFromUi(UpdateMessageSpec s)
    {
        FullyQualifiedParameter p = s.Par;
        p.StringValue = s.DisplayValue;
        _integra7Communicator?.WriteSingleParameterToIntegra(p);
        if (p.ParSpec.IsParent)
        {
            _integra7Communicator?.GetDomain(p).ReadFromIntegra();
            ForceUiRefresh(p);
        }
    }

    public void UpdateUiFromIntegra(UpdateFromSysexSpec s)
    {
        List<UpdateMessageSpec> parameters = SysexDataTransmissionParser.ConvertSysexToParameterUpdates(s.SysexMsg, _integra7Communicator);
        bool ParentControlModified = parameters.Any(spec => spec.Par.ParSpec.IsParent);
        if (!ParentControlModified)
        {
            // update only the affected parameters
            foreach (UpdateMessageSpec spec in parameters)
            {
                _integra7Communicator?.GetDomain(spec.Par).ModifySingleParameterDisplayedValue(spec.Par.ParSpec.Path, spec.DisplayValue);
                ForceUiRefresh(parameters.First().Par);
            }
        }
        else
        {
            // need to resync all relevant parameters instead of just updating the modified parameters
            HashSet<string> alreadyEncountered = [];
            foreach (UpdateMessageSpec spec in parameters)
            {
                string domainName = spec.Par.Start + spec.Par.Offset;
                if (!alreadyEncountered.Contains(domainName))
                {
                    alreadyEncountered.Add(domainName);
                    _integra7Communicator?.GetDomain(spec.Par).ReadFromIntegra();
                    ForceUiRefresh(spec.Par);
                }
            }
        }
    }
    private void ForceUiRefresh(FullyQualifiedParameter p)
    {
        ForceUiRefresh(p.Start, p.Offset, p.ParSpec.Path);
    }

    private void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string ParPath)
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

        foreach (PartViewModel pvm in _partViewModels)
        {
            pvm.ForceUiRefresh(StartAddressName, OffsetAddressName, ParPath);
        }
    }

    public void ResyncPart(byte part)
    {
        foreach (PartViewModel pvm in _partViewModels)
        {
            pvm.ResyncPart(part);
        }
    }

#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS8618 // nullable must be assigned in constructor
}

