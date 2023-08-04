﻿// <auto-generated />
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
    [Migration("20230804102739_a0000")]
    partial class a0000
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cars.Entities.CarEntity", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarId"));

                    b.Property<int>("AdminUserId")
                        .HasColumnType("integer");

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

                    b.HasIndex("AdminUserId");

                    b.ToTable("CarsInfo");
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.Property<int>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RentalId"));

                    b.Property<int>("CarId")
                        .HasColumnType("integer");

                    b.Property<int>("ClientUserId")
                        .HasColumnType("integer");

                    b.Property<long>("DateFrom")
                        .HasColumnType("bigint");

                    b.Property<long>("DateTo")
                        .HasColumnType("bigint");

                    b.HasKey("RentalId");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientUserId");

                    b.ToTable("RentalInfo");
                });

            modelBuilder.Entity("Users.Entities.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PostalCode")
                        .HasColumnType("integer");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId");

                    b.ToTable("UserInfo");

                    b.HasDiscriminator<int>("Role").HasValue(3);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Postgres.Context.Entities.AdminEntity", b =>
                {
                    b.HasBaseType("Users.Entities.UserEntity");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Postgres.Context.Entities.ClientEntity", b =>
                {
                    b.HasBaseType("Users.Entities.UserEntity");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Cars.Entities.CarEntity", b =>
                {
                    b.HasOne("Postgres.Context.Entities.AdminEntity", "Admin")
                        .WithMany("Cars")
                        .HasForeignKey("AdminUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("RentInfo.Entities.RentalEntity", b =>
                {
                    b.HasOne("Cars.Entities.CarEntity", "Car")
                        .WithMany("Rents")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Postgres.Context.Entities.ClientEntity", "Client")
                        .WithMany("Rents")
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Cars.Entities.CarEntity", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("Postgres.Context.Entities.AdminEntity", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Postgres.Context.Entities.ClientEntity", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
