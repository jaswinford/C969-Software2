using System;
using System.Windows;
using scheduler.database;

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
        public int Id { get; set; }

        public override bool IsValid =>
            Customer.Id != -1 &&
            User.Id != -1 &&
            Start.DayOfWeek != DayOfWeek.Saturday &&
            Start.DayOfWeek != DayOfWeek.Sunday &&
            End.DayOfWeek != DayOfWeek.Saturday &&
            End.DayOfWeek != DayOfWeek.Sunday &&
            Start.Hour > 9 && Start.Hour < 17 &&
            End.Hour > 9 && End.Hour < 17 &&
            Start.TimeOfDay < End.TimeOfDay;

        public Appointment(int id)
        {
            Id = id;
            Load(id);
        }

        public Appointment()
        {
            Id = -1;
        }

        public override void Load()
        {
            if (Id == -1) throw new Exception("Cannot load appointment with no ID");

            var result = DatabaseManager.Instance.ExecuteQuery(
                "SELECT appointmentId, customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy FROM appointment WHERE id = ?",
                new object[] { Id })[0];
            Id = Convert.ToInt32(result[0]);
            Customer.Id = Convert.ToInt32(result[1]);
            User.Id = Convert.ToInt32(result[2]);
            Title = result[3].ToString();
            Description = result[4].ToString();
            Location = result[5].ToString();
            Contact = result[6].ToString();
            Type = result[7].ToString();
            Url = result[8].ToString();
            Start = DateTime.Parse(result[9].ToString());
            End = DateTime.Parse(result[10].ToString());
            CreatedAt = DateTime.Parse(result[11].ToString());
            CreatedBy = result[12].ToString();
            UpdatedAt = DateTime.Parse(result[13].ToString());
            UpdatedBy = result[14].ToString();
        }

        public override void Create()
        {
            if (Id != -1) throw new Exception("Cannot create appointment with ID");
            if (!IsValid) throw new Exception("Cannot create appointment with invalid data");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string query =
                "INSERT INTO appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            var parameters = new object[]
            {
                Customer.Id,
                User.Id,
                Title,
                Description,
                Location,
                Contact,
                Type,
                Url,
                Start.ToString("yyyy-MM-dd HH:mm:ss"),
                End.ToString("yyyy-MM-dd HH:mm:ss"),
                timestamp,
                State.Instance.CurrentUser.Name,
                timestamp,
                State.Instance.CurrentUser.Name
            };
            try
            {
                DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void Update()
        {
            if (Id == -1) throw new Exception("Cannot update appointment with no ID");
            if (!IsValid) throw new Exception("Cannot update appointment with invalid data");

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                "UPDATE appointment SET customerId = ?, userId = ?, title = ?, description = ?, location = ?, contact = ?, type = ?, url = ?, start = ?, end = ?, lastUpdate = ?, lastUpdateBy = ? WHERE id = ?";
            var parameters = new object[]
            {
                Customer.Id,
                User.Id,
                Title,
                Description,
                Location,
                Contact,
                Type,
                Url,
                Start.ToString("yyyy-MM-dd HH:mm:ss"),
                End.ToString("yyyy-MM-dd HH:mm:ss"),
                timestamp,
                State.Instance.CurrentUser.Name,
                Id
            };
            DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
        }

        public override void Delete()
        {
            string query = "DELETE FROM appointment WHERE id = ?";
            DatabaseManager.Instance.ExecuteNonQuery(query, new object[] { Id });
        }
    }
}