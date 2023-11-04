using System;
using System.Linq;
using System.Reflection.Emit;
using Avalonia.Markup.Xaml;
using ILScriptDemo.IL.Opcodes;

namespace ILScriptDemo.IL;

public static class ILRunner
{
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
            else if (opcode is Call call)
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

    public static void Execute(string path)
    {
        var ilScript = (ILScript?)AvaloniaRuntimeXamlLoader.Load("ILScript.xaml");
        if (ilScript is not null)
        {
            Execute(ilScript);
        }
    }
}
