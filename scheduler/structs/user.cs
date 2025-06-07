using System;
using System.Windows;
using MySql.Data.MySqlClient;
using scheduler.database;

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
        private bool IsActive { get; set; } = false;

        public User(string name)
        {
            Name = name;
            Load();
        }

        public User(int id)
        {
            Id = id;
            var result =
                DatabaseManager.Instance.ExecuteQuery("SELECT userName FROM user WHERE userId = ?",
                    new object[] { Id })[0];
            Name = result[0].ToString();
            Load();
        }

        /// <summary>
        ///     Authenticates the user by verifying the provided password against the stored password in the database.
        /// </summary>
        /// <param name="password">The password to authenticate the user with.</param>
        /// <returns>
        ///     Returns true if the provided password matches the stored password for the user and the user is active, otherwise
        ///     returns false.
        /// </returns>
        public bool Authenticated(string password)
        {
            Load(); // Always make sure we're up to date.
            if (!IsActive) return false;

            try
            {
                var result =
                    DatabaseManager.Instance.ExecuteQuery("SELECT password FROM user WHERE userName = '" + Name + "'")
                        [0][0];
                var storedPassword = result.ToString();
                return storedPassword == password; // Compare stored password with provided password;
            }
            catch (MySqlException)
            {
                LanguageManager.Instance.ShowMessageBox("Message.SQLError", "Title.SQLError", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return false;
        }

        public override void Load()
        {
            var result =
                DatabaseManager.Instance.ExecuteQuery("SELECT userId,userName,active FROM user WHERE userName = ?",
                    new object[] { Name });
            if (result.Count == 0) return; //Don't load if doesn't exist'
            var row = result[0];
            Id = Convert.ToInt32(row[0]);
            Name = row[1].ToString();
            IsActive = Convert.ToBoolean(row[2]);
        }
    }
}