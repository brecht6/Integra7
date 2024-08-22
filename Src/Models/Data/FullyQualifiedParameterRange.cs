using System.Collections.Generic;
using System.Diagnostics;
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

    public void Initialize(List<FullyQualifiedParameter> parameters)
    {
        _range.Clear();
        for (int i = 0; i < parameters.Count; i++)
        {
            _range.Add(new FullyQualifiedParameter(_start, _offset, parameters[i].ParSpec, parameters[i].RawNumericValue, parameters[i].StringValue));
        }
    }

    public void WriteToIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        byte[] startAddr = startAddresses.Lookup(_start).Address;
        byte[] offsetAddr = startAddresses.Lookup(_offset).Address;
        byte[] firstParameterAddr = _firstPar.Address;
        byte[] totalAddr = ByteUtils.AddressWithOffset(startAddr, offsetAddr, firstParameterAddr);
        byte[] data = [];
        ParserContext ctx = new ParserContext();
        ctx.InitializeFromExistingData(_range);
        for (int i = 0; i < _range.Count; i++)
        {
            FullyQualifiedParameter p = _range[i];
            if (p.ValidInContext(ctx))
            {
                data = ByteUtils.Flatten(data, p.GetSysexDataFragment());
            }
        }
        integra7Api.MakeDataTransmission(totalAddr, data);
    }

    public void RetrieveFromIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        byte[] startAddr = startAddresses.Lookup(_start).Address;
        byte[] offsetAddr = startAddresses.Lookup(_offset).Address;
        byte[] firstParameterAddr = _firstPar.Address;
        byte[] totalAddr = ByteUtils.AddressWithOffset(startAddr, offsetAddr, firstParameterAddr);
        List<Integra7ParameterSpec> allRelevantPars = parameters.GetParametersFromTo(_firstPar.Path, _lastPar.Path);
        _range.Clear();
        for (int i = 0; i < allRelevantPars.Count; i++)
        {
            // range must contain all possible FullyQualifiedParameters between the first and last one for parsing.
            // This includes all copies needed for data dependencies.
            _range.Add(new FullyQualifiedParameter(_start, _offset, allRelevantPars[i]));
        }
        // size, however, must not count duplicates needed for data dependencies multiple times since only one of them
        // will be actually used during parsing (based on which value was read for its master control)
        long size = ParameterListSysexSizeCalculator.CalculateSysexSize(allRelevantPars);
        byte[] reply = integra7Api.MakeDataRequest(totalAddr, size);
        if (reply.Length > 0)
        {
            ParseFromSysexReply(reply, parameters, _firstPar);
        }
        else
        {
            Debug.WriteLine("Unfortunately, no reply received after making a sysex data request. This may indicate a bug in the program, e.g. requesting parameters for a PCM synth tone if no PCM synth patch is active or having multiple instances of the application running at the same time.");
        }
    }

    public void ParseFromSysexReply(byte[] reply, Integra7Parameters parameters, Integra7ParameterSpec? firstParameterInSysexReply = null)
    {
        ParserContext ctx = new ParserContext();
        for (int i = 0; i < _range.Count; i++)
        {
            FullyQualifiedParameter p = _range[i];
            if (p.ValidInContext(ctx))
            {
                p.ParseFromSysexReply(reply, parameters, firstParameterInSysexReply);
                if (p.ParSpec.IsParent)
                {
                    ctx.Register(p.ParSpec.Path, p.StringValue);
                }
                p.DebugLog();
            }
        }
    }
}