namespace CompWeek.Web.Domain.Models;

public class Credential
{
    public int? Id { get; set; }
    public int? RoleId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Scope { get; set; }
}