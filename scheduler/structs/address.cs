namespace scheduler.structs
{
    /// <summary>
    /// Address object and logic to interact with DB for address records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class Address : DBObject
    {
        public string Address1 { get; set; } = string.Empty; // address VARCHAR(50)
        public string Address2 { get; set; } = string.Empty; // address2 VARCHAR(50)
        public City City { get; set; } = new City(); // cityId INT(10)
        public string PostalCode { get; set; } = string.Empty; // postalCode VARCHAR(10)
        public string Phone { get; set; } = string.Empty; // phone VARCHAR(20)
    }
}