using System;
using System.Text;
using FluentAssertions;
using Serilog.Events;
using Xunit;

namespace Serilog.Sinks.DelegatingText.Tests
{
    public class DelegatingTextSinkTests
    {
        [Fact]
        public void LoggedEvents_should_yield_all_buffered_messages()
        {
            // ASSIGN
            StringBuilder  logEventBuffer = new StringBuilder();

            Log.Logger = new LoggerConfiguration()
                // Using a delegate to buffer log messages
                .WriteTo.DelegatingTextSink(
                    write: text => logEventBuffer.AppendLine(text.TrimEnd(Environment.NewLine.ToCharArray())),
                    outputTemplate:"[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            string expectedLogEventBuffer = @"[INF] Hello from the sandbox
[WRN] Sample warning
[ERR] Sample error
[ERR] Sample error with exception
System.Exception: A sample exception
";

            // ACT
            Log.Information("Hello from the sandbox");
            Log.Warning("Sample warning");
            Log.Error("Sample error");
            Log.Error(new Exception("A sample exception"), "Sample error with exception");

            Log.CloseAndFlush();

            // ASSERT
            logEventBuffer.Length.Should().BeGreaterThan(0, "items have been logged");
            logEventBuffer.ToString().Should().Be(expectedLogEventBuffer);


        }

        [Fact]
        public void LoggedEvents_with_restrictedToMinimumLevel_should_yield_only_warning_and_error_buffered_messages()
        {
            // ASSIGN
            StringBuilder  logEventBuffer = new StringBuilder();

            Log.Logger = new LoggerConfiguration()
                // Using a delegate to buffer log messages
                .WriteTo.DelegatingTextSink(
                    write: text => logEventBuffer.AppendLine(text.TrimEnd(Environment.NewLine.ToCharArray())),
                    restrictedToMinimumLevel: LogEventLevel.Warning,
                    outputTemplate:"[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            string expectedLogEventBuffer = @"[WRN] Sample warning
[ERR] Sample error
[ERR] Sample error with exception
System.Exception: A sample exception
";

            // ACT
            Log.Information("Hello from the sandbox");
            Log.Warning("Sample warning");
            Log.Error("Sample error");
            Log.Error(new Exception("A sample exception"), "Sample error with exception");

            Log.CloseAndFlush();

            // ASSERT
            logEventBuffer.Length.Should().BeGreaterThan(0, "items have been logged");
            logEventBuffer.ToString().Should().Be(expectedLogEventBuffer, "only WARNING and up messages should be buffered");
        }
    }
}
