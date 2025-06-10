using Microsoft.Data.SqlClient;

namespace scheduler
{
    /// <summary>
    /// Customer object and logic to interact with DB for customer records
    /// </summary>
    public class Customer
    {
        // Public Variables
        public int customerId = -1;
        public string customerName = string.Empty;
        public Address Address = new Address();
        public bool active = true;
        public DateTime createDate = DateTime.Now;
        public string createdBy = string.Empty;
        public DateTime lastUpdate = DateTime.Now;
        public string lastUpdateBy = string.Empty;


        public static explicit operator Customer(SqlDataReader reader)
        {
            var customer = new Customer();
            if (reader.Read())
            {
                customer.customerId = (int)reader["customerId"]; // customerId INT
                customer.customerName = (string)reader["customerName"]; // customerName VARCHAR(50)
                customer.active = (bool)reader["active"]; // active BOOLEAN
                customer.createDate = (DateTime)reader["createDate"]; // createDate DATETIME
                customer.createdBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                customer.lastUpdate = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                customer.lastUpdateBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
                customer.Address = new Address((int)reader["addressId"]);
            }

            return customer;
        }

        public Customer(int id = -1)
        {
            customerId = id;
            if (customerId == -1) return; // If the customerId is -1, do not load the customer
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLConnection"]
                .ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM customer WHERE customerId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", customerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customerName = (string)reader["customerName"]; // customerName VARCHAR(50)
                            active = (bool)reader["active"]; // active BOOLEAN
                            createDate = (DateTime)reader["createDate"]; // createDate DATETIME
                            createdBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                            lastUpdate = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                            lastUpdateBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
                            Address = new Address((int)reader["addressId"]);
                        }
                    }
                }
            }
        }

        public void Create()
        {
            throw new NotImplementedException("Create method not implemented for Customer class.");
        }

        public void Update()
        {
            throw new NotImplementedException("Update method not implemented for Customer class.");
        }

        public void Delete()
        {
            throw new NotImplementedException("Delete method not implemented for Customer class.");
        }

        public bool IsValid =>
            customerName != string.Empty &&
            Address.IsValid;
    }
}