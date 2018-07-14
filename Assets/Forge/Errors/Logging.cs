using Forge.Settings;
using UnityEngine;
using System;
using System.IO;

namespace Forge.Errors
{
    /// <summary>
    ///     Forge logging helper class, logs messages and errors to Forge logs
    /// </summary>
    public class Logging
    {
        /// <summary>
        ///     Gets and creates the Logs directory for the Forge Logs
        /// </summary>
        internal static DirectoryInfo LogsDirectory {
            get
            {
                if (_logsDirectory != null)
                {
                    return _logsDirectory;
                }

                string loggingPath = Path.Combine(Directory.GetCurrentDirectory(), ForgeSettings.LogDirectory);

                if (!Directory.Exists(loggingPath))
                {
                    _logsDirectory = Directory.CreateDirectory(loggingPath);
                }
                else
                {
                    _logsDirectory = new DirectoryInfo(loggingPath);
                }

                return _logsDirectory;
            }
        }

        /// <summary>
        ///     Gets and creates the info logging file for the Forge info logs
        /// </summary>
        internal static string InfoLogFilePath
        {
            get
            {
                if(_infoLogFilePath != null)
                {
                    return _infoLogFilePath;
                }

                _infoLogFilePath = GetLogFilePath(ForgeExceptionType.Info);

                if (!File.Exists(_infoLogFilePath))
                {
                    File.Create(_infoLogFilePath);
                }

                return _infoLogFilePath;
            }
        }

        /// <summary>
        ///     The logs directory
        /// </summary>
        private static DirectoryInfo _logsDirectory;
        /// <summary>
        ///     The info log file
        /// </summary>
        private static string _infoLogFilePath;

        /// <summary>
        ///     General logging function, you can use this to log custom Forge exceptions.
        /// </summary>
        /// <param name="logItem">The Forge Exception type</param>
        /// <param name="logLevel">The Forge log level</param>
        public static void Log(ForgeExceptionBase logItem, int logLevel = 4)
        {
            if (ForgeSettings.LogLevel < logLevel)
            {
                return;
            }

            Debug.Log(logItem.Message);

            WriteLogToFile(logItem);
        }

        /// <summary>
        ///     Logs a standard message into the Forge logs.
        /// </summary>
        /// <param name="logItem">The logged string</param>
        /// <param name="logLevel">The logLevel required to log this message</param>
        public static void Info(string logItem, int logLevel = 4)
        {
            ForgeInfo info = new ForgeInfo(logItem);

            Log(info, logLevel);
        }

        /// <summary>
        ///     This will log a warning message to the Forge logs
        /// </summary>
        /// <param name="logItem"></param>
        /// <param name="logLevel"></param>
        public static void Warning(string logItem, int logLevel = 3)
        {
            ForgeWarning info = new ForgeWarning(logItem);

            Log(info, logLevel);
        }

        /// <summary>
        ///     Writes a ForgeException to the Forge log files
        /// </summary>
        /// <param name="ForgeException">The Forge exception to write</param>
        private static void WriteLogToFile(ForgeExceptionBase ForgeException)
        {
            StreamWriter fileWriter = new StreamWriter(_infoLogFilePath);

            fileWriter.WriteLine(ForgeException.Message);

            fileWriter.Close();
        }

        /// <summary>
        ///     Gets the corresponding log file path to the ForgeExceptionType
        /// </summary>
        /// <param name="exceptionType">The ForgeExceptionType</param>
        /// <returns>The absolute log file path</returns>
        private static string GetLogFilePath(ForgeExceptionType exceptionType = ForgeExceptionType.Info)
        {
            string logFileName;

            switch (exceptionType)
            {
                case ForgeExceptionType.Info:
                    logFileName = ForgeSettings.InfoLogFile;
                    break;
                case ForgeExceptionType.Critical:
                    logFileName = ForgeSettings.CriticalLogFile;
                    break;
                case ForgeExceptionType.Warning:
                    logFileName = ForgeSettings.WarningLogFile;
                    break;
                default:
                    logFileName = ForgeSettings.InfoLogFile;
                    break;
            }

            return Path.Combine(
                Path.Combine(
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                        InternalData.GAMES_DIR
                    ), 
                    ForgeSettings.LogDirectory
                ), 
                logFileName
            );
        }
    }
}
