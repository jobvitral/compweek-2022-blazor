using CompWeek.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Mappings;

public class UserRequestMapping
{
    public static void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRequest>(entity =>
        {
            //key
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            
            //properties
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.ValidationKey).IsRequired().HasMaxLength(250);

            //relationships
            entity.HasOne(a => a.User)
                .WithMany(a => a.Requests)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}