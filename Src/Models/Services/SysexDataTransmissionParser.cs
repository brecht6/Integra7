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
        foreach (byte[] s in sysexMsgList)
        {
            if (s != null)
            {
                if (Integra7SysexHelpers.CheckIsDataSetMsg(s))
                {
                    byte[] payload = Integra7SysexHelpers.ExtractPayload(s);

                    int currentLocation = 0;
                    byte[] address = ByteUtils.Slice(payload, currentLocation, 4);
                    currentLocation += 4; // skip address
                    while (currentLocation < payload.Length)
                    {
                        FullyQualifiedParameter? p = i7?.LookupAddress(address);
                        int? bytes = p?.ParSpec.Bytes;
                        byte[] parResult = ByteUtils.Slice(payload, currentLocation, bytes ?? 0);
                        if (p is not null)
                        {
                            SysexParameterValueInterpreter.Interpret(parResult, p?.ParSpec, out long rawVal, out string displayValue);
                            result.Add(new UpdateMessageSpec(p, displayValue));
                        }
                        currentLocation += bytes ?? 0 /* skip parameter data */;
                        address = ByteUtils.IntToBytes7_4(ByteUtils.Bytes7ToInt(address) + bytes ?? 0);
                    }
                }
            }
        }
        return result;
    }
}