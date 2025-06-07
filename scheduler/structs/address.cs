using System;
using System.Text.RegularExpressions;
using System.Windows;
using scheduler.database;

namespace scheduler.structs
{
    /// <summary>
    /// Address object and logic to interact with DB for address records
    /// </summary>
    public class Address : DBObject
    {
        private string _address1;
        private string _address2;
        private string _phone;
        private string _postalCode;

        public int Id { get; set; } = -1;

        public string Address1
        {
            get => _address1;
            set => _address1 = DatabaseManager.SanitizeString(value, 50);
        } // address VARCHAR(50)

        public string Address2
        {
            get => _address2;
            set => _address2 = DatabaseManager.SanitizeString(value, 50);
        } // address2 VARCHAR(50)

        public City City { get; set; } // cityId INT(10)

        public string PostalCode
        {
            get => _postalCode;
            set => _postalCode = DatabaseManager.SanitizeString(value, 10);
        } // postalCode VARCHAR(10)

        public string Phone
        {
            get => _phone;
            set => _phone = DatabaseManager.SanitizeString(value, 20);
        } // phone VARCHAR(20)

        /// <summary>
        ///     Determines whether the current address instance satisfies defined validation rules.
        /// </summary>
        /// <remarks>
        ///     A.2.a - Validation is based on the following criteria:
        ///     - The `Address1`, `City.Name`, `PostalCode`, and `Phone` fields must be non-empty.
        ///     - The `Phone` field must match a valid pattern that permits only digits and dashes.
        /// </remarks>
        /// <returns>
        ///     Returns true if the address is valid according to the specified conditions; otherwise, false.
        /// </returns>
        public override bool IsValid
        {
            get
            {
                if (_address1 == string.Empty || City.Name == string.Empty || _postalCode == string.Empty ||
                    _phone == string.Empty) return false; //verify address is provided
                if (!Regex.IsMatch(_phone, @"^[0-9-]+$")) return false;
                return true;
            }
        }

        public override void Load()
        {
            var result =
                DatabaseManager.Instance.ExecuteQuery(
                    "SELECT addressId, address, address2, cityId, postalCode, phone, createDate, createdBy,  FROM address WHERE id = ?",
                    new object[] { Id });
            if (result.Count == 0) return; //Don't load if doesn't exist'
            var row = result[0];
            Id = Convert.ToInt32(row[0]);
            Address1 = row[1].ToString();
            Address2 = row[2].ToString();
            City.Id = Convert.ToInt32(row[3]);
            PostalCode = row[4].ToString();
            Phone = row[5].ToString();
            CreatedAt = DateTime.Parse(row[6].ToString());
            CreatedBy = row[7].ToString();
            UpdatedAt = DateTime.Parse(row[8].ToString());
            UpdatedBy = row[9].ToString();
        }

        public override void Create()
        {
            if (Id != -1) return; //Don't create if already exists'
            if (!IsValid) return; //Don't create if invalid'

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var query =
                "INSERT INTO address (address1, address2, cityId, postalCode, phone, createDate, createdBy,lastUpdate, lastUpdateBy) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
            var parameters = new object[]
            {
                Address1,
                Address2,
                City.Id,
                PostalCode,
                Phone,
                timestamp,
                State.Instance.CurrentUser.Name,
                timestamp,
                State.Instance.CurrentUser.Name,
            };
            try
            {
                DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void Update()
        {
            if (Id == -1) return; //Don't update if doesn't exist'
            if (!IsValid) return; //Don't update if invalid'

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var query =
                "UPDATE address SET address1 = ?, address2 = ?, cityId = ?, postalCode = ?, phone = ? , lastUpdate = ? , lastUpdateBy = ? WHERE id = ?";
            var parameters = new object[]
            {
                Address1,
                Address2,
                City.Id,
                PostalCode,
                Phone,
                Id,
                timestamp,
                State.Instance.CurrentUser.Name
            };
            try
            {
                DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void Delete()
        {
            if (Id == -1) return; //Don't delete if doesn't exist'
            var query = "DELETE FROM address WHERE id = ?";
            var parameters = new object[] { Id };
            try
            {
                DatabaseManager.Instance.ExecuteNonQuery(query, parameters);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}