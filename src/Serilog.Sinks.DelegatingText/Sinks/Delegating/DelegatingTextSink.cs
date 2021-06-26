// DelegatingTextLogSink.cs
// 18-5-2021
// Copyright 2021 Dramatic Development - Victor Vogelpoel
// If this works, it was written by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.
//
using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Delegating
{
    /// <summary>
    /// Serilog Delegating Text sink
    /// </summary>
    public class DelegatingTextSink : ILogEventSink
    {
        private readonly Action<String> _write;
        private readonly ITextFormatter _formatter;

        /// <summary>
        /// A sink that formats the log event using output template and writes the result using the Action.
        /// </summary>
        /// <param name="write">The Action delegate to write the formatted log event.</param>
        /// <param name="formatter">a text formatter for converting the log event into a string with event arguments</param>
        public DelegatingTextSink(Action<String> write, ITextFormatter formatter)
        {
            _write      = write ?? throw new ArgumentNullException(nameof(write));
            _formatter  = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Emit the log event formatted to the Action writer.
        /// </summary>
        /// <param name="logEvent"></param>
        public void Emit(LogEvent logEvent)
        {
            // Format the event according to the OutputTemplate, eg "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            // see https://github.com/serilog/serilog-sinks-console#output-templates

            using (var s = new StringWriter())
            {
                _formatter.Format(logEvent, s);

                _write(s.ToString());
            }
        }
    }
}
