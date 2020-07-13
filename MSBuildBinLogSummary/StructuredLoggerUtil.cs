using System;
using System.Collections.Generic;
using Microsoft.Build.Logging.StructuredLogger;

namespace MSBuildBinLogSummarizer
{
    /// <summary>
    /// 
    /// </summary>
    public class StructuredLoggerUtil
    {
        private readonly List<(string Code, string Text, DateTime Timestamp)> errors = 
            new List<(string Code, string Text, DateTime Timestamp)>();

        private readonly List<(string Code, string Text, DateTime Timestamp)> warnings =
            new List<(string Code, string Text, DateTime Timestamp)>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName"></param>
        public StructuredLoggerUtil(string fileName)
        {
            var reader = new BinLogReader();

            reader.WarningRaised += (_, e) => this.warnings.Add((e.Code, e.Message, e.Timestamp));
            reader.ErrorRaised += (_, e) => this.errors.Add((e.Code, e.Message, e.Timestamp));
            reader.BuildFinished += (_, e) => Succeeded = e.Succeeded;

            reader.Replay(fileName);
        }

        /// <summary>
        /// Success vs. Failure
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// Errors
        /// </summary>
        public IEnumerable<(string Code, string Text, DateTime Timestamp)> Errors => this.errors.AsReadOnly();

        /// <summary>
        /// Warnings
        /// </summary>
        public IEnumerable<(string Code, string Text, DateTime Timestamp)> Warnings => this.warnings.AsReadOnly();
    }
}
