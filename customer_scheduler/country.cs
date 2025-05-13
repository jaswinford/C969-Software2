namespace customer_scheduler
{
    /// <summary>
    /// Country object and logic to interact with DB for country records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class Country : DBObject
    {
        /// <summary>
        /// Constructor for the Country class
        /// </summary>

        public string Name { get; set; } // country VARCHAR(50)

        public Country()
        {
            Name = string.Empty;
        }
    }
    {

    }
}
