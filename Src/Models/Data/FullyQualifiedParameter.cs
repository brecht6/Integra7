using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Integra7AuralAlchemist.Models.Services;
using Serilog;

namespace Integra7AuralAlchemist.Models.Data;

public class FullyQualifiedParameter : INotifyPropertyChanged
{
    private readonly string _start;
    public string Start => _start;
    private readonly string _offset;
    public string Offset => _offset;
    private readonly string _offset2;
    public string Offset2 => _offset2;
    private readonly Integra7ParameterSpec _parspec;
    public Integra7ParameterSpec ParSpec => _parspec;

    private bool _numeric;
    public bool IsNumeric => _numeric;
    private long _rawNumericValue;

    public long RawNumericValue
    {
        get => _rawNumericValue;
        set => _rawNumericValue = value;
    }

    private string _stringValue = "";

    public string StringValue
    {
        get => _stringValue;

        set
        {
            //Debug.Write($"changing _stringValue from {_stringValue} to {value}.");
            _stringValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StringValue)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public FullyQualifiedParameter(string start, string offset, string offset2, Integra7ParameterSpec parspec)
    {
        _start = start;
        _offset = offset;
        _offset2 = offset2;
        _parspec = parspec;
        _numeric = parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC;
    }

    public FullyQualifiedParameter(string start, string offset, string offset2, Integra7ParameterSpec parspec,
        long rawNumericValue, string stringValue)
    {
        _start = start;
        _offset = offset;
        _offset2 = offset2;
        _parspec = parspec;
        _numeric = parspec.Type == Integra7ParameterSpec.SpecType.NUMERIC;
        _rawNumericValue = rawNumericValue;
        _stringValue = stringValue;
    }

    public bool ValidInContext(ParserContext ctx)
    {
        if (ParSpec.ParentCtrl != "")
        {
            if (ctx.Contains(ParSpec.ParentCtrl))
            {
                string value = ctx.Lookup(ParSpec.ParentCtrl);
                bool stillValid = ParSpec.ParentCtrlDispValue == value;
                if (stillValid)
                {
                    if (ParSpec.ParentCtrl2 != "")
                    {
                        // StillValid, but a second level dependency also must be fulfilled
                        if (ctx.Contains(ParSpec.ParentCtrl2))
                        {
                            string value2 = ctx.Lookup(ParSpec.ParentCtrl2);
                            return value2 == ParSpec.ParentCtrlDispValue2;
                        }

                        Debug.Assert(false,
                            $"Cannot parse {ParSpec.Path} without context {ParSpec.ParentCtrl2}. Did you forget to set isparent==true in {ParSpec.ParentCtrl}?");
                        return false;
                    }

                    return true; // StillValid and no need to check second level dependency
                }

                return false; // no longer valid, no need to check deeper
            }

            Debug.Assert(false, $"Cannot parse {ParSpec.Path} without context {ParSpec.ParentCtrl}");
            return false;
        }

        return true;
    }

    public byte[] CompleteAddress(Integra7StartAddresses startAddresses, Integra7Parameters parameters)
    {
        byte[] startAddr = startAddresses.Lookup(_start).Address;
        byte[] offsetAddr = startAddresses.Lookup(_offset).Address;
        byte[] offset2Addr = startAddresses.Lookup(_offset2).Address;
        byte[] parameterAddr = _parspec.Address;
        byte[] totalAddr = ByteUtils.AddressWithOffset(startAddr, offsetAddr, offset2Addr, parameterAddr);
        return totalAddr;
    }

    public void RetrieveFromIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses,
        Integra7Parameters parameters)
    {
        byte[] totalAddr = CompleteAddress(startAddresses, parameters);
        byte[] reply = integra7Api.MakeDataRequest(totalAddr, _parspec.Bytes);
        ParseFromSysexReply(reply, parameters);
    }

    public void WriteToIntegra(IIntegra7Api integra7Api, Integra7StartAddresses startAddresses,
        Integra7Parameters parameters)
    {
        byte[] totalAddr = CompleteAddress(startAddresses, parameters);
        byte[] data = GetSysexDataFragment();
        integra7Api.MakeDataTransmission(totalAddr, data);
    }

    public void ParseFromSysexReply(byte[] reply, Integra7Parameters parameters,
        Integra7ParameterSpec? firstParameterInSysexReply = null)
    {
        if (firstParameterInSysexReply == null)
        {
            firstParameterInSysexReply = _parspec;
        }

        const int SYSEX_DATA_REPLY_HEADER_LENGTH = 11;
        List<Integra7ParameterSpec> parametersInSysexReply =
            parameters.GetParametersFromTo(firstParameterInSysexReply.Path, _parspec.Path);
        int dataToSkip = SYSEX_DATA_REPLY_HEADER_LENGTH;
        int gap = ParameterListSysexSizeCalculator.CalculateSysexGapBetweenFirstAndLast(parametersInSysexReply);
        dataToSkip += gap;
        if (reply.Length > dataToSkip + _parspec.Bytes)
        {
            byte[] parResult = ByteUtils.Slice(reply, dataToSkip, _parspec.Bytes);
            SysexParameterValueInterpreter.Interpret(parResult, _parspec, out _rawNumericValue, out _stringValue);
        }
        else
        {
            Log.Error(
                $"Sysex msg out of data while trying to parse {_parspec.Path} from sysex reply. Are we looking at the wrong reply?");
        }
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

        if (_stringValue.Length > _parspec.Bytes)
        {
            _stringValue = _stringValue[.._parspec.Bytes]; // clip to max length
        }

        return ByteUtils.PadString(Encoding.ASCII.GetBytes(_stringValue), _parspec.Bytes);
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
            double mapped = Mapping.linlin(RawNumericValue, ParSpec.IMin, ParSpec.IMax, ParSpec.OMin, ParSpec.OMax);
            if (!float.IsNaN(ParSpec.IMin2) && !float.IsNaN(ParSpec.IMax2) && !float.IsNaN(ParSpec.OMin2) && !float.IsNaN(ParSpec.OMax2))
            {
                mapped = Mapping.linlin(mapped, ParSpec.IMin2, ParSpec.IMax2, ParSpec.OMin2, ParSpec.OMax2);
            }
            Log.Debug($"{Wrn} parameter {ParSpec.Path} at parameter address {address} has value raw {RawNumericValue}, mapped {Math.Round(mapped, 2)}, (meaning: {StringValue}{unit})");
        }
        else
        {
            Log.Debug($"{Wrn} parameter {ParSpec.Path} at parameter address {address} has value \"{StringValue}\"");
        }
    }
}