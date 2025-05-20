using System;

namespace scheduler.structs
{
    /// <summary>
    /// Appointment object and logic to interact with DB for appointment records
    /// </summary>
    ///
    /// TODO: Add member variables
    /// TODO: 'Add' Function
    /// TODO: 'Update' Function
    /// TODO: 'Delete' Function
    public class Appointment : DBObject
    {
        public Customer Customer { get; set; } // customerId INT(10)
        public User User { get; set; } // userId INT
        public string Title { get; set; } // title VARCHAR(255)
        public string Description { get; set; } // description TEXT
        public string Location { get; set; } // location TEXT
        public string Contact { get; set; } // contact TEXT
        public string Type { get; set; } // type TEXT
        public string Url { get; set; } // url VARCHAR(255)
        public DateTime Start { get; set; } // start DATETIME
        public DateTime End { get; set; } // end DATETIME
    }
}