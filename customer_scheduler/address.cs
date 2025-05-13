namespace customer_scheduler
{
    /// <summary>
    /// Address object and logic to interact with DB for address records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class Address : DBObject
    {
        public string Address { get; set; } // address VARCHAR(50)
        public string Address2 { get; set; } // address2 VARCHAR(50)
        public City City { get; set; } // cityId INT(10)
        public string PostalCode { get; set; } // postalCode VARCHAR(10)
        public string Phone { get; set; } // phone VARCHAR(20)

        public Address()
        {
            Address = string.Empty;
            Address2 = string.Empty;
            City = new City();
            PostalCode = string.Empty;
            Phone = string.Empty;
        }
    }
}
