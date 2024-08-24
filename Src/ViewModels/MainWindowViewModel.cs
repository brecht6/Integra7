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

    public bool SelectedPresetCh0IsSynthTone
    {
        get => SelectedPresetCh0.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh1IsSynthTone
    {
        get => SelectedPresetCh1.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh2IsSynthTone
    {
        get => SelectedPresetCh2.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh3IsSynthTone
    {
        get => SelectedPresetCh3.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh4IsSynthTone
    {
        get => SelectedPresetCh4.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh5IsSynthTone
    {
        get => SelectedPresetCh5.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh6IsSynthTone
    {
        get => SelectedPresetCh6.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh7IsSynthTone
    {
        get => SelectedPresetCh7.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh8IsSynthTone
    {
        get => SelectedPresetCh8.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh9IsSynthTone
    {
        get => SelectedPresetCh9.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh10IsSynthTone
    {
        get => SelectedPresetCh10.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh11IsSynthTone
    {
        get => SelectedPresetCh11.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh12IsSynthTone
    {
        get => SelectedPresetCh12.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh13IsSynthTone
    {
        get => SelectedPresetCh13.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh14IsSynthTone
    {
        get => SelectedPresetCh14.ToneTypeStr == "PCMS";
    }
    public bool SelectedPresetCh15IsSynthTone
    {
        get => SelectedPresetCh15.ToneTypeStr == "PCMS";
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

    [Reactive]
    private string _searchTextStudioSetPart0;
    [Reactive]
    private string _searchTextStudioSetPart1;
    [Reactive]
    private string _searchTextStudioSetPart2;
    [Reactive]
    private string _searchTextStudioSetPart3;
    [Reactive]
    private string _searchTextStudioSetPart4;
    [Reactive]
    private string _searchTextStudioSetPart5;
    [Reactive]
    private string _searchTextStudioSetPart6;
    [Reactive]
    private string _searchTextStudioSetPart7;
    [Reactive]
    private string _searchTextStudioSetPart8;
    [Reactive]
    private string _searchTextStudioSetPart9;
    [Reactive]
    private string _searchTextStudioSetPart10;
    [Reactive]
    private string _searchTextStudioSetPart11;
    [Reactive]
    private string _searchTextStudioSetPart12;
    [Reactive]
    private string _searchTextStudioSetPart13;
    [Reactive]
    private string _searchTextStudioSetPart14;
    [Reactive]
    private string _searchTextStudioSetPart15;

    [Reactive]
    public string _refreshStudioSetPart0;
    [Reactive]
    public string _refreshStudioSetPart1;
    [Reactive]
    public string _refreshStudioSetPart2;
    [Reactive]
    public string _refreshStudioSetPart3;
    [Reactive]
    public string _refreshStudioSetPart4;
    [Reactive]
    public string _refreshStudioSetPart5;
    [Reactive]
    public string _refreshStudioSetPart6;
    [Reactive]
    public string _refreshStudioSetPart7;
    [Reactive]
    public string _refreshStudioSetPart8;
    [Reactive]
    public string _refreshStudioSetPart9;
    [Reactive]
    public string _refreshStudioSetPart10;
    [Reactive]
    public string _refreshStudioSetPart11;
    [Reactive]
    public string _refreshStudioSetPart12;
    [Reactive]
    public string _refreshStudioSetPart13;
    [Reactive]
    public string _refreshStudioSetPart14;
    [Reactive]
    public string _refreshStudioSetPart15;
    [Reactive]
    private string _searchTextStudioSetPartEQ0;
    [Reactive]
    private string _searchTextStudioSetPartEQ1;
    [Reactive]
    private string _searchTextStudioSetPartEQ2;
    [Reactive]
    private string _searchTextStudioSetPartEQ3;
    [Reactive]
    private string _searchTextStudioSetPartEQ4;
    [Reactive]
    private string _searchTextStudioSetPartEQ5;
    [Reactive]
    private string _searchTextStudioSetPartEQ6;
    [Reactive]
    private string _searchTextStudioSetPartEQ7;
    [Reactive]
    private string _searchTextStudioSetPartEQ8;
    [Reactive]
    private string _searchTextStudioSetPartEQ9;
    [Reactive]
    private string _searchTextStudioSetPartEQ10;
    [Reactive]
    private string _searchTextStudioSetPartEQ11;
    [Reactive]
    private string _searchTextStudioSetPartEQ12;
    [Reactive]
    private string _searchTextStudioSetPartEQ13;
    [Reactive]
    private string _searchTextStudioSetPartEQ14;
    [Reactive]
    private string _searchTextStudioSetPartEQ15;

    [Reactive]
    public string _refreshStudioSetPartEQ0;
    [Reactive]
    public string _refreshStudioSetPartEQ1;
    [Reactive]
    public string _refreshStudioSetPartEQ2;
    [Reactive]
    public string _refreshStudioSetPartEQ3;
    [Reactive]
    public string _refreshStudioSetPartEQ4;
    [Reactive]
    public string _refreshStudioSetPartEQ5;
    [Reactive]
    public string _refreshStudioSetPartEQ6;
    [Reactive]
    public string _refreshStudioSetPartEQ7;
    [Reactive]
    public string _refreshStudioSetPartEQ8;
    [Reactive]
    public string _refreshStudioSetPartEQ9;
    [Reactive]
    public string _refreshStudioSetPartEQ10;
    [Reactive]
    public string _refreshStudioSetPartEQ11;
    [Reactive]
    public string _refreshStudioSetPartEQ12;
    [Reactive]
    public string _refreshStudioSetPartEQ13;
    [Reactive]
    public string _refreshStudioSetPartEQ14;
    [Reactive]
    public string _refreshStudioSetPartEQ15;
    [Reactive]
    private string _searchTextPCMSynthToneCommon0;
    [Reactive]
    private string _searchTextPCMSynthToneCommon1;
    [Reactive]
    private string _searchTextPCMSynthToneCommon2;
    [Reactive]
    private string _searchTextPCMSynthToneCommon3;
    [Reactive]
    private string _searchTextPCMSynthToneCommon4;
    [Reactive]
    private string _searchTextPCMSynthToneCommon5;
    [Reactive]
    private string _searchTextPCMSynthToneCommon6;
    [Reactive]
    private string _searchTextPCMSynthToneCommon7;
    [Reactive]
    private string _searchTextPCMSynthToneCommon8;
    [Reactive]
    private string _searchTextPCMSynthToneCommon9;
    [Reactive]
    private string _searchTextPCMSynthToneCommon10;
    [Reactive]
    private string _searchTextPCMSynthToneCommon11;
    [Reactive]
    private string _searchTextPCMSynthToneCommon12;
    [Reactive]
    private string _searchTextPCMSynthToneCommon13;
    [Reactive]
    private string _searchTextPCMSynthToneCommon14;
    [Reactive]
    private string _searchTextPCMSynthToneCommon15;

    [Reactive]
    public string _refreshPCMSynthToneCommon0;
    [Reactive]
    public string _refreshPCMSynthToneCommon1;
    [Reactive]
    public string _refreshPCMSynthToneCommon2;
    [Reactive]
    public string _refreshPCMSynthToneCommon3;
    [Reactive]
    public string _refreshPCMSynthToneCommon4;
    [Reactive]
    public string _refreshPCMSynthToneCommon5;
    [Reactive]
    public string _refreshPCMSynthToneCommon6;
    [Reactive]
    public string _refreshPCMSynthToneCommon7;
    [Reactive]
    public string _refreshPCMSynthToneCommon8;
    [Reactive]
    public string _refreshPCMSynthToneCommon9;
    [Reactive]
    public string _refreshPCMSynthToneCommon10;
    [Reactive]
    public string _refreshPCMSynthToneCommon11;
    [Reactive]
    public string _refreshPCMSynthToneCommon12;
    [Reactive]
    public string _refreshPCMSynthToneCommon13;
    [Reactive]
    public string _refreshPCMSynthToneCommon14;
    [Reactive]
    public string _refreshPCMSynthToneCommon15;

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

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters0 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters0;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters0 => _studioSetPartParameters0;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters1 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters1;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters1 => _studioSetPartParameters1;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters2 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters2;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters2 => _studioSetPartParameters2;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters3 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters3;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters3 => _studioSetPartParameters3;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters4 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters4;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters4 => _studioSetPartParameters4;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters5 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters5;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters5 => _studioSetPartParameters5;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters6 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters6;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters6 => _studioSetPartParameters6;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters7 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters7;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters7 => _studioSetPartParameters7;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters8 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters8;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters8 => _studioSetPartParameters8;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters9 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters9;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters9 => _studioSetPartParameters9;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters10 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters10;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters10 => _studioSetPartParameters10;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters11 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters11;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters11 => _studioSetPartParameters11;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters12 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters12;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters12 => _studioSetPartParameters12;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters13 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters13;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters13 => _studioSetPartParameters13;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters14 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters14;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters14 => _studioSetPartParameters14;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartParameters15 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _studioSetPartParameters15;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartParameters15 => _studioSetPartParameters15;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters0 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters0;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters0 => _StudioSetPartEQParameters0;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters1 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters1;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters1 => _StudioSetPartEQParameters1;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters2 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters2;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters2 => _StudioSetPartEQParameters2;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters3 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters3;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters3 => _StudioSetPartEQParameters3;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters4 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters4;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters4 => _StudioSetPartEQParameters4;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters5 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters5;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters5 => _StudioSetPartEQParameters5;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters6 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters6;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters6 => _StudioSetPartEQParameters6;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters7 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters7;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters7 => _StudioSetPartEQParameters7;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters8 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters8;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters8 => _StudioSetPartEQParameters8;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters9 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters9;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters9 => _StudioSetPartEQParameters9;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters10 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters10;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters10 => _StudioSetPartEQParameters10;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters11 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters11;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters11 => _StudioSetPartEQParameters11;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters12 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters12;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters12 => _StudioSetPartEQParameters12;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters13 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters13;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters13 => _StudioSetPartEQParameters13;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters14 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters14;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters14 => _StudioSetPartEQParameters14;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCacheStudioSetPartEQParameters15 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _StudioSetPartEQParameters15;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> StudioSetPartEQParameters15 => _StudioSetPartEQParameters15;

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters0 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters0;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters0 => _PCMSynthToneCommonParameters0;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters1 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters1;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters1 => _PCMSynthToneCommonParameters1;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters2 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters2;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters2 => _PCMSynthToneCommonParameters2;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters3 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters3;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters3 => _PCMSynthToneCommonParameters3;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters4 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters4;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters4 => _PCMSynthToneCommonParameters4;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters5 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters5;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters5 => _PCMSynthToneCommonParameters5;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters6 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters6;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters6 => _PCMSynthToneCommonParameters6;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters7 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters7;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters7 => _PCMSynthToneCommonParameters7;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters8 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters8;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters8 => _PCMSynthToneCommonParameters8;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters9 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters9;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters9 => _PCMSynthToneCommonParameters9;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters10 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters10;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters10 => _PCMSynthToneCommonParameters10;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters11 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters11;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters11 => _PCMSynthToneCommonParameters11;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters12 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters12;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters12 => _PCMSynthToneCommonParameters12;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters13 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters13;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters13 => _PCMSynthToneCommonParameters13;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters14 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters14;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters14 => _PCMSynthToneCommonParameters14;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthToneCommonParameters15 = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthToneCommonParameters15;
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthToneCommonParameters15 => _PCMSynthToneCommonParameters15;


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

            _integra7Communicator.StudioSetPart(0).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part0 = _integra7Communicator.StudioSetPart(0).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters0.AddOrUpdate(p_part0);
            _integra7Communicator.StudioSetPart(1).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part1 = _integra7Communicator.StudioSetPart(1).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters1.AddOrUpdate(p_part1);
            _integra7Communicator.StudioSetPart(2).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part2 = _integra7Communicator.StudioSetPart(2).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters2.AddOrUpdate(p_part2);
            _integra7Communicator.StudioSetPart(3).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part3 = _integra7Communicator.StudioSetPart(3).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters3.AddOrUpdate(p_part3);
            _integra7Communicator.StudioSetPart(4).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part4 = _integra7Communicator.StudioSetPart(4).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters4.AddOrUpdate(p_part4);
            _integra7Communicator.StudioSetPart(5).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part5 = _integra7Communicator.StudioSetPart(5).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters5.AddOrUpdate(p_part5);
            _integra7Communicator.StudioSetPart(6).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part6 = _integra7Communicator.StudioSetPart(6).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters6.AddOrUpdate(p_part6);
            _integra7Communicator.StudioSetPart(7).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part7 = _integra7Communicator.StudioSetPart(7).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters7.AddOrUpdate(p_part7);
            _integra7Communicator.StudioSetPart(8).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part8 = _integra7Communicator.StudioSetPart(8).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters8.AddOrUpdate(p_part8);
            _integra7Communicator.StudioSetPart(9).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part9 = _integra7Communicator.StudioSetPart(9).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters9.AddOrUpdate(p_part9);
            _integra7Communicator.StudioSetPart(10).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part10 = _integra7Communicator.StudioSetPart(10).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters10.AddOrUpdate(p_part10);
            _integra7Communicator.StudioSetPart(11).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part11 = _integra7Communicator.StudioSetPart(11).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters11.AddOrUpdate(p_part11);
            _integra7Communicator.StudioSetPart(12).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part12 = _integra7Communicator.StudioSetPart(12).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters12.AddOrUpdate(p_part12);
            _integra7Communicator.StudioSetPart(13).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part13 = _integra7Communicator.StudioSetPart(13).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters13.AddOrUpdate(p_part13);
            _integra7Communicator.StudioSetPart(14).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part14 = _integra7Communicator.StudioSetPart(14).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters14.AddOrUpdate(p_part14);
            _integra7Communicator.StudioSetPart(15).ReadFromIntegra();
            List<FullyQualifiedParameter> p_part15 = _integra7Communicator.StudioSetPart(15).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartParameters15.AddOrUpdate(p_part15);

            _integra7Communicator.StudioSetPartEQ(0).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq0 = _integra7Communicator.StudioSetPartEQ(0).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters0.AddOrUpdate(p_parteq0);
            _integra7Communicator.StudioSetPartEQ(1).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq1 = _integra7Communicator.StudioSetPartEQ(1).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters1.AddOrUpdate(p_parteq1);
            _integra7Communicator.StudioSetPartEQ(2).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq2 = _integra7Communicator.StudioSetPartEQ(2).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters2.AddOrUpdate(p_parteq2);
            _integra7Communicator.StudioSetPartEQ(3).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq3 = _integra7Communicator.StudioSetPartEQ(3).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters3.AddOrUpdate(p_parteq3);
            _integra7Communicator.StudioSetPartEQ(4).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq4 = _integra7Communicator.StudioSetPartEQ(4).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters4.AddOrUpdate(p_parteq4);
            _integra7Communicator.StudioSetPartEQ(5).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq5 = _integra7Communicator.StudioSetPartEQ(5).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters5.AddOrUpdate(p_parteq5);
            _integra7Communicator.StudioSetPartEQ(6).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq6 = _integra7Communicator.StudioSetPartEQ(6).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters6.AddOrUpdate(p_parteq6);
            _integra7Communicator.StudioSetPartEQ(7).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq7 = _integra7Communicator.StudioSetPartEQ(7).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters7.AddOrUpdate(p_parteq7);
            _integra7Communicator.StudioSetPartEQ(8).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq8 = _integra7Communicator.StudioSetPartEQ(8).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters8.AddOrUpdate(p_parteq8);
            _integra7Communicator.StudioSetPartEQ(9).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq9 = _integra7Communicator.StudioSetPartEQ(9).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters9.AddOrUpdate(p_parteq9);
            _integra7Communicator.StudioSetPartEQ(10).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq10 = _integra7Communicator.StudioSetPartEQ(10).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters10.AddOrUpdate(p_parteq10);
            _integra7Communicator.StudioSetPartEQ(11).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq11 = _integra7Communicator.StudioSetPartEQ(11).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters11.AddOrUpdate(p_parteq11);
            _integra7Communicator.StudioSetPartEQ(12).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq12 = _integra7Communicator.StudioSetPartEQ(12).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters12.AddOrUpdate(p_parteq12);
            _integra7Communicator.StudioSetPartEQ(13).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq13 = _integra7Communicator.StudioSetPartEQ(13).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters13.AddOrUpdate(p_parteq13);
            _integra7Communicator.StudioSetPartEQ(14).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq14 = _integra7Communicator.StudioSetPartEQ(14).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters14.AddOrUpdate(p_parteq14);
            _integra7Communicator.StudioSetPartEQ(15).ReadFromIntegra();
            List<FullyQualifiedParameter> p_parteq15 = _integra7Communicator.StudioSetPartEQ(15).GetRelevantParameters(true, true);
            _sourceCacheStudioSetPartEQParameters15.AddOrUpdate(p_parteq15);

            if (GetSelectedPreset(0)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(0).ReadFromIntegra();

            }
            List<FullyQualifiedParameter> p_pcmstc0 = _integra7Communicator.PCMSynthToneCommon(0).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters0.AddOrUpdate(p_pcmstc0);

            if (GetSelectedPreset(1)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(1).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc1 = _integra7Communicator.PCMSynthToneCommon(1).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters1.AddOrUpdate(p_pcmstc1);

            if (GetSelectedPreset(2)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(2).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc2 = _integra7Communicator.PCMSynthToneCommon(2).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters2.AddOrUpdate(p_pcmstc2);

            if (GetSelectedPreset(3)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(3).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc3 = _integra7Communicator.PCMSynthToneCommon(3).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters3.AddOrUpdate(p_pcmstc3);

            if (GetSelectedPreset(4)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(4).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc4 = _integra7Communicator.PCMSynthToneCommon(4).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters4.AddOrUpdate(p_pcmstc4);

            if (GetSelectedPreset(5)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(5).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc5 = _integra7Communicator.PCMSynthToneCommon(5).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters5.AddOrUpdate(p_pcmstc5);
            if (GetSelectedPreset(6)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(6).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc6 = _integra7Communicator.PCMSynthToneCommon(6).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters6.AddOrUpdate(p_pcmstc6);
            if (GetSelectedPreset(7)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(7).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc7 = _integra7Communicator.PCMSynthToneCommon(7).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters7.AddOrUpdate(p_pcmstc7);
            if (GetSelectedPreset(8)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(8).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc8 = _integra7Communicator.PCMSynthToneCommon(8).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters8.AddOrUpdate(p_pcmstc8);
            if (GetSelectedPreset(9)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(9).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc9 = _integra7Communicator.PCMSynthToneCommon(9).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters9.AddOrUpdate(p_pcmstc9);
            if (GetSelectedPreset(10)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(10).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc10 = _integra7Communicator.PCMSynthToneCommon(10).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters10.AddOrUpdate(p_pcmstc10);
            if (GetSelectedPreset(11)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(11).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc11 = _integra7Communicator.PCMSynthToneCommon(11).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters11.AddOrUpdate(p_pcmstc11);
            if (GetSelectedPreset(12)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(12).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc12 = _integra7Communicator.PCMSynthToneCommon(12).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters12.AddOrUpdate(p_pcmstc12);
            if (GetSelectedPreset(13)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(13).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc13 = _integra7Communicator.PCMSynthToneCommon(13).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters13.AddOrUpdate(p_pcmstc13);
            if (GetSelectedPreset(14)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(14).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc14 = _integra7Communicator.PCMSynthToneCommon(14).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters14.AddOrUpdate(p_pcmstc14);
            if (GetSelectedPreset(15)?.ToneTypeStr == "PCMS")
            {
                _integra7Communicator.PCMSynthToneCommon(15).ReadFromIntegra();
            }
            List<FullyQualifiedParameter> p_pcmstc15 = _integra7Communicator.PCMSynthToneCommon(15).GetRelevantParameters(true, true);
            _sourceCachePCMSynthToneCommonParameters15.AddOrUpdate(p_pcmstc15);
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

        var parFilterStudioSetPartParameters0 = this.WhenAnyValue(x => x.SearchTextStudioSetPart0)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters1 = this.WhenAnyValue(x => x.SearchTextStudioSetPart1)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters2 = this.WhenAnyValue(x => x.SearchTextStudioSetPart2)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters3 = this.WhenAnyValue(x => x.SearchTextStudioSetPart3)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters4 = this.WhenAnyValue(x => x.SearchTextStudioSetPart4)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters5 = this.WhenAnyValue(x => x.SearchTextStudioSetPart5)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters6 = this.WhenAnyValue(x => x.SearchTextStudioSetPart6)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters7 = this.WhenAnyValue(x => x.SearchTextStudioSetPart7)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters8 = this.WhenAnyValue(x => x.SearchTextStudioSetPart8)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters9 = this.WhenAnyValue(x => x.SearchTextStudioSetPart9)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters10 = this.WhenAnyValue(x => x.SearchTextStudioSetPart10)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters11 = this.WhenAnyValue(x => x.SearchTextStudioSetPart11)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters12 = this.WhenAnyValue(x => x.SearchTextStudioSetPart12)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters13 = this.WhenAnyValue(x => x.SearchTextStudioSetPart13)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters14 = this.WhenAnyValue(x => x.SearchTextStudioSetPart14)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartParameters15 = this.WhenAnyValue(x => x.SearchTextStudioSetPart15)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart0 = this.WhenAnyValue(x => x.RefreshStudioSetPart0)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart1 = this.WhenAnyValue(x => x.RefreshStudioSetPart1)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart2 = this.WhenAnyValue(x => x.RefreshStudioSetPart2)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart3 = this.WhenAnyValue(x => x.RefreshStudioSetPart3)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart4 = this.WhenAnyValue(x => x.RefreshStudioSetPart4)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart5 = this.WhenAnyValue(x => x.RefreshStudioSetPart5)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart6 = this.WhenAnyValue(x => x.RefreshStudioSetPart6)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart7 = this.WhenAnyValue(x => x.RefreshStudioSetPart7)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart8 = this.WhenAnyValue(x => x.RefreshStudioSetPart8)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart9 = this.WhenAnyValue(x => x.RefreshStudioSetPart9)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart10 = this.WhenAnyValue(x => x.RefreshStudioSetPart10)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart11 = this.WhenAnyValue(x => x.RefreshStudioSetPart11)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart12 = this.WhenAnyValue(x => x.RefreshStudioSetPart12)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart13 = this.WhenAnyValue(x => x.RefreshStudioSetPart13)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart14 = this.WhenAnyValue(x => x.RefreshStudioSetPart14)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPart15 = this.WhenAnyValue(x => x.RefreshStudioSetPart15)
                                            .Select(_parameterFilter);

        var parFilterStudioSetPartEQParameters0 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ0)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters1 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ1)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters2 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ2)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters3 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ3)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters4 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ4)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters5 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ5)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters6 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ6)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters7 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ7)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters8 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ8)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters9 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ9)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters10 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ10)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters11 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ11)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters12 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ12)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters13 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ13)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters14 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ14)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterStudioSetPartEQParameters15 = this.WhenAnyValue(x => x.SearchTextStudioSetPartEQ15)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ0 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ0)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ1 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ1)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ2 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ2)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ3 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ3)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ4 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ4)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ5 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ5)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ6 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ6)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ7 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ7)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ8 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ8)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ9 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ9)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ10 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ10)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ11 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ11)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ12 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ12)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ13 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ13)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ14 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ14)
                                            .Select(_parameterFilter);
        var refreshFilterStudioSetPartEQ15 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ15)
                                            .Select(_parameterFilter);

        var parFilterPCMSynthToneCommonParameters0 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon0)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters1 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon1)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters2 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon2)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters3 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon3)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters4 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon4)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters5 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon5)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters6 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon6)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters7 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon7)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters8 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon8)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters9 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon9)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters10 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon10)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters11 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon11)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters12 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon12)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters13 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon13)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters14 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon14)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var parFilterPCMSynthToneCommonParameters15 = this.WhenAnyValue(x => x.SearchTextPCMSynthToneCommon15)
                                            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                            .DistinctUntilChanged()
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon0 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon0)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon1 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon1)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon2 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon2)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon3 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon3)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon4 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon4)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon5 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon5)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon6 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon6)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon7 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon7)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon8 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon8)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon9 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon9)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon10 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon10)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon11 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon11)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon12 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon12)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon13 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon13)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon14 = this.WhenAnyValue(x => x.RefreshPCMSynthToneCommon14)
                                            .Select(_parameterFilter);
        var refreshFilterPCMSynthToneCommon15 = this.WhenAnyValue(x => x.RefreshStudioSetPartEQ15)
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

        _cleanUp[38] = _sourceCacheStudioSetPartParameters0.Connect()
                                    .Filter(refreshFilterStudioSetPart0)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters0)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters0
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters0
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters0,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[39] = _sourceCacheStudioSetPartParameters1.Connect()
                                    .Filter(refreshFilterStudioSetPart1)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters1)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters1
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters1
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters1,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[40] = _sourceCacheStudioSetPartParameters2.Connect()
                                    .Filter(refreshFilterStudioSetPart2)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters2)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters2
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters2
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters2,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[41] = _sourceCacheStudioSetPartParameters3.Connect()
                                    .Filter(refreshFilterStudioSetPart3)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters3)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters3
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters3
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters3,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[42] = _sourceCacheStudioSetPartParameters4.Connect()
                                    .Filter(refreshFilterStudioSetPart4)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters4)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters4
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters4
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters4,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[43] = _sourceCacheStudioSetPartParameters5.Connect()
                                    .Filter(refreshFilterStudioSetPart5)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters5)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters5
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters5
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters5,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[44] = _sourceCacheStudioSetPartParameters6.Connect()
                                    .Filter(refreshFilterStudioSetPart6)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters6)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters6
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters6
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters6,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[45] = _sourceCacheStudioSetPartParameters7.Connect()
                                    .Filter(refreshFilterStudioSetPart7)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters7)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters7
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters7
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters7,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[46] = _sourceCacheStudioSetPartParameters8.Connect()
                                    .Filter(refreshFilterStudioSetPart8)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters8)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters8
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters8
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters8,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[47] = _sourceCacheStudioSetPartParameters9.Connect()
                                    .Filter(refreshFilterStudioSetPart9)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters9)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters9
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters9
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters9,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[48] = _sourceCacheStudioSetPartParameters10.Connect()
                                    .Filter(refreshFilterStudioSetPart10)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters10)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters10
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters10
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters10,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[49] = _sourceCacheStudioSetPartParameters11.Connect()
                                    .Filter(refreshFilterStudioSetPart11)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters11)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters11
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters11
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters11,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[50] = _sourceCacheStudioSetPartParameters12.Connect()
                                    .Filter(refreshFilterStudioSetPart12)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters12)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters12
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters12
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters12,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[51] = _sourceCacheStudioSetPartParameters13.Connect()
                                    .Filter(refreshFilterStudioSetPart13)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters13)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters13
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters13
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters13,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[52] = _sourceCacheStudioSetPartParameters14.Connect()
                                    .Filter(refreshFilterStudioSetPart14)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters14)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters14
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters14
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters14,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[53] = _sourceCacheStudioSetPartParameters15.Connect()
                                    .Filter(refreshFilterStudioSetPart15)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartParameters15)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartParameters15
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartParameters15
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _studioSetPartParameters15,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[54] = _sourceCacheStudioSetPartEQParameters0.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ0)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters0)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters0
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters0
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters0,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[55] = _sourceCacheStudioSetPartEQParameters1.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ1)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters1)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters1
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters1
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters1,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[56] = _sourceCacheStudioSetPartEQParameters2.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ2)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters2)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters2
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters2
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters2,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[57] = _sourceCacheStudioSetPartEQParameters3.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ3)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters3)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters3
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters3
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters3,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[58] = _sourceCacheStudioSetPartEQParameters4.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ4)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters4)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters4
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters4
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters4,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[59] = _sourceCacheStudioSetPartEQParameters5.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ5)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters5)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters5
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters5
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters5,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[60] = _sourceCacheStudioSetPartEQParameters6.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ6)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters6)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters6
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters6
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters6,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[61] = _sourceCacheStudioSetPartEQParameters7.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ7)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters7)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters7
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters7
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters7,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[62] = _sourceCacheStudioSetPartEQParameters8.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ8)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters8)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters8
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters8
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters8,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[63] = _sourceCacheStudioSetPartEQParameters9.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ9)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters9)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters9
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters9
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters9,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[64] = _sourceCacheStudioSetPartEQParameters10.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ10)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters10)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters10
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters10
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters10,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[65] = _sourceCacheStudioSetPartEQParameters11.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ11)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters11)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters11
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters11
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters11,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[66] = _sourceCacheStudioSetPartEQParameters12.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ12)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters12)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters12
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters12
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters12,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[67] = _sourceCacheStudioSetPartEQParameters13.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ13)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters13)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters13
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters13
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters13,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[68] = _sourceCacheStudioSetPartEQParameters14.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ14)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters14)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters14
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters14
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters14,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[69] = _sourceCacheStudioSetPartEQParameters15.Connect()
                                    .Filter(refreshFilterStudioSetPartEQ15)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterStudioSetPartEQParameters15)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCacheStudioSetPartEQParameters15
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCacheStudioSetPartEQParameters15
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _StudioSetPartEQParameters15,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[70] = _sourceCachePCMSynthToneCommonParameters0.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon0)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters0)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters0
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters0
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters0,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[71] = _sourceCachePCMSynthToneCommonParameters1.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon1)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters1)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters1
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters1
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters1,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[72] = _sourceCachePCMSynthToneCommonParameters2.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon2)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters2)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters2
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters2
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters2,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[73] = _sourceCachePCMSynthToneCommonParameters3.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon3)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters3)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters3
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters3
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters3,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[74] = _sourceCachePCMSynthToneCommonParameters4.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon4)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters4)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters4
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters4
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters4,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[75] = _sourceCachePCMSynthToneCommonParameters5.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon5)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters5)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters5
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters5
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters5,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[76] = _sourceCachePCMSynthToneCommonParameters6.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon6)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters6)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters6
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters6
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters6,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[77] = _sourceCachePCMSynthToneCommonParameters7.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon7)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters7)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters7
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters7
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters7,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[78] = _sourceCachePCMSynthToneCommonParameters8.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon8)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters8)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters8
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters8
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters8,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[79] = _sourceCachePCMSynthToneCommonParameters9.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon9)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters9)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters9
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters9
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters9,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[80] = _sourceCachePCMSynthToneCommonParameters10.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon10)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters10)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters10
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters10
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters10,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[81] = _sourceCachePCMSynthToneCommonParameters11.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon11)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters11)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters11
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters11
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters11,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[82] = _sourceCachePCMSynthToneCommonParameters12.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon12)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters12)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters12
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters12
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters12,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[83] = _sourceCachePCMSynthToneCommonParameters13.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon13)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters13)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters13
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters13
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters13,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();

        _cleanUp[84] = _sourceCachePCMSynthToneCommonParameters14.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon14)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters14)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters14
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters14
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters14,
                                        SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
                                    .DisposeMany()
                                    .Subscribe();
        _cleanUp[85] = _sourceCachePCMSynthToneCommonParameters15.Connect()
                                    .Filter(refreshFilterPCMSynthToneCommon15)
                                    .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
                                    .Filter(parFilterPCMSynthToneCommonParameters15)
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                                            ? _sourceCachePCMSynthToneCommonParameters15
                                                .Watch(parentId)
                                                .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                                            : Observable.Return(true))
                                    .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                                            ? _sourceCachePCMSynthToneCommonParameters15
                                                .Watch(parentId2)
                                                .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                                            : Observable.Return(true))
                                    .ObserveOn(RxApp.MainThreadScheduler)
                                    .SortAndBind(
                                        out _PCMSynthToneCommonParameters15,
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
                if (spec.Par.ParSpec.Name.StartsWith("Tone Bank Select") || spec.Par.ParSpec.Name.StartsWith("Tone Bank Program Number"))
                {
                    switch (spec.Par.Offset)
                    {
                        // using MessageBus instead of direct call because it is automatically throttled
                        case "Offset/Studio Set Part 1": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(0)); break;
                        case "Offset/Studio Set Part 2": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(1)); break;
                        case "Offset/Studio Set Part 3": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(2)); break;
                        case "Offset/Studio Set Part 4": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(3)); break;
                        case "Offset/Studio Set Part 5": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(4)); break;
                        case "Offset/Studio Set Part 6": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(5)); break;
                        case "Offset/Studio Set Part 7": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(6)); break;
                        case "Offset/Studio Set Part 8": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(7)); break;
                        case "Offset/Studio Set Part 9": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(8)); break;
                        case "Offset/Studio Set Part 10": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(9)); break;
                        case "Offset/Studio Set Part 11": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(10)); break;
                        case "Offset/Studio Set Part 12": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(11)); break;
                        case "Offset/Studio Set Part 13": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(12)); break;
                        case "Offset/Studio Set Part 14": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(13)); break;
                        case "Offset/Studio Set Part 15": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(14)); break;
                        case "Offset/Studio Set Part 16": MessageBus.Current.SendMessage<UpdateResyncPart>(new UpdateResyncPart(15)); break;
                        default: break;
                    }
                }
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
        ForceUiRefresh(p.Start, p.Offset);
    }

    private void ForceUiRefresh(string StartAddressName, string OffsetAddressName)
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
        else if (OffsetAddressName == "Offset/Studio Set Part 0")
        {
            RefreshStudioSetPart0 = ".";
            RefreshStudioSetPart0 = SearchTextStudioSetPart0;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 1")
        {
            RefreshStudioSetPart1 = ".";
            RefreshStudioSetPart1 = SearchTextStudioSetPart1;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 2")
        {
            RefreshStudioSetPart2 = ".";
            RefreshStudioSetPart2 = SearchTextStudioSetPart2;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 3")
        {
            RefreshStudioSetPart3 = ".";
            RefreshStudioSetPart3 = SearchTextStudioSetPart3;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 4")
        {
            RefreshStudioSetPart4 = ".";
            RefreshStudioSetPart4 = SearchTextStudioSetPart4;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 5")
        {
            RefreshStudioSetPart5 = ".";
            RefreshStudioSetPart5 = SearchTextStudioSetPart5;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 6")
        {
            RefreshStudioSetPart6 = ".";
            RefreshStudioSetPart6 = SearchTextStudioSetPart6;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 7")
        {
            RefreshStudioSetPart7 = ".";
            RefreshStudioSetPart7 = SearchTextStudioSetPart7;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 8")
        {
            RefreshStudioSetPart8 = ".";
            RefreshStudioSetPart8 = SearchTextStudioSetPart8;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 9")
        {
            RefreshStudioSetPart9 = ".";
            RefreshStudioSetPart9 = SearchTextStudioSetPart9;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 10")
        {
            RefreshStudioSetPart10 = ".";
            RefreshStudioSetPart10 = SearchTextStudioSetPart10;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 11")
        {
            RefreshStudioSetPart11 = ".";
            RefreshStudioSetPart11 = SearchTextStudioSetPart11;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 12")
        {
            RefreshStudioSetPart12 = ".";
            RefreshStudioSetPart12 = SearchTextStudioSetPart12;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 13")
        {
            RefreshStudioSetPart13 = ".";
            RefreshStudioSetPart13 = SearchTextStudioSetPart13;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 14")
        {
            RefreshStudioSetPart14 = ".";
            RefreshStudioSetPart14 = SearchTextStudioSetPart14;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part 15")
        {
            RefreshStudioSetPart15 = ".";
            RefreshStudioSetPart15 = SearchTextStudioSetPart15;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 0")
        {
            RefreshStudioSetPart0 = ".";
            RefreshStudioSetPart0 = SearchTextStudioSetPart0;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 1")
        {
            RefreshStudioSetPart1 = ".";
            RefreshStudioSetPart1 = SearchTextStudioSetPart1;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 2")
        {
            RefreshStudioSetPart2 = ".";
            RefreshStudioSetPart2 = SearchTextStudioSetPart2;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 3")
        {
            RefreshStudioSetPart3 = ".";
            RefreshStudioSetPart3 = SearchTextStudioSetPart3;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 4")
        {
            RefreshStudioSetPart4 = ".";
            RefreshStudioSetPart4 = SearchTextStudioSetPart4;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 5")
        {
            RefreshStudioSetPart5 = ".";
            RefreshStudioSetPart5 = SearchTextStudioSetPart5;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 6")
        {
            RefreshStudioSetPart6 = ".";
            RefreshStudioSetPart6 = SearchTextStudioSetPart6;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 7")
        {
            RefreshStudioSetPart7 = ".";
            RefreshStudioSetPart7 = SearchTextStudioSetPart7;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 8")
        {
            RefreshStudioSetPart8 = ".";
            RefreshStudioSetPart8 = SearchTextStudioSetPart8;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 9")
        {
            RefreshStudioSetPart9 = ".";
            RefreshStudioSetPart9 = SearchTextStudioSetPart9;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 10")
        {
            RefreshStudioSetPart10 = ".";
            RefreshStudioSetPart10 = SearchTextStudioSetPart10;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 11")
        {
            RefreshStudioSetPart11 = ".";
            RefreshStudioSetPart11 = SearchTextStudioSetPart11;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 12")
        {
            RefreshStudioSetPart12 = ".";
            RefreshStudioSetPart12 = SearchTextStudioSetPart12;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 13")
        {
            RefreshStudioSetPart13 = ".";
            RefreshStudioSetPart13 = SearchTextStudioSetPart13;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 14")
        {
            RefreshStudioSetPart14 = ".";
            RefreshStudioSetPart14 = SearchTextStudioSetPart14;
        }
        else if (OffsetAddressName == "Offset/Studio Set Part EQ 15")
        {
            RefreshStudioSetPart15 = ".";
            RefreshStudioSetPart15 = SearchTextStudioSetPart15;
        }
        else if (StartAddressName == "Temporary Tone Part 1" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon0 = ".";
            RefreshPCMSynthToneCommon0 = SearchTextPCMSynthToneCommon0;
        }
        else if (StartAddressName == "Temporary Tone Part 2" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon1 = ".";
            RefreshPCMSynthToneCommon1 = SearchTextPCMSynthToneCommon1;
        }
        else if (StartAddressName == "Temporary Tone Part 3" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon2 = ".";
            RefreshPCMSynthToneCommon2 = SearchTextPCMSynthToneCommon2;
        }
        else if (StartAddressName == "Temporary Tone Part 4" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon3 = ".";
            RefreshPCMSynthToneCommon3 = SearchTextPCMSynthToneCommon3;
        }
        else if (StartAddressName == "Temporary Tone Part 5" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon4 = ".";
            RefreshPCMSynthToneCommon4 = SearchTextPCMSynthToneCommon4;
        }
        else if (StartAddressName == "Temporary Tone Part 6" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon5 = ".";
            RefreshPCMSynthToneCommon5 = SearchTextPCMSynthToneCommon5;
        }
        else if (StartAddressName == "Temporary Tone Part 7" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon6 = ".";
            RefreshPCMSynthToneCommon6 = SearchTextPCMSynthToneCommon6;
        }
        else if (StartAddressName == "Temporary Tone Part 8" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon7 = ".";
            RefreshPCMSynthToneCommon7 = SearchTextPCMSynthToneCommon7;
        }
        else if (StartAddressName == "Temporary Tone Part 9" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon8 = ".";
            RefreshPCMSynthToneCommon8 = SearchTextPCMSynthToneCommon8;
        }
        else if (StartAddressName == "Temporary Tone Part 10" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon9 = ".";
            RefreshPCMSynthToneCommon9 = SearchTextPCMSynthToneCommon9;
        }
        else if (StartAddressName == "Temporary Tone Part 11" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon10 = ".";
            RefreshPCMSynthToneCommon10 = SearchTextPCMSynthToneCommon10;
        }
        else if (StartAddressName == "Temporary Tone Part 12" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon11 = ".";
            RefreshPCMSynthToneCommon11 = SearchTextPCMSynthToneCommon11;
        }
        else if (StartAddressName == "Temporary Tone Part 13" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon12 = ".";
            RefreshPCMSynthToneCommon12 = SearchTextPCMSynthToneCommon12;
        }
        else if (StartAddressName == "Temporary Tone Part 14" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon13 = ".";
            RefreshPCMSynthToneCommon13 = SearchTextPCMSynthToneCommon13;
        }
        else if (StartAddressName == "Temporary Tone Part 15" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon14 = ".";
            RefreshPCMSynthToneCommon14 = SearchTextPCMSynthToneCommon14;
        }
        else if (StartAddressName == "Temporary Tone Part 16" && OffsetAddressName == "Offset/PCM Synth Tone Common")
        {
            RefreshPCMSynthToneCommon15 = ".";
            RefreshPCMSynthToneCommon15 = SearchTextPCMSynthToneCommon15;
        }
    }
    public void ResyncPart(byte part)
    {
        DomainBase midiPart = _integra7Communicator.StudioSetMidi(part);
        midiPart.ReadFromIntegra();
        ForceUiRefresh(midiPart.StartAddressName, midiPart.OffsetAddressName);
        DomainBase setPart = _integra7Communicator.StudioSetPart(part);
        setPart.ReadFromIntegra();
        ForceUiRefresh(setPart.StartAddressName, setPart.OffsetAddressName);

        if (GetSelectedPreset(part)?.ToneTypeStr == "PCMS")
        {
            DomainBase setPCMSTone = _integra7Communicator.PCMSynthToneCommon(part);
            setPCMSTone.ReadFromIntegra();
            ForceUiRefresh(setPCMSTone.StartAddressName, setPCMSTone.OffsetAddressName);
            switch (part)
            {
                case 0: this.RaisePropertyChanged(nameof(SelectedPresetCh0IsSynthTone)); break;
                case 1: this.RaisePropertyChanged(nameof(SelectedPresetCh1IsSynthTone)); break;
                case 2: this.RaisePropertyChanged(nameof(SelectedPresetCh2IsSynthTone)); break;
                case 3: this.RaisePropertyChanged(nameof(SelectedPresetCh3IsSynthTone)); break;
                case 4: this.RaisePropertyChanged(nameof(SelectedPresetCh4IsSynthTone)); break;
                case 5: this.RaisePropertyChanged(nameof(SelectedPresetCh5IsSynthTone)); break;
                case 6: this.RaisePropertyChanged(nameof(SelectedPresetCh6IsSynthTone)); break;
                case 7: this.RaisePropertyChanged(nameof(SelectedPresetCh7IsSynthTone)); break;
                case 8: this.RaisePropertyChanged(nameof(SelectedPresetCh8IsSynthTone)); break;
                case 9: this.RaisePropertyChanged(nameof(SelectedPresetCh9IsSynthTone)); break;
                case 10: this.RaisePropertyChanged(nameof(SelectedPresetCh10IsSynthTone)); break;
                case 11: this.RaisePropertyChanged(nameof(SelectedPresetCh11IsSynthTone)); break;
                case 12: this.RaisePropertyChanged(nameof(SelectedPresetCh12IsSynthTone)); break;
                case 13: this.RaisePropertyChanged(nameof(SelectedPresetCh13IsSynthTone)); break;
                case 14: this.RaisePropertyChanged(nameof(SelectedPresetCh14IsSynthTone)); break;
                case 15: this.RaisePropertyChanged(nameof(SelectedPresetCh15IsSynthTone)); break;
                default: break;
            }
        }
    }


#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS8618 // nullable must be assigned in constructor
}

