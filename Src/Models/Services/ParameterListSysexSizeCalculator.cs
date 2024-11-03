using System.Collections.Generic;
using System.Linq;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Models.Services;

public class ParameterListSysexSizeCalculator
{
    public static int CalculateSysexSize(List<Integra7ParameterSpec> listOfConsecutiveParameterSpecs)
    {
        // calculate sysex size by adding up size for each individual parameter
        // care must be taken not to count duplicate parameters multiple times
        // duplicate parameters occur when modeling data dependencies (where some parameters change meaning based on the value of another parameter)
        var size = 0;
        HashSet<string> avoidDoubleCounting = []; // data dependencies make everything harder
        for (var i = 0; i < listOfConsecutiveParameterSpecs.Count; i++)
        {
            var dataDependentParameter = listOfConsecutiveParameterSpecs[i].ParentCtrl != "";
            var commonPrefix = ""; // common prefix is how the system recognizes parameters that are "duplicates"
            if (dataDependentParameter)
                commonPrefix = string.Join("/", listOfConsecutiveParameterSpecs[i].Path.Split("/")[..2]);
            if (!avoidDoubleCounting
                    .Contains(commonPrefix)) // data dependent parameters occur multiple times but should be counted only once
            {
                size += listOfConsecutiveParameterSpecs[i].Bytes;
                if (commonPrefix != "") avoidDoubleCounting.Add(commonPrefix);
            }
            //Debug.WriteLine($"Not counting {allRelevantPars[i].Path} multiple times.");   
        }

        return size;
    }

    public static int CalculateSysexGapBetweenFirstAndLast(List<Integra7ParameterSpec> listOfConsecutiveParameterSpec)
    {
        // calculate sysex size, then subtract size of last entry
        // this way of working correctly handles cases where the last parameters are duplicates for data dependency modeling
        var size = CalculateSysexSize(listOfConsecutiveParameterSpec);
        size -= listOfConsecutiveParameterSpec.Last().Bytes;
        return size;
    }
}