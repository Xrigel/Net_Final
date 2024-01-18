﻿// <auto-generated />
using System;
using Final_correct.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Finalcorrect.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Final_correct.Model.AuthorizedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuthorizedUsers");
                });

            modelBuilder.Entity("Final_correct.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Final_correct.Model.InsuranceProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("AuthorizedUserId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("AuthorizedUserId")
                        .IsUnique();

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.HasIndex("PackageId")
                        .IsUnique();

                    b.HasIndex("TypeId")
                        .IsUnique();

                    b.ToTable("InsuranceProducts");
                });

            modelBuilder.Entity("Final_correct.Model.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Final_correct.Model.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<decimal?>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("PriceId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PackageId");

                    b.HasIndex("TypeId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Final_correct.Model.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Final_correct.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InsuranceProductUser", b =>
                {
                    b.Property<int>("InsuranceProductsProductId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("InsuranceProductsProductId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserInsuranceProducts", (string)null);
                });

            modelBuilder.Entity("Final_correct.Model.InsuranceProduct", b =>
                {
                    b.HasOne("Final_correct.Model.AuthorizedUser", "AuthorizedUser")
                        .WithOne("InsuranceProduct")
                        .HasForeignKey("Final_correct.Model.InsuranceProduct", "AuthorizedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.Category", "Category")
                        .WithOne("InsuranceProduct")
                        .HasForeignKey("Final_correct.Model.InsuranceProduct", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.Package", "Package")
                        .WithOne("InsuranceProduct")
                        .HasForeignKey("Final_correct.Model.InsuranceProduct", "PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.Type", "Type")
                        .WithOne("InsuranceProduct")
                        .HasForeignKey("Final_correct.Model.InsuranceProduct", "TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorizedUser");

                    b.Navigation("Category");

                    b.Navigation("Package");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Final_correct.Model.Price", b =>
                {
                    b.HasOne("Final_correct.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.InsuranceProduct", "InsuranceProduct")
                        .WithOne("ProductPrice")
                        .HasForeignKey("Final_correct.Model.Price", "PriceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("InsuranceProduct");

                    b.Navigation("Package");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("InsuranceProductUser", b =>
                {
                    b.HasOne("Final_correct.Model.InsuranceProduct", null)
                        .WithMany()
                        .HasForeignKey("InsuranceProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Final_correct.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Final_correct.Model.AuthorizedUser", b =>
                {
                    b.Navigation("InsuranceProduct")
                        .IsRequired();
                });

            modelBuilder.Entity("Final_correct.Model.Category", b =>
                {
                    b.Navigation("InsuranceProduct")
                        .IsRequired();
                });

            modelBuilder.Entity("Final_correct.Model.InsuranceProduct", b =>
                {
                    b.Navigation("ProductPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Final_correct.Model.Package", b =>
                {
                    b.Navigation("InsuranceProduct")
                        .IsRequired();
                });

            modelBuilder.Entity("Final_correct.Model.Type", b =>
                {
                    b.Navigation("InsuranceProduct")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
