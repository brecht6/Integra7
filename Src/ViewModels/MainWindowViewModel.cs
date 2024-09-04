using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.Platform;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Domain;
using Integra7AuralAlchemist.Models.Services;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Serilog;

namespace Integra7AuralAlchemist.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS8618 // Non-nullable field 'xxx' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the field as nullable.
    private Integra7StartAddresses _i7startAddresses = new();
    private Integra7Parameters _i7parameters = new();

    [Reactive]
    private bool _rescanButtonEnabled = true;
    private Integra7Domain? _integra7Communicator;

    private ReadOnlyObservableCollection<PartViewModel> _partViewModels;
    public ReadOnlyObservableCollection<PartViewModel> PartViewModels => _partViewModels;
    private const string INTEGRA_CONNECTION_STRING = "INTEGRA-7";
    private IIntegra7Api? Integra7 { get; set; }

    [Reactive]
    private bool _connected;

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
    public void PlayPhrase()
    {
        byte zeroBasedMidiChannel = 0;
        if (_currentPartSelection > 0 && _currentPartSelection < 17)
        {
            zeroBasedMidiChannel = (byte)(_currentPartSelection - 1);
        }
        Integra7?.SendStopPreviewPhraseMsg();
        Integra7?.SendPlayPreviewPhraseMsg(zeroBasedMidiChannel);
    }

    [ReactiveCommand]
    public void StopPhrase()
    {
        Integra7?.SendStopPreviewPhraseMsg();
    }

    [ReactiveCommand]
    public void Panic()
    {
        Integra7?.AllNotesOff();
        Integra7?.SendStopPreviewPhraseMsg();
    }

    [ReactiveCommand]
    public void RescanMidiDevices()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING));
        List<Integra7Preset> presets = LoadPresets();
        UpdateConnected(Integra7, presets);
    }
    private void UpdateConnected(IIntegra7Api integra7Api, List<Integra7Preset> presets)
    {
        Connected = integra7Api.ConnectionOk();
        if (_connected)
        {
            Log.Information("Connected to Integra7");
            MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING + " with device id " + integra7Api.DeviceId().ToString("x2");
            _integra7Communicator = new Integra7Domain(integra7Api, _i7startAddresses, _i7parameters);
            
            ObservableCollection<PartViewModel> pvm = [];
            for (byte i = 0; i < 17; i++)
            {
                if (i == 0)
                {
                    Log.Information("Creating view model for common tab.");
                }
                else
                {
                    Log.Information($"Creating view model for tab part {i}.");   
                }
                bool commonTab = i == 0;
                pvm.Add(new PartViewModel(this, commonTab ? (byte)255 : (byte)(i - 1),
                    _i7startAddresses, _i7parameters, Integra7,
                    _integra7Communicator, presets, commonTab));
            }
            _partViewModels = new ReadOnlyObservableCollection<PartViewModel>(pvm);
        }
        else
        {
            Log.Information("Failed to connect to Integra7");
            MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
        }
        RescanButtonEnabled = !_connected;
    }

    [Reactive]
    private int _currentPartSelection;

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
            Presets.Add(new Integra7Preset(id, tonetype, tonebank, number, name, msb, lsb, pc, category));
            id++;
        }
        return Presets;
    }

    public MainWindowViewModel()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING));
        List<Integra7Preset> presets = LoadPresets();
        UpdateConnected(Integra7, presets);
        
        MessageBus.Current.Listen<UpdateMessageSpec>("ui2hw").Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE)).Subscribe(m => UpdateIntegraFromUi(m));
        MessageBus.Current.Listen<UpdateFromSysexSpec>("hw2ui").Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE)).Subscribe(m => UpdateUiFromIntegra(m));
        MessageBus.Current.Listen<UpdateResyncPart>().Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE)).Subscribe(m => ResyncPart(m.PartNo));
        MessageBus.Current.Listen<UpdateSetPresetAndResyncPart>().Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE)).Subscribe(m => SetPresetAndResyncPart(m.PartNo));
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
        ForceUiRefresh(p.Start, p.Offset, p.Offset2, p.ParSpec.Path, p.ParSpec.IsParent);
    }

    private void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string Offset2AddressName, string ParPath, bool ResyncNeeded)
    {
        if (_partViewModels != null)
        {
            foreach (PartViewModel pvm in _partViewModels)
            {
                pvm.ForceUiRefresh(StartAddressName, OffsetAddressName, Offset2AddressName, ParPath, ResyncNeeded);
            }
        } 
    }

    public void ResyncPart(byte part)
    {
        if (_partViewModels != null)
        {
            foreach (PartViewModel pvm in _partViewModels)
            {
                pvm.EnsurePreselectIsNotNull();
                pvm.ResyncPart(part);
            }   
        }
    }

    public void SetPresetAndResyncPart(byte part)
    {
        
        if (_partViewModels != null)
        {
            foreach (PartViewModel pvm in _partViewModels)
            {
                if (part == pvm.PartNo)
                {
                    DomainBase b = _integra7Communicator.StudioSetPart(part);
                    b.ReadFromIntegra();
                    pvm.PreSelectConfiguredPreset(b);
                    pvm.ResyncPart(part);
  
                } 
            }   
        }
    }

#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS8618 // nullable must be assigned in constructor
}

