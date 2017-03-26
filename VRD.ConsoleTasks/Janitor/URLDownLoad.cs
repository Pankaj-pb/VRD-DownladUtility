using System;
using System.Net;
using System.Runtime.Serialization;
using VRD.Common.Utilities;

namespace VRD.Janitor
{
    /// <summary>
    /// Represents a disk janitor task.
    /// The task has the main goal to download file from specified url.
    /// </summary>
    [DataContract]
    public sealed class URLDownLoad : JanitorTaskBase
    {
        /// <summary>
        /// Enable info logging.
        /// </summary>
        [DataMember]
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Flag to indicate that the task is completed with errors.
        /// </summary>
        private bool _completedWithErrors;

        /// <summary>
        /// URL for the file to download.
        /// </summary>
        [DataMember]
        public string URL { get; set; }

        /// <summary>
        /// Path of the folder.
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiskJanitorTask"/> class.
        /// </summary>
        public URLDownLoad()
        {
            URL = string.Empty;

            FilePath = string.Empty;

        }

        /// <summary>
        /// Fills the task with dummy values.
        /// </summary>
        public override void FillDummyValues()
        {
            FilePath = "C:\\Windows\\Temp";
            URL = "https://www.google.com";
        }

        /// <summary>
        /// Run the task.
        /// </summary>
        public override void RunTask()
        {
            if (string.IsNullOrWhiteSpace(URL) || string.IsNullOrWhiteSpace(FilePath))
            {
                throw new ArgumentException("Cannot find folder path defined in disk janitor task.", nameof(FilePath));
            }

            _completedWithErrors = false;

            _DownLoadFile(URL, FilePath);

            if (_completedWithErrors)
            {
                LogUtils.Write($"Error occured in URL Download janitor while downloading the file from '{URL}'", LogUtils.LogMessageType.Error);
            }
            else
            {
                if (EnableLogging)
                {
                    LogUtils.Write($"The file has been downloaded from following '{URL}' on Date '{DateTime.UtcNow}' ", LogUtils.LogMessageType.Info);
                }
            }
        }

        /// <summary>
        /// Download the file to specified location.
        /// </summary>
        /// <param name="url">Url for the download</param>
        /// <param name="filePath">Location for saving the file</param>
        private void _DownLoadFile(string url, string filePath)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, filePath);
                }
            }
            catch (Exception ex)
            {
                LogUtils.Write(ex.Message, LogUtils.LogMessageType.Fatal);
            }
        }
    }
}
