﻿// <auto-generated />
using System;
using APILabb3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APILabb3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APILabb3.Models.Link", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LinkId"));

                    b.Property<int?>("FK_InterestsId")
                        .HasColumnType("int");

                    b.Property<int?>("InterestsInterestId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LinkId");

                    b.HasIndex("InterestsInterestId");

                    b.ToTable("links");
                });

            modelBuilder.Entity("APILabb3.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("PersonId");

                    b.ToTable("persons");
                });

            modelBuilder.Entity("APILabb3.NewFolder.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestId"));

                    b.Property<int?>("FK_PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("InterestId");

                    b.HasIndex("FK_PersonId");

                    b.ToTable("interests");
                });

            modelBuilder.Entity("APILabb3.Models.Link", b =>
                {
                    b.HasOne("APILabb3.NewFolder.Interest", "Interests")
                        .WithMany("Links")
                        .HasForeignKey("InterestsInterestId");

                    b.Navigation("Interests");
                });

            modelBuilder.Entity("APILabb3.NewFolder.Interest", b =>
                {
                    b.HasOne("APILabb3.Models.Person", "Persons")
                        .WithMany("interests")
                        .HasForeignKey("FK_PersonId");

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("APILabb3.Models.Person", b =>
                {
                    b.Navigation("interests");
                });

            modelBuilder.Entity("APILabb3.NewFolder.Interest", b =>
                {
                    b.Navigation("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
