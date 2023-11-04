using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using Avalonia.Reactive;

namespace ILScriptDemo.IL;

public class ILScript : AvaloniaObject
{
    public static readonly AttachedProperty<ILScript> ScriptProperty =
        AvaloniaProperty.RegisterAttached<ILScript, Control, ILScript>("Script");

    public static void SetScript(Control element, ILScript value)
    {
        element.SetValue(ScriptProperty, value);
    }

    public static ILScript GetScript(Control element)
    {
        return element.GetValue(ScriptProperty);
    }

    static ILScript()
    {
        ScriptProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ILScript>>(ScriptChanged));
    }

    private static void ScriptChanged(AvaloniaPropertyChangedEventArgs<ILScript> obj)
    {
        var ilScript = obj.GetNewValue<ILScript>();

        if (obj.Sender is Button button)
        {
            void OnButtonOnClick(object? o, RoutedEventArgs routedEventArgs)
            {
                try
                {
                    ILRunner.Execute(ilScript);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            button.Click += OnButtonOnClick;
        }
    }

    [Content] 
    public List<Opcode> Opcodes { get; set; } = new();
}
