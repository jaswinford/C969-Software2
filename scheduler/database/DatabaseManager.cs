using System;
using System.Configuration;
using System.Data;
using System.Threading;
using MySql.Data.MySqlClient;

namespace scheduler.database
{
    /// <summary>
    ///     Provides singleton-based management for database connections and operations.
    /// </summary>
    /// <remarks>
    ///     DatabaseManager is designed to handle connection-related tasks to a MySQL database,
    ///     ensuring that only one active instance of the manager exists in the application.
    /// </remarks>
    public class DatabaseManager
    {
        private const int CONNECTION_TIMEOUT_SECONDS = 30;
        private static DatabaseManager _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private MySqlConnection _connection;

        private DatabaseManager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        }

        public static DatabaseManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new DatabaseManager();
                }
            }
        }

        public MySqlConnection Connection
        {
            get
            {
                if (_connection == null) _connection = new MySqlConnection(_connectionString);

                return _connection;
            }
        }

        public void Connect()
        {
            try
            {
                if (_connection?.State == ConnectionState.Open) return; // Already connected

                Connection.Open();

                // Wait until connection is fully established or timeout occurs
                var timeoutTime = DateTime.Now.AddSeconds(CONNECTION_TIMEOUT_SECONDS);
                while (!IsConnectionValid())
                {
                    if (DateTime.Now > timeoutTime)
                        throw new TimeoutException(
                            $"Database connection could not be established within {CONNECTION_TIMEOUT_SECONDS} seconds");

                    Thread.Sleep(100); // Wait 100ms before checking again
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database connection failed", ex);
            }
        }

        private bool IsConnectionValid()
        {
            try
            {
                using (var cmd = new MySqlCommand("SELECT 1", Connection))
                {
                    cmd.ExecuteScalar();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_connection?.State == ConnectionState.Open) _connection.Close();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database disconnection failed", ex);
            }
        }

        public static string SanitizeString(string input)
        {
            input = input.Replace("'", "''");
            input = input.Trim();
            return input;
        }

        public static string SanitizeString(string input, int maxLength)
        {
            input = SanitizeString(input);
            if (input.Length > maxLength) input = input.Substring(0, maxLength);
            return input;
        }
    }
}