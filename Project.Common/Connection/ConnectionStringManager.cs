using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Common.Connection
{
    /// <summary>
    /// Used to get individual properties from the connection string
    /// </summary>
    public class ConnectionStringManager
    {
        const string pattern = @"[a-zA-Z0-9_.\-]*";

        public ConnectionStringManager(string connectionString)
        {
            SetConnectionStringProperties(connectionString);
        }

        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string DatabaseHost { get; set; }


        /// <summary>
        /// Gets information from the connection string and separates it into local propeprties
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetConnectionStringProperties(string connectionString)
        {
            SetUserName(connectionString);
            SetUserPassword(connectionString);
            SetDatabaseName(connectionString);
            SetDatabaseHost(connectionString);
        }

        /// <summary>
        /// Get the UserName from the connection string and place it to local property UserName
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetUserName(string connectionString)
        {
            Match userName = Regex.Match(connectionString, @"User ID=" + pattern);
            UserName = Regex.Replace(userName.Value, @"User ID=", "");
        }

        /// <summary>
        /// Get the User password from the connection string and place it to local property UserPassword
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetUserPassword(string connectionString)
        {
            Match userPassword = Regex.Match(connectionString, @"Password=" + pattern);
            UserPassword = Regex.Replace(userPassword.Value, @"Password=", "");
        }

        /// <summary>
        /// Get the Database name from the connection string and place it to local property DatabaseName
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetDatabaseName(string connectionString)
        {
            Match databaseName = Regex.Match(connectionString, @"Database=" + pattern);
            DatabaseName = Regex.Replace(databaseName.Value, @"Database=", "");
        }

        /// <summary>
        /// Get the Database host from the connection string and place it to local property DatabaseHost
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetDatabaseHost(string connectionString)
        {
            Match databaseHost = Regex.Match(connectionString, @"Server=" + pattern);
            DatabaseHost = Regex.Replace(databaseHost.Value, @"Server=", "");
        }



    }
}
