 using EpilLaserLab.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EpilLaserLab.Server.Data;

public class EpilLaserLabContext : DbContext
{
    public EpilLaserLabContext(DbContextOptions<EpilLaserLabContext> options) : base(options)
    {

    }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Models.Type> Types { get; set; }
    public DbSet<Models.User> Users { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServicePrice> ServicePrices { get; set; }
    public DbSet<Models.SeasonTicket> SeasonTickets { get; set; }
    public DbSet<SeasonTicketPrice> SeasonTicketsPrice { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Interval> Intervals { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<PurchasedSeasonTicket> PurchasedSeasonTickets { get; set; }

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
            entity.HasData(new Role() { RoleId = 1, Name = "root", Title = "Главный"});
            entity.HasData(new Role() { RoleId = 2, Name = "admin", Title = "Администратор"});
            entity.HasData(new Role() { RoleId = 3, Name = "client", Title = "Клиент"});
        });

        modelBuilder.Entity<Models.User>(entity =>
        {
            entity.HasIndex(e => e.Login).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasData(new User { UserId = 1, Login = "Admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin"), RoleId = 1 });

        });

        //modelBuilder.Entity<Status>(entity =>
        //{
        //    entity.HasIndex(e => e.Name).IsUnique();
        //    entity.HasData(new Status() { StatusId = 1, Name = "Создана" });
        //});

        //modelBuilder.Entity<Tag>(entity =>
        //{
        //    entity.HasIndex(e => e.Name).IsUnique();
        //    entity.HasData(new Tag() { TagId = 1, Name = "Новый" });
        //});

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(new Category() { CategoryId=1, Name= "Создано пользователем" });
        });

        modelBuilder.Entity<Models.Type>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(new Models.Type() { TypeId = 1, Name = "Знакомство" });
            entity.HasData(new Models.Type() { TypeId = 4, Name = "Стандартный" });
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

            entity.HasMany(e => e.Applications)
                .WithOne(a => a.ServicePrice);
        });

        modelBuilder.Entity<Models.SeasonTicket>(entity =>
        {
            entity.HasOne(e => e.Service)
                .WithMany(e => e.SeasonTickets)
                .HasPrincipalKey(e => e.ServiceId)
                .HasForeignKey(e => e.ServiceId);
        });

        modelBuilder.Entity<SeasonTicketPrice>(entity =>
        {
            entity.HasOne(e => e.SeasonTicket)
                .WithMany(e => e.SeasonTicketPrices)
                .HasPrincipalKey(e => e.SeasonTicketId)
                .HasForeignKey(e => e.SeasonTicketId);
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasOne(e => e.Employee)
                .WithOne(e => e.Master);

            entity.HasOne(e => e.Branch)
                .WithMany(e => e.Masters);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasOne(e => e.Master)
                .WithMany(e => e.Schedules);
        });

        modelBuilder.Entity<Interval>(entity =>
        {
            entity.HasOne(i => i.Schedule)
                .WithMany(s => s.Intervals);
       });

        modelBuilder.Entity<Client>(entity => 
        {
            entity.HasOne(e => e.User)
                .WithOne(e => e.Client);


            entity.HasIndex(e => e.Phone).IsUnique();
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasOne(e => e.Branch)
                .WithMany(e => e.Admins);

            entity.HasOne(e => e.Employee)
                .WithOne(e => e.Admin);

            entity.HasOne(e => e.User)
                .WithOne(e => e.Admin);
        });

        modelBuilder.Entity<PurchasedSeasonTicket>(entity =>
        {
            entity.HasOne(e => e.SeasonTicketPrice)
                .WithMany(e => e.PurchasedSeasonTickets);

            entity.HasOne(e => e.Client)
                .WithMany(e => e.PurchasedSeasonTickets);
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasOne(e => e.Interval)
                .WithOne(e => e.Application);
            
            entity.HasOne(e => e.Client)
                .WithMany(e => e.Applications);

            entity.HasOne(e => e.PurchasedSeasonTicket)
                .WithMany(e => e.Applications);
        });
    }
}

