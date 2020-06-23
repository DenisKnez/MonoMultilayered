using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Project.Common.Connection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;
using Npgsql;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.Data;

namespace Project.DAL.System
{
    //TODO: replace the hard coded connection string
    public class Initial_Maintenance : IInitial_Maintenance
    {
        public IConfiguration Configuration { get; }

        public Initial_Maintenance(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private string ConnectionString { get { return Configuration.GetConnectionString("PostgreDatabase"); } } 

        /// <summary>
        /// Execute all the necessary commands that need to setup the database so the migrations can be applied
        /// </summary>
        public async Task Initiate()
        {
            await CreateDatabaseAsync();
        }

        /// <summary>
        /// Creates the database if it does not exist, it creates it by using a database name provided in the connection string
        /// </summary>
        /// <returns></returns>
        private async Task CreateDatabaseAsync()
        {
            ConnectionStringManager conStrManager = new ConnectionStringManager(ConnectionString);

            // connection string to connect to a database machine, and not a particular database so it can be created if it does not exist
            string connectionString = RemoveDbNameFromConnectionString(ConnectionString);


            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    await connection.ExecuteAsync(
                        $@"do $$
                        DECLARE
                            _db TEXT := '{conStrManager.DatabaseName}';
                            _user TEXT := '{conStrManager.UserName}';
                            _password TEXT := '{conStrManager.UserPassword}';
                            _host TEXT := '{conStrManager.DatabaseHost}';
                        BEGIN
                            CREATE EXTENSION IF NOT EXISTS dblink; -- enable extension
                            IF EXISTS (SELECT 1 FROM pg_database WHERE datname = _db) THEN
                            RAISE NOTICE 'Database already exists';
                            ELSE
                            PERFORM dblink_connect('host=' || _host || ' user=' || _user || ' password=' || _password || ' dbname=' || current_database());
                            PERFORM dblink_exec('CREATE DATABASE ' || _db);
                            END IF;
                        end $$
                        ");

                }
                catch (Exception e)
                {
                    Debug.WriteLine("ERROR: " + e.Message);
                }

            }
        }


        /// <summary>
        /// Removes the database name from the connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private string RemoveDbNameFromConnectionString(string connectionString)
        {
            const string pattern = @"[a-zA-Z0-9_\-;]*";

            string databaseNamePattern = $"Database={pattern}";

            return Regex.Replace(connectionString, databaseNamePattern, string.Empty);

        }

    }
}
 