namespace scheduler.structs
{
    /// <summary>
    /// Country object and logic to interact with DB for country records
    /// </summary>
    public partial class Country : DBObject
    {
        public string Name { get; set; } = string.Empty; // country VARCHAR(50)
    }
}