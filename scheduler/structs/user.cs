namespace customer_scheduler
{
    /// <summary>
    /// User object and logic to interact with DB for user records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class User : DBObject
    {
        public string Name { get; set; } // userName VARCHAR(50)
        public bool IsActive { get; set; } // active TINYINT
        // We don't store the users password, if at all possible.

        public User()
        {
            Name = string.Empty;
            IsActive = true;
        }
    }
}
