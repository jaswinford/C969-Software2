using System;
using System.Runtime.InteropServices;
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
        public Address Address;
        public bool IsActive;

        public override void Load()
        {
            if (Id == -1) throw new Exception("Cannot load customer with no ID");

            var result = DatabaseManager.Instance.ExecuteQuery(
                "SELECT customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, LastUpdateBy FROM customer WHERE customerId = ?",
                new object[] { Id })[0];
            Id = Convert.ToInt32(result[0]);
            Name = result[1].ToString();
            Address.Id = Convert.ToInt32(result[2]);
            IsActive = Convert.ToBoolean(result[3]);
            CreatedAt = DateTime.Parse(result[4].ToString());
            CreatedBy = result[5].ToString();
            UpdatedAt = DateTime.Parse(result[6].ToString());
            UpdatedBy = result[7].ToString();
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