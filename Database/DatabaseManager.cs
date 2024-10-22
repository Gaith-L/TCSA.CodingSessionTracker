using Microsoft.Data.Sqlite;

namespace CodingTracker.Database;

internal class DatabaseManager
{
    private readonly string _connectionString;
    private readonly SqliteConnection _connection;

    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        return connection;
    }

    internal void CreateDatabaseTable()
    {
        using (var dbConnection = CreateConnection())
        {
            dbConnection.Open();

            using (var tableCmd = dbConnection.CreateCommand())
            {
                tableCmd.CommandText = @$"CREATE TABLE IF NOT EXISTS code_sessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT
                )";
                tableCmd.ExecuteNonQuery();
            }
        }
    }

}
