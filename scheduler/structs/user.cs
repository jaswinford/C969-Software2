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

        /// <summary>
        ///     Indicates whether the user is currently active in the system.
        ///     This property retrieves the active status of the user by performing a database query
        ///     using the user's name.
        /// </summary>
        /// <remarks>
        ///     Returns true if the user is active and exists in the database; otherwise, false.
        ///     If a database error occurs during the query, false is returned, and an error message
        ///     is displayed to the user.
        /// </remarks>
        private bool IsActive
        {
            get
            {
                try
                {
                    var result =
                        DatabaseManager.Instance.ExecuteQuery("SELECT active FROM user WHERE userName = '" + Name + "'")
                            [0][0];
                    return Convert.ToSByte(result) == 1; // Convert to SByte to avoid overflows;
                }
                catch
                {
                    return false;
                }
            }
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

        public int Id
        {
            get
            {
                try
                {
                    var result =
                        DatabaseManager.Instance.ExecuteQuery("SELECT id FROM user WHERE userName = '" + Name + "'")
                            [0][0];
                    return Convert.ToInt32(result); // Convert to Int32 to avoid overflows;
                }
                catch
                {
                    return -1;
                }
            }
        }
    }
}