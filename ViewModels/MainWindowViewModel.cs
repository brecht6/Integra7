using ReactiveUI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System;
using System.Threading;
using System.Collections.ObjectModel;
using Avalonia.Platform;
using System.IO;
using Avalonia;
using DynamicData;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Services;
using Avalonia.Controls;
using System.Diagnostics;
using System.Collections.Generic;
using Avalonia.Collections;

namespace Integra7AuralAlchemist.ViewModels;


public partial class MainWindowViewModel : ObservableObject
{
#pragma warning disable CA1822 // Mark members as static
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
                return null;
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

    private ReadOnlyObservableCollection<Integra7Preset> GetPresetsCollection(byte Channel)
    {
        switch (Channel)
        {
            case 0:
                return _presetsCh0;
            case 1:
                return _presetsCh1;
            case 2:
                return _presetsCh2;
            case 3:
                return _presetsCh3;
            case 4:
                return _presetsCh4;
            case 5:
                return _presetsCh5;
            case 6:
                return _presetsCh6;
            case 7:
                return _presetsCh7;
            case 8:
                return _presetsCh8;
            case 9:
                return _presetsCh9;
            case 10:
                return _presetsCh10;
            case 11:
                return _presetsCh11;
            case 12:
                return _presetsCh12;
            case 13:
                return _presetsCh13;
            case 14:
                return _presetsCh14;
            case 15:
                return _presetsCh15;
            default:
                return _presetsCh0;
        }
    }

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

    [ObservableProperty]
    private bool connected = false;

    [ObservableProperty]
    private string midiDevices = "No Midi Devices Detected";

    [RelayCommand]
    private void PlayNote()
    {
        byte currentTab = (byte)CurrentPartSelection;
        Integra7?.NoteOn(currentTab, 65, 100);
        Thread.Sleep(1000);
        Integra7?.NoteOff(currentTab, 65);
    }

    [RelayCommand]
    private void ChangePreset(byte MidiChannel)
    {
        Integra7Preset CurrentSelection = GetSelectedPreset(MidiChannel);
        if (CurrentSelection != null)
        {
            Integra7?.ChangePreset(MidiChannel, CurrentSelection.Msb, CurrentSelection.Lsb, CurrentSelection.Pc);
        }
    }

    [RelayCommand]
    private void RescanMidiDevices()
    {
        Integra7 = new Integra7Api(new MidiOut(INTEGRA_CONNECTION_STRING), new MidiIn(INTEGRA_CONNECTION_STRING));
        Connected = Integra7.ConnectionOk();
        if (Connected)
        {
            MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING;
        }
        else
        {
            MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
        }
    }

    [ObservableProperty]
    private int currentPartSelection = 0;
    

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
        Connected = Integra7.ConnectionOk();
        if (Connected)
        {
            MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING;
        }
        else
        {
            MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
        }

        LoadPresets();
        for (byte i = 0; i < 16; ++i)
        {
            SetSelectedPreset(i, GetSourceCache(i).Items.First());
        }
        _cleanUp[0] = _sourceCacheCh0.Connect()
                                .Bind(out _presetsCh0)
                                .DisposeMany()
                                .Subscribe();

        _cleanUp[1] = _sourceCacheCh1.Connect()
                            .Bind(out _presetsCh1)
                            .DisposeMany()
                            .Subscribe();

        _cleanUp[2] = _sourceCacheCh2.Connect()
                                    .Bind(out _presetsCh2)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[3] = _sourceCacheCh3.Connect()
                                    .Bind(out _presetsCh3)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[4] = _sourceCacheCh4.Connect()
                                    .Bind(out _presetsCh4)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[5] = _sourceCacheCh5.Connect()
                                    .Bind(out _presetsCh5)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[6] = _sourceCacheCh6.Connect()
                                    .Bind(out _presetsCh6)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[7] = _sourceCacheCh7.Connect()
                                    .Bind(out _presetsCh7)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[8] = _sourceCacheCh8.Connect()
                                    .Bind(out _presetsCh8)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[9] = _sourceCacheCh9.Connect()
                                    .Bind(out _presetsCh9)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[10] = _sourceCacheCh10.Connect()
                                    .Bind(out _presetsCh10)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[11] = _sourceCacheCh11.Connect()
                                    .Bind(out _presetsCh11)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[12] = _sourceCacheCh12.Connect()
                                    .Bind(out _presetsCh12)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[13] = _sourceCacheCh13.Connect()
                                    .Bind(out _presetsCh13)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[14] = _sourceCacheCh14.Connect()
                                    .Bind(out _presetsCh14)
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[15] = _sourceCacheCh15.Connect()
                                    .Bind(out _presetsCh15)
                                    .DisposeMany()
                                    .Subscribe();

    }

#pragma warning restore CA1822 // Mark members as static
}

