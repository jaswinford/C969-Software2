using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows;
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
        private const int ConnectionTimeoutSeconds = 30;
        private static DatabaseManager _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private MySqlConnection _connection;

        private DatabaseManager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        }

        public void ExecuteNonQuery(string query)
        {
            ExecuteNonQuery(query, null);
        }

        public void ExecuteNonQuery(string query, params object[] parameters)
        {
            try
            {
                Connect();
                using (var cmd = new MySqlCommand(query, Connection))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                LanguageManager.Instance.ShowMessageBox("Message.SQLError", "Title.SQLError", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                Disconnect();
            }
        }

        public List<object[]> ExecuteQuery(string query)
        {
            return ExecuteQuery(query, null);
        }

        public List<object[]> ExecuteQuery(string query, params object[] parameters)
        {
            try
            {
                Instance.Connect();
                using (var cmd = new MySqlCommand(query, Instance.Connection))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<object[]>();
                        while (reader.Read())
                        {
                            var row = new object[reader.FieldCount];
                            reader.GetValues(row);
                            result.Add(row);
                        }

                        return result;
                    }
                }
            }
            catch (MySqlException)
            {
                LanguageManager.Instance.ShowMessageBox("Message.SQLError", "Title.SQLError", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return null;
            }
            catch (Exception)
            {
                LanguageManager.Instance.ShowMessageBox("Message.UnspecifiedError", "Title.UnspecifiedError",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                Disconnect();
            }
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
                var timeoutTime = DateTime.Now.AddSeconds(ConnectionTimeoutSeconds);
                while (!IsConnectionValid())
                {
                    if (DateTime.Now > timeoutTime)
                        throw new TimeoutException(
                            $"Database connection could not be established within {ConnectionTimeoutSeconds} seconds");

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