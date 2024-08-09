
using System.Collections.Generic;
using System.Diagnostics;
using Integra7AuralAlchemist.Models.Services;
using ReactiveUI;

namespace Integra7AuralAlchemist.Models.Data;

public class FullyQualifiedParameter
{
    private readonly string _start;
    private readonly string _offset;
    private readonly Integra7ParameterSpec _parspec;
    public Integra7ParameterSpec ParSpec { get => _parspec; }

    private bool _numeric;
    public bool IsNumeric { get => _numeric; }
    private long _rawNumericValue = 0;
    public long RawNumericValue { get => _rawNumericValue; }
    private string _stringValue = "";
    public string StringValue { get => _stringValue; }

    public FullyQualifiedParameter(string start, string offset, Integra7ParameterSpec parspec)
    {
        _start = start;
        _offset = offset;
        _parspec = parspec;
        _numeric = parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC;
    }

    public void RetrieveFromIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        byte[] startAddr = startAddresses.Lookup(_start).Address;
        byte[] offsetAddr = startAddresses.Lookup(_offset).Address;
        byte[] parameterAddr = _parspec.Address;
        byte[] totalAddr = ByteUtils.AddressWithOffset(startAddr, offsetAddr, parameterAddr);
        byte[] reply = integra7Api.MakeDataRequest(totalAddr, _parspec.Bytes);
        ParseFromSysexReply(reply, parameters);
    }

    void ParseFromSysexReply(byte[] reply, Integra7Parameters parameters, Integra7ParameterSpec? firstParameterInSysexReply = null)
    {
        if (firstParameterInSysexReply == null)
        {
            firstParameterInSysexReply = _parspec;
        }
        const int SYSEX_DATA_REPLY_HEADER_LENGTH = 11;
        List<Integra7ParameterSpec> parametersInSysexReply = parameters.GetParametersFromTo(firstParameterInSysexReply.Path, _parspec.Path);
        int dataToSkip = SYSEX_DATA_REPLY_HEADER_LENGTH;
        for (int i = 0; i < parametersInSysexReply.Count - 1; i++)
        {
            dataToSkip += parametersInSysexReply[i].Bytes;
        }
        byte[] parResult = ByteUtils.Slice(reply, dataToSkip, _parspec.Bytes);
        if (_numeric)
        {
            _rawNumericValue = ByteUtils.Bytes7ToInt(parResult);
            if (_parspec.Repr != null)
            {
                _stringValue = _parspec.Repr[(int)_rawNumericValue];
            }
            else if (_parspec.IMin != _parspec.OMin || _parspec.IMax != _parspec.OMax) {
                _stringValue = $"{(long)Mapping.linlin(_rawNumericValue, _parspec.IMin, _parspec.IMax, _parspec.OMin, _parspec.OMax)}";
            }
            else
            {
                _stringValue = $"{_rawNumericValue}";
            }
        }
        else 
        {
            _stringValue = System.Text.Encoding.ASCII.GetString(parResult);
        }

    }

}