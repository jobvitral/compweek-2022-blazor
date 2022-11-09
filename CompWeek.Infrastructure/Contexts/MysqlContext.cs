using CompWeek.Domain.Models;
using CompWeek.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompWeek.Infrastructure.Contexts;

public class MysqlContext : DbContext
{
    private IConfiguration _configuration;
    
    public DbSet<Role>? Role { get; set; }
    public DbSet<User>? User { get; set; }
    public DbSet<UserPassword>? UserPassword { get; set; }
    public DbSet<UserRequest>? UserRequest { get; set; }
    
    public MysqlContext(IConfiguration configuration) : base()
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString));

        optionsBuilder
            .UseMySql(connectionString!, serverVersion)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        RoleMapping.Map(modelBuilder);
        UserMapping.Map(modelBuilder);
        UserPasswordMapping.Map(modelBuilder);
        UserRequestMapping.Map(modelBuilder);
    }
}