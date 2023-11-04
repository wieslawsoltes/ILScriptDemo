using System.Reflection.Emit;
using Avalonia;

namespace ILScriptDemo.IL;

public abstract class Opcode : AvaloniaObject
{
    public abstract void Emit(ILGenerator ilGenerator);
}
