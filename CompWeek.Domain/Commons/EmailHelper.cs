using System.Text.RegularExpressions;

namespace CompWeek.Domain.Commons;

public class EmailHelper
{
    public static bool IsValid(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            bool isEmail = Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            return isEmail;
        }

        return false;
    }
}
