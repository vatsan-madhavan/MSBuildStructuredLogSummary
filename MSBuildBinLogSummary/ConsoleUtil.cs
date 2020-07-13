using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MSBuildBinLogSummarizer
{
    /// <summary>
    /// Console utilities - Write and WriteLine methods with colors
    /// </summary>
    public static class ConsoleUtil
    {
        /// <summary>
        /// Write to the <see cref="Console"/> using a specific foreground and background colors
        /// </summary>
        /// <typeparam name="T">type of the information being written</typeparam>
        /// <param name="t">Content to be output to console</param>
        /// <param name="foregroundColor">Foreground color</param>
        /// <param name="backgroundColor">Background color</param>
        public static void Write<T>(T t, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            WriteImpl(t, foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Write to the <see cref="Console"/> using a specific foreground color
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="foregroundColor"></param>
        public static void Write<T>(T t, ConsoleColor foregroundColor)
        {
            WriteImpl(t, foregroundColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        public static void WriteLine<T>(T t, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            WriteLineImpl(t, foregroundColor, backgroundColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="foregroundColor"></param>
        public static void WriteLine<T>(T t, ConsoleColor foregroundColor)
        {
            WriteLineImpl(t, foregroundColor);
        }

        /// <summary>
        /// Writes <paramref name="error"/> to Console with <see cref="ConsoleColor.Red"/> foreground
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        public static void WriteError<T>(T error)
        {
            Write(error, ConsoleColor.Red);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        public static void WriteLineError<T>(T error)
        {
            WriteLine(error, ConsoleColor.Red);
        }

        /// <summary>
        /// Writes <paramref name="warning"/> to Console with <see cref="ConsoleColor.DarkYellow"/> foreground
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="warning"></param>
        public static void WriteWarning<T>(T warning)
        {
            Write(warning, ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="warning"></param>
        public static void WriteLineWarning<T>(T warning)
        {
            WriteLine(warning, ConsoleColor.DarkYellow);
        }


        private static void WriteLineImpl<T>(T t, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
        {
            OutImpl(
                to => Console.WriteLine(to),
                t,
                foregroundColor,
                backgroundColor);
        }

        private static void WriteImpl<T>(T t, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
        {
            OutImpl(
                to => Console.Write(to),
                t,
                foregroundColor,
                backgroundColor);
        }

        private static void OutImpl<T>(Action<T> writer, T t, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
        {
            ConsoleColor? oldFgColor = null;
            ConsoleColor? oldBgColor = null;

            if (foregroundColor.HasValue)
            {
                oldFgColor = Console.ForegroundColor;
            }

            if (backgroundColor.HasValue)
            {
                oldBgColor = Console.BackgroundColor;
            }

            if (foregroundColor.HasValue)
            {
                Console.ForegroundColor = foregroundColor.Value;
            }

            if (backgroundColor.HasValue)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }

            try
            {
                writer?.Invoke(t);
            }
            finally
            {
;                if (oldFgColor.HasValue)
                {
                    Console.ForegroundColor = oldFgColor.Value;
                }

                if (oldBgColor.HasValue)
                {
                    Console.BackgroundColor = oldBgColor.Value;
                }
            }
        }
    }
}
