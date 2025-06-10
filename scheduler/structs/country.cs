using Microsoft.Data.SqlClient;

namespace scheduler
{
    public class Country
    {
        public int countryId = -1; // countryId INT
        public string Name = string.Empty; // country VARCHAR(50)
        public DateTime CreatedAt = DateTime.Now; // createDate DATETIME
        public string CreatedBy = string.Empty; // createdBy VARCHAR(50)
        public DateTime UpdatedAt = DateTime.Now; // lastUpdate DATETIME
        public string UpdatedBy = string.Empty; // lastUpdateBy VARCHAR(50)


        public static explicit operator Country(SqlDataReader reader)
        {
            var country = new Country();
            if (reader.Read())
            {
                country.countryId = (int)reader["countryId"]; // countryId INT
                country.Name = (string)reader["country"]; // country VARCHAR(50)
                country.CreatedAt = (DateTime)reader["createDate"]; // createDate DATETIME
                country.CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                country.UpdatedAt = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                country.UpdatedBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
            }

            return country;
        }

        public Country(int id = -1)
        {
            countryId = id;
            if (countryId == -1) return; // If the countryId is -1, do not load the country
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLConnection"]
                .ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM country WHERE countryId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", countryId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Name = (string)reader["country"]; // country VARCHAR(50)
                            CreatedAt = (DateTime)reader["createDate"]; // createDate DATETIME
                            CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                            UpdatedAt = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                            UpdatedBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
                        }
                    }
                }
            }
        }

        public void Create()
        {
            throw new NotImplementedException(); // Create method not implemented for Country class
        }

        public void Update()
        {
            throw new NotImplementedException(); // Update method not implemented for Country class
        }

        public void Delete()
        {
            throw new NotImplementedException(); // Delete method not implemented for Country class
        }

        public bool IsValid => !string.IsNullOrEmpty(Name); // Check if the country name is not empty
    }
}