using System.Collections.Generic;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Data;

public class FullyQualifiedParameterRange
{
    private string _start;
    private string _offset;
    private Integra7ParameterSpec _firstPar;
    private Integra7ParameterSpec _lastPar;
    private List<FullyQualifiedParameter> _range;
    public List<FullyQualifiedParameter> Range => _range ?? [];

    public FullyQualifiedParameterRange(string start, string offset, Integra7ParameterSpec firstPar, Integra7ParameterSpec lastPar)
    {
        _start = start;
        _offset = offset;
        _firstPar = firstPar;
        _lastPar = lastPar;
        _range = new List<FullyQualifiedParameter>();
    }

    public void RetrieveFromIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        byte[] startAddr = startAddresses.Lookup(_start).Address;
        byte[] offsetAddr = startAddresses.Lookup(_offset).Address;
        byte[] firstParameterAddr = _firstPar.Address;
        byte[] lastParameterAddr = _lastPar.Address;
        byte[] totalAddr = ByteUtils.AddressWithOffset(startAddr, offsetAddr, firstParameterAddr);
        List<Integra7ParameterSpec> allRelevantPars = parameters.GetParametersFromTo(_firstPar.Path, _lastPar.Path);
        long size = 0;
        _range.Clear();
        for (int i = 0; i < allRelevantPars.Count; i++)
        {
            size += allRelevantPars[i].Bytes;
            _range.Add(new FullyQualifiedParameter(_start, _offset, allRelevantPars[i]));
        }

        byte[] reply = integra7Api.MakeDataRequest(totalAddr, size);
        ParseFromSysexReply(reply, parameters, _firstPar);
    }

    public void ParseFromSysexReply(byte[] reply, Integra7Parameters parameters, Integra7ParameterSpec? firstParameterInSysexReply = null)
    {
        for (int i = 0; i < _range.Count; i++)
        {
            FullyQualifiedParameter p = _range[i];
            p.ParseFromSysexReply(reply, parameters, firstParameterInSysexReply);
            p.DebugLog();
        }
    }
}