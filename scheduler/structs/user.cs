using Microsoft.Data.SqlClient;
using System.Configuration;

namespace scheduler
{
    public class User
    {
        public int userId = -1;
        public string Name = string.Empty; // userName VARCHAR(50)
        public bool IsActive;
        public string Password = string.Empty; // password VARCHAR(50)
        public DateTime CreatedAt; // createDate DATETIME
        public string CreatedBy = string.Empty; // createdBy VARCHAR(50)
        public DateTime UpdatedAt; // lastUpdate DATETIME
        public string UpdatedBy = string.Empty; // lastUpdateBy VARCHAR(50)

        public static explicit operator User(SqlDataReader reader)
        {
            var user = new User();
            if (reader.Read())
            {
                user.userId = (int)reader["userId"]; // userId INT
                user.Name = (string)reader["userName"]; // userName VARCHAR(50)
                user.IsActive = (bool)reader["active"]; // active BOOLEAN
                user.CreatedAt = (DateTime)reader["createDate"]; // createDate DATETIME
                user.CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                user.UpdatedAt = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                user.UpdatedBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
                user.Password = (string)reader["password"]; // password VARCHAR(50)
            }

            return user;
        }

        public User(int id = -1)
        {
            userId = id;
            if (userId == -1) return; // If the userId is -1, do not load the user
            string _connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM user WHERE userId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Name = (string)reader["userName"];
                            IsActive = (bool)reader["active"];
                            CreatedAt = (DateTime)reader["createDate"];
                            CreatedBy = (string)reader["createdBy"];
                            UpdatedAt = (DateTime)reader["lastUpdate"];
                            UpdatedBy = (string)reader["lastUpdateBy"];
                            Password = (string)reader["password"];
                        }
                    }
                }
            }
        }

        public bool Authenticated(string password)
        {
            if (userId == -1) return false; //If the user is not loaded, return false
            if (string.IsNullOrEmpty(password)) return false; //If the password is empty, return false
            if (password == Password) return true; //If the password matches, return true
            return false;
        }


        // These methods are placeholders for database operations that are not implemented in this example.
        public void Create()
        {
            throw new NotImplementedException("Create method not implemented for User class.");
        }

        public void Update()
        {
            throw new NotImplementedException("Update method not implemented for User class.");
        }

        public void Delete()
        {
            throw new NotImplementedException("Delete method not implemented for User class.");
        }

        public bool IsValid => Name != string.Empty;
    }
}