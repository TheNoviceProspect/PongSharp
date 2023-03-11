using System.Runtime.InteropServices;

namespace PongSharp.App
{
    internal unsafe class AppLog
    {
        private static string LoggerTag = String.Empty;
        internal static void SetCustomLogger(string name = "<AppLogger>")
        {
            LoggerTag= name;
            SetTraceLogCallback(&LogCustom);
        }

        internal static void ResetLog()
        {
            SetTraceLogCallback(&Logging.LogConsole);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        internal static void LogCustom(int logLevel, sbyte* text, sbyte* args)
        {
            var message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));

            Console.ForegroundColor = (TraceLogLevel)logLevel switch
            {
                TraceLogLevel.LOG_ALL => ConsoleColor.White,
                TraceLogLevel.LOG_TRACE => ConsoleColor.Gray,
                TraceLogLevel.LOG_DEBUG => ConsoleColor.Blue,
                TraceLogLevel.LOG_INFO => ConsoleColor.Gray,
                TraceLogLevel.LOG_WARNING => ConsoleColor.DarkYellow,
                TraceLogLevel.LOG_ERROR => ConsoleColor.Red,
                TraceLogLevel.LOG_FATAL => ConsoleColor.Red,
                TraceLogLevel.LOG_NONE => ConsoleColor.White,
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };
            string logLevelName  = (TraceLogLevel)logLevel switch
            {
                TraceLogLevel.LOG_ALL => "ALL",
                TraceLogLevel.LOG_TRACE => "TRACE",
                TraceLogLevel.LOG_DEBUG => "DEBUG",
                TraceLogLevel.LOG_INFO => "INFO",
                TraceLogLevel.LOG_WARNING => "WARNING",
                TraceLogLevel.LOG_ERROR => "ERROR",
                TraceLogLevel.LOG_FATAL => "FATAL",
                TraceLogLevel.LOG_NONE => "NONE",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };
            Console.WriteLine($" {LoggerTag}: [{logLevelName}] {message}");
            Console.ResetColor();
        }
    }
}
