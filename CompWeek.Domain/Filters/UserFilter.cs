namespace CompWeek.Domain.Filters;

public class UserFilter
{
    public int[]? Ids { get; set; }
    public int[]? Roles { get; set; }
    public string? Name { get; set; }
    public string? Document { get; set; }
    public Tuple<DateTime?, DateTime?>? RegistrationPeriod { get; set; }
    public Tuple<DateTime?, DateTime?>? ConfirmationPeriod { get; set; }
    public bool? IsConfirmed { get; set; }
    public bool? IsActive { get; set; }
}
