# ILScriptDemo

Define and run [Microsoft Intermediate Language (MSIL)](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes?view=net-7.0) code directly from Avalonia UI Xaml.

```xaml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:sys="using:System"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="ILScriptDemo.MainWindow"
        Width="500" Height="500"
        Title="ILScriptDemo">
  <Button Content="Run IL">
    <ILScript.Script>
      <ILScript>
        <Ldstr Value="{Binding $parent[Window].Title, StringFormat={}Welcome to the {0}}" />
        <Call Type="{x:Type sys:Console}" Method="WriteLine">
          <Call.Parameters>
            <x:Type TypeName="sys:String" />
          </Call.Parameters>
        </Call>
        <Ret />
      </ILScript>
    </ILScript.Script>
  </Button>
</Window>
```

### Supported OpCodes

- Ldstr
- Call
- Ret

