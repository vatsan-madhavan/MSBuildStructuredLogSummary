using System;
using System.IO;
using System.Linq;
using Microsoft.Build.Logging.StructuredLogger;

namespace MSBuildBinLogSummarizer
{
    class Program
    {
        static void Main(FileInfo logFile)
        {
            if (logFile == null)
            {
                Console.WriteLine("Filename missing");
                return;
            }

            if (!logFile.Extension.EndsWith(".binlog", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Input is not a .binlog");
                return;
            }

            ProcessBinaryLog(logFile.FullName);
        }

        private static void ProcessBinaryLog(string fullName)
        {
            var util = new StructuredLoggerUtil(fullName);

            if (!util.Succeeded)
            {
                ConsoleUtil.WriteLineError("Build Failed");
            }
            else
            {
                ConsoleUtil.WriteLine("Build Succceeded", ConsoleColor.Green);
            }

            foreach (var (_, Text, _) in util.Warnings)
            {
                ConsoleUtil.WriteLineWarning($"{Text}");

            }
            if (util.Warnings.Count() > 0) Console.WriteLine();

            foreach (var (_, Text, _) in util.Errors)
            {
                ConsoleUtil.WriteLineError($"{Text}");
            }
            if (util.Errors.Count() > 0) Console.WriteLine();


            if (util.Warnings.Count() == 0)
            {
                ConsoleUtil.WriteLine("No Warnings", ConsoleColor.Green);
            }

            if (util.Errors.Count() == 0)
            {
                ConsoleUtil.WriteLine("No errors", ConsoleColor.Green);
            }
        }
    }
}
