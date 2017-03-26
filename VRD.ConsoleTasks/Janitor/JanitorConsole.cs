using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using VRD.Common.Tools;

namespace VRD.Janitor
{
    /// <summary>
    /// Represents the janitor console.
    /// </summary>
    public sealed class JanitorConsole
    {
        /// <summary>
        /// Serializer used to read and write the list of tasks.
        /// </summary>
        private static readonly VrdDataContractSerialzer<JanitorTaskList> serializer = new VrdDataContractSerialzer<JanitorTaskList>();

        /// <summary>
        /// Path of the file on disk stores information about all the tasks.
        /// </summary>
        private readonly string _taskFilePath;

        /// <summary>
        /// List of tasks.
        /// </summary>
        public JanitorTaskList TaskList { get; private set; }

        /// <summary>
        /// Flag to indicate whether the console is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JanitorConsole"/> class.
        /// </summary>
        public JanitorConsole()
        {
            _taskFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Tasks.FilePath"].ToString());

            if (string.IsNullOrWhiteSpace(_taskFilePath)
                || !File.Exists(_taskFilePath))
            {
                throw new Exception("Cannot find the tasks configuration file.");
            }

            _LoadTasks();

            IsRunning = false;
        }

        /// <summary>
        /// Loads tasks from disk.
        /// </summary>
        private void _LoadTasks()
        {
            TaskList = serializer.DeserializeFromFile(_taskFilePath);
        }

        /// <summary>
        /// Saves the tasks back to disk.
        /// This is mainly done to preserve the last execution date and time.
        /// </summary>
        private void _SaveTasks()
        {
            serializer.SerializeToFile(TaskList, _taskFilePath);
        }

        /// <summary>
        /// Runs the tasks.
        /// </summary>
        private void _Run()
        {
            if (IsRunning)
            {
                return;
            }

            try
            {
                IsRunning = true;

                TaskList.ForEach(task => task.Run());

                _SaveTasks();
            }
            finally
            {
                IsRunning = false;
            }
        }

        /// <summary>
        /// Runs the tasks.
        /// </summary>
        public static void Run()
        {
            var janitorConsole = new JanitorConsole();

            janitorConsole._Run();
        }

        /// <summary>
        /// Creates a file with all types of tasks that are possible to manage in the console.
        /// This file can be used to create various configurations.
        /// </summary>
        /// <param name="filePath">Full path of the file where to store it.</param>
        public static void CreateDummyTasks(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            // Find all implementations of "JanitorTaskBase".

            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IJanitorTask))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IJanitorTask;


            // Create a list of default tasks with a new instance of each type.

            var taskList = new JanitorTaskList();

            foreach (var instance in instances)
            {
                instance.FillDummyValues();

                taskList.Add(instance);
            }

            // Save to disk.

            serializer.SerializeToFile(taskList, filePath);
        }
    }
}