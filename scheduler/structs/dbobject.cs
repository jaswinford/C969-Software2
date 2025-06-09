using System;

namespace scheduler.structs
{
    /// <summary>
    /// DBObject object and logic to interact with DB for DBObject records
    /// </summary>
    public abstract class DBObject
    {
        public int Id;
        public DateTime CreatedAt;
        public string CreatedBy;
        public DateTime UpdatedAt;
        public string UpdatedBy;

        public abstract void Load();
        public abstract void Create();
        public abstract void Update();
        public abstract void Delete();
        public abstract bool IsValid { get; }

        public DBObject()
        {
        }

        public void Save()
        {
            if (Id == -1) Create();
            else Update();
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