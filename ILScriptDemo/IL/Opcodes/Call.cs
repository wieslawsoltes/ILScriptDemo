using System;
using System.Collections.Generic;

namespace ILScriptDemo.IL.Opcodes;

public class Call : Opcode
{
    public Type Type { get; set; }
    public string Method { get; set; }
    public List<Type> Parameters { get; set; } = new();
}
