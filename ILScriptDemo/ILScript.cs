using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

namespace ILScriptDemo;

public class ILScript
{
    [Content] 
    public List<Opcode> Opcodes { get; set; } = new();
}

public abstract class Opcode { }

public class Ldstr : Opcode
{
    public string Value { get; set; }
}

public class Call : Opcode
{
    public Type Type { get; set; }
    public string Method { get; set; }
    public List<Type> Parameters { get; set; } = new();
}

public class Ret : Opcode { }

public static class ILRunner
{
    public static void Execute(string path)
    {
        var ilScript = (ILScript?)AvaloniaRuntimeXamlLoader.Load("ILScript.xaml");
        if (ilScript is not null)
        {
            Execute(ilScript);
        }
    }

    public static void Execute(ILScript ilScript)
    {
        var dynamicMethod = new DynamicMethod($"Script", typeof(void), Type.EmptyTypes);
        var ilGenerator = dynamicMethod.GetILGenerator();

        foreach (var opcode in ilScript.Opcodes)
        {
            if (opcode is Ldstr ldstr)
            {
                ilGenerator.Emit(OpCodes.Ldstr, ldstr.Value);
            }
            if (opcode is Call call)
            {
                var methods = call.Type.GetMethods().Where(x => x.Name == call.Method);
                var mi = methods.FirstOrDefault(x =>
                {
                    var parameters = x.GetParameters();
                    if (parameters.Length != call.Parameters.Count)
                    {
                        return false;
                    }
                    for (var i = 0; i < call.Parameters.Count; i++)
                    {
                        if (parameters[i].ParameterType != call.Parameters[i])
                        {
                            return false;
                        }
                    }
                    return true;
                });
                if (mi is not null)
                {
                    ilGenerator.Emit(OpCodes.Call, mi);
                }
            }
            else if (opcode is Ret)
            {
                ilGenerator.Emit(OpCodes.Ret);
            }
        }

        var action = (Action)dynamicMethod.CreateDelegate(typeof(Action));
        action();
    }
}
