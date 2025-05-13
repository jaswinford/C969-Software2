namespace customer_scheduler
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
        public string Name { get; set; } // customerName VARCHAR(45)
        public Address Address { get; set; } // addressId INT(10)
        public bool Active { get; set; } // active TINYINT(1)

        public Customer()
        {

        }
    }
}
