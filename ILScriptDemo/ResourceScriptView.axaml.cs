using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ILScriptDemo.IL;

namespace ILScriptDemo;

public partial class ResourceScriptView : UserControl
{
    public ResourceScriptView()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (Resources["ILScript"] is ILScript ilScript)
            {
                ILRunner.Execute(ilScript);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}

