using Microsoft.Data.SqlClient;

namespace scheduler
{
    public class Appointment
    {
        public int appointmentId = -1; // appointmentId INT
        public Customer Customer = new Customer(); // customerId INT
        public User User = new User(); // userId INT
        public string Title = string.Empty; // title VARCHAR(50)
        public string Description = string.Empty; // description VARCHAR(50)
        public string Location = string.Empty; // location VARCHAR(50)
        public string Contact = string.Empty; // contact VARCHAR(50)
        public string Type = string.Empty; // type VARCHAR(50)
        public string Url = string.Empty; // url VARCHAR(100)
        public DateTime Start = DateTime.Now; // start DATETIME
        public DateTime End = DateTime.Now; // end DATETIME
        public DateTime CreatedAt = DateTime.Now; // createDate DATETIME
        public string CreatedBy = string.Empty; // createdBy VARCHAR(50)
        public DateTime UpdatedAt = DateTime.Now; // lastUpdate DATETIME
        public string UpdatedBy = string.Empty; // lastUpdateBy VARCHAR(50)


        public static explicit operator Appointment(SqlDataReader reader)
        {
            var appointment = new Appointment();
            if (reader.Read())
            {
                appointment.appointmentId = (int)reader["appointmentId"]; // appointmentId INT
                appointment.Customer = new Customer((int)reader["customerId"]); // customerId INT
                appointment.User = new User((int)reader["userId"]); // userId INT
                appointment.Title = (string)reader["title"]; // title VARCHAR(50)
                appointment.Description = (string)reader["description"]; // description VARCHAR(50)
                appointment.Location = (string)reader["location"]; // location VARCHAR(50)
                appointment.Contact = (string)reader["contact"]; // contact VARCHAR(50)
                appointment.Type = (string)reader["type"]; // type VARCHAR(50)
                appointment.Url = (string)reader["url"]; // url VARCHAR(100)
                appointment.Start = (DateTime)reader["start"]; // start DATETIME
                appointment.End = (DateTime)reader["end"]; // end DATETIME
                appointment.CreatedAt = (DateTime)reader["createDate"]; // createDate DATETIME
                appointment.CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                appointment.UpdatedAt = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                appointment.UpdatedBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
            }

            return appointment;
        }

        public Appointment(int id = -1)
        {
            appointmentId = id;
            if (appointmentId == -1) return; // If the appointmentId is -1, do not load the appointment
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLConnection"]
                .ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM appointment WHERE appointmentId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", appointmentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Customer = new Customer((int)reader["customerId"]); // customerId INT
                            User = new User((int)reader["userId"]); // userId INT
                            Title = (string)reader["title"]; // title VARCHAR(50)
                            Description = (string)reader["description"]; // description VARCHAR(50)
                            Location = (string)reader["location"]; // location VARCHAR(50)
                            Contact = (string)reader["contact"]; // contact VARCHAR(50)
                            Type = (string)reader["type"]; // type VARCHAR(50)
                            Url = (string)reader["url"]; // url VARCHAR(100)
                            Start = (DateTime)reader["start"]; // start DATETIME
                            End = (DateTime)reader["end"]; // end DATETIME
                            CreatedAt = (DateTime)reader["createDate"]; // createDate DATETIME
                            CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                            UpdatedAt = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                            UpdatedBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
                        }
                    }
                }
            }
        }

        public bool IsValid =>
            Customer.customerId != -1 &&
            User.userId != -1 &&
            Start.DayOfWeek != DayOfWeek.Saturday &&
            Start.DayOfWeek != DayOfWeek.Sunday &&
            End.DayOfWeek != DayOfWeek.Saturday &&
            End.DayOfWeek != DayOfWeek.Sunday &&
            Start.Hour > 9 && Start.Hour < 17 &&
            End.Hour > 9 && End.Hour < 17 &&
            Start.TimeOfDay < End.TimeOfDay;

        public void Create()
        {
        }

        public void Update()
        {
        }

        public void Delete()
        {
        }
    }
}