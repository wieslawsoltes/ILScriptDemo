using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Avalonia;
using Avalonia.Metadata;

namespace ILScriptDemo.IL.Opcodes;

public class Call : Opcode
{
    public static readonly StyledProperty<Type?> TypeProperty =
        AvaloniaProperty.Register<Call, Type?>(nameof(Type));

    public static readonly StyledProperty<string?> MethodProperty =
        AvaloniaProperty.Register<Call, string?>(nameof(Method));

    public static readonly DirectProperty<Call, List<Type>> ParametersProperty =
        AvaloniaProperty.RegisterDirect<Call, List<Type>>(
            nameof(Parameters),
            o => o.Parameters,
            (o, v) => o.Parameters = v);

    private List<Type> _parameters = new();

    public Type? Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public string? Method
    {
        get => GetValue(MethodProperty);
        set => SetValue(MethodProperty, value);
    }

    [Content]
    public List<Type> Parameters
    {
        get => _parameters;
        set => SetAndRaise(ParametersProperty, ref _parameters, value);
    }

    public override void Emit(ILGenerator ilGenerator)
    {
        if (Type is null)
        {
            return;
        }

        var methods = Type
            .GetMethods()
            .Where(x => x.Name == Method);

        var mi = methods.FirstOrDefault(x =>
        {
            var parameters = x.GetParameters();
            if (parameters.Length != Parameters.Count)
            {
                return false;
            }
            for (var i = 0; i < Parameters.Count; i++)
            {
                if (parameters[i].ParameterType != Parameters[i])
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
}
