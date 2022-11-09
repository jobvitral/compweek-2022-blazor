namespace CompWeek.Domain.Filters;

public class UserRequestFilter
{
    public int[]? Ids { get; set; }
    public int[]? Users { get; set; }
    public string[]? Keys { get; set; }
    public Tuple<DateTime?, DateTime?>? CreationPeriod { get; set; }
    public Tuple<DateTime?, DateTime?>? ExpirationPeriod { get; set; }
    public Tuple<DateTime?, DateTime?>? UsedPeriod { get; set; }
    public bool? IsExpired { get; set; }
    public bool? IsUsed { get; set; }
}
