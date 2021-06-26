using System;
using System.Text;
using Serilog;

namespace SANDBOX
{
    class Program
    {
        private static readonly StringBuilder  _logEventBuffer = new StringBuilder();

        static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    // Using a delegate to buffer log messages
                    .WriteTo.DelegatingTextSink(w => WriteToBuffer(w), outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();

                Log.Information("Hello from the sandbox");
                Log.Warning("Sample warning");
                Log.Error("Sample error");
                Log.Error(new Exception("A sample exception"), "Sample error with exception");

                Log.CloseAndFlush();

                Console.WriteLine("This was logged:");
                Console.WriteLine(_logEventBuffer.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("{1} while testing{0}{2}", Environment.NewLine, ex.GetType().FullName, ex.Message);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        private static void WriteToBuffer(string formattedLogMessage)
        {
            _logEventBuffer.AppendLine(formattedLogMessage.TrimEnd(Environment.NewLine.ToCharArray()));
        }


    }
}
