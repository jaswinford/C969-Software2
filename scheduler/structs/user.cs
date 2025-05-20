namespace scheduler.structs
{
    /// <summary>
    /// User object and logic to interact with DB for user records
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class User : DBObject
    {
        public string Name { get; set; } = string.Empty; // userName VARCHAR(50)

        public bool IsActive { get; set; } = true; // active TINYINT
        // We don't store the user password, if at all possible.

        public bool Authenticated
        {
            get
            {
                if (!IsActive) return false;

                return false;
            }
        }
    }
}