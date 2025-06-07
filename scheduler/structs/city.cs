using System;
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

        public override void Load()
        {
            if (Id == -1) throw new Exception("Cannot load city with no ID");

            var result = DatabaseManager.Instance.ExecuteQuery(
                "SELECT cityId, countryId, name, createDate,createdBy,lastUpdate,lastUpdateBy FROM city WHERE id = ?",
                new object[] { Id })[0];
            Id = Convert.ToInt32(result[0]);
            Country = new Country(Convert.ToInt32(result[1]));
            Name = result[2].ToString();
            CreatedAt = DateTime.Parse(result[3].ToString());
            CreatedBy = result[4].ToString();
            UpdatedAt = DateTime.Parse(result[5].ToString());
            UpdatedBy = result[6].ToString();
        }

        public City(int id)
        {
            Id = id;
            Load();
        }

        public static City Lookup(string name)
        {
            var result =
                DatabaseManager.Instance.ExecuteQuery("SELECT cityId FROM city WHERE name = ?", new object[] { name });
            if (result.Count == 0) return null;
            return new City(Convert.ToInt32(result[0][0]));
        }

        public override void Create()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Delete()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsValid => Name != string.Empty && Country.Id != -1;
    }
}