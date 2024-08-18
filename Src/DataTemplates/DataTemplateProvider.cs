using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
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
            b.PropertyChanged += (s, e) =>
            {
                if (e.Property.Name == "Text")
                {
                    //Debug.WriteLine($"{p.ParSpec.Path} changed from \"{e.OldValue}\" to \"{e.NewValue}\"");
                    MessageBus.Current.SendMessage<UpdateMessageSpec>(new UpdateMessageSpec(p, $"{e.NewValue}"), "ui2hw");
                }
            };
            return b;
        }
        if (p.ParSpec.Repr != null)
        {
            if (p.ParSpec.Repr.Count == 2 && p.ParSpec.Repr[0].ToUpper() == "OFF" && p.ParSpec.Repr[1].ToUpper() == "ON")
            {
                CheckBox c = new();
                c.IsChecked = p.StringValue == p.ParSpec.Repr[1];
                c.IsCheckedChanged += (s, e) =>
                {
                    if (s is CheckBox checkBox)
                    {
                        string msg = "OFF";
                        if ((bool)checkBox.IsChecked)
                        {
                            msg = "ON";
                        }
                        MessageBus.Current.SendMessage<UpdateMessageSpec>(new UpdateMessageSpec(p, msg), "ui2hw");
                    }
                };
                return c;
            }
            else
            {
                ComboBox c = new();
                foreach (var el in p.ParSpec.Repr)
                {
                    c.Items.Add(el.Value);
                }
                c.SelectedItem = p.StringValue;
                c.SelectionChanged += (s, e) =>
                {
                    //Debug.WriteLine($"{p.ParSpec.Path} changed from \"{e.RemovedItems[0]}\" to \"{e.AddedItems[0]}\"");
                    MessageBus.Current.SendMessage<UpdateMessageSpec>(new UpdateMessageSpec(p, $"{e.AddedItems[0]}"), "ui2hw");
                };
                return c;
            }
        }
        if (!float.IsNaN(p.ParSpec.OMin2) && !float.IsNaN(p.ParSpec.OMax2))
        {
            Slider s = new()
            {
                Minimum = p.ParSpec.OMin2,
                Maximum = p.ParSpec.OMax2,
                Width = 256,
                Orientation = Orientation.Horizontal,
                IsSnapToTickEnabled = true,
                Ticks = p.ParSpec.Ticks,
                Value = long.Parse(p.StringValue),
            };
            s.ValueChanged += (s, e) =>
            {
                //Debug.WriteLine($"{p.ParSpec.Path} changed from \"{e.OldValue}\" to \"{e.NewValue}\"");
                MessageBus.Current.SendMessage<UpdateMessageSpec>(new UpdateMessageSpec(p, $"{e.NewValue}"), "ui2hw");
            };
            TextBlock v = new()
            {
                VerticalAlignment = VerticalAlignment.Center,
                [!TextBlock.TextProperty] = new Binding
                {
                    Source = s,
                    Path = nameof(s.Value)
                }
            };
            StackPanel pan = new()
            {
                Orientation = Orientation.Horizontal,
            };
            pan.Children.Add(s);
            pan.Children.Add(v);
            if (p.ParSpec.Unit != "")
            {
                TextBlock u = new()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = " [" + p.ParSpec.Unit + "]"
                };
                pan.Children.Add(u);
            }
            return pan;
        }
        if (p.ParSpec.OMin != -20000)
        {
            Slider s = new()
            {
                Minimum = p.ParSpec.OMin,
                Maximum = p.ParSpec.OMax,
                Width = 256,
                Orientation = Orientation.Horizontal,
                IsSnapToTickEnabled = true,
                Ticks = p.ParSpec.Ticks,
                Value = Math.Round(double.Parse(p.StringValue)),
            };
            s.ValueChanged += (s, e) =>
            {
                //Debug.WriteLine($"{p.ParSpec.Path} changed from \"{e.OldValue}\" to \"{e.NewValue}\"");
                MessageBus.Current.SendMessage<UpdateMessageSpec>(new UpdateMessageSpec(p, $"{e.NewValue}"), "ui2hw");
            };
            TextBlock v = new()
            {
                VerticalAlignment = VerticalAlignment.Center,
                [!TextBlock.TextProperty] = new Binding
                {
                    Source = s,
                    Path = nameof(s.Value)
                }
            };
            StackPanel pan = new()
            {
                Orientation = Orientation.Horizontal,
            };
            pan.Children.Add(s);
            pan.Children.Add(v);
            if (p.ParSpec.Unit != "")
            {
                TextBlock u = new()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = " [" + p.ParSpec.Unit + "]"
                };
                pan.Children.Add(u);
            }
            return pan;
        }
        else
        {
            Slider s = new()
            {
                Minimum = 0,
                Maximum = 127,
                Width = 256,
                Orientation = Orientation.Horizontal,
                Value = long.Parse(p.StringValue),
                IsSnapToTickEnabled = true,
                Ticks = p.ParSpec.Ticks,
            };
            s.ValueChanged += (s, e) =>
            {
                //Debug.WriteLine($"{p.ParSpec.Path} changed from \"{e.OldValue}\" to \"{e.NewValue}\"");
                MessageBus.Current.SendMessage<UpdateMessageSpec>(new UpdateMessageSpec(p, $"{e.NewValue}"), "ui2hw");
            };
            TextBlock v = new()
            {
                VerticalAlignment = VerticalAlignment.Center,
                [!TextBlock.TextProperty] = new Binding
                {
                    Source = s,
                    Path = nameof(s.Value)
                }
            };
            StackPanel pan = new()
            {
                Orientation = Orientation.Horizontal,
            };
            pan.Children.Add(s);
            pan.Children.Add(v);
            if (p.ParSpec.Unit != "")
            {
                TextBlock u = new()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = " [" + p.ParSpec.Unit + "]"
                };
                pan.Children.Add(u);
            }

            return pan;
        }
    }

    public static FuncDataTemplate<FullyQualifiedParameter> ParameterValueTemplate { get; }
        = new FuncDataTemplate<FullyQualifiedParameter>((person) => person is not null, BuildParameterValuePresenter);
}
