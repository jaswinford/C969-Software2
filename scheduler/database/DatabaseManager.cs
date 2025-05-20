using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace scheduler.database
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;
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
                if (_instance == null) _instance = new DatabaseManager();

                return _instance;
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
                if (_connection != null && _connection.State != ConnectionState.Open) _connection.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database connection failed", ex);
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_connection != null && _connection.State != ConnectionState.Closed) _connection.Close();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database disconnection failed", ex);
            }
        }
    }
}