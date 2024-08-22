using System.Collections.Generic;
using Integra7AuralAlchemist.Models.Data;
using Integra7AuralAlchemist.Models.Domain;
using Integra7AuralAlchemist.Models.Services;


public class SysexDataTransmissionParser
{
    public static List<UpdateMessageSpec> ConvertSysexToParameterUpdates(byte[] sysexMsg, Integra7Domain? i7)
    {
        List<UpdateMessageSpec> result = [];
        if (Integra7SysexHelpers.CheckIsDataSetMsg(sysexMsg))
        {
            byte[] payload = Integra7SysexHelpers.ExtractPayload(sysexMsg);

            int currentLocation = 0;
            while (currentLocation < payload.Length)
            {
                byte[] address = ByteUtils.Slice(payload, currentLocation, currentLocation + 4);
                currentLocation += 4; // skip address
                FullyQualifiedParameter? p = i7?.LookupAddress(address);
                int? bytes = p?.ParSpec.Bytes;
                byte[] parResult = ByteUtils.Slice(payload, currentLocation, bytes ?? 0);
                if (p is not null)
                {
                    SysexParameterValueInterpreter.Interpret(parResult, p?.ParSpec, out long rawVal, out string displayValue);
                    result.Add(new UpdateMessageSpec(p, displayValue));
                }
                currentLocation += bytes ?? 0 /* skip parameter data */;
            }
        }
        return result;
    }
}