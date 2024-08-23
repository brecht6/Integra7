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


public partial class MainWindowViewModel : ReactiveObject
{
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS8618 // Non-nullable field 'xxx' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the field as nullable.
    private Integra7StartAddresses _i7startAddresses = new();
    private Integra7Parameters _i7parameters = new();
    private Integra7Preset _selectedPreset0;
    private Integra7Preset _selectedPreset1;
    private Integra7Preset _selectedPreset2;
    private Integra7Preset _selectedPreset3;
    private Integra7Preset _selectedPreset4;
    private Integra7Preset _selectedPreset5;
    private Integra7Preset _selectedPreset6;
    private Integra7Preset _selectedPreset7;
    private Integra7Preset _selectedPreset8;
    private Integra7Preset _selectedPreset9;
    private Integra7Preset _selectedPreset10;
    private Integra7Preset _selectedPreset11;
    private Integra7Preset _selectedPreset12;
    private Integra7Preset _selectedPreset13;
    private Integra7Preset _selectedPreset14;
    private Integra7Preset _selectedPreset15;
    public Integra7Preset SelectedPresetCh0
    {
        get => _selectedPreset0;
        set
        {
            _selectedPreset0 = value;
            ChangePreset(0);
        }
    }
    public Integra7Preset SelectedPresetCh1
    {
        get => _selectedPreset1;
        set
        {
            _selectedPreset1 = value;
            ChangePreset(1);
        }
    }
    public Integra7Preset SelectedPresetCh2
    {
        get => _selectedPreset2;
        set
        {
            _selectedPreset2 = value;
            ChangePreset(2);
        }
    }
    public Integra7Preset SelectedPresetCh3
    {
        get => _selectedPreset3;
        set
        {
            _selectedPreset3 = value;
            ChangePreset(3);
        }
    }
    public Integra7Preset SelectedPresetCh4
    {
        get => _selectedPreset4;
        set
        {
            _selectedPreset4 = value;
            ChangePreset(4);
        }
    }
    public Integra7Preset SelectedPresetCh5
    {
        get => _selectedPreset5;
        set
        {
            _selectedPreset5 = value;
            ChangePreset(5);
        }
    }
    public Integra7Preset SelectedPresetCh6
    {
        get => _selectedPreset6;
        set
        {
            _selectedPreset6 = value;
            ChangePreset(6);
        }
    }
    public Integra7Preset SelectedPresetCh7
    {
        get => _selectedPreset7;
        set
        {
            _selectedPreset7 = value;
            ChangePreset(7);
        }
    }
    public Integra7Preset SelectedPresetCh8
    {
        get => _selectedPreset8;
        set
        {
            _selectedPreset8 = value;
            ChangePreset(8);
        }
    }
    public Integra7Preset SelectedPresetCh9
    {
        get => _selectedPreset9;
        set
        {
            _selectedPreset9 = value;
            ChangePreset(9);
        }
    }
    public Integra7Preset SelectedPresetCh10
    {
        get => _selectedPreset10;
        set
        {
            _selectedPreset10 = value;
            ChangePreset(10);
        }
    }
    public Integra7Preset SelectedPresetCh11
    {
        get => _selectedPreset11;
        set
        {
            _selectedPreset11 = value;
            ChangePreset(11);
        }
    }
    public Integra7Preset SelectedPresetCh12
    {
        get => _selectedPreset12;
        set
        {
            _selectedPreset12 = value;
            ChangePreset(12);
        }
    }
    public Integra7Preset SelectedPresetCh13
    {
        get => _selectedPreset13;
        set
        {
            _selectedPreset13 = value;
            ChangePreset(13);
        }
    }
    public Integra7Preset SelectedPresetCh14
    {
        get => _selectedPreset14;
        set
        {
            _selectedPreset14 = value;
            ChangePreset(14);
        }
    }
    public Integra7Preset SelectedPresetCh15
    {
        get => _selectedPreset15;
        set
        {
            _selectedPreset15 = value;
            ChangePreset(15);
        }
    }

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
    private string _searchTextStudioSetMidi0;
    [Reactive]
    private string _searchTextStudioSetMidi1;
    [Reactive]
    private string _searchTextStudioSetMidi2;
    [Reactive]
    private string _searchTextStudioSetMidi3;
    [Reactive]
    private string _searchTextStudioSetMidi4;
    [Reactive]
    private string _searchTextStudioSetMidi5;
    [Reactive]
    private string _searchTextStudioSetMidi6;
    [Reactive]
    private string _searchTextStudioSetMidi7;
    [Reactive]
    private string _searchTextStudioSetMidi8;
    [Reactive]
    private string _searchTextStudioSetMidi9;
    [Reactive]
    private string _searchTextStudioSetMidi10;
    [Reactive]
    private string _searchTextStudioSetMidi11;
    [Reactive]
    private string _searchTextStudioSetMidi12;
    [Reactive]
    private string _searchTextStudioSetMidi13;
    [Reactive]
    private string _searchTextStudioSetMidi14;
    [Reactive]
    private string _searchTextStudioSetMidi15;


    Func<FullyQualifiedParameter, bool> _parameterFilter(string text) => par =>
    {
        bool ReturnValue = !par.ParSpec.Reserved && (string.IsNullOrEmpty(text) || par.ParSpec.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase));
        return ReturnValue;
    };
    Func<FullyQualifiedParameter, bool> _allpassFilter(bool dummy) => par =>
    {
        return true;
    };

    [Reactive]
    private bool _rescanButtonEnabled = true;

    Integra7Preset GetSelectedPreset(byte Channel)
    {
        switch (Channel)
        {
            case 0:
                return SelectedPresetCh0;
            case 1:
                return SelectedPresetCh1;
            case 2:
                return SelectedPresetCh2;
            case 3:
                return SelectedPresetCh3;
            case 4:
                return SelectedPresetCh4;
            case 5:
                return SelectedPresetCh5;
            case 6:
                return SelectedPresetCh6;
            case 7:
                return SelectedPresetCh7;
            case 8:
                return SelectedPresetCh8;
            case 9:
                return SelectedPresetCh9;
            case 10:
                return SelectedPresetCh10;
            case 11:
                return SelectedPresetCh11;
            case 12:
                return SelectedPresetCh12;
            case 13:
                return SelectedPresetCh13;
            case 14:
                return SelectedPresetCh14;
            case 15:
                return SelectedPresetCh15;
            default:
                return SelectedPresetCh0;
        }
    }

    void SetSelectedPreset(byte Channel, Integra7Preset SelectedPreset)
    {
        switch (Channel)
        {
            case 0:
                SelectedPresetCh0 = SelectedPreset;
                break;
            case 1:
                SelectedPresetCh1 = SelectedPreset;
                break;
            case 2:
                SelectedPresetCh2 = SelectedPreset;
                break;
            case 3:
                SelectedPresetCh3 = SelectedPreset;
                break;
            case 4:
                SelectedPresetCh4 = SelectedPreset;
                break;
            case 5:
                SelectedPresetCh5 = SelectedPreset;
                break;
            case 6:
                SelectedPresetCh6 = SelectedPreset;
                break;
            case 7:
                SelectedPresetCh7 = SelectedPreset;
                break;
            case 8:
                SelectedPresetCh8 = SelectedPreset;
                break;
            case 9:
                SelectedPresetCh9 = SelectedPreset;
                break;
            case 10:
                SelectedPresetCh10 = SelectedPreset;
                break;
            case 11:
                SelectedPresetCh11 = SelectedPreset;
                break;
            case 12:
                SelectedPresetCh12 = SelectedPreset;
                break;
            case 13:
                SelectedPresetCh13 = SelectedPreset;
                break;
            case 14:
                SelectedPresetCh14 = SelectedPreset;
                break;
            case 15:
                SelectedPresetCh15 = SelectedPreset;
                break;
        }
    }

    private SourceCache<Integra7Preset, int> _sourceCacheCh0 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh0;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh0 => _presetsCh0;
    private SourceCache<Integra7Preset, int> _sourceCacheCh1 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh1;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh1 => _presetsCh1;
    private SourceCache<Integra7Preset, int> _sourceCacheCh2 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh2;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh2 => _presetsCh2;
    private SourceCache<Integra7Preset, int> _sourceCacheCh3 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh3;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh3 => _presetsCh3;
    private SourceCache<Integra7Preset, int> _sourceCacheCh4 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh4;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh4 => _presetsCh4;
    private SourceCache<Integra7Preset, int> _sourceCacheCh5 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh5;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh5 => _presetsCh5;
    private SourceCache<Integra7Preset, int> _sourceCacheCh6 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh6;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh6 => _presetsCh6;
    private SourceCache<Integra7Preset, int> _sourceCacheCh7 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh7;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh7 => _presetsCh7;
    private SourceCache<Integra7Preset, int> _sourceCacheCh8 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh8;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh8 => _presetsCh8;
    private SourceCache<Integra7Preset, int> _sourceCacheCh9 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh9;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh9 => _presetsCh9;
    private SourceCache<Integra7Preset, int> _sourceCacheCh10 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh10;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh10 => _presetsCh10;
    private SourceCache<Integra7Preset, int> _sourceCacheCh11 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh11;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh11 => _presetsCh11;
    private SourceCache<Integra7Preset, int> _sourceCacheCh12 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh12;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh12 => _presetsCh12;
    private SourceCache<Integra7Preset, int> _sourceCacheCh13 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh13;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh13 => _presetsCh13;
    private SourceCache<Integra7Preset, int> _sourceCacheCh14 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh14;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh14 => _presetsCh14;
    private SourceCache<Integra7Preset, int> _sourceCacheCh15 = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presetsCh15;
    public ReadOnlyObservableCollection<Integra7Preset> PresetsCh15 => _presetsCh15;

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

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters0 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters0;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters0 => _studioSetMidiParameters0;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters1 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters1;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters1 => _studioSetMidiParameters1;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters2 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters2;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters2 => _studioSetMidiParameters2;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters3 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters3;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters3 => _studioSetMidiParameters3;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters4 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters4;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters4 => _studioSetMidiParameters4;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters5 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters5;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters5 => _studioSetMidiParameters5;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters6 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters6;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters6 => _studioSetMidiParameters6;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters7 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters7;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters7 => _studioSetMidiParameters7;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters8 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters8;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters8 => _studioSetMidiParameters8;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters9 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters9;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters9 => _studioSetMidiParameters9;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters10 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters10;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters10 => _studioSetMidiParameters10;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters11 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters11;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters11 => _studioSetMidiParameters11;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters12 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters12;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters12 => _studioSetMidiParameters12;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters13 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters13;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters13 => _studioSetMidiParameters13;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters14 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters14;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters14 => _studioSetMidiParameters14;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetMidiParameters15 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetMidiParameters15;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetMidiParameters15 => _studioSetMidiParameters15;

    private SourceCache<Integra7Preset, int> GetSourceCache(byte Channel)
    {
        switch (Channel)
        {
            case 0: return _sourceCacheCh0;
            case 1: return _sourceCacheCh1;
            case 2: return _sourceCacheCh2;
            case 3: return _sourceCacheCh3;
            case 4: return _sourceCacheCh4;
            case 5: return _sourceCacheCh5;
            case 6: return _sourceCacheCh6;
            case 7: return _sourceCacheCh7;
            case 8: return _sourceCacheCh8;
            case 9: return _sourceCacheCh9;
            case 10: return _sourceCacheCh10;
            case 11: return _sourceCacheCh11;
            case 12: return _sourceCacheCh12;
            case 13: return _sourceCacheCh13;
            case 14: return _sourceCacheCh14;
            case 15: return _sourceCacheCh15;
            default:
                return _sourceCacheCh0;
        }
    }

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
    public void ChangePreset(byte MidiChannel)
    {
        Integra7Preset CurrentSelection = GetSelectedPreset(MidiChannel);
        if (CurrentSelection != null)
        {
            Integra7?.ChangePreset(MidiChannel, CurrentSelection.Msb, CurrentSelection.Lsb, CurrentSelection.Pc);
        }
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

            _integra7Communicator.StudioSetMidi(0).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid0 = _integra7Communicator.StudioSetMidi(0).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters0.AddOrUpdate(p_mid0);
            _integra7Communicator.StudioSetMidi(1).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid1 = _integra7Communicator.StudioSetMidi(1).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters1.AddOrUpdate(p_mid1);
            _integra7Communicator.StudioSetMidi(2).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid2 = _integra7Communicator.StudioSetMidi(2).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters2.AddOrUpdate(p_mid2);
            _integra7Communicator.StudioSetMidi(3).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid3 = _integra7Communicator.StudioSetMidi(3).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters3.AddOrUpdate(p_mid3);
            _integra7Communicator.StudioSetMidi(4).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid4 = _integra7Communicator.StudioSetMidi(4).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters4.AddOrUpdate(p_mid4);
            _integra7Communicator.StudioSetMidi(5).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid5 = _integra7Communicator.StudioSetMidi(5).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters5.AddOrUpdate(p_mid5);
            _integra7Communicator.StudioSetMidi(6).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid6 = _integra7Communicator.StudioSetMidi(6).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters6.AddOrUpdate(p_mid6);
            _integra7Communicator.StudioSetMidi(7).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid7 = _integra7Communicator.StudioSetMidi(7).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters7.AddOrUpdate(p_mid7);
            _integra7Communicator.StudioSetMidi(8).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid8 = _integra7Communicator.StudioSetMidi(8).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters8.AddOrUpdate(p_mid8);
            _integra7Communicator.StudioSetMidi(9).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid9 = _integra7Communicator.StudioSetMidi(9).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters9.AddOrUpdate(p_mid9);
            _integra7Communicator.StudioSetMidi(10).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid10 = _integra7Communicator.StudioSetMidi(10).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters10.AddOrUpdate(p_mid10);
            _integra7Communicator.StudioSetMidi(11).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid11 = _integra7Communicator.StudioSetMidi(11).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters11.AddOrUpdate(p_mid11);
            _integra7Communicator.StudioSetMidi(12).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid12 = _integra7Communicator.StudioSetMidi(12).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters12.AddOrUpdate(p_mid12);
            _integra7Communicator.StudioSetMidi(13).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid13 = _integra7Communicator.StudioSetMidi(13).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters13.AddOrUpdate(p_mid13);
            _integra7Communicator.StudioSetMidi(14).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid14 = _integra7Communicator.StudioSetMidi(14).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters14.AddOrUpdate(p_mid14);
            _integra7Communicator.StudioSetMidi(15).ReadFromIntegra();
            List<FullyQualifiedParameter> p_mid15 = _integra7Communicator.StudioSetMidi(15).GetRelevantParameters(true, true);
            _sourceCacheStudioSetMidiParameters15.AddOrUpdate(p_mid15);
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

    private async void LoadPresets()
    {
        var uri = @"avares://" + "Integra7AuralAlchemist/" + "Assets/Presets.csv";
        var file = new StreamReader(AssetLoader.Open(new Uri(uri)));
        var data = file.ReadLine();
        char[] separators = { ',' };
        int id = 0;
        while ((data = await file.ReadLineAsync()) != null)
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
                GetSourceCache(ch).AddOrUpdate(new Integra7Preset(id, tonetype, tonebank, number, name, msb, lsb, pc, category));
            }
            id++;
        }
    }

    public MainWindowViewModel()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING));
        UpdateConnected(Integra7);

        LoadPresets();
        for (byte i = 0; i < 16; ++i)
        {
            SetSelectedPreset(i, GetSourceCache(i).Items.First());
        }
        const int THROTTLE = 250;
        var parFilterSetup = this.WhenAnyValue(x => x.SearchTextSetup)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var parFilterSystem = this.WhenAnyValue(x => x.SearchSystem)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var parFilterStudioSetCommon = this.WhenAnyValue(x => x.SearchTextStudioSetCommon)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var parFilterStudioSetCommonChorus = this.WhenAnyValue(x => x.SearchTextStudioSetCommonChorus)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var refreshCommonChorus = this.WhenAnyValue(x => x.RefreshCommonChorusNeeded)
                                            .Select(_parameterFilter);

        var parFilterStudioSetCommonReverb = this.WhenAnyValue(x => x.SearchTextStudioSetCommonReverb)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var refreshCommonReverb = this.WhenAnyValue(x => x.RefreshCommonReverbNeeded)
                                            .Select(_parameterFilter);

        var parFilterStudioSetCommonMotionalSurroundParameters = this.WhenAnyValue(x => x.SearchTextStudioSetCommonMotionalSurround)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var refreshCommonMotionalSurround = this.WhenAnyValue(x => x.RefreshCommonMotionalSurroundNeeded)
                                            .Select(_parameterFilter);

        var parFilterStudioSetCommonMasterEQParameters = this.WhenAnyValue(x => x.SearchTextStudioSetCommonMasterEQ)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        var refreshCommonMasterEQ = this.WhenAnyValue(x => x.RefreshCommonMasterEQNeeded)
                                            .Select(_parameterFilter);

        var parFilterStudioSetMidiParameters0 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi0)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters1 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi1)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters2 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi2)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters3 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi3)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters4 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi4)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters5 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi5)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters6 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi6)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters7 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi7)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters8 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi8)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters9 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi9)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters10 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi10)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters11 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi11)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters12 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi12)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters13 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi13)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters14 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi14)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetMidiParameters15 = this.WhenAnyValue(x => x.SearchTextStudioSetMidi15)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);

        _cleanUp[0] = _sourceCacheCh0.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh0)
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[1] = _sourceCacheCh1.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh1)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[2] = _sourceCacheCh2.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh2)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[3] = _sourceCacheCh3.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh3)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[4] = _sourceCacheCh4.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh4)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[5] = _sourceCacheCh5.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh5)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[6] = _sourceCacheCh6.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh6)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[7] = _sourceCacheCh7.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh7)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[8] = _sourceCacheCh8.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh8)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[9] = _sourceCacheCh9.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh9)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[10] = _sourceCacheCh10.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh10)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[11] = _sourceCacheCh11.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh11)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[12] = _sourceCacheCh12.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh12)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[13] = _sourceCacheCh13.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh13)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[14] = _sourceCacheCh14.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh14)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[15] = _sourceCacheCh15.Connect()
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .Bind(out _presetsCh15)
                                    .DisposeMany()
                                    .Subscribe();

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

        _cleanUp[22] = _sourceCacheStudioSetMidiParameters0.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters0)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters0,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[23] = _sourceCacheStudioSetMidiParameters1.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters1)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters1,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[24] = _sourceCacheStudioSetMidiParameters2.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters2)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters2,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[25] = _sourceCacheStudioSetMidiParameters3.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters3)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters3,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[26] = _sourceCacheStudioSetMidiParameters4.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters4)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters4,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[27] = _sourceCacheStudioSetMidiParameters5.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters5)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters5,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[28] = _sourceCacheStudioSetMidiParameters6.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters6)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters6,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[29] = _sourceCacheStudioSetMidiParameters7.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters7)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters7,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[30] = _sourceCacheStudioSetMidiParameters8.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters8)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters8,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[31] = _sourceCacheStudioSetMidiParameters9.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters9)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters9,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[32] = _sourceCacheStudioSetMidiParameters10.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters10)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters10,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[33] = _sourceCacheStudioSetMidiParameters11.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters11)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters11,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[34] = _sourceCacheStudioSetMidiParameters12.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters12)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters12,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[35] = _sourceCacheStudioSetMidiParameters13.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters13)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters13,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[36] = _sourceCacheStudioSetMidiParameters14.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters14)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters14,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[37] = _sourceCacheStudioSetMidiParameters15.Connect()
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetMidiParameters15)
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetMidiParameters15,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();


        MessageBus.Current.Listen<UpdateMessageSpec>("ui2hw").Throttle(TimeSpan.FromMilliseconds(250)).Subscribe(m => UpdateIntegraFromUi(m));
        MessageBus.Current.Listen<UpdateFromSysexSpec>("hw2ui").Throttle(TimeSpan.FromMilliseconds(250)).Subscribe(m => UpdateUiFromIntegra(m));
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
        if (p.Offset == "Offset/Studio Set Common Chorus")
        {
            // force re-evaluation of the dynamic data filters after the parameters were read from integra-7
            // this feels like a very ugly hack, but i currently do not know how to do it properly
            // i tried tons of other stuff (like: "this.RaisePropertyChanged(nameof(RefreshCommonChorusNeeded))"), but nothing seems to work
            // ... shiver ...
            RefreshCommonChorusNeeded = "."; // RefreshCommonChorusNeeded must not have any .Throttle clauses
            RefreshCommonChorusNeeded = SearchTextStudioSetCommonChorus;
        }

        if (p.Offset == "Offset/Studio Set Common Reverb")
        {
            RefreshCommonReverbNeeded = ".";
            RefreshCommonReverbNeeded = SearchTextStudioSetCommonReverb;
        }
    }

#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS8618 // nullable must be assigned in constructor
}

