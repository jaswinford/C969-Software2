namespace customer_scheduler
{
    /// <summary>
    /// DBObject object and logic to interact with DB for DBObject records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class DBObject
    {
        public int Id { get; }
        public DateTime CreatedAt { get; }
        public User CreatedBy { get; }
        public DateTime UpdatedAt { get; }
        public User UpdatedBy { get; }

        public  DBObject()
        {
            Id = null;
            CreatedAt = null;
            CreatedBy = null;
            UpdatedAt = null;
            UpdatedBy = null;
        }
    }
}
