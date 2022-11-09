using CompWeek.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Mappings;

public class RoleMapping
{
    public static void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            //key
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            
            //properties
            entity.Property(e => e.Name).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Scope).HasMaxLength(250);
            
            //relationships
        });
    }
}