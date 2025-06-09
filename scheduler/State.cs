using scheduler.structs;

namespace scheduler
{
    public class State
    {
        private static State _instance;
        private static readonly object _lock = new object();

        public User CurrentUser = new User();
        public Customer CurrentCustomer = new Customer();
        public Address CurrentAddress = new Address();

        // Private constructor to prevent direct instantiation
        private State()
        {
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

        public Appointment CurrentAppointment { get; set; }

        // Method to reset state
        public void Reset()
        {
            CurrentUser = null;
            CurrentCustomer = null;
            CurrentAddress = null;
            CurrentAppointment = null;
        }
    }
}