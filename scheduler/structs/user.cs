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
                    DatabaseManager.Instance.Connect();
                    using (var cmd = new MySqlCommand("SELECT active FROM user WHERE userName = '" + Name + "'",
                               DatabaseManager.Instance.Connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) return reader.GetBoolean(0);
                        }
                    }
                }
                catch
                {
                    LanguageManager.Instance.ShowMessageBox("Message.SQLError", "Title.SQLError", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    DatabaseManager.Instance.Disconnect();
                }

                return false;
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
                DatabaseManager.Instance.Connect();
                using (var cmd = new MySqlCommand("SELECT password FROM user WHERE userName = '" + Name + "'",
                           DatabaseManager.Instance.Connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return reader.GetString(0) == password;
                    }
                }
            }
            catch (MySqlException)
            {
                LanguageManager.Instance.ShowMessageBox("Message.SQLError", "Title.SQLError", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                DatabaseManager.Instance.Disconnect();
            }

            return false;
        }
    }
}