using System;
using System.Configuration;
using System.IO;
using VRD.Common.Utilities;
using VRD.ConsoleTasks.Utils;
using VRD.Janitor;

namespace VRD.ConsoleTasks.FileDownloader
{
    public class JanitorApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void ExecuteTask(string[] args)
        {
            DateTime appStart = DateTime.Now;

            var _parser = new ArgumentParser(args);

            LogUtils.Write($"Start VRD.Console.Janitor.DownloadFile", LogUtils.LogMessageType.Info);

            try
            {
                var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Tasks.FilePath"].ToString());

                if (!File.Exists(filename))
                {
                    JanitorConsole.CreateDummyTasks(filename);
                }

                JanitorConsole.Run();

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                LogUtils.Write($"General error in 'DownloadUtility ({ex.Message})", LogUtils.LogMessageType.Fatal);
            }

            DateTime appEnd = DateTime.Now;
            TimeSpan appExecTime = appEnd - appStart;

            LogUtils.Write($"VRD.Console.Janitor successfully completed. \nExecution Time : {appExecTime.ToString()}", LogUtils.LogMessageType.Info);

        }

    }
}
