﻿// <auto-generated />
using System;
using MobilnoQatarBack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MobilnoQatarBack.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240801180231_goloviNullable")]
    partial class goloviNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Model.Grupa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImeGrupe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Grupe");
                });

            modelBuilder.Entity("Domain.Model.Stadion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImeStadiona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stadioni");
                });

            modelBuilder.Entity("Domain.Model.Tim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrojDatihGolova")
                        .HasColumnType("int");

                    b.Property<int>("BrojNeresenih")
                        .HasColumnType("int");

                    b.Property<int>("BrojPobeda")
                        .HasColumnType("int");

                    b.Property<int>("BrojPoena")
                        .HasColumnType("int");

                    b.Property<int>("BrojPoraza")
                        .HasColumnType("int");

                    b.Property<int>("BrojPrimljenihGolova")
                        .HasColumnType("int");

                    b.Property<int?>("GrupaId")
                        .HasColumnType("int");

                    b.Property<string>("ImeTima")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zastavica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GrupaId");

                    b.ToTable("Timovi");
                });

            modelBuilder.Entity("Domain.Model.Utakmica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Predato")
                        .HasColumnType("bit");

                    b.Property<int>("StadionId")
                        .HasColumnType("int");

                    b.Property<int?>("Tim1Golovi")
                        .HasColumnType("int");

                    b.Property<int>("Tim1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Tim2Golovi")
                        .HasColumnType("int");

                    b.Property<int>("Tim2Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("VremePocetka")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StadionId");

                    b.HasIndex("Tim1Id");

                    b.HasIndex("Tim2Id");

                    b.ToTable("Utakmice");
                });

            modelBuilder.Entity("Domain.Model.Tim", b =>
                {
                    b.HasOne("Domain.Model.Grupa", "Grupa")
                        .WithMany("Timovi")
                        .HasForeignKey("GrupaId");

                    b.Navigation("Grupa");
                });

            modelBuilder.Entity("Domain.Model.Utakmica", b =>
                {
                    b.HasOne("Domain.Model.Stadion", "Stadion")
                        .WithMany("Utakmice")
                        .HasForeignKey("StadionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Model.Tim", "Tim1")
                        .WithMany()
                        .HasForeignKey("Tim1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Model.Tim", "Tim2")
                        .WithMany()
                        .HasForeignKey("Tim2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Stadion");

                    b.Navigation("Tim1");

                    b.Navigation("Tim2");
                });

            modelBuilder.Entity("Domain.Model.Grupa", b =>
                {
                    b.Navigation("Timovi");
                });

            modelBuilder.Entity("Domain.Model.Stadion", b =>
                {
                    b.Navigation("Utakmice");
                });
#pragma warning restore 612, 618
        }
    }
}
