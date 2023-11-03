using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ILScriptDemo;

public partial class MainWindow : Window
{
    public MainWindow()
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
