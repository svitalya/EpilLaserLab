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
    [DbContext(typeof(EpilLaserContext))]
    [Migration("20240415121806_CreateSeasonTickets")]
    partial class CreateSeasonTickets
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
                            Name = "admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "user"
                        });
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
                            PasswordHash = "$2a$11$CWU.I/jndl7YWQNQcZRFKOlvkUogqJFLRHKSs17LbFcoWQ7ZigX6S",
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            Login = "User",
                            PasswordHash = "$2a$11$MpAKwe5kJ07AjVQKnpOkZORnGlGz4cu3OAfkLPMPVyJ2iPxUpEPV.",
                            RoleId = 2
                        });
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

            modelBuilder.Entity("EpilLaserLab.Server.Models.Role", b =>
                {
                    b.Navigation("Users");
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
#pragma warning restore 612, 618
        }
    }
}
