﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WervenProj.Data;

#nullable disable

namespace WervenProj.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240531110151_AddAndSeedRoles")]
    partial class AddAndSeedRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WervenProj.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Roy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Nina"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Jeff"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Lenne"
                        });
                });

            modelBuilder.Entity("WervenProj.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RoleNr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Metselaar",
                            RoleNr = 0
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Schrijnwerker",
                            RoleNr = 1
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "Administratie",
                            RoleNr = 2
                        },
                        new
                        {
                            Id = 4,
                            RoleName = "Manager",
                            RoleNr = 3
                        });
                });

            modelBuilder.Entity("WervenProj.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StatusNr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StatusName = "Aangemakt",
                            StatusNr = 0
                        },
                        new
                        {
                            Id = 2,
                            StatusName = "Goedgekeurd",
                            StatusNr = 1
                        },
                        new
                        {
                            Id = 3,
                            StatusName = "In Werking",
                            StatusNr = 2
                        },
                        new
                        {
                            Id = 4,
                            StatusName = "Afgerond",
                            StatusNr = 3
                        });
                });

            modelBuilder.Entity("WervenProj.Models.Werf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Werven");
                });

            modelBuilder.Entity("WervenProj.Models.Werf", b =>
                {
                    b.HasOne("WervenProj.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
