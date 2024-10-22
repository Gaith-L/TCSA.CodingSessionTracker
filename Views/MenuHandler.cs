using Spectre.Console;

internal class MenuHandler
{
    private CodingController _codingController = new CodingController();
    private string _dateTimeFormat = "HH:mm";

    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What do you want to do next?")
                .AddChoices(MenuOptions.AllMenuOptions)
                );

            switch (menuChoice)
            {
                case MenuOptions.ViewSessions:
                    {
                        var sessions = _codingController.RetrieveSessions();
                        var table = new Table();
                        table.AddColumn("Id");
                        table.AddColumn("Start Time");
                        table.AddColumn("End Time");
                        table.AddColumn("Duration");

                        DisplaySessionsAsTable(sessions, table);

                        AnsiConsole.MarkupLine("Press Any Key to Continue.");
                        Console.ReadKey();

                        break;
                    }

                case MenuOptions.AddSessions:
                    {
                        CodingSession newSession = AddSessionsHelper();
                        _codingController.AddSession(newSession);
                        break;
                    }

                case MenuOptions.DeleteSessions:
                    {
                        DeleteItem();
                        break;
                    }
            }
        }
    }

    private static class MenuOptions
    {
        public const string ViewSessions = "View Sessions";
        public const string AddSessions = "Add Session";
        public const string DeleteSessions = "Delete Session";

        public static string[] AllMenuOptions = new[] { ViewSessions, AddSessions, DeleteSessions };
    }

    private CodingSession AddSessionsHelper()
    {
        DateTime startSession = PromptUserDateTime("Enter the start date and time of the coding session", _dateTimeFormat);
        DateTime endSession = PromptUserDateTime("Enter the end date and time of the coding session", _dateTimeFormat);
        TimeSpan duration = endSession - startSession;

        return new CodingSession(startSession, endSession, duration);
    }

    public void DeleteItem()
    {
        var sessions = _codingController.RetrieveSessions();
        if (sessions.Count() == 0)
        {
            AnsiConsole.MarkupLine("[bold red] There are no entries to delete.[/]");
            return;
        }

        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        DisplaySessionsAsTable(sessions, table);

        int sessionId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
            .Title("Choose a [red]session[/] to delete")
            .AddChoices(sessions.Select(session => Convert.ToInt32(session.Id)))); // Can this fail??? IDK!

        var confirm = AnsiConsole.Confirm($"Are you sure you want to delete session with Id {sessionId}?");
        if (confirm)
        {
            // Delete the selected session
            _codingController.DeleteById(sessionId);
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Deletion canceled.[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }

    private static void DisplaySessionsAsTable(List<CodingSession> sessions, Table table)
    {
        foreach (var session in sessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.StartTime.ToString(),
                session.EndTime.ToString(),
                session.Duration.ToString());
        }

        AnsiConsole.Write(table);
    }

    private DateTime PromptUserDateTime(string prompt, string format)
    {
        while (true)
        {
            Console.WriteLine($"{prompt} {format}"); // TODO: Change to use Spectre.Console
            string userInputStartSession = Console.ReadLine();
            if (DateTime.TryParse(userInputStartSession, out DateTime result))
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Invalid date/time. Please use the format: {format}");
            }
        }
    }
}
