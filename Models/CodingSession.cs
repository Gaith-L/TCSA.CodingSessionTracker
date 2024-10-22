internal class CodingSession
{
    internal string Id { get; private set; }
    internal DateTime StartTime { get; set; }
    internal DateTime EndTime { get; set; }
    internal TimeSpan Duration { get; set; }


    public CodingSession() { }
    public CodingSession(DateTime startTime, DateTime endTime, TimeSpan duration)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
    }
}

internal static class CodingSessionSerializer
{
    internal static void SerializeDbToCodingSessionObj()
    {

    }
}
