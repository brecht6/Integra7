using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Integra7AuralAlchemist.Models.Services;

namespace Integra7AuralAlchemist.Models.Data;

public class FullyQualifiedParameter
{
    private readonly string _start;
    public string Start { get => _start; }
    private readonly string _offset;
    public string Offset { get => _offset; }
    private readonly Integra7ParameterSpec _parspec;
    public Integra7ParameterSpec ParSpec { get => _parspec; }

    private bool _numeric;
    public bool IsNumeric { get => _numeric; }
    private long _rawNumericValue = 0;
    public long RawNumericValue { get => _rawNumericValue; set => _rawNumericValue = value; }
    private string _stringValue = "";
    public string StringValue { get => _stringValue; set => _stringValue = value; }

    public FullyQualifiedParameter(string start, string offset, Integra7ParameterSpec parspec)
    {
        _start = start;
        _offset = offset;
        _parspec = parspec;
        _numeric = parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC;
    }

    public FullyQualifiedParameter(string start, string offset, Integra7ParameterSpec parspec, long rawNumericValue, string stringValue)
    {
        _start = start;
        _offset = offset;
        _parspec = parspec;
        _numeric = parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC;
        _rawNumericValue = rawNumericValue;
        _stringValue = stringValue;
    }

    public bool ValidInContext(ParserContext ctx)
    {
        if (ParSpec.MasterCtrl != "")
        {
            if (ctx.Contains(ParSpec.MasterCtrl))
            {
                string value = ctx.Lookup(ParSpec.MasterCtrl);
                return ParSpec.MasterCtrlDispValue == value;
            }
            else
            {
                Debug.Assert(false, $"Cannot parse {ParSpec.Path} without context {ParSpec.MasterCtrl}");
                return false;
            }
        }

        return true;
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

    public void WriteToIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        byte[] startAddr = startAddresses.Lookup(_start).Address;
        byte[] offsetAddr = startAddresses.Lookup(_offset).Address;
        byte[] firstParameterAddr = _parspec.Address;
        byte[] totalAddr = ByteUtils.AddressWithOffset(startAddr, offsetAddr, firstParameterAddr);
        byte[] data = GetSysexDataFragment();
        integra7Api.MakeDataTransmission(totalAddr, data);
    }

    public void ParseFromSysexReply(byte[] reply, Integra7Parameters parameters, Integra7ParameterSpec? firstParameterInSysexReply = null)
    {
        if (firstParameterInSysexReply == null)
        {
            firstParameterInSysexReply = _parspec;
        }
        const int SYSEX_DATA_REPLY_HEADER_LENGTH = 11;
        List<Integra7ParameterSpec> parametersInSysexReply = parameters.GetParametersFromTo(firstParameterInSysexReply.Path, _parspec.Path);
        int dataToSkip = SYSEX_DATA_REPLY_HEADER_LENGTH;
        int gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(parametersInSysexReply);
        dataToSkip += gap;

        byte[] parResult = ByteUtils.Slice(reply, dataToSkip, _parspec.Bytes);
        SysexParameterValueInterpreter.Interpret(parResult, _parspec, out _rawNumericValue, out _stringValue);
    }

    public byte[] GetSysexDataFragment()
    {
        if (_numeric)
        {
            byte[] sysex = new byte[_parspec.Bytes];
            if (_parspec.PerNibble)
            {
                sysex = ByteUtils.IntToNibbled(_rawNumericValue, _parspec.Bytes);
            }
            else
            {
                if (_parspec.Bytes == 1)
                {
                    sysex = ByteUtils.IntToBytes7_1(_rawNumericValue);
                }
                else if (_parspec.Bytes == 2)
                {
                    sysex = ByteUtils.IntToBytes7_2(_rawNumericValue);
                }
                else if (_parspec.Bytes == 4)
                {
                    sysex = ByteUtils.IntToBytes7_4(_rawNumericValue);
                }
                else
                {
                    Debug.Assert(false);
                }

            }
            return sysex;
        }
        else
        {
            if (_stringValue.Length > _parspec.Bytes)
            {
                _stringValue = _stringValue[.._parspec.Bytes]; // clip to max length
            }
            return ByteUtils.PadString(Encoding.ASCII.GetBytes(_stringValue), _parspec.Bytes);
        }
    }

    public void CopyParsedDataFrom(FullyQualifiedParameter other)
    {
        _numeric = other.IsNumeric;
        _rawNumericValue = other.RawNumericValue;
        _stringValue = other.StringValue;
    }

    public void DebugLog()
    {
        StringBuilder hex = new StringBuilder(ParSpec.Address.Length * 2);
        for (int i = 0; i < ParSpec.Address.Length; i++)
        {
            hex.AppendFormat("{0:x2} ", ParSpec.Address[i]);
        }
        string address = "[ " + hex.ToString() + "]";
        string Wrn = "";
        if (ParSpec.Reserved)
        {
            Wrn = " (reserved!)";
        }
        string unit = "";
        if (ParSpec.Unit != "")
        {
            unit = "[" + ParSpec.Unit + "]";
        }
        if (IsNumeric)
        {
            Debug.WriteLine($"[DebugLog] {Wrn} parameter {ParSpec.Path} at parameter address {address} has value raw {RawNumericValue} (meaning: {StringValue}{unit})");
        }
        else
        {
            Debug.WriteLine($"[DebugLog] {Wrn} parameter {ParSpec.Path} at parameter address {address} has value \"{StringValue}\"");
        }
    }
}