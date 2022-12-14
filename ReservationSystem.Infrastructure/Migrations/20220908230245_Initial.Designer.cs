// <auto-generated />
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
    [Migration("20220908230245_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("ReservationSystem.Domain.Entities.Host", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Hosts");

                    b.HasData(
                        new
                        {
                            Name = "s1",
                            Status = ""
                        },
                        new
                        {
                            Name = "s2",
                            Status = ""
                        },
                        new
                        {
                            Name = "s3",
                            Status = ""
                        },
                        new
                        {
                            Name = "s4",
                            Status = ""
                        },
                        new
                        {
                            Name = "s5",
                            Status = ""
                        },
                        new
                        {
                            Name = "s6",
                            Status = ""
                        },
                        new
                        {
                            Name = "s7",
                            Status = ""
                        },
                        new
                        {
                            Name = "s8",
                            Status = ""
                        },
                        new
                        {
                            Name = "s9",
                            Status = ""
                        });
                });

            modelBuilder.Entity("ReservationSystem.Domain.Entities.OS", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("OSs");

                    b.HasData(
                        new
                        {
                            Name = "archlinux console"
                        },
                        new
                        {
                            Name = "windows 10"
                        });
                });

            modelBuilder.Entity("ReservationSystem.Domain.Entities.ReservationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OS")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ReseservationLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
