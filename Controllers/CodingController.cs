using System.Configuration;
using CodingTracker.Database;
using Dapper;

internal class CodingController
{
    private readonly DatabaseManager _database;
    private readonly string codingSessionTable = "code_sessions";

    public CodingController()
    {
        string? connectionString = ConfigurationManager.AppSettings["connectionString"];
        _database = new DatabaseManager(connectionString!);
        _database.CreateDatabaseTable();
    }

    public List<CodingSession> RetrieveSessions()
    {
        // Register the custom type handler
        SqlMapper.AddTypeHandler(new SqliteDateTimeHandler());
        SqlMapper.AddTypeHandler(new SqliteTimeSpanHandler());

        using (var dbConnection = _database.CreateConnection())
        {
            dbConnection.Open();
            string query = @$"SELECT * FROM {codingSessionTable}";

            var sessions = dbConnection.Query<CodingSession>(query).ToList();

            return sessions;
        }
    }

    public CodingSession GetById(int id)
    {
        using (var dbConnection = _database.CreateConnection())
        {
            dbConnection.Open();
            string query = @$"
            SELECT * FROM {codingSessionTable} WHERE Id = @Id";

            CodingSession session = dbConnection.QuerySingleOrDefault<CodingSession>(query, new { Id = id });
            return session;
        }
    }

    public void AddSession(CodingSession session)
    {
        using (var dbConnection = _database.CreateConnection())
        {
            dbConnection.Open();

            string query = @$"
            INSERT INTO {codingSessionTable} (StartTime, EndTime, Duration)
            VALUES (@StartTime, @EndTime, @Duration)";

            var parameters = new
            {
                StartTime = session.StartTime.ToString(),
                EndTime = session.EndTime.ToString(),
                Duration = session.Duration.ToString()
            };
            dbConnection.Execute(query, parameters);
        }
    }

    public bool DeleteById(int id)
    {
        using (var dbConnection = _database.CreateConnection())
        {
            dbConnection.Open();
            string query = @$"
            DELETE FROM {codingSessionTable} WHERE Id = @Id";

            return (dbConnection.Execute(query, new { Id = id }) > 0) ? true : false;
        }
    }
}
