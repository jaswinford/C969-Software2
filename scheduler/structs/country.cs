using System;
using System.Collections.Generic;
using System.Windows.Documents;
using MySql.Data.MySqlClient;
using scheduler.database;

namespace scheduler.structs
{
    /// <summary>
    /// Country object and logic to interact with DB for country records
    /// </summary>
    public class Country : DBObject
    {
        public string Name { get; set; } = string.Empty; // country VARCHAR(50)

        public override void Load()
        {
            if (Id == -1) throw new System.Exception("Cannot load country with no ID");

            var result = DatabaseManager.Instance.ExecuteQuery(
                "SELECT countryId, country, createDate,createdBy,lastUpdate,lastUpdateBy FROM country WHERE countryId = @id",
                new List<MySqlParameter> { new MySqlParameter("@id", Id) })[0];
            Id = Convert.ToInt32(result[0]);
            Name = result[1].ToString();
            CreatedAt = DateTime.Parse(result[2].ToString());
            CreatedBy = result[3].ToString();
            UpdatedAt = DateTime.Parse(result[4].ToString());
            UpdatedBy = result[5].ToString();
        }

        public override void Create()
        {
            if (Id != -1) throw new System.Exception("Cannot create country with ID");
            if (!IsValid) throw new System.Exception("Cannot create country with invalid data");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, ?, ?, ?)";
            var parameters = new object[]
            {
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
            if (Id == -1) throw new Exception("Cannot update country with no ID");
            if (!IsValid) throw new Exception("Cannot update country with invalid data");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "UPDATE country SET country = ?, lastUpdate = ?, lastUpdateBy = ? WHERE countryId = ?";
            var parameters = new object[]
            {
                Name,
                timestamp,
                State.Instance.CurrentUser.Name,
                Id
            };
            DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
        }

        public override void Delete()
        {
            string query = "DELETE FROM country WHERE countryId = ?";
            DatabaseManager.Instance.ExecuteNonQuery(query, new object[] { Id });
        }

        public override bool IsValid { get; }
    }
}