using scheduler.database;

namespace scheduler.structs
{
    /// <summary>
    /// City object and logic to interact with DB for city records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class City : DBObject
    {
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; } = new Country();

        public void Update()
        {
        }
    }
}