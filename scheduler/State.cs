using scheduler.structs;

namespace scheduler
{
    public class State
    {
        private static State _instance;
        private static readonly object _lock = new object();

        public User CurrentUser { get; set; }
        public ApplicationSettings Settings { get; set; }

        // Private constructor to prevent direct instantiation
        private State()
        {
            Settings = new ApplicationSettings();
        }

        public static State Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new State();
                        }
                    }
                }

                return _instance;
            }
        }

        // Method to reset state
        public void Reset()
        {
            CurrentUser = null;
            Settings = new ApplicationSettings();
        }
    }

    // You can define this class based on your needs
    public class ApplicationSettings
    {
        // Add other application-wide settings as needed
    }
}