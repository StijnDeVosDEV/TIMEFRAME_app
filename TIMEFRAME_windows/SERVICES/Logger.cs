using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TIMEFRAME_windows.SERVICES
{
    public static class Logger
    {
        // Class variables
        // ---------------
        private static string logPath = "";
        private static string logContent = "";

        // Class methods
        // -------------
        /// <summary>
        /// Initialize log file
        /// </summary>
        /// <param name="path">Local path where log file will be saved to</param>
        public static void Initialize()
        {
            // Add header to log content
            logContent =
                " ------------------------- " + Environment.NewLine +
                "| TIMEFRAME (Windows app) |" + Environment.NewLine +
                " ------------------------- " + Environment.NewLine;

            // Write header to console
            Console.WriteLine(logContent);
        }

        /// <summary>
        /// Set path where log file will be saved
        /// </summary>
        /// <param name="path">Folder in which log file will be saved</param>
        public static void SetPath(string path)
        {
            // Populate target path for log file
            logPath = path + @"\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" +
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "__TIMEFRAME_windows_LOG.txt";

            Console.WriteLine("Log path set to: " + logPath);
        }

        /// <summary>
        /// Write new message in log file
        /// </summary>
        /// <param name="msg">Log message content</param>
        public static void Write(string msg)
        {
            logContent += msg + Environment.NewLine;
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Write new message in log file, depending on ToLog switch value
        /// </summary>
        /// <param name="msg">Log message content</param>
        /// <param name="ToLog">Decide whether to add message to log file or not</param>
        public static void Write(string msg, bool ToLog)
        {
            if (ToLog == true)
            {
                logContent += msg + Environment.NewLine;
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// Save log file
        /// </summary>
        public static void Save()
        {
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }

            if (!File.Exists(logPath))
            {
                // Create log file
                using (StreamWriter sw = File.CreateText(logPath))
                {
                    sw.WriteLine(logContent);
                }

                Console.WriteLine("Log file saved to: " + logPath);
            }
        }

        /// <summary>
        /// Returns current content of log file
        /// </summary>
        /// <returns></returns>
        public static string GetLogContent()
        {
            return logContent;
        }
    }
}
