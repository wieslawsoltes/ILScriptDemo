using System.Reflection.Emit;

namespace ILScriptDemo.IL.Opcodes;

public class Ret : Opcode
{
    public override void Emit(ILGenerator ilGenerator)
    {
        ilGenerator.Emit(OpCodes.Ret);
    }
}
