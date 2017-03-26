using System;

namespace VRD.Janitor
{
    /// <summary>
    /// Defines the interface of a janitor task.
    /// </summary>

    public interface IJanitorTask
    {
        /// <summary>
        /// Title of the task.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Date and time in UTC when the task was last executed.
        /// </summary>
        DateTime LastExecutionDateTimeUtc { get; set; }

        /// <summary>
        /// Fills the task with dummy values.
        /// </summary>
        void FillDummyValues();

        /// <summary>
        /// Run the task.
        /// </summary>
        void Run();
    }
}