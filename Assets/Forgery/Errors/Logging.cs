using Forgery.Settings;
using UnityEngine;
using System;
using System.IO;

namespace Forgery.Errors
{
    /// <summary>
    ///     Forgery logging helper class, logs messages and errors to forgery logs
    /// </summary>
    public class Logging
    {
        /// <summary>
        ///     Gets and creates the Logs directory for the forgery Logs
        /// </summary>
        internal static DirectoryInfo LogsDirectory {
            get
            {
                if (_logsDirectory != null)
                {
                    return _logsDirectory;
                }

                string loggingPath = Path.Combine(Directory.GetCurrentDirectory(), ForgerySettings.LogDirectory);

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
        ///     Gets and creates the info logging file for the Forgery info logs
        /// </summary>
        internal static string InfoLogFilePath
        {
            get
            {
                if(_infoLogFilePath != null)
                {
                    return _infoLogFilePath;
                }

                _infoLogFilePath = GetLogFilePath(ForgeryExceptionType.Info);

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
        ///     General logging function, you can use this to log custom forgery exceptions.
        /// </summary>
        /// <param name="logItem">The Forgery Exception type</param>
        /// <param name="logLevel">The forgery log level</param>
        public static void Log(ForgeryExceptionBase logItem, int logLevel = 4)
        {
            if (ForgerySettings.LogLevel < logLevel)
            {
                return;
            }

            Debug.Log(logItem.Message);

            WriteLogToFile(logItem);
        }

        /// <summary>
        ///     Logs a standard message into the forgery logs.
        /// </summary>
        /// <param name="logItem">The logged string</param>
        /// <param name="logLevel">The logLevel required to log this message</param>
        public static void Info(string logItem, int logLevel = 4)
        {
            ForgeryInfo info = new ForgeryInfo(logItem);

            Log(info, logLevel);
        }

        /// <summary>
        ///     This will log a warning message to the forgery logs
        /// </summary>
        /// <param name="logItem"></param>
        /// <param name="logLevel"></param>
        public static void Warning(string logItem, int logLevel = 3)
        {
            ForgeryWarning info = new ForgeryWarning(logItem);

            Log(info, logLevel);
        }

        /// <summary>
        ///     Writes a ForgeryException to the forgery log files
        /// </summary>
        /// <param name="forgeryException">The forgery exception to write</param>
        private static void WriteLogToFile(ForgeryExceptionBase forgeryException)
        {
            StreamWriter fileWriter = new StreamWriter(_infoLogFilePath);

            fileWriter.WriteLine(forgeryException.Message);

            fileWriter.Close();
        }

        /// <summary>
        ///     Gets the corresponding log file path to the ForgeryExceptionType
        /// </summary>
        /// <param name="exceptionType">The ForgeryExceptionType</param>
        /// <returns>The absolute log file path</returns>
        private static string GetLogFilePath(ForgeryExceptionType exceptionType = ForgeryExceptionType.Info)
        {
            string logFileName;

            switch (exceptionType)
            {
                case ForgeryExceptionType.Info:
                    logFileName = ForgerySettings.InfoLogFile;
                    break;
                case ForgeryExceptionType.Critical:
                    logFileName = ForgerySettings.CriticalLogFile;
                    break;
                case ForgeryExceptionType.Warning:
                    logFileName = ForgerySettings.WarningLogFile;
                    break;
                default:
                    logFileName = ForgerySettings.InfoLogFile;
                    break;
            }

            return Path.Combine(
                Path.Combine(
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                        InternalData.GAMES_DIR
                    ), 
                    ForgerySettings.LogDirectory
                ), 
                logFileName
            );
        }
    }
}
