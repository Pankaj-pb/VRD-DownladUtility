using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using VRD.Common.Utilities;
using VRD.Janitor;

namespace VRD.ConsoleTasks.Janitor
{
    public class FTPDownload : JanitorTaskBase
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
        public string FtpUrl { get; set; }

        /// <summary>
        /// Path of the folder.
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// Ftp UserName.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Ftp Password.
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskJanitorTask"/> class.
        /// </summary>
        public FTPDownload()
        {
            FtpUrl = string.Empty;

            FilePath = string.Empty;

            UserName = string.Empty;

            Password = string.Empty;

        }

        /// <summary>
        /// Fills the task with dummy values.
        /// </summary>
        public override void FillDummyValues()
        {
            FilePath = "C:\\";
            FtpUrl = "ftp://speedtest.tele2.net";
            UserName = "username";
            Password = "****";
        }

        /// <summary>
        /// Run the task.
        /// </summary>
        public override void RunTask()
        {
            if (string.IsNullOrWhiteSpace(FtpUrl) || !Directory.Exists(FilePath) || string.IsNullOrEmpty(FtpUrl) || string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Cannot find folder path defined in disk janitor task.", nameof(FilePath));
            }

            _completedWithErrors = false;

            _DownLoadFile(UserName, Password, FtpUrl, FilePath);

            if (_completedWithErrors)
            {
                LogUtils.Write($"Error occured in URL Download janitor while downloading the file from '{FtpUrl}'", LogUtils.LogMessageType.Error);
            }
            else
            {
                if (EnableLogging)
                {
                    LogUtils.Write($"The file has been downloaded from following '{FtpUrl}' on Date '{DateTime.UtcNow}' ", LogUtils.LogMessageType.Info);
                }
            }
        }

        /// <summary>
        /// Create Ftp web request
        /// </summary>
        /// <param name="ftpDirectoryPath"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="keepAlive"></param>
        /// <returns></returns>
        private FtpWebRequest _CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));

            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
            request.Proxy = null;

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;

            request.Credentials = new NetworkCredential(userName, password);

            return request;
        }

        /// <summary>
        /// Download file from given ftp url.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="ftpSourceFilePath"></param>
        /// <param name="localDestinationFilePath"></param>
        private void _DownLoadFile(string userName, string password, string ftpSourceFilePath, string localDestinationFilePath)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[2048];

            FtpWebRequest request = _CreateFtpWebRequest(ftpSourceFilePath, userName, password, true);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            Stream reader = request.GetResponse().GetResponseStream();
            FileStream fileStream = new FileStream(localDestinationFilePath, FileMode.Create);

            while (true)
            {
                bytesRead = reader.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                    break;

                fileStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
        }
    }
}
