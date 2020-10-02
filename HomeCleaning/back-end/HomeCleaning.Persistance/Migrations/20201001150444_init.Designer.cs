﻿// <auto-generated />
using System;
using HomeCleaning.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeCleaning.Persistance.Migrations
{
    [DbContext(typeof(HomeCleaningContext))]
    [Migration("20201001150444_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeCleaning.Domain.CleaningCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CleaningCategory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Home Cleaning"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Empty Home Cleaning"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cleaning after construction"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Villa Cleaning"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Office Cleaning"
                        });
                });

            modelBuilder.Entity("HomeCleaning.Domain.CleaningExtraOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CleaningCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CleaningCategoryId");

                    b.ToTable("CleaningExtraOption");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CleaningCategoryId = 1,
                            Name = "ironing"
                        },
                        new
                        {
                            Id = 2,
                            CleaningCategoryId = 1,
                            Name = "refrigerator cleaning"
                        },
                        new
                        {
                            Id = 3,
                            CleaningCategoryId = 1,
                            Name = "wardrobe cleaning"
                        },
                        new
                        {
                            Id = 4,
                            CleaningCategoryId = 5,
                            Name = "interior glass cleaning"
                        },
                        new
                        {
                            Id = 5,
                            CleaningCategoryId = 4,
                            Name = "ironing"
                        },
                        new
                        {
                            Id = 6,
                            CleaningCategoryId = 4,
                            Name = "refrigerator cleaning"
                        },
                        new
                        {
                            Id = 7,
                            CleaningCategoryId = 4,
                            Name = "wardrobe cleaning"
                        });
                });

            modelBuilder.Entity("HomeCleaning.Domain.CleaningPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CleaningCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CleaningCategoryId");

                    b.ToTable("CleaningPackage");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CleaningCategoryId = 1,
                            Name = "First Cleaning Package"
                        },
                        new
                        {
                            Id = 2,
                            CleaningCategoryId = 1,
                            Name = "Second Cleaning Package"
                        },
                        new
                        {
                            Id = 3,
                            CleaningCategoryId = 1,
                            Name = "Third Cleaning Package"
                        },
                        new
                        {
                            Id = 4,
                            CleaningCategoryId = 4,
                            Name = "First Cleaning Package"
                        },
                        new
                        {
                            Id = 5,
                            CleaningCategoryId = 4,
                            Name = "Second Cleaning Package"
                        },
                        new
                        {
                            Id = 6,
                            CleaningCategoryId = 4,
                            Name = "Third Cleaning Package"
                        });
                });

            modelBuilder.Entity("HomeCleaning.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CleaningPackageId")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ScheduledTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SpaceSizeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("SpaceSizeId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("HomeCleaning.Domain.SpaceSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CleaningCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CleaningCategoryId");

                    b.ToTable("SpaceSize");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CleaningCategoryId = 1,
                            Name = "1 + 1"
                        },
                        new
                        {
                            Id = 2,
                            CleaningCategoryId = 1,
                            Name = "2 + 1"
                        },
                        new
                        {
                            Id = 3,
                            CleaningCategoryId = 1,
                            Name = "3 + 1"
                        },
                        new
                        {
                            Id = 4,
                            CleaningCategoryId = 1,
                            Name = "4 + 1"
                        },
                        new
                        {
                            Id = 5,
                            CleaningCategoryId = 2,
                            Name = "1 + 1"
                        },
                        new
                        {
                            Id = 6,
                            CleaningCategoryId = 2,
                            Name = "2 + 1"
                        },
                        new
                        {
                            Id = 7,
                            CleaningCategoryId = 2,
                            Name = "3 + 1"
                        },
                        new
                        {
                            Id = 8,
                            CleaningCategoryId = 2,
                            Name = "4 + 1"
                        },
                        new
                        {
                            Id = 9,
                            CleaningCategoryId = 3,
                            Name = "1 + 1"
                        },
                        new
                        {
                            Id = 10,
                            CleaningCategoryId = 3,
                            Name = "2 + 1"
                        },
                        new
                        {
                            Id = 11,
                            CleaningCategoryId = 3,
                            Name = "3 + 1"
                        },
                        new
                        {
                            Id = 12,
                            CleaningCategoryId = 3,
                            Name = "4 + 1"
                        },
                        new
                        {
                            Id = 13,
                            CleaningCategoryId = 5,
                            Name = "100 m2 and below"
                        },
                        new
                        {
                            Id = 14,
                            CleaningCategoryId = 5,
                            Name = "100-150 m2"
                        },
                        new
                        {
                            Id = 15,
                            CleaningCategoryId = 5,
                            Name = "150-200 m2"
                        },
                        new
                        {
                            Id = 16,
                            CleaningCategoryId = 5,
                            Name = "200-250 m2"
                        },
                        new
                        {
                            Id = 17,
                            CleaningCategoryId = 5,
                            Name = "250 m2 above"
                        },
                        new
                        {
                            Id = 18,
                            CleaningCategoryId = 4,
                            Name = "200-275 m2"
                        },
                        new
                        {
                            Id = 19,
                            CleaningCategoryId = 4,
                            Name = "275-350 m2"
                        },
                        new
                        {
                            Id = 20,
                            CleaningCategoryId = 4,
                            Name = "350-425 m2"
                        },
                        new
                        {
                            Id = 21,
                            CleaningCategoryId = 4,
                            Name = "425 m2 above"
                        });
                });

            modelBuilder.Entity("HomeCleaning.Domain.CleaningExtraOption", b =>
                {
                    b.HasOne("HomeCleaning.Domain.CleaningCategory", "CleaningCategory")
                        .WithMany()
                        .HasForeignKey("CleaningCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeCleaning.Domain.CleaningPackage", b =>
                {
                    b.HasOne("HomeCleaning.Domain.CleaningCategory", "CleaningCategory")
                        .WithMany()
                        .HasForeignKey("CleaningCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeCleaning.Domain.Order", b =>
                {
                    b.HasOne("HomeCleaning.Domain.SpaceSize", "SpaceSize")
                        .WithMany()
                        .HasForeignKey("SpaceSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("HomeCleaning.Domain.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Alley")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Block")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Floor")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("HomeCleaning.Domain.Customer", "Customer", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });
                });

            modelBuilder.Entity("HomeCleaning.Domain.SpaceSize", b =>
                {
                    b.HasOne("HomeCleaning.Domain.CleaningCategory", "CleaningCategory")
                        .WithMany()
                        .HasForeignKey("CleaningCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
