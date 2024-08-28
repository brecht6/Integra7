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

public partial class PartialViewModel : ViewModelBase
{
    private PartViewModel _parent;
    private byte _zeroBasedPart;
    private byte _zeroBasedPartial;
    private Integra7StartAddresses _i7addr;
    private Integra7Parameters _i7par;
    private IIntegra7Api _i7api;
    private Integra7Domain _i7domain;
    public Integra7Domain I7Domain
    {
        set => _i7domain = value; 
    }

    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMSynthTonePartialParameters = new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMSynthTonePartialParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMSynthTonePartialParameters => _PCMSynthTonePartialParameters;

    [Reactive] private string _searchTextPCMSynthTonePartial = "";
    [Reactive] private string _refreshPCMSynthTonePartial = "";
    
    IDisposable? _cleanupPCMSynthTonePartialParameters;
    
    public PartialViewModel(PartViewModel parent, byte zeroBasedPart, byte zeroBasedPartial, Integra7StartAddresses i7addr,
        Integra7Parameters par, IIntegra7Api i7api, Integra7Domain i7dom)
    {
        _parent = parent;
        _zeroBasedPart = zeroBasedPart;
        _zeroBasedPartial = zeroBasedPartial;
        _i7addr = i7addr;
        _i7par = par;
        _i7api = i7api;
        _i7domain = i7dom;

        const int THROTTLE = 250;
        
        var parFilterPCMSynthTonePartialParameters = this.WhenAnyValue(x => x.SearchTextPCMSynthTonePartial)
            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
            .DistinctUntilChanged()
            .Select(FilterProvider.ParameterFilter);
        var refreshFilterPCMSynthTonePartialParameters = this.WhenAnyValue(x => x.RefreshPCMSynthTonePartial)
            .Select(FilterProvider.ParameterFilter);

        _cleanupPCMSynthTonePartialParameters = _sourceCachePCMSynthTonePartialParameters.Connect()
            .Filter(refreshFilterPCMSynthTonePartialParameters)
            .Throttle(TimeSpan.FromMilliseconds(THROTTLE))
            .Filter(parFilterPCMSynthTonePartialParameters)
            .FilterOnObservable(par => ((par.ParSpec.ParentCtrl != "") && (par.ParSpec.ParentCtrl is string parentId))
                ? _sourceCachePCMSynthTonePartialParameters
                    .Watch(parentId)
                    .Select(parentChange => parentChange.Current.StringValue == par.ParSpec.ParentCtrlDispValue)
                : Observable.Return(true))
            .FilterOnObservable(par => ((par.ParSpec.ParentCtrl2 != "") && (par.ParSpec.ParentCtrl2 is string parentId2))
                ? _sourceCachePCMSynthTonePartialParameters
                    .Watch(parentId2)
                    .Select(parentChange2 => parentChange2.Current.StringValue == par.ParSpec.ParentCtrlDispValue2)
                : Observable.Return(true))
            .ObserveOn(RxApp.MainThreadScheduler)
            .SortAndBind(
                out _PCMSynthTonePartialParameters,
                SortExpressionComparer<FullyQualifiedParameter>.Ascending(t => ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
            .DisposeMany()
            .Subscribe();
        
    }

    public void ForceUiRefresh(string StartAddressName, string OffsetAddressName, string ParPath, bool ResyncNeeded)
    {
        if (StartAddressName == $"Temporary Tone Part {_zeroBasedPart + 1}" && OffsetAddressName == $"Offset/PCM Synth Tone Partial {_zeroBasedPartial + 1}")
        {
            RefreshPCMSynthTonePartial = ".";
            RefreshPCMSynthTonePartial = SearchTextPCMSynthTonePartial;
        }
    }
    
    public string Header => $"Partial {_zeroBasedPartial+1}";

    public void InitializeParameterSourceCaches()
    {
        List<FullyQualifiedParameter> par = _i7domain?.PCMSynthTonePartial(_zeroBasedPart, _zeroBasedPartial).GetRelevantParameters(true, true);
        _sourceCachePCMSynthTonePartialParameters.AddOrUpdate(par);
    }

    public void ResyncPart(byte part)
    {
        DomainBase b = _i7domain.PCMSynthTonePartial(_zeroBasedPart, _zeroBasedPartial);
        b.ReadFromIntegra();
        ForceUiRefresh(b.StartAddressName, b.OffsetAddressName, "", false /* don't cause inf loop */);
    }

}