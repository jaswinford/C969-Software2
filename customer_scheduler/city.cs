namespace customer_scheduler
{
    /// <summary>
    /// City object and logic to interact with DB for city records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class City : DBObject
    {

        public string Name { get; set; }
        public Country Country { get; set; }

        public City()
        {
            Name = string.Empty;
            Country = new Country();
        }
    }
}
