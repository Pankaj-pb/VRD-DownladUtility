using System;
using System.Reflection;
using VRD.Common.Utilities;
using VRD.ConsoleTasks.FileDownloader;
using VRD.ConsoleTasks.Utils;

namespace VRD.ConsoleTasks
{
    public class TaskLauncher
    {
        public static void Main(string[] args)
        {
            PrintConsoleMessage();

            DateTime appStart = DateTime.Now;

            InitializeLog4net();


            Console.WriteLine("Start VRD.ConsoleTasks.TaskLauncher");


            var parser = new ArgumentParser(args);

            ConsoleTaskTypes consoleTask = ConsoleTaskTypes.None;

            if (!string.IsNullOrWhiteSpace(parser["Task"]))
            {
                string task = parser["Task"];
                try
                {
                    consoleTask = (ConsoleTaskTypes)Enum.Parse(typeof(ConsoleTaskTypes), task);
                }
                catch
                {
                    Console.WriteLine("VRD.TalentRecruiter.ConsoleTasks.TaskLauncher: Wrong Task Specified");
                }

                Console.WriteLine("Start Task: " + task + " By VRD.TalentRecruiter.ConsoleTasks.TaskLauncher");

                switch (consoleTask)
                {

                    case ConsoleTaskTypes.FileDownLoader:
                        JanitorApplication.ExecuteTask(args);
                        break;


                    default:
                        break;
                }
                Console.WriteLine("Exit Task: " + task + " By VRD.TalentRecruiter.ConsoleTasks.TaskLauncher");
            }

            DateTime appEnd = DateTime.Now;
            TimeSpan appExecTime = appEnd - appStart;
            LogUtils.Write(string.Format("Exit VRD.TalentRecruiter.ConsoleTasks.TaskLauncher. Exec time: {0}", appExecTime.ToString()), LogUtils.LogMessageType.Info, typeof(TaskLauncher));
            Console.WriteLine(string.Format("Exit VRD.TalentRecruiter.ConsoleTasks.TaskLauncher. Exec time: {0}", appExecTime.ToString()));

            Environment.Exit(0);

        }

        private static void InitializeLog4net()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = path.Substring(0, path.LastIndexOf('\\') + 1);
            try
            {
                LogUtils.Init(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't initialize logger: {0}", ex);
            }
        }

        private static void PrintConsoleMessage()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("--------------------------Scheduled Task----------------");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("--------Date:" + DateTime.Now + "-----------------------");
            Console.WriteLine("------Note: This application will end automatically-----");
            Console.WriteLine("            after completing the task.");
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}
