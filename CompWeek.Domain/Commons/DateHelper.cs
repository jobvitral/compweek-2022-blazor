namespace CompWeek.Domain.Commons;

public class DateHelper
{
    public static DateTime GetNow()
    {
        return DateTime.UtcNow.AddHours(-3);
    }
}
