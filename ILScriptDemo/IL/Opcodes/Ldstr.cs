using System.Reflection.Emit;
using Avalonia;

namespace ILScriptDemo.IL.Opcodes;

public class Ldstr : Opcode
{
    public static readonly StyledProperty<string?> ValueProperty =
        AvaloniaProperty.Register<Ldstr, string?>(nameof(Value));

    public string? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public override void Emit(ILGenerator ilGenerator)
    {
        if (Value is not null)
        {
            ilGenerator.Emit(OpCodes.Ldstr, Value);
        }
    }
}
