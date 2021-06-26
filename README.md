# Serilog.Sinks.DelegatingText

[![Nuget status](https://img.shields.io/nuget/v/Serilog.Sinks.DelegatingText.svg)](https://www.nuget.org/packages/Serilog.Sinks.DelegatingText)

A Serilog sink wrapper to write formatted log events to a delegate.

To use the sink, install the **Serilog.Sinks.DelegatingText nupkg** into your solution; see below or browse to the [sample sandbox code in this repository](src/Serilog.Sinks.DelegatingText/SANDBOX/Program.cs) for pointers how to use it. And here is the nuget.org page for [Serilog.Sinks.DelegatingText](https://www.nuget.org/packages/Serilog.Sinks.DelegatingText/).

*Please note that this library is provided "as-is" and with no warranty, explicit or otherwise. You should ensure that the functionality meets your requirements, and thoroughly test them, prior to using in any production scenarios.*

## Use case

The **Serilog.Sinks.DelegatingText** sink makes it possible buffer formatted log events in your application. For more information about Serilog, see [Serilog.net](https://serilog.net/) and [https://github.com/serilog/serilog](https://github.com/serilog/serilog).


```csharp
private readonly StringBuilder  _logEventBuffer = new StringBuilder();

private void ConfigureApp()
{
    Log.Logger = new LoggerConfiguration()
        // Using a delegate to buffer log messages
        .WriteTo.DelegatingTextSink(w => WriteToBuffer(w), outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

    Log.Information("Hello from the sandbox");
    Log.Warning("Sample warning");
    Log.Error("Sample error");
    Log.Error(new Exception("A sample exception"), "Sample error with exception");

    // ...
}

private void WriteToBuffer(string formattedLogMessage)
{
    _logEventBuffer.AppendLine(formattedLogMessage.TrimEnd(Environment.NewLine.ToCharArray()));
}

private void SomeFunction()
{
    // do something with the buffered log events
    _logEventBuffer.ToString();
}
```

