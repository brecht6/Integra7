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

public sealed partial class PCMDrumKitPartialViewModel : PartialViewModel
{
    private int _partialOffset = Constants.FIRST_PARTIAL_DRUM;
    private readonly SourceCache<FullyQualifiedParameter, string> _sourceCachePCMDrumKitPartialParameters =
        new(x => x.ParSpec.Path);
    private readonly ReadOnlyObservableCollection<FullyQualifiedParameter> _PCMDrumKitPartialParameters = new([]);
    public ReadOnlyObservableCollection<FullyQualifiedParameter> PCMDrumKitPartialParameters =>
        _PCMDrumKitPartialParameters;
    
    [Reactive] private string _searchTextPCMDrumKitPartial = "";
    [Reactive] private string _refreshPCMDrumKitPartial = "";
    
    IDisposable? _cleanupPCMDrumKitPartialParameters;
    
    public PCMDrumKitPartialViewModel(PartViewModel parent, byte zeroBasedPart, byte zeroBasedPartial, 
        string toneTypeStr, Integra7StartAddresses i7addr,
        Integra7Parameters par, IIntegra7Api i7api, Integra7Domain i7dom) : 
        base(parent, zeroBasedPart, zeroBasedPartial, toneTypeStr, i7addr, par, i7api, i7dom)
    {
        var parFilterPCMDrumKitPartialParameters = this.WhenAnyValue(x => x.SearchTextPCMDrumKitPartial)
            .Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .DistinctUntilChanged()
            .Select(FilterProvider.ParameterFilter);
        var refreshFilterPCMDrumKitPartialParameters = this.WhenAnyValue(x => x.RefreshPCMDrumKitPartial)
            .Select(FilterProvider.ParameterFilter);
        
        _cleanupPCMDrumKitPartialParameters = _sourceCachePCMDrumKitPartialParameters.Connect()
            .Filter(refreshFilterPCMDrumKitPartialParameters)
            .Throttle(TimeSpan.FromMilliseconds(Constants.THROTTLE))
            .Filter(parFilterPCMDrumKitPartialParameters)
            .FilterOnObservable(fullyQualifiedParameter =>
                ((fullyQualifiedParameter.ParSpec.ParentCtrl != "") &&
                 (fullyQualifiedParameter.ParSpec.ParentCtrl is string parentId))
                    ? _sourceCachePCMDrumKitPartialParameters
                        .Watch(parentId)
                        .Select(parentChange => parentChange.Current.StringValue ==
                                                fullyQualifiedParameter.ParSpec.ParentCtrlDispValue)
                    : Observable.Return(true))
            .FilterOnObservable(fullyQualifiedParameter =>
                ((fullyQualifiedParameter.ParSpec.ParentCtrl2 != "") &&
                 (fullyQualifiedParameter.ParSpec.ParentCtrl2 is string parentId2))
                    ? _sourceCachePCMDrumKitPartialParameters
                        .Watch(parentId2)
                        .Select(parentChange2 => parentChange2.Current.StringValue ==
                                                 fullyQualifiedParameter.ParSpec.ParentCtrlDispValue2)
                    : Observable.Return(true))
            .ObserveOn(RxApp.MainThreadScheduler)
            .SortAndBind(
                out _PCMDrumKitPartialParameters,
                SortExpressionComparer<FullyQualifiedParameter>.Ascending(t =>
                    ByteUtils.Bytes7ToInt(t.ParSpec.Address)))
            .DisposeMany()
            .Subscribe();

        InitializeParameterSourceCaches();
    }

    public override int GetPartialOffset()
    {
        return 21;
    }

    public override string GetPartialName()
    {
        return "";
    }
    
    public override string GetSearchTextPartial()
    {
        return _searchTextPCMDrumKitPartial;
    }

    public override void SetSearchTextPartial(string value)
    {
        _searchTextPCMDrumKitPartial = value;
    }

    public override ReadOnlyObservableCollection<FullyQualifiedParameter> GetPartialParameters()
    {
        return _PCMDrumKitPartialParameters;
    }

    public override void ForceUiRefresh(string startAddressName, string offsetAddressName, string offset2AddressName, string parPath, bool resyncNeeded)
    {
        if (startAddressName == $"Temporary Tone Part {_zeroBasedPart + 1}" &&
            offset2AddressName == $"Offset2/PCM Drum Kit Partial {_zeroBasedPartial + 1}")
        {
            RefreshPCMDrumKitPartial = ".";
            RefreshPCMDrumKitPartial = SearchTextPCMDrumKitPartial;
        }
    }

    public override bool IsValidForCurrentPreset()
    {
        return _toneTypeStr == "PCMD";
    }
    
    public override void InitializeParameterSourceCaches()
    {
        if (_i7domain == null)
            return;
        
        if (IsValidForCurrentPreset())
        {
            _i7domain.PCMDrumKitPartial(_zeroBasedPart, _zeroBasedPartial).ReadFromIntegra();   
        }
        List<FullyQualifiedParameter> par2 = _i7domain.PCMDrumKitPartial(_zeroBasedPart, _zeroBasedPartial)
            .GetRelevantParameters(true, true);
        _sourceCachePCMDrumKitPartialParameters.AddOrUpdate(par2);
    }

    public override void ResyncPart(byte part)
    {
        if (part == _zeroBasedPart && IsValidForCurrentPreset())
        {
            DomainBase b = _i7domain.PCMDrumKitPartial(_zeroBasedPart, _zeroBasedPartial);
            b.ReadFromIntegra();
            ForceUiRefresh(b.StartAddressName, b.OffsetAddressName, b.Offset2AddressName, "",
                false /* don't cause inf loop */);
        }
    }
    
}