using System;
using System.Reflection.Emit;
using Avalonia.Markup.Xaml;

namespace ILScriptDemo.IL;

public static class ILRunner
{
    public static void Execute(ILScript ilScript)
    {
        var dynamicMethod = new DynamicMethod("Script", typeof(void), Type.EmptyTypes);

        var ilGenerator = dynamicMethod.GetILGenerator();

        foreach (var opcode in ilScript.Opcodes)
        {
            opcode.Emit(ilGenerator);
        }

        var action = (Action?)dynamicMethod.CreateDelegate(typeof(Action)).DynamicInvoke();

        action?.Invoke();
    }

    public static void Execute(string path)
    {
        var ilScript = (ILScript?)AvaloniaRuntimeXamlLoader.Load("ILScript.xaml");
        if (ilScript is not null)
        {
            Execute(ilScript);
        }
    }
}
