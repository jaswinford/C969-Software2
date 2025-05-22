using System.Text.RegularExpressions;
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
        public bool IsValid
        {
            get
            {
                if (_address1 == string.Empty || City.Name == string.Empty || _postalCode == string.Empty ||
                    _phone == string.Empty) return false; //verify address is provided
                if (!Regex.IsMatch(_phone, @"^[0-9-]+$")) return false;
                return true;
            }
        }
    }
}