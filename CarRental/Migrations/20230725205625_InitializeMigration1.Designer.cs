﻿// <auto-generated />
using System;
using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarRental.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    [Migration("20230725205625_InitializeMigration1")]
    partial class InitializeMigration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cars.Entities.CarEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

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

                    b.Property<string>("RentalId")
                        .HasColumnType("text");

                    b.Property<int>("Seats")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("RentalId");

                    b.ToTable("CarsInfo");
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("UserEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("RentalInfo");
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
                        .HasMaxLength(7)
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId");

                    b.ToTable("UsersInfo");
                });

            modelBuilder.Entity("Users.Model.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PostalCode")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Cars.Entities.CarEntity", b =>
                {
                    b.HasOne("RentInfo.Entities.RentalEntity", "Rental")
                        .WithMany("Car")
                        .HasForeignKey("RentalId");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.HasOne("Users.Entities.UserEntity", null)
                        .WithMany("Rental")
                        .HasForeignKey("UserEntityId");

                    b.HasOne("Users.Model.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.Navigation("Car");
                });

            modelBuilder.Entity("Users.Entities.UserEntity", b =>
                {
                    b.Navigation("Rental");
                });
#pragma warning restore 612, 618
        }
    }
}
