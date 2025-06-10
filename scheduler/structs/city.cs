using Microsoft.Data.SqlClient;

namespace scheduler
{
    /// <summary>
    /// City object and logic to interact with DB for city records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class City
    {
        public int cityId = -1; // cityId INT
        public string Name = string.Empty;
        public Country Country = new Country();
        public DateTime CreatedAt = DateTime.Now; // createDate DATETIME
        public string CreatedBy = string.Empty; // createdBy VARCHAR(50)
        public DateTime UpdatedAt = DateTime.Now; // lastUpdate DATETIME
        public string UpdatedBy = string.Empty; // lastUpdateBy VARCHAR(50)

        public static explicit operator City(SqlDataReader reader)
        {
            var city = new City();
            if (reader.Read())
            {
                city.cityId = (int)reader["cityId"]; // cityId INT
                city.Name = (string)reader["city"]; // city VARCHAR(50)
                city.Country = new Country((int)reader["countryId"]); // countryId INT
                city.CreatedAt = (DateTime)reader["createDate"]; // createDate DATETIME
                city.CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                city.UpdatedAt = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                city.UpdatedBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
            }

            return city;
        }

        public City(int id = -1)
        {
            cityId = id;
            if (cityId == -1) return; // If the cityId is -1, do not load the city
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLConnection"]
                .ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM city WHERE cityId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cityId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Name = (string)reader["city"]; // city VARCHAR(50)
                            Country = new Country((int)reader["countryId"]); // countryId INT
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
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool IsValid => Name != string.Empty && Country.countryId != -1;
    }
}