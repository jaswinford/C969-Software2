using System;

namespace scheduler.structs
{
    /// <summary>
    /// DBObject object and logic to interact with DB for DBObject records
    /// </summary>
    public abstract class DBObject
    {
        private int _id = -1;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                this.Load();
            }
        }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public abstract void Load();
        public abstract void Create();
        public abstract void Update();
        public abstract void Delete();
        public abstract bool IsValid { get; }

        public DBObject()
        {
        }

        public DBObject(int id)
        {
            Id = id;
            Load();
        }

        public void Load(int id)
        {
            Id = id;
            Load();
        }
    }
}