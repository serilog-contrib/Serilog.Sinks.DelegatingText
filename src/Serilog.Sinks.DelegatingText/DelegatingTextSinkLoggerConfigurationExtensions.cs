// DelegatingTextSinkLoggerConfigurationExtensions.cs
// 18-5-2021
// Copyright 2021 Dramatic Development - Victor Vogelpoel
// If this works, it was written by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.
//
using System;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Delegating;

namespace Serilog
{
    /// <summary>
    /// Configuration extension for the DelegatingText sinks
    /// </summary>
    public static class DelegatingTextSinkLoggerConfigurationExtensions
    {
        /// <summary>
        /// Default output message template = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        /// </summary>
        public const string DefaultOutputTemplate                   = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";


        /// <summary>
        /// Configuration extension to configure the DelegatingText sink.
        /// </summary>
        /// <param name="loggerSinkConfiguration">The Serilog configuration builder to add Serilog.Sinks.MFilesObject sink configuration to</param>
        /// <param name="write">Delegate to write the formatted log event text</param>
        /// <param name="restrictedToMinimumLevel">The minimal event log level that this sink emits for.</param>
        /// <param name="outputTemplate">A message template describing the output messages.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="levelSwitch">If provided, the switch will be updated based on the log level setting in vault application</param>
        /// <returns>A LoggerSinkConfiguration with configuration for MFilesObject sink added.</returns>
        public static LoggerConfiguration DelegatingTextSink(
                    this LoggerSinkConfiguration loggerSinkConfiguration,
                    Action<String> write,
                    LogEventLevel restrictedToMinimumLevel  = LevelAlias.Minimum,
                    string outputTemplate                   = DelegatingTextSinkLoggerConfigurationExtensions.DefaultOutputTemplate,
                    IFormatProvider formatProvider          = null,
                    LoggingLevelSwitch levelSwitch          = null
                    )
        {
            if (loggerSinkConfiguration == null)    throw new ArgumentNullException(nameof(loggerSinkConfiguration));
            if (write == null)                      throw new ArgumentNullException(nameof(write));
            if (outputTemplate == null)             throw new ArgumentNullException(nameof(outputTemplate));

            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return loggerSinkConfiguration.Sink(new DelegatingTextSink(write, formatter), restrictedToMinimumLevel, levelSwitch);
        }
    }
}
