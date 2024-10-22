# Coding Time Logger

This application is part of the CSharp Academy project roadmap. It is a tool for logging daily coding time, designed to reinforce concepts such as handling Dates and Times, using external libraries, and applying the Separation of Concerns principle. It uses Spectre.Console for data display and Dapper ORM for database access.

## Installation

1. Clone the repository:

    ```sh
    git clone https://github.com/htiaGG/TCSA.CodingSessionTracker.git
    cd TCSA.CodingSessionTracker
    ```

2. Install dependencies:
    ```sh
    dotnet add package Dapper
    dotnet add package Spectre.Console
    dotnet add package Microsoft.Data.Sqlite
    dotnet add package System.Configuration.ConfigurationManager
    ```

## Usage

1. Run the application:

    ```sh
    dotnet run
    ```

2. Follow the prompts to input start and end times for your coding sessions. The application will calculate and display the session duration.

## Configuration

Ensure the configuration .xml file `App.config` contains the correct database path and connection strings.

Example `App.config`:

```xml
<add
    key="connectionString"
    value="Data Source=code-tracker.db"
/>
```
