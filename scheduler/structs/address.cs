using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace scheduler
{
    /// <summary>
    /// Address object and logic to interact with DB for address records
    /// </summary>
    public class Address
    {
        // Public Variables
        public int AddressId = -1; // addressId INT
        public string Address1 = string.Empty;
        public string Address2 = string.Empty;
        public City City = new City();
        public string PostalCode = string.Empty;
        public string Phone = string.Empty;
        public DateTime CreateDate = DateTime.Now; // createDate DATETIME
        public string CreatedBy = string.Empty; // createdBy VARCHAR(50)
        public DateTime LastUpdate = DateTime.Now; // lastUpdate DATETIME
        public string LastUpdateBy = string.Empty; // lastUpdateBy VARCHAR(50)


        public static explicit operator Address(SqlDataReader reader)
        {
            var address = new Address();
            if (reader.Read())
            {
                address.AddressId = (int)reader["addressId"]; // addressId INT
                address.Address1 = (string)reader["address"]; // address VARCHAR(50)
                address.Address2 = (string)reader["address2"]; // address2 VARCHAR(50)
                address.City = new City((int)reader["cityId"]);
                address.PostalCode = (string)reader["postalCode"]; // postalCode VARCHAR(20)
                address.Phone = (string)reader["phone"]; // phone VARCHAR(20)
                address.CreateDate = (DateTime)reader["createDate"]; // createDate DATETIME
                address.CreatedBy = (string)reader["createdBy"]; // createdBy VARCHAR(50)
                address.LastUpdate = (DateTime)reader["lastUpdate"]; // lastUpdate DATETIME
                address.LastUpdateBy = (string)reader["lastUpdateBy"]; // lastUpdateBy VARCHAR(50)
            }

            return address;
        }

        public Address(int id = -1)
        {
            AddressId = id;
            if (AddressId == -1) return; // If the AddressId is -1, do not load the address
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLConnection"]
                .ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM address WHERE addressId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", AddressId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Address1 = (string)reader["address"];
                            Address2 = (string)reader["address2"];
                            City = new City((int)reader["cityId"]);
                            PostalCode = (string)reader["postalCode"];
                            Phone = (string)reader["phone"];
                            CreateDate = (DateTime)reader["createDate"];
                            CreatedBy = (string)reader["createdBy"];
                            LastUpdate = (DateTime)reader["lastUpdate"];
                            LastUpdateBy = (string)reader["lastUpdateBy"];
                        }
                    }
                }
            }
        }


        public bool IsValid
        {
            get
            {
                if (Address1 == string.Empty || City.Name == string.Empty || PostalCode == string.Empty ||
                    Phone == string.Empty) return false; //verify address is provided
                if (!Regex.IsMatch(Phone, @"^[0-9-]+$")) return false;
                return true;
            }
        }

        // Functions

        public void Create()
        {
            throw new NotImplementedException(); //Create method not implemented for Address class
        }

        public void Update()
        {
            throw new NotImplementedException(); //Update method not implemented for Address class
        }

        public void Delete()
        {
            throw new NotImplementedException(); //Delete method not implemented for Address class
        }

        public override string ToString()
        {
            string output = "";
            if (Address1 != string.Empty) output += Address1 + "\n";
            if (Address2 != string.Empty) output += Address2 + "\n";
            if (City.Name != string.Empty) output += City.Name + ", ";
            if (PostalCode != string.Empty) output += PostalCode + "\n";
            if (Phone != string.Empty) output += Phone;
            return output;
        }
    }
}