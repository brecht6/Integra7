using System;
using System.Collections.Generic;
using System.Diagnostics;
using Integra7AuralAlchemist.Models.Data;

namespace Integra7AuralAlchemist.Models.Services;

public class Integra7ParameterDatabaseAnalyzer
{
    public static void CheckProgrammingErrorDuplicatePaths(IList<Integra7ParameterSpec> database)
    {
        HashSet<string> PathsEncountered = [];
        foreach (Integra7ParameterSpec s in database)
        {
            if (PathsEncountered.Contains(s.Path))
            {
                Debug.WriteLine($"Path {s.Path} is used multiple times. Please fix.");
            }
            PathsEncountered.Add(s.Path);
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
        IDictionary<string, Tuple<string, string> > ParametersDependingOnOtherParameter = new Dictionary<string, Tuple<string, string>>();
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
                    new Tuple<string, string>(b.Item1,b.Item2), 
                    new Tuple<string, string>(c.Item1,c.Item2)));
                if (ParametersDependingOnOtherParameter.ContainsKey(c.Item1))
                {
                    Debug.Assert(false, "3-level deep dependencies not supported!");
                }
            }
        }
        // pass three, update database with the two level dependencies
        foreach (Integra7ParameterSpec s in database)
        {
            foreach (Tuple<string, Tuple<string, string>, Tuple<string, string> > abc in twoLevelDep)
            {
                if (s.Path == abc.Item1)
                {
                    string b = abc.Item2.Item1;
                    string b_disp = abc.Item2.Item2;
                    string c  = abc.Item3.Item1;
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