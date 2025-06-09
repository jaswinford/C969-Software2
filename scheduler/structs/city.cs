using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using scheduler.database;

namespace scheduler.structs
{
    /// <summary>
    /// City object and logic to interact with DB for city records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class City : DBObject
    {
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; }

        public override void Load()
        {
            if (Id == -1) throw new Exception("Cannot load city with no ID");

            var result = DatabaseManager.Instance.ExecuteQuery(
                "SELECT cityId, countryId, city, createDate,createdBy,lastUpdate,lastUpdateBy FROM city WHERE cityId = @id",
                new List<MySqlParameter> { new MySqlParameter("@id", Id) })[0];
            Id = Convert.ToInt32(result[0]);
            Country.Id = Convert.ToInt32(result[1]);
            Name = result[2].ToString();
            CreatedAt = DateTime.Parse(result[3].ToString());
            CreatedBy = result[4].ToString();
            UpdatedAt = DateTime.Parse(result[5].ToString());
            UpdatedBy = result[6].ToString();
        }

        public City(int id)
        {
            Id = id;
            Load();
        }

        public City()
        {
        }

        public static City Lookup(string name)
        {
            var result =
                DatabaseManager.Instance.ExecuteQuery("SELECT cityId FROM city WHERE city = @name",
                    new List<MySqlParameter> { new MySqlParameter("@name", name) });
            if (result.Count == 0) return null;
            return new City(Convert.ToInt32(result[0][0]));
        }

        public override void Create()
        {
            if (Id != -1) throw new Exception("Cannot create city with ID");
            if (!IsValid) throw new Exception("Cannot create city with invalid data");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                "INSERT INTO city (countryId, city, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, ?, ?, ?, ?)";
            var parameters = new object[]
            {
                Country.Id,
                Name,
                timestamp,
                State.Instance.CurrentUser.Name,
                timestamp,
                State.Instance.CurrentUser.Name
            };
            DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
        }

        public override void Update()
        {
            if (Id == -1) throw new Exception("Cannot update city with no ID");
            if (!IsValid) throw new Exception("Cannot update city with invalid data");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                "UPDATE city SET countryId = ?, city = ?, lastUpdate = ?, lastUpdateBy = ? WHERE cityId = ?";
            var parameters = new object[]
            {
                Country.Id,
                Name,
                timestamp,
                State.Instance.CurrentUser.Name,
                Id
            };
            DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
        }

        public override void Delete()
        {
            string query = "DELETE FROM city WHERE cityId = ?";
            DatabaseManager.Instance.ExecuteNonQuery(query, new object[] { Id });
        }

        public override bool IsValid => Name != string.Empty && Country.Id != -1;
    }
}