﻿// <auto-generated />
using System;
using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarRental.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cars.Entities.CarEntity", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Seats")
                        .HasColumnType("integer");

                    b.HasKey("CarId");

                    b.ToTable("CarsInfo");

                    b.HasData(
                        new
                        {
                            CarId = 1,
                            Brand = "Toyota",
                            Image = "toyota_camry.jpg",
                            Model = "Camry",
                            Price = 25000f,
                            Seats = 5
                        },
                        new
                        {
                            CarId = 2,
                            Brand = "Honda",
                            Image = "honda_civic.jpg",
                            Model = "Civic",
                            Price = 22000f,
                            Seats = 5
                        },
                        new
                        {
                            CarId = 3,
                            Brand = "Ford",
                            Image = "ford_mustang.jpg",
                            Model = "Mustang",
                            Price = 35000f,
                            Seats = 4
                        });
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.Property<int>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RentalId"));

                    b.Property<int?>("CarsCarId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RentalId");

                    b.HasIndex("CarsCarId");

                    b.HasIndex("UserId");

                    b.ToTable("RentalInfo");

                    b.HasData(
                        new
                        {
                            RentalId = 1,
                            DateFrom = new DateTime(2023, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateTo = new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            RentalId = 2,
                            DateFrom = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateTo = new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Users.Entities.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("integer");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId");

                    b.ToTable("UsersInfo");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Address = "123 Main Street",
                            City = "New York",
                            Email = "john.doe@example.com",
                            Password = "p@ssw0rd",
                            PostalCode = 10001,
                            Role = 0,
                            Username = "JohnDoe"
                        },
                        new
                        {
                            UserId = 2,
                            Address = "456 Elm Avenue",
                            City = "Los Angeles",
                            Email = "jane.smith@example.com",
                            Password = "s3cur3p@ss",
                            PostalCode = 90001,
                            Role = 0,
                            Username = "JaneSmith"
                        },
                        new
                        {
                            UserId = 3,
                            Address = "789 Oak Street",
                            City = "Chicago",
                            Email = "admin@example.com",
                            Password = "adm!n123",
                            PostalCode = 60601,
                            Role = 0,
                            Username = "AdminUser"
                        });
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.HasOne("Cars.Entities.CarEntity", "Cars")
                        .WithMany()
                        .HasForeignKey("CarsCarId");

                    b.HasOne("Users.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Cars");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
