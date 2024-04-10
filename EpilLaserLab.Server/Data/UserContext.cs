using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .HasPrincipalKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasData(new Role() { RoleId = 1, Name = "Админ", LevelAccess = uint.MaxValue });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Login).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasData(new User {UserId = 1, Login = "Admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin"), RoleId = 1});
             
        });
    }
}

