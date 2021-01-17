using System;

namespace ConsoleTypesetting
{
    public static class logger
    {
        private static logOptions _options;

        public static Action<string> WriteMessage;
        public static Severity LogLevel { get; set; } = Severity.Warning;

        public static void LogMessage(string message)
        {
            WriteMessage(message);
        }
        public static void LogMessage(Severity severity, string component, string message)
        {
            var outputMsg = $"{DateTime.Now}\t{severity}\t{component}\t{message}";
            System.Console.WriteLine(outputMsg);

        }
    }

    public static class LoggingMethods
    {
        public static void LogToConsole(string message)
        {
            Console.WriteLine(message);
        }

    }

    public class logOptions
    {
        public string AppName;
    }

    public enum Severity
    {
        Verbose,
        Trace,
        Information,
        Warning,
        Error,
        Critial
    }
}