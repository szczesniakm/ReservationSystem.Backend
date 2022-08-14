﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservationSystem.Infrastructure;

#nullable disable

namespace ReservationSystem.Infrastructure.Migrations
{
    [DbContext(typeof(ReservationSystemContext))]
    [Migration("20220808190416_linkreservations")]
    partial class linkreservations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("ReservationSystem.Domain.Entities.Host", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Hosts");
                });

            modelBuilder.Entity("ReservationSystem.Domain.Entities.OS", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("OSs");
                });

            modelBuilder.Entity("ReservationSystem.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OSName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HostName");

                    b.HasIndex("OSName");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("ReservationSystem.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("ReservationSystem.Domain.Entities.Host", "Host")
                        .WithMany("Reservations")
                        .HasForeignKey("HostName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReservationSystem.Domain.Entities.OS", "OS")
                        .WithMany()
                        .HasForeignKey("OSName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");

                    b.Navigation("OS");
                });

            modelBuilder.Entity("ReservationSystem.Domain.Entities.Host", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
