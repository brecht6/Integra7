using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Models.Services;

public class Integra7ParameterDatabaseAnalyzer
{
    public static void CheckProgrammingErrorDuplicatePaths(IList<Integra7ParameterSpec> database)
    {
        HashSet<string> PathsEncountered = [];
        string previousCommonPrefix = "";
        long prevAddress = 0;
        string prevMst = "";
        string prevMstVal = "";
        string prevMst2 = "";
        string prevMstVal2 = "";
        long prevBytes = 0;

        foreach (Integra7ParameterSpec s in database)
        {
            string[] el = s.Path.Split('/');
            foreach (string e in el)
            {
                if (e == "")
                {
                    Debug.WriteLine($"double slash found in {el}. Please fix.");
                }
                else
                {
                    if (e[0] == ' ' || e[^1] == ' ')
                    {
                        Debug.WriteLine($"Extra spaces found in path for {s.Path}. Please fix.");
                    }
                }
                if (s.MasterCtrl != "")
                {
                    if (s.MasterCtrl[0] == ' ' || s.MasterCtrl[^1] == ' ')
                    {
                        Debug.WriteLine($"Extra spaces found in mst:path for {s.Path}. Please fix.");
                    }
                }
                if (s.MasterCtrl2 != "")
                {
                    if (s.MasterCtrl2[0] == ' ' || s.MasterCtrl2[^1] == ' ')
                    {
                        Debug.WriteLine($"Extra spaces found in mst2:path for {s.Path}. Please fix.");
                    }
                }
            }

            if (PathsEncountered.Contains(s.Path))
            {
                Debug.WriteLine($"Path {s.Path} is used multiple times. Please fix.");
            }
            PathsEncountered.Add(s.Path);
            if (s.Path.Contains("Reserved") && !s.Reserved)
            {
                Debug.WriteLine($"Path {s.Path} is named reserved but doesn't have Reserved flag. Please fix.");
            }
            if (!s.Path.Contains("Reserved") && s.Reserved)
            {
                Debug.WriteLine($"Path {s.Path} probably shouldn't have its Reserved flag turned on (otherwise, use Reserved in its name). Please fix.");
            }
            if (s.MasterCtrl != "")
            {
                if (!PathsEncountered.Contains(s.MasterCtrl))
                {
                    Debug.WriteLine($"Path {s.Path} refers to a non-existing mst:{s.MasterCtrl}. Please fix. Parameters can only depend on parameters that came before them.");
                }
            }
            if (s.MasterCtrl2 != "")
            {
                if (!PathsEncountered.Contains(s.MasterCtrl2))
                {
                    Debug.WriteLine($"Path {s.Path} refers to a non-existing mst2:{s.MasterCtrl2}. Please fix. Parameters can only depend on parameters that came before them.");
                }
            }
            if (previousCommonPrefix != "")
            {
                int noOfSlash = s.Path.Count(c => c == '/');
                string newCommonPrefix = String.Join("/", s.Path.Split("/")[..noOfSlash]);
                if (newCommonPrefix == previousCommonPrefix)
                {
                    long newAddress = ByteUtils.Bytes7ToInt(s.Address);
                    if ((newAddress <= prevAddress && s.MasterCtrl == "") || (newAddress < prevAddress && s.MasterCtrl != ""))
                    {
                        Debug.WriteLine($"Successive offsets/addresses should increase at {s.Path}. Please check.");
                    }
                    if (s.MasterCtrl == prevMst && s.MasterCtrlDispValue == prevMstVal && s.MasterCtrl2 == prevMst2 && s.MasterCtrlDispValue2 == prevMstVal2)
                    {
                        if (prevMst != "" && prevMst2 == "")
                        {
                            Debug.WriteLine($"{s.Path}: No two parameters should have exact same mst:{prevMst}, mstval:{prevMstVal}. Please fix.");
                        }
                        if (prevMst != "" && prevMst2 != "")
                        {
                            Debug.WriteLine($"{s.Path}: No two parameters should have exact same mst:{prevMst}, mstval:{prevMstVal}, mst2:{prevMst2}, mstval2:{prevMstVal2}. Please fix.");
                        }
                    }
                    if (ByteUtils.Bytes7ToInt(s.Address) != prevAddress + prevBytes)
                    {
                        if (prevMstVal == s.MasterCtrlDispValue)
                            Debug.WriteLine($"{s.Path}: something seems fishy with the offset address. It doesn't correspond to previous address + previous #bytes). Please check.");
                    }
                    previousCommonPrefix = newCommonPrefix;
                    prevAddress = newAddress;
                    prevMst = s.MasterCtrl;
                    prevMstVal = s.MasterCtrlDispValue;
                    prevMst2 = s.MasterCtrl2;
                    prevMstVal2 = s.MasterCtrlDispValue2;
                    prevBytes = s.Bytes;
                }
                else
                {
                    int noOfSlash2 = s.Path.Count(c => c == '/');
                    previousCommonPrefix = String.Join("/", s.Path.Split("/")[..noOfSlash2]);
                    long address = ByteUtils.Bytes7ToInt(s.Address);
                    prevAddress = address;
                    prevMst = s.MasterCtrl;
                    prevMstVal = s.MasterCtrlDispValue;
                    prevMst2 = s.MasterCtrl2;
                    prevMstVal2 = s.MasterCtrlDispValue2;
                    prevBytes = s.Bytes;
                }
            }
            else
            {
                int noOfSlash = s.Path.Count(c => c == '/');
                previousCommonPrefix = String.Join("/", s.Path.Split("/")[..noOfSlash]);
                long address = ByteUtils.Bytes7ToInt(s.Address);
                prevAddress = address;
                prevMst = s.MasterCtrl;
                prevMstVal = s.MasterCtrlDispValue;
                prevMst2 = s.MasterCtrl2;
                prevMstVal2 = s.MasterCtrlDispValue2;
                prevBytes = s.Bytes;
            }

            if (!s.Reserved)
            {
                if (s.OMin == -20000) // generic parameter
                {
                    if (s.Repr == null) // no repr to determine ui limits
                    {
                        if (float.IsNaN(s.IMin2)) // no omin2 to determine ui limit
                        {
                            Debug.WriteLine($"{s.Path} does not specify a usable ui limit. Please add imin2, imax2, omin2, omax2 or repr");
                        }
                    }
                }
            }
        }
    }

    public static void MarkAllMasterParametersAsStoreTrue(IList<Integra7ParameterSpec> database)
    {
        HashSet<string> ParametersRequiringStoreTrue = [];
        // pass one: collect all master parameters
        foreach (Integra7ParameterSpec s in database)
        {
            if (s.MasterCtrl != "")
            {
                ParametersRequiringStoreTrue.Add(s.MasterCtrl);
            }
            if (s.MasterCtrl2 != "")
            {
                ParametersRequiringStoreTrue.Add(s.MasterCtrl2);
            }
        }

        // pass two: mark all master parameters as master parameters
        foreach (Integra7ParameterSpec s in database)
        {
            if (ParametersRequiringStoreTrue.Contains(s.Path))
            {
                s.Store = true;
            }
        }
    }

    public static void FillInSecondaryDependencies(IList<Integra7ParameterSpec> database)
    {
        IDictionary<string, Tuple<string, string>> ParametersDependingOnOtherParameter = new Dictionary<string, Tuple<string, string>>();
        // pass one: collect all parameters that depend on another parameter
        foreach (Integra7ParameterSpec s in database)
        {
            if (s.MasterCtrl != "")
            {
                ParametersDependingOnOtherParameter[s.Path] = new Tuple<string, string>(s.MasterCtrl, s.MasterCtrlDispValue);
            }
            if (s.MasterCtrl2 != "")
            {
                ParametersDependingOnOtherParameter[s.Path] = new Tuple<string, string>(s.MasterCtrl2, s.MasterCtrlDispValue2);
            }
        }

        // pass two, for each parameter taht depends on another parameter, check if that other parameter in turn also depends on another parameter
        IList<Tuple<string, Tuple<string, string>, Tuple<string, string>>> twoLevelDep = [];
        foreach (string a in ParametersDependingOnOtherParameter.Keys)
        {
            Tuple<string, string> b = ParametersDependingOnOtherParameter[a];
            if (ParametersDependingOnOtherParameter.ContainsKey(b.Item1))
            {
                // a depends on b, and b depends on c
                Tuple<string, string> c = ParametersDependingOnOtherParameter[b.Item1];
                twoLevelDep.Add(new Tuple<string, Tuple<string, string>, Tuple<string, string>>(
                    a,
                    new Tuple<string, string>(b.Item1, b.Item2),
                    new Tuple<string, string>(c.Item1, c.Item2)));
                //Debug.WriteLine($"{a} depends on {b.Item1}[{b.Item2}] and {c.Item1}[{c.Item2}]");
                if (ParametersDependingOnOtherParameter.ContainsKey(c.Item1))
                {
                    Debug.Assert(false, "3-level deep dependencies not supported!");
                }
            }
        }
        // pass three, update database with the two level dependencies
        foreach (Integra7ParameterSpec s in database)
        {
            foreach (Tuple<string, Tuple<string, string>, Tuple<string, string>> abc in twoLevelDep)
            {
                if (s.Path == abc.Item1)
                {
                    string b = abc.Item2.Item1;
                    string b_disp = abc.Item2.Item2;
                    string c = abc.Item3.Item1;
                    string c_disp = abc.Item3.Item2;
                    s.MasterCtrl = c;
                    s.MasterCtrlDispValue = c_disp;
                    s.MasterCtrl2 = b;
                    s.MasterCtrlDispValue2 = b_disp;
                }
            }
        }
    }
}