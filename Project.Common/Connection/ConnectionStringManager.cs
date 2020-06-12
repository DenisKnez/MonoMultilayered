using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Common.Connection
{
    public class ConnectionStringManager
    {
        const string pattern = @"[a-zA-Z0-9_.\-]*";

        public ConnectionStringManager(string connectionString)
        {
            SetConnectionStringProperties(connectionString);
            //SetConnectionStringProperties(RuntimeConfiguration.GetConnectionString("PostgreSQL_Connection_Key"));
        }

        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string DatabaseHost { get; set; }

        public void SetConnectionStringProperties(string connectionString)
        {
            Console.WriteLine("Connection String: " + connectionString);
            SetUserName(connectionString);
            SetUserPassword(connectionString);
            SetDatabaseName(connectionString);
            SetDatabaseHost(connectionString);
        }

        public void SetUserName(string connectionString)
        {
            Match userName = Regex.Match(connectionString, @"User ID=" + pattern);
            UserName = Regex.Replace(userName.Value, @"User ID=", "");
        }

        public void SetUserPassword(string connectionString)
        {
            Match userPassword = Regex.Match(connectionString, @"Password=" + pattern);
            UserPassword = Regex.Replace(userPassword.Value, @"Password=", "");
        }

        public void SetDatabaseName(string connectionString)
        {
            //User ID=postgres;Password=denis;Host=192.168.1.3;Port=5432;Database=postgres;

            Match databaseName = Regex.Match(connectionString, @"Database=" + pattern);

            DatabaseName = Regex.Replace(databaseName.Value, @"Database=", "");

            // FOR DEBUGGING
            //Debug.WriteLine("Database value: " + connectionStringDatabasePart.Value);
            //Debug.WriteLine("Database name: " + databaseName);
        }

        public void SetDatabaseHost(string connectionString)
        {
            //User ID=postgres;Password=denis;Host=192.168.1.3;Port=5432;Database=postgres;

            Match databaseHost = Regex.Match(connectionString, @"Server=" + pattern);

            DatabaseHost = Regex.Replace(databaseHost.Value, @"Server=", "");

            // FOR DEBUGGING
            //Debug.WriteLine("Database value: " + connectionStringDatabasePart.Value);
            //Debug.WriteLine("Database name: " + databaseName);
        }



    }
}
