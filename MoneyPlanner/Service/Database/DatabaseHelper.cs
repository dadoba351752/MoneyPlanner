using Microsoft.Data.Sqlite;
using System;
using System.CodeDom;
using System.IO;
using System.Runtime.InteropServices;

namespace MoneyPlanner.Service.Database
{
    internal class DatabaseHelper
    {
        public static string BaseDir = AppContext.BaseDirectory;
        public static string ProjectDir = Directory.GetParent(BaseDir).Parent.Parent.Parent.FullName;
        public static string DatabasePath = Path.Combine(ProjectDir, "Files", "MoneyPlanner.db");

        private static string ConnectionString = $"Data Source={DatabasePath};Foreign Keys=True";
        public static void InitializeDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath));
                CreateUsersTable(connection);
                CreateTransactionsTable(connection);
                CreateSettingsTable(connection);
            }
        }
        public static void CreateUsersTable(SqliteConnection connection)
        {
                string sql =
                @"
                    CREATE TABLE IF NOT EXISTS Uzivatele (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Surname TEXT NOT NULL,
                        BirthNumber TEXT NOT NULL UNIQUE
                    );
                ";
                using (var command = new SqliteCommand(sql, connection))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
        }
        public static void CreateTransactionsTable(SqliteConnection connection)
        {
            string sql =
                @"
                    CREATE TABLE IF NOT EXISTS Transactions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Symbol TEXT,
                    Price INTEGER NOT NULL,
                    Currency TEXT NOT NULL,
                    Amount INTEGER NOT NULL,
                    Volume INTEGER NOT NULL,
                    Date TEXT NOT NULL, -- 'YYYY-MM-DD'
                    UserId INTEGER NOT NULL,
                    FOREIGN KEY (UserId) REFERENCES Uzivatele(Id) ON DELETE RESTRICT
                    );
                ";
            using (var command = new SqliteCommand(sql, connection))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
        public static void CreateSettingsTable(SqliteConnection connection)
        {
            string sql =
                @"
                    CREATE TABLE IF NOT EXISTS Settings (
                    Name TEXT NOT NULL,
                    Value TEXT NOT NULL);
                ";
            using (var command = new SqliteCommand(sql, connection))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
