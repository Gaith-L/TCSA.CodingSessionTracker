using System.Data;
using Dapper;

public class SqliteTimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
    {
        if (value is string stringValue)
        {
            if (TimeSpan.TryParse(stringValue, out TimeSpan result))
            {
                return result;
            }
            throw new FormatException($"Unable to parse '{stringValue}' to TimeSpan");
        }
        throw new ArgumentException("Unable to convert to TimeSpan");
    }

    public override void SetValue(IDbDataParameter parameter, TimeSpan value)
    {
        parameter.Value = value.ToString();
    }
}
