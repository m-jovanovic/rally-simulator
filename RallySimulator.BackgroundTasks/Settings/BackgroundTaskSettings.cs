namespace RallySimulator.BackgroundTasks.Settings
{
    /// <summary>
    /// Represents the background task settings.
    /// </summary>
    public class BackgroundTaskSettings
    {
        /// <summary>
        /// The settings key.
        /// </summary>
        public const string SettingsKey = "BackgroundTasks";

        /// <summary>
        /// Gets or sets the sleep time in milliseconds.
        /// </summary>
        public int SleepTimeInMilliseconds { get; set; }
    }
}
