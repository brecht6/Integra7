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

namespace Integra7AuralAlchemist.ViewModels;


public partial class MainWindowViewModel : ObservableObject
{
#pragma warning disable CA1822 // Mark members as static
    private Integra7Preset _selectedPreset;
    public Integra7Preset SelectedPreset { 
        get => _selectedPreset;
        set { 
            _selectedPreset = value;
            ChangePreset();
        }
    }
    private SourceCache<Integra7Preset, int> _sourceCache = new SourceCache<Integra7Preset, int>(x => x.Id);
    private readonly ReadOnlyObservableCollection<Integra7Preset> _presets;
    public ReadOnlyObservableCollection<Integra7Preset> Presets => _presets;    
    private readonly IDisposable _cleanUp;

    private const string INTEGRA_CONNECTION_STRING = "INTEGRA-7";
    private IMidiOut? TheMidiOut {get; set;} = null;

    [ObservableProperty]
    private bool connected = false;

    [ObservableProperty]
    private string midiDevices = "No Midi Devices Detected";


    [RelayCommand]
    private void PlayNote()
    {
        TheMidiOut?.NoteOn(0, 65, 100);
        Thread.Sleep(1000);
        TheMidiOut?.NoteOff(0, 65);
    }

    [RelayCommand]
    private void ChangePreset()
    {
        if (SelectedPreset != null) {
            TheMidiOut?.ChangePreset(0, SelectedPreset.Msb, SelectedPreset.Lsb, SelectedPreset.Pc);
        }
    }

    [RelayCommand]
    private void RescanMidiDevices()
    {
        TheMidiOut = new MidiOut(INTEGRA_CONNECTION_STRING);
        Connected = TheMidiOut.ConnectionOk();
        if (Connected) {
            MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING;
        } else {
            MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
        }
    }

    private async void LoadPresets()
    {
        var uri = @"avares://" + "Integra7AuralAlchemist/" + "Assets/Presets.csv";
        var file = new StreamReader(AssetLoader.Open(new Uri(uri)));
        var data = file.ReadLine();
        char[] separators = {','};
        int id = 0;
        while((data = await file.ReadLineAsync()) != null)
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
            _sourceCache.AddOrUpdate(new Integra7Preset(id, tonetype, tonebank, number, name, msb, lsb, pc, category));
            id++;
        }
    }

    public MainWindowViewModel()
    {
        TheMidiOut = new MidiOut(INTEGRA_CONNECTION_STRING);
        Connected = TheMidiOut.ConnectionOk();
        if (Connected) {
            MidiDevices = "Connected to: " + INTEGRA_CONNECTION_STRING;
        } else {
            MidiDevices = "Could not find " + INTEGRA_CONNECTION_STRING;
        }
        LoadPresets();
        SelectedPreset = _sourceCache.Items.First();        
        _cleanUp = _sourceCache.Connect()
                               .Bind(out _presets)
                               .DisposeMany()
                               .Subscribe();
    }

#pragma warning restore CA1822 // Mark members as static
}

