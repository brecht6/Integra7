﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Domain;
using Integra7AuralAlchemist.Models.Services;
using Integra7AuralAlchemist.Views;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Serilog;

namespace Integra7AuralAlchemist.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS8618 // Non-nullable field 'xxx' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the field as nullable.
    private readonly Integra7StartAddresses _i7startAddresses = new();
    private readonly Integra7Parameters _i7parameters = new();

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    [Reactive] private bool _rescanButtonEnabled = true;
    private Integra7Domain? _integra7Communicator;

    [Reactive] private bool _isSyncing = true;
    private string _syncInfo = "";
    public string SyncInfo
    {
        get => _syncInfo;
        set
        {
            this.RaiseAndSetIfChanged(ref _syncInfo, value);
            if (value != "")
            {
                Log.Information(value);   
            }
        }
    }
    private int _syncLevels = 0;

    private ReadOnlyObservableCollection<PartViewModel> _partViewModels;
    public ReadOnlyObservableCollection<PartViewModel> PartViewModels => _partViewModels;
    private const string INTEGRA_CONNECTION_STRING = "INTEGRA-7";
    private IIntegra7Api? Integra7 { get; set; }

    [Reactive] private bool _connected;

    [Reactive] private string _midiDevices = "No Midi Devices Detected";
    public bool CurrentPartIsNotCommonPart { get => CurrentPartSelection > 0; }
    public Interaction<SaveUserToneViewModel, UserToneToSave?> ShowSaveUserToneDialog { get; }
    
    [ReactiveCommand]
    public async Task SaveUserTone()
    {
        if (_currentPartSelection == 0)
            return;
        
        if (_partViewModels is null || PartViewModels.Count < 2)
        {
            Log.Error("Cannot save user tone because there are no parts initialized.");
            return;
        }

        if (_partViewModels[_currentPartSelection].SelectedPreset is null)
        {
            Log.Error("Cannot save user tone because there is  no preset selected.");
            return;
        }
       
        List<Integra7Preset> presets = _partViewModels[1].Presets.ToList();
        Integra7Preset preset = _partViewModels[_currentPartSelection].SelectedPreset;
        string toneType = preset.ToneTypeStr;
        SaveUserToneViewModel vm = new SaveUserToneViewModel(presets, toneType);
        UserToneToSave tone = await ShowSaveUserToneDialog.Handle(vm);
        if (tone != null)
            await Integra7.WriteToneToUserMemory(_integra7Communicator, toneType, (byte)(_currentPartSelection-1), "some name", 5);
    }
    
    [ReactiveCommand]
    public async Task DebugCode()
    {
    }

    [ReactiveCommand]
    public async Task PlayNoteAsync()
    {
        byte zeroBasedMidiChannel = 0;
        if (_currentPartSelection is > 0 and < 17)
        {
            zeroBasedMidiChannel = (byte)(_currentPartSelection - 1);
        }

        await Integra7?.NoteOnAsync(zeroBasedMidiChannel, 65, 100);
        Thread.Sleep(1000);
        await Integra7?.NoteOffAsync(zeroBasedMidiChannel, 65);
    }

    [ReactiveCommand]
    public async Task PlayPhraseAsync()
    {
        byte zeroBasedMidiChannel = 0;
        if (_currentPartSelection is > 0 and < 17)
        {
            zeroBasedMidiChannel = (byte)(_currentPartSelection - 1);
        }

        await Integra7?.SendStopPreviewPhraseMsgAsync();
        await Integra7?.SendPlayPreviewPhraseMsgAsync(zeroBasedMidiChannel);
    }

    [ReactiveCommand]
    public async Task StopPhraseAsync()
    {
        await Integra7?.SendStopPreviewPhraseMsgAsync();
    }

    [ReactiveCommand]
    public async Task PanicAsync()
    {
        await Integra7?.AllNotesOffAsync();
        await Integra7?.SendStopPreviewPhraseMsgAsync();
    }

    [ReactiveCommand]
    public async Task RescanMidiDevicesAsync()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING),
            _semaphore);
        await Integra7.CheckIdentityAsync();
        List<Integra7Preset> presets = LoadPresets();
        await UpdateConnectedAsync(Integra7, presets);
    }

    [Reactive] private int _srxSlot1;
    [Reactive] private int _srxSlot2;
    [Reactive] private int _srxSlot3;
    [Reactive] private int _srxSlot4;

    [ReactiveCommand]
    public async Task LoadSrx()
    {
        if (_connected)
        {
            await Integra7?.SendLoadSrxAsync((byte)_srxSlot1, (byte)_srxSlot2, (byte)_srxSlot3, (byte)_srxSlot4);
        }
    }

    private void SignalStartSync()
    {
        IsSyncing = true;
        _syncLevels = _syncLevels + 1;
    }

    private void SignalStopSync()
    {
        _syncLevels = _syncLevels - 1;
        if (_syncLevels == 0)
        {
            IsSyncing = false;
            SyncInfo = "";
        }
    }

    private async Task UpdateConnectedAsync(IIntegra7Api integra7Api, List<Integra7Preset> presets)
    {
        Connected = integra7Api.ConnectionOk();
        try
        {
            if (_connected)
            {
                SignalStartSync();
                SyncInfo = "Fetch loaded SRX...";
                (SrxSlot1, SrxSlot2, SrxSlot3, SrxSlot4) = await integra7Api.GetLoadedSrxAsync();
                Log.Information("Connected to Integra7");
                MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING + " with device id " +
                              integra7Api.DeviceId().ToString("x2");
                _integra7Communicator = new Integra7Domain(integra7Api, _i7startAddresses, _i7parameters, _semaphore);

                await AddUserDefinedPresets(presets);

                ObservableCollection<PartViewModel> pvm = [];
                for (byte i = 0; i < 17; i++)
                {
                    if (i == 0)
                    {
                        SyncInfo = "Initializing common tab...";
                        Log.Information("Creating view model for common tab.");
                    }
                    else
                    {
                        SyncInfo = $"Initializing part {i}/16 tab...";
                        Log.Information($"Creating view model for tab part {i}.");
                    }

                    bool commonTab = i == 0;
                    PartViewModel vm = new PartViewModel(this, commonTab ? (byte)255 : (byte)(i - 1),
                        _i7startAddresses, _i7parameters, Integra7,
                        _integra7Communicator, _semaphore, presets, commonTab);
                    await vm.InitializeParameterSourceCachesAsync();
                    pvm.Add(vm);
                }

                _partViewModels = new ReadOnlyObservableCollection<PartViewModel>(pvm);
                this.RaisePropertyChanged(nameof(PartViewModels));
            }
            else
            {
                Log.Information("Failed to connect to Integra7");
                MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
            }

            RescanButtonEnabled = !_connected;
        }
        finally
        {
            SignalStopSync();
        }
    }

    private async Task AddUserDefinedPresets(List<Integra7Preset> presets)
    {
        SyncInfo = "Loading PCM Drum Kit User Names 0-31...";
        List<string> names = await Integra7?.GetPCMDrumKitUserNames0to31();
        int pc = 0;
        int id = presets.Count;
        foreach (string n in names)
        {
            int msb = 86;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "PCMD", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Drums" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading PCM Synth Tone User Names 0-63...";
        names = await Integra7?.GetPCMToneUserNames0to63();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 87;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "PCMS", "PRST" /* todo incorrect */, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading PCM Synth Tone User Names 64-127...";
        names = await Integra7?.GetPCMToneUserNames64to127();
        foreach (string n in names)
        {
            int msb = 87;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "PCMS", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading PCM Synth Tone User Names 128-191...";
        names = await Integra7?.GetPCMToneUserNames128to191();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 87;
            int lsb = 1;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "PCMS", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading PCM Synth Tone User Names 192-255...";
        names = await Integra7?.GetPCMToneUserNames192to255();
        foreach (string n in names)
        {
            int msb = 87;
            int lsb = 1;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "PCMS", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Drum Kit User Names 0-63...";
        names = await Integra7?.GetSuperNATURALDrumKitUserNames0to63();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 88;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-D", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Drums" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Acoustic Tone User Names 0-63...";
        names = await Integra7?.GetSuperNATURALAcousticToneUserNames0to63();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 89;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-A", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Ac.Piano" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Acoustic Tone User Names 64-127...";
        names = await Integra7?.GetSuperNATURALAcousticToneUserNames64to127();
        foreach (string n in names)
        {
            int msb = 89;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-A", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Ac.Piano" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Acoustic Tone User Names 128-191...";
        names = await Integra7?.GetSuperNATURALAcousticToneUserNames128to191();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 89;
            int lsb = 1;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-A", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Ac.Piano" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Acoustic Tone User Names 192-255...";
        names = await Integra7?.GetSuperNATURALAcousticToneUserNames192to255();
        foreach (string n in names)
        {
            int msb = 89;
            int lsb = 1;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-A", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Ac.Piano" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 0-63...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames0to63();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 64-127...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames64to127();
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 0;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 128-191...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames128to191();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 1;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 192-255...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames192to255();
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 1;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 256-319...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames256to319();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 2;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 320-383...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames320to383();
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 2;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 384-447...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames384to447();
        pc = 0;
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 3;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }

        SyncInfo = "Loading SuperNATURAL Synth Tone User Names 448-511...";
        names = await Integra7?.GetSuperNATURALSynthToneUserNames448to511();
        foreach (string n in names)
        {
            int msb = 95;
            int lsb = 3;
            pc++;
            presets.Add(new Integra7Preset(id, "USR", "SN-S", "PRST" /*todo incorrect*/, pc,
                n, msb, lsb, pc, "Synth Lead" /*todo incorrect*/));
            id++;
        }
    }

    private int _currentPartSelection = 0;
    public int CurrentPartSelection
    {
        get => _currentPartSelection;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentPartSelection, value);
            this.RaisePropertyChanged(nameof(CurrentPartIsNotCommonPart));
        }
    }

    private List<Integra7Preset> LoadPresets()
    {
        var uri = @"avares://" + "Integra7AuralAlchemist/" + "Assets/Presets.csv";
        var file = new StreamReader(AssetLoader.Open(new Uri(uri)));
        var data = file.ReadLine();
        char[] separators = [','];
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
            Presets.Add(new Integra7Preset(id, "INT", tonetype, tonebank, number, name, msb, lsb, pc, category));
            id++;
        }

        return Presets;
    }
    
    public MainWindowViewModel()
    {
        MessageBus.Current.Listen<UpdateMessageSpec>("ui2hw").Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .Subscribe(async m => await UpdateIntegraFromUiAsync(m));
        MessageBus.Current.Listen<UpdateFromSysexSpec>("hw2ui").Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .Subscribe(async m => await UpdateUiFromIntegraAsync(m));
        MessageBus.Current.Listen<UpdateResyncPart>().Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .Subscribe(async m => await ResyncPartAsync(m.PartNo));
        MessageBus.Current.Listen<UpdateSetPresetAndResyncPart>()
            .Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .Subscribe(async m => await SetPresetAndResyncPartAsync(m.PartNo));
        
        ShowSaveUserToneDialog = new Interaction<SaveUserToneViewModel, UserToneToSave?>();
    }

    public async Task InitializeAsync()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING),
            _semaphore);
        await Integra7.CheckIdentityAsync();
        List<Integra7Preset> presets = LoadPresets();
        await UpdateConnectedAsync(Integra7, presets);
    }

    private async Task UpdateIntegraFromUiAsync(UpdateMessageSpec s)
    {
        FullyQualifiedParameter p = s.Par;
        p.StringValue = s.DisplayValue;
        await _integra7Communicator?.WriteSingleParameterToIntegraAsync(p);
        if (p.ParSpec.IsParent)
        {
            await _integra7Communicator?.GetDomain(p).ReadFromIntegraAsync();
            ForceUiRefresh(p);
        }
    }

    private async Task UpdateUiFromIntegraAsync(UpdateFromSysexSpec s)
    {
        List<UpdateMessageSpec> parameters =
            SysexDataTransmissionParser.ConvertSysexToParameterUpdates(s.SysexMsg, _integra7Communicator);
        bool ParentControlModified = parameters.Any(spec => spec.Par.ParSpec.IsParent);
        bool PresetChanged = parameters.Any(spec =>
            spec.Par.ParSpec.Path.Contains("Tone Bank Select") ||
            spec.Par.ParSpec.Path.Contains("Tone Bank Program Number"));
        bool HighImpactControlChanged = ParentControlModified || PresetChanged;
        if (!HighImpactControlChanged)
        {
            // update only the affected parameters
            foreach (UpdateMessageSpec spec in parameters)
            {
                _integra7Communicator?.GetDomain(spec.Par)
                    .ModifySingleParameterDisplayedValue(spec.Par.ParSpec.Path, spec.DisplayValue);
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
                if (alreadyEncountered.Add(domainName))
                {
                    await _integra7Communicator?.GetDomain(spec.Par).ReadFromIntegraAsync();
                    ForceUiRefresh(spec.Par);
                }
            }
        }
    }

    private void ForceUiRefresh(FullyQualifiedParameter p)
    {
        ForceUiRefresh(p.Start, p.Offset, p.Offset2, p.ParSpec.Path, p.ParSpec.IsParent);
    }

    private void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string Offset2AddressName,
        string ParPath, bool ResyncNeeded)
    {
        if (_partViewModels != null)
        {
            foreach (PartViewModel pvm in _partViewModels)
            {
                pvm.ForceUiRefresh(StartAddressName, OffsetAddressName, Offset2AddressName, ParPath, ResyncNeeded);
            }
        }
    }

    private async Task ResyncPartAsync(byte part)
    {
        try
        {
            SignalStartSync();
            if (_partViewModels != null)
            {
                int i = 0;
                foreach (PartViewModel pvm in _partViewModels)
                {
                    SyncInfo = $"Resync part {i}";
                    i++;
                    await pvm.EnsurePreselectIsNotNullAsync();
                    await pvm.ResyncPartAsync(part);
                }
            }
        }
        finally
        {
            SignalStopSync();
        }
    }

    private async Task SetPresetAndResyncPartAsync(byte part)
    {
        try
        {
            SignalStartSync();
            if (_partViewModels != null)
            {
                foreach (PartViewModel pvm in _partViewModels)
                {
                    if (part == pvm.PartNo)
                    {
                        SyncInfo = $"Resync part {pvm.PartNo}";
                        DomainBase b = _integra7Communicator.StudioSetPart(part);
                        await b.ReadFromIntegraAsync();
                        pvm.PreSelectConfiguredPreset(b);
                        await pvm.ResyncPartAsync(part);
                    }
                }
            }
        }
        finally
        {
            SignalStopSync();
        }
    }

#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS8618 // nullable must be assigned in constructor
}