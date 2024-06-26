﻿// <auto-generated />
using System;
using EpilLaserLab.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    [DbContext(typeof(EpilLaserLabContext))]
    [Migration("20240417145226_UpdateRoles3")]
    partial class UpdateRoles3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EpilLaserLab.Server.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("BranchId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ClientId");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsWork")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Interval", b =>
                {
                    b.Property<int>("IntervalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("TimeEnd")
                        .HasColumnType("int");

                    b.Property<int>("TimeStart")
                        .HasColumnType("int");

                    b.HasKey("IntervalId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Intervals");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Master", b =>
                {
                    b.Property<int>("MasterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MasterId");

                    b.HasIndex("BranchId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "root"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "admin"
                        },
                        new
                        {
                            RoleId = 3,
                            Name = "client"
                        });
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MasterId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleId");

                    b.HasIndex("MasterId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.SeasonTicket", b =>
                {
                    b.Property<int>("SeasonTicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("Sessions")
                        .HasColumnType("int");

                    b.Property<int>("ValidityPeriod")
                        .HasColumnType("int");

                    b.HasKey("SeasonTicketId");

                    b.HasIndex("ServiceId");

                    b.ToTable("SeasonTickets");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.SeasonTicketPrice", b =>
                {
                    b.Property<int>("SeasonTicketPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SeasonTicketId")
                        .HasColumnType("int");

                    b.HasKey("SeasonTicketPriceId");

                    b.HasIndex("SeasonTicketId");

                    b.ToTable("SeasonTicketsPrice");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<uint>("TimeCost")
                        .HasColumnType("int unsigned");

                    b.HasKey("ServiceId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Services");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.ServicePrice", b =>
                {
                    b.Property<int>("ServicePriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("ServicePriceId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TypeId");

                    b.ToTable("ServicePrices");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("StatusId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            Name = "Создана"
                        });
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("TagId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Name = "Новый"
                        });
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Type", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("TypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Types");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Login = "Admin",
                            PasswordHash = "$2a$11$U4M8uuzgXun3lddN/kYD..HWw69IOlC5CxaikyGntVU.MbwYhkfVe",
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            Login = "User",
                            PasswordHash = "$2a$11$RSkL9E6q2DLZqM91aOXhGuPrfxp4TvCdoxmzCDIdDbpal4I1EG7vm",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Client", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("EpilLaserLab.Server.Models.Client", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Interval", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.Schedule", "Schedule")
                        .WithMany("Intervals")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Master", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.Branch", "Branch")
                        .WithMany("Masters")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EpilLaserLab.Server.Models.Employee", "Employee")
                        .WithOne("Master")
                        .HasForeignKey("EpilLaserLab.Server.Models.Master", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Schedule", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.Master", "Master")
                        .WithMany("Schedules")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.SeasonTicket", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.Service", "Service")
                        .WithMany("SeasonTickets")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.SeasonTicketPrice", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.SeasonTicket", "SeasonTicket")
                        .WithMany("SeasonTicketPrices")
                        .HasForeignKey("SeasonTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeasonTicket");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.ServicePrice", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.Service", "Service")
                        .WithMany("ServicePrices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EpilLaserLab.Server.Models.Type", "Type")
                        .WithMany("ServicePrices")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.User", b =>
                {
                    b.HasOne("EpilLaserLab.Server.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Branch", b =>
                {
                    b.Navigation("Masters");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Employee", b =>
                {
                    b.Navigation("Master");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Master", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Schedule", b =>
                {
                    b.Navigation("Intervals");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.SeasonTicket", b =>
                {
                    b.Navigation("SeasonTicketPrices");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Service", b =>
                {
                    b.Navigation("SeasonTickets");

                    b.Navigation("ServicePrices");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.Type", b =>
                {
                    b.Navigation("ServicePrices");
                });

            modelBuilder.Entity("EpilLaserLab.Server.Models.User", b =>
                {
                    b.Navigation("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
