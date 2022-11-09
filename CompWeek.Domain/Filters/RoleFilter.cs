namespace CompWeek.Domain.Filters;

public class RoleFilter
{
    public int[]? Ids { get; set; }
    public string? Name { get; set; }
    public string? Scope { get; set; }
    public bool? IsDefault { get; set; }
}
