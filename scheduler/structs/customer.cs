using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using scheduler.database;

namespace scheduler.structs
{
    /// <summary>
    /// Customer object and logic to interact with DB for customer records
    /// </summary>
    public class Customer : DBObject
    {
        // Public Variables
        public string Name;
        public Address Address = new Address();
        public bool IsActive;

        public Customer()
        {
            Id = -1;
        }

        public Customer(int id)
        {
            Id = id;
            Load();
        }

        public override void Load()
        {
            Address = new Address();
            if (Id == -1) throw new Exception("Cannot load customer with no ID");

            try
            {
                var result = DatabaseManager.Instance.ExecuteQuery(
                    "SELECT customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, LastUpdateBy FROM customer WHERE customerId = @id",
                    new List<MySqlParameter> { new MySqlParameter("@id", Id) })[0];
                Id = (int)result[0];
                Name = (string)result[1];
                Address.Id = (int)result[2];
                Address.Load();
                IsActive = Convert.ToBoolean(result[3]);
                CreatedAt = DateTime.Parse(result[4].ToString());
                CreatedBy = result[5].ToString();
                UpdatedAt = DateTime.Parse(result[6].ToString());
                UpdatedBy = result[7].ToString();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot load customer with ID: " + Id + "\n" + e.Message);
            }
        }

        public override void Create()
        {
            if (!IsValid) throw new Exception("Cannot create customer with invalid data");
            if (Id != -1) throw new Exception("Cannot create customer with ID");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                "INSERT INTO customer (customerName, addressId, active, createDate, createdBy) VALUES (?, ?, ?, ?, ?)";
            var parameters = new object[]
            {
                Name,
                Address.Id,
                IsActive,
                timestamp,
                State.Instance.CurrentUser.Name,
                timestamp,
                State.Instance.CurrentUser.Name
            };
            DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
        }

        public override void Update()
        {
            if (!IsValid) throw new Exception("Cannot update customer with invalid data");
            if (Id == -1) throw new Exception("Cannot update customer with no ID");

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                "UPDATE customer SET customerName = ?, addressId = ?, active = ?, lastUpdate = ?, lastUpdateBy = ? WHERE customerId = ?";
            var parameters = new object[]
            {
                Name,
                Address.Id,
                IsActive,
                timestamp,
                State.Instance.CurrentUser.Name,
                Id
            };
            DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
        }

        public override void Delete()
        {
            string query = "DELETE FROM customer WHERE customerId = ?";
            DatabaseManager.Instance.ExecuteNonQuery(query, new object[] { Id });
        }

        public override bool IsValid =>
            Name != string.Empty &&
            Address.IsValid;
    }
}