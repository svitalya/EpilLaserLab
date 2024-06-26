﻿// <auto-generated />
using EpilLaserLab.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    [DbContext(typeof(EpilLaserLabContext))]
    [Migration("20240414065234_UpdateRoles")]
    partial class UpdateRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EpilLaserLab.Server.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<uint>("LevelAccess")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            LevelAccess = 0u,
                            Name = "admin"
                        },
                        new
                        {
                            RoleId = 2,
                            LevelAccess = 0u,
                            Name = "user"
                        });
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
                            PasswordHash = "$2a$11$.yq54P4LuavFwjwA6l4HQ.lgU8lMLhEEjOlIXSeoA/IharO9Zq4yG",
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            Login = "User",
                            PasswordHash = "$2a$11$J7bSLeGi9STb0GVLXqeL8eoW/J/DB3HQg0BFGEtdhRw5dNth5d19S",
                            RoleId = 2
                        });
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
#pragma warning restore 612, 618
        }
    }
}
