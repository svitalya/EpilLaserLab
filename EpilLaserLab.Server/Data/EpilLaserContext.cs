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
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Models.Type> Types { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServicePrice> ServicePrices { get; set; }

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
            entity.HasData(new Role() { RoleId = 1, Name = "admin"});
            entity.HasData(new Role() { RoleId = 2, Name = "user"});
        });

        modelBuilder.Entity<Models.User>(entity =>
        {
            entity.HasIndex(e => e.Login).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasData(new User { UserId = 1, Login = "Admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin"), RoleId = 1 });
            entity.HasData(new User { UserId = 2, Login = "User", PasswordHash = BCrypt.Net.BCrypt.HashPassword("User"), RoleId = 2 });

        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(new Status() { StatusId = 1, Name = "Создана" });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(new Tag() { TagId = 1, Name = "Новый" });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Models.Type>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasOne(e => e.Service)
                .WithMany(e => e.ServicePrices);

            entity.HasOne(e => e.Type)
                .WithMany(e => e.ServicePrices);

        });
    }
}

