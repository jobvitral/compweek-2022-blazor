using CompWeek.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Mappings;

public class UserPasswordMapping
{
    public static void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPassword>(entity =>
        {
            //key
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            
            //properties
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.Password).IsRequired().HasMaxLength(250);
            entity.Property(e => e.RemindTip).HasMaxLength(250);
            
            //relationships
            entity.HasOne(a => a.User)
                .WithMany(a => a.Passwords)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}