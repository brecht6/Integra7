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

namespace Integra7AuralAlchemist.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS8618 // Non-nullable field 'xxx' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the field as nullable.
    const int THROTTLE = 250;
    private Integra7StartAddresses _i7startAddresses = new();
    private Integra7Parameters _i7parameters = new();

    [Reactive]
    private bool _rescanButtonEnabled = true;
    private Integra7Domain? _integra7Communicator = null;

    private readonly ReadOnlyObservableCollection<PartViewModel> _partViewModels;
    public ReadOnlyObservableCollection<PartViewModel> PartViewModels => _partViewModels;
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
        if (_currentPartSelection > 0 && _currentPartSelection < 17)
        {
            zeroBasedMidiChannel = (byte)(_currentPartSelection - 1);
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

            if (_partViewModels is not null)
            {
                foreach (PartViewModel pvm in _partViewModels)
                {
                    pvm.InitializeParameterSourceCaches();
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
        for (byte i = 0; i < 17; i++)
        {
            bool commonTab = i == 0;
            pvm.Add(new PartViewModel(this, commonTab ? (byte)255 : (byte)(i - 1),
                                        _i7startAddresses, _i7parameters, Integra7,
                                        _integra7Communicator, presets, commonTab));
        }
        _partViewModels = new ReadOnlyObservableCollection<PartViewModel>(pvm);

        foreach (PartViewModel pa in _partViewModels)
        {
            pa.InitializeParameterSourceCaches();
        }

        MessageBus.Current.Listen<UpdateMessageSpec>("ui2hw").Throttle(TimeSpan.FromMilliseconds(THROTTLE)).Subscribe(m => UpdateIntegraFromUi(m));
        MessageBus.Current.Listen<UpdateFromSysexSpec>("hw2ui").Throttle(TimeSpan.FromMilliseconds(THROTTLE)).Subscribe(m => UpdateUiFromIntegra(m));
        MessageBus.Current.Listen<UpdateResyncPart>().Throttle(TimeSpan.FromMilliseconds(THROTTLE)).Subscribe(m => ResyncPart(m.PartNo));
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
        bool PresetChanged = parameters.Any(spec => spec.Par.ParSpec.Path.Contains("Tone Bank Select") || spec.Par.ParSpec.Path.Contains("Tone Bank Program Number"));
        bool HighImpactControlChanged = ParentControlModified || PresetChanged;
        if (!HighImpactControlChanged)
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
        ForceUiRefresh(p.Start, p.Offset, p.ParSpec.Path, p.ParSpec.IsParent);
    }

    private void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string ParPath, bool ResyncNeeded)
    {
        foreach (PartViewModel pvm in _partViewModels)
        {
            pvm.ForceUiRefresh(StartAddressName, OffsetAddressName, ParPath, ResyncNeeded);
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

