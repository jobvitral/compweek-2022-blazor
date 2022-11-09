using CompWeek.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Mappings;

public class UserMapping
{
    public static void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            //key
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            
            //properties
            entity.Property(e => e.RoleId).IsRequired();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Nickname).IsRequired().HasMaxLength(250);
            entity.Property(e => e.DocumentNumber).HasMaxLength(100);
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.PhoneNumer).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Thumbnail).HasMaxLength(250);
            
            //relationships
            entity.HasOne(a => a.Role)
                .WithMany(a => a.Users)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}