using System;
using System.Runtime.InteropServices;
using scheduler.database;

namespace scheduler.structs
{
    /// <summary>
    /// Customer object and logic to interact with DB for customer records
    /// </summary>
    ///
    /// TODO: Add member variables
    /// TODO: 'Add' Function
    /// TODO: 'Update' Function
    /// TODO: 'Delete' Function
    /// TODO: 'Validate' Function
    public partial class Customer : DBObject
    {
        private bool _modified = false; //Track if the object is different from DB
        private string _name = string.Empty;
        private string _phone = string.Empty;
        private Address _address = new Address();

        public string Name
        {
            get => _name;
            set
            {
                if (value == String.Empty) return; //Don't allow empty names
                _modified = true;
                _name = value;
            }
        }

        public Address Address
        {
            get => _address;
            set
            {
                if (!value.IsValid) return; //Do not update with invalid address
                _modified = true;
                _address = value;
            }
        } // addressId INT(10)

        public bool IsActive
        {
            get //Important enough to warrant always getting from DB 
            {
                if (Id == -1) return false; //If they're not in the DB, they're not active
                var result =
                    DatabaseManager.Instance.ExecuteQuery("SELECT active FROM customer WHERE id = ?",
                        new object[] { Id })[0][0];
                return Convert.ToBoolean(result); //Convert to bool to avoid overflows;
            }
            set
            {
                var parameters = new object[] { value, Id };
                DatabaseManager.Instance.ExecuteNonQuery("UPDATE customer SET active = ? WHERE id = ?", parameters);
            }
        } // active TINYINT(1)

        public string Phone
        {
            get => _phone;
            set
            {
                if (!ValidatePhone(value)) return; //Don't allow invalid phone numbers
                _modified = true;
                _phone = value;
            }
        } // phone VARCHAR(20)

        public bool IsValid =>
            Name != string.Empty &&
            ValidatePhone(Phone) &&
            Address.IsValid;

        public static bool ValidatePhone(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, "^[0-9-]+$") && value.Length <= 20 &&
                   value.Length >= 10;
        }
    }
}