using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
#if DEBUG
using System.Diagnostics;
#endif

namespace TicketingSystem.Framework
{
    /// <summary>
    /// Used to debug code by outputing log messages, warnings and errors
    /// </summary>
    public static class Debug
    {
        private static ILogger logger;

        /// <summary>
        /// Sets the logger type on startup
        /// </summary>
        /// <param name="loggerToUse"></param>
        public static void SetLogger(ILogger loggerToUse)
        {
            logger = loggerToUse;
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            if(logger != null)
            {
                logger.Log(DateTime.Now.ToString() + " - " + "Message - " + message);
            }
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.LogWarning(DateTime.Now.ToString() + " - " + "Warning - " + message);
            }
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            if (logger != null)
            {
                logger.LogError(DateTime.Now.ToString() + " - " + "Error - " + message);
            }
        }

        /// <summary>
        /// Interface for a logger
        /// </summary>
        public interface ILogger
        {
            void Log(string message);
            void LogWarning(string message);
            void LogError(string message);

        }

        /// <summary>
        /// Logger which logs to a .txt file
        /// </summary>
        public class LogTxt : ILogger
        {
            StreamWriter file = new StreamWriter("Log.txt", append: true);
            public void Log(string message)
            {
                file.WriteLine(message);
                file.Flush();
            }
            public void LogWarning(string message)
            {
                file.WriteLine(message);
                file.Flush();
            }
            public void LogError(string message)
            {
                file.WriteLine(message);
                file.Flush();
            }
        }

        /// <summary>
        /// Logger which logs to a debug console in visual studio
        /// </summary>
        public class LogConsole : ILogger
        {
            public void Log(string message)
            {
                Trace.WriteLine(message);
            }
            public void LogWarning(string message)
            {
                Trace.WriteLine(message);
            }
            public void LogError(string message)
            {
                Trace.WriteLine(message);
            }
        }

    }
}
