using Microsoft.Data.Sqlite;
using System.IO;

namespace MoneyPlanner.Service.Database
{
    internal class DatabaseHelper
    {
        public static string DatabasePath = @"C:\Users\adams\source\repos\MoneyPlanner\MoneyPlanner\Files\MoneyPlanner.db";
        private static string ConnectionString = $"Data Source={DatabasePath}";

        public static void InitializeDatabase()
        {
            string fullPath = DatabasePath;
            if (!File.Exists(fullPath))
            {
                using (var tempConnection = new SqliteConnection($"Data Source={fullPath}"))
                {
                    tempConnection.Open();
                }
            }
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

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
        }

        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
