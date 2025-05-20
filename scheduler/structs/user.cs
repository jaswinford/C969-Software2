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

        public bool IsActive
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
        // We don't store the user password, if at all possible.

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