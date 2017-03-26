using System;
using System.Runtime.Serialization;
using VRD.Common.Utilities;

namespace VRD.Janitor
{
    /// <summary>
    /// Represents the base class for a janitor task.
    /// </summary>
    [DataContract]
    public abstract class JanitorTaskBase : IJanitorTask
    {
        /// <summary>
        /// Title of the task.
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Date and time in UTC when the task was last executed.
        /// </summary>
        [DataMember]
        public DateTime LastExecutionDateTimeUtc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JanitorTaskBase"/> class.
        /// </summary>
        protected JanitorTaskBase()
        {
            Title = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Fills the task with dummy values.
        /// </summary>
        public abstract void FillDummyValues();

        /// <summary>
        /// Run the task.
        /// </summary>
        public void Run()
        {
            try
            {
                RunTask();

                LastExecutionDateTimeUtc = DateTime.UtcNow;
            }
            catch (Exception exception)
            {
                LogUtils.Write($"Exception caught in task '{Title}': {exception.Message}", LogUtils.LogMessageType.Fatal);
            }
        }

        /// <summary>
        /// Run the task.
        /// </summary>
        public abstract void RunTask();

    }
}