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
    [Migration("20240602122111_updateEnrollmentsDTOAddNames")]
    partial class updateEnrollmentsDTOAddNames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WervenProj.Models.ConstractionSite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("EndDate")
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

                    b.ToTable("ConstractionSites");
                });

            modelBuilder.Entity("WervenProj.Models.ConstractionSiteEnrollments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConstractionSiteId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConstractionSiteId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ConstractionSiteEnrollments");
                });

            modelBuilder.Entity("WervenProj.Models.ConstractionStatus", b =>
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

                    b.ToTable("ConstractionStatuses");

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

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WervenProj.Models.EmployeeRole", b =>
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

                    b.ToTable("EmployeeRoles");

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

            modelBuilder.Entity("WervenProj.Models.ConstractionSite", b =>
                {
                    b.HasOne("WervenProj.Models.ConstractionStatus", "Status")
                        .WithMany("ConstractionSites")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WervenProj.Models.ConstractionSiteEnrollments", b =>
                {
                    b.HasOne("WervenProj.Models.ConstractionSite", "ConstractionSite")
                        .WithMany("Enrollments")
                        .HasForeignKey("ConstractionSiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WervenProj.Models.Employee", "Employee")
                        .WithMany("Enrollments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConstractionSite");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("WervenProj.Models.Employee", b =>
                {
                    b.HasOne("WervenProj.Models.EmployeeRole", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WervenProj.Models.ConstractionSite", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("WervenProj.Models.ConstractionStatus", b =>
                {
                    b.Navigation("ConstractionSites");
                });

            modelBuilder.Entity("WervenProj.Models.Employee", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("WervenProj.Models.EmployeeRole", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
