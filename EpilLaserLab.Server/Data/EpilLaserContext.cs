using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data;

public class EpilLaserContext : DbContext
{
    public EpilLaserContext(DbContextOptions<EpilLaserContext> options) : base(options)
    {

    }

    public DbSet<Models.User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.User>()
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

        modelBuilder.Entity<Models.User>(entity =>
        {
            entity.HasIndex(e => e.Login).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasData(new Models.User { UserId = 1, Login = "Admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin"), RoleId = 1 });

        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(new Status() { StatusId = 1, Name = "Создана" });
        });
    }
}

