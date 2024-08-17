
using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.OpenGL;
using Integra7AuralAlchemist.Models.Data;
using ReactiveUI;

namespace Integra7AuralAlchemist.DataTemplates;

public static class DataTemplateProvider
{
    private static Control BuildParameterValuePresenter(FullyQualifiedParameter p)
    {
        if (!p.IsNumeric)
        {
            TextBox b = new() { Text = p.StringValue };
            return b;
        }
        if (p.ParSpec.Repr != null)
        {
            ComboBox c = new();
            foreach (var el in p.ParSpec.Repr)
            {
                c.Items.Add(el.Value);
            }
            c.SelectedItem = p.StringValue;
            return c;
        }
        if (!float.IsNaN(p.ParSpec.OMin2) && !float.IsNaN(p.ParSpec.OMax2))
        {
            Slider s = new()
            {
                Minimum = p.ParSpec.OMin2,
                Maximum = p.ParSpec.OMax2,
                Width = 127,
                Orientation = Orientation.Horizontal,
                Value = long.Parse(p.StringValue)
            };
            return s;
        }
        if (p.ParSpec.OMin != -20000)
        {
            Slider s = new()
            {
                Minimum = p.ParSpec.OMin,
                Maximum = p.ParSpec.OMax,
                Width = 127,
                Orientation = Orientation.Horizontal,
                Value = Math.Round(double.Parse(p.StringValue))
            };
            return s;
        }
        else
        {
            Slider s = new()
            {
                Minimum = 0,
                Maximum = 127,
                Width = 127,
                Orientation = Orientation.Horizontal,
                Value = long.Parse(p.StringValue),
            };
            return s;
        }
    }

    public static FuncDataTemplate<FullyQualifiedParameter> ParameterValueTemplate { get; }
        = new FuncDataTemplate<FullyQualifiedParameter>((person) => person is not null, BuildParameterValuePresenter);
}
