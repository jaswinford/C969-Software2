using System;

namespace scheduler.structs
{
    /// <summary>
    /// DBObject object and logic to interact with DB for DBObject records
    /// </summary>
    public class DBObject
    {
        public int Id { get; } = -1;
        public DateTime CreatedAt { get; }
        public User CreatedBy { get; } = null;
        public DateTime UpdatedAt { get; }
        public User UpdatedBy { get; } = null;
    }
}