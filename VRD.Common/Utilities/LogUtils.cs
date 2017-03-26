using System;

namespace VRD.Common.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class LogUtils
    {
        /// <summary>
        /// 
        /// </summary>
        public enum LogMessageType
        {
            /// <summary>
            /// 
            /// </summary>
            Info,

            /// <summary>
            /// 
            /// </summary>
            Debug,

            /// <summary>
            /// 
            /// </summary>
            Warn,

            /// <summary>
            /// 
            /// </summary>
            Error,

            /// <summary>
            /// 
            /// </summary>
            Fatal
        }

        /// <summary>
        /// Initialize log4net from the very
        /// beginning of your application
        /// </summary>
        /// <param name="configFilePath">The path including the ending slash. Ex: d:\\myPath\\</param>
        public static void Init(string configFilePath)
        {
            // Nothing
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        public static void Write(string message, LogMessageType messageType)
        {
            WriteLogEntry(message, messageType, null, Type.GetType("System.Object"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="type"></param>
        public static void Write(string message, LogMessageType messageType, Type type)
        {
            WriteLogEntry(message, messageType, null, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="ex"></param>
        public static void Write(string message, LogMessageType messageType, Exception ex)
        {
            WriteLogEntry(message, messageType, ex, Type.GetType("System.Object"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        public static void Write(string message, LogMessageType messageType, Exception ex, Type type)
        {
            WriteLogEntry(message, messageType, ex, type);
        }


        /// <summary>
        /// Log the information
        /// </summary>
        /// <param name="message">The message you want to write</param>
        /// <param name="messageType">Type of the message that is Info,Debug,Warn,Error or Fatal</param>
        /// <param name="ex">The exception caused the error</param>
        /// <param name="type">This is the logger type i.e class name</param>
        private static void WriteLogEntry(string message, LogMessageType messageType, Exception ex, Type type)
        {
            //Do logging in database here
        }
    }
}