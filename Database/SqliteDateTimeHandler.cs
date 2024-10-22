using System.Data;
using System.Globalization;
using Dapper;

public class SqliteDateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override DateTime Parse(object value)
    {
        if (value is string stringValue)
        {
            if (DateTime.TryParseExact(stringValue,
                                       "dd/MM/yyyy hh:mm:ss tt",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out DateTime result))
            {
                return result;
            }

            if (DateTime.TryParseExact(stringValue,
                                       "dd/MM/yyyy h:mm:ss tt",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out DateTime result2))
            {
                return result2;
            }

            // If the above fails, might have to try other formats here

            throw new FormatException($"Unable to parse '{stringValue}' to DateTime");
        }
        throw new ArgumentException("Unable to convert to DateTime");
    }

    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value.ToString("dd-MM-yyyy HH:mm:ss");
    }
}
