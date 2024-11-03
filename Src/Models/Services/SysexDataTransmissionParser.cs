using System.Collections.Generic;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Domain;
using Integra7AuralAlchemist.Models.Services;

public class SysexDataTransmissionParser
{
    public static List<UpdateMessageSpec> ConvertSysexToParameterUpdates(byte[] sysexMsg, Integra7Domain? i7)
    {
        List<UpdateMessageSpec> result = [];
        byte[][] sysexMsgList = ByteUtils.SplitAfterF7(sysexMsg);
        foreach (var s in sysexMsgList)
            if (s != null)
                if (Integra7SysexHelpers.CheckIsDataSetMsg(s))
                {
                    var payload = Integra7SysexHelpers.ExtractPayload(s);

                    var currentLocation = 0;
                    var address = ByteUtils.Slice(payload, currentLocation, 4);
                    currentLocation += 4; // skip address
                    while (currentLocation < payload.Length)
                    {
                        var p = i7?.LookupAddress(address);
                        var bytes = p?.ParSpec.Bytes;
                        var parResult = ByteUtils.Slice(payload, currentLocation, bytes ?? 0);
                        if (p is not null)
                        {
                            SysexParameterValueInterpreter.Interpret(parResult, p?.ParSpec, out var rawVal,
                                out var displayValue);
                            result.Add(new UpdateMessageSpec(p, displayValue));
                        }

                        currentLocation += bytes ?? 0 /* skip parameter data */;
                        address = ByteUtils.IntToBytes7_4(ByteUtils.Bytes7ToInt(address) + bytes ?? 0);
                    }
                }

        return result;
    }
}