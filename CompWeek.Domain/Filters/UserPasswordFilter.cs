namespace CompWeek.Domain.Filters;

public class UserPasswordFilter
{
    public int[]? Ids { get; set; }
    public int[]? Users { get; set; }
    public Tuple<DateTime?, DateTime?>? CreationPeriod { get; set; }
    public Tuple<DateTime?, DateTime?>? ExpirationPeriod { get; set; }
    public Tuple<DateTime?, DateTime?>? InactivationPeriod { get; set; }
    public bool? IsActive { get; set; }
}