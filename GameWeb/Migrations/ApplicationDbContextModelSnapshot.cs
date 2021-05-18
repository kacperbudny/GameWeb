﻿// <auto-generated />
using System;
using GameWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameWeb.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MinimalRequirementsId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecommendedRequirementsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MinimalRequirementsId");

                    b.HasIndex("RecommendedRequirementsId");

                    b.ToTable("Game");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer ut mattis dolor. Curabitur accumsan sapien quam, vel volutpat lacus euismod eget. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eget ornare quam, nec lobortis nunc. Ut dui ex, faucibus ut finibus dictum, pretium in elit. Aliquam ac molestie sem. Morbi dictum nisl in justo venenatis, id ultricies elit porta.",
                            Developer = "Mojang",
                            Genre = "Sandbox",
                            Image = "1.jpg",
                            MinimalRequirementsId = 1,
                            Name = "Minecraft",
                            Platform = "PC, Xbox, PlayStation, Android",
                            Publisher = "Mojang",
                            RecommendedRequirementsId = 2,
                            ReleaseDate = new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Description = "Aliquam molestie mollis sagittis. Curabitur a nibh eu tortor dapibus sodales. Proin a aliquet turpis. Proin augue massa, posuere ac interdum at, aliquam sed sem. Donec felis turpis, viverra eget elit ut, facilisis auctor risus. Donec aliquam augue urna, et accumsan nisi lobortis in. Ut sagittis nunc feugiat nibh venenatis tempor. Sed rutrum ullamcorper sapien et eleifend. \n\nInteger et odio tincidunt, luctus felis non, eleifend libero. Etiam a vulputate enim. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In ac ante iaculis, scelerisque orci varius, ornare ligula. Donec nec faucibus eros. Phasellus lacinia elit arcu, condimentum sodales libero pretium sed. In ut commodo orci. Sed sagittis viverra ante, id dignissim metus mollis ut.",
                            Developer = "Ubisoft",
                            Genre = "Platformówki",
                            Image = "2.jpg",
                            MinimalRequirementsId = 3,
                            Name = "Rayman 3: Hoodlum Havoc",
                            Platform = "PC, Xbox, PlayStation",
                            Publisher = "Ubisoft",
                            RecommendedRequirementsId = 4,
                            ReleaseDate = new DateTime(2003, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Description = "Donec vehicula ornare elit, nec tempor ante ornare a. In hac habitasse platea dictumst. Etiam rhoncus ornare vestibulum. Quisque id odio pellentesque, maximus massa in, aliquam quam. Donec lacus sem, lobortis a dolor vel, condimentum pellentesque orci. Aenean nunc ipsum, cursus a blandit at, pellentesque id purus. Quisque tincidunt urna vel sem maximus, a efficitur urna suscipit.",
                            Developer = "Bugbear Entertainment",
                            Genre = "Wyścigi",
                            Image = "3.jpg",
                            MinimalRequirementsId = 5,
                            Name = "FlatOut 2",
                            Platform = "PC, Xbox, PlayStation",
                            Publisher = "Empire Interactive",
                            RecommendedRequirementsId = 6,
                            ReleaseDate = new DateTime(2006, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("GameWeb.Models.GameComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ThreadId")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.ToTable("GameComment");
                });

            modelBuilder.Entity("GameWeb.Models.GameCommentThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("GameCommentThread");
                });

            modelBuilder.Entity("GameWeb.Models.Requirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DriveSize")
                        .HasColumnType("int");

                    b.Property<string>("GPU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("OS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RAM")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Requirement");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CPU = "Intel Core i3 3210 / AMD A8 7600 APU",
                            DriveSize = 180,
                            GPU = "NVIDIA GeForce 400 Series / AMD Radeon HD 7000 series",
                            OS = "64-bit Windows 7",
                            RAM = 4.0
                        },
                        new
                        {
                            Id = 2,
                            CPU = "Intel Core i5 4690 / AMD A10 7800",
                            DriveSize = 4000,
                            GPU = "NVIDIA GeForce 700 Series / AMD Radeon Rx 200 Series",
                            OS = "64-bit Windows 10",
                            RAM = 8.0
                        },
                        new
                        {
                            Id = 3,
                            CPU = "Pentium III 600 MHz / AMD Athlon 600 MHz",
                            DriveSize = 800,
                            GPU = "Kompatybilna z DirectX 8.1",
                            OS = "Windows XP",
                            RAM = 0.25600000000000001
                        },
                        new
                        {
                            Id = 4,
                            CPU = "Pentium III 1 GHz / AMD Athlon 1 Ghz",
                            DriveSize = 800,
                            GPU = "Kompatybilna z DirectX 9",
                            OS = "Windows XP",
                            RAM = 0.51200000000000001
                        },
                        new
                        {
                            Id = 5,
                            CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2000+",
                            DriveSize = 3500,
                            GPU = "AMD Radeon HD 4350 / NVIDIA GeForce 6600 GT",
                            OS = "32-bit Windows XP",
                            RAM = 0.25600000000000001
                        },
                        new
                        {
                            Id = 6,
                            CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2500+",
                            DriveSize = 4000,
                            GPU = "AMD Radeon 9600 Series / NVIDIA GeForce 6600",
                            OS = "32-bit Windows XP",
                            RAM = 0.51200000000000001
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GameWeb.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("GameWeb.Models.Game", b =>
                {
                    b.HasOne("GameWeb.Models.Requirement", "MinimalRequirements")
                        .WithMany()
                        .HasForeignKey("MinimalRequirementsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GameWeb.Models.Requirement", "RecommendedRequirements")
                        .WithMany()
                        .HasForeignKey("RecommendedRequirementsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MinimalRequirements");

                    b.Navigation("RecommendedRequirements");
                });

            modelBuilder.Entity("GameWeb.Models.GameComment", b =>
                {
                    b.HasOne("GameWeb.Models.GameCommentThread", "Thread")
                        .WithMany("Comments")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("GameWeb.Models.GameCommentThread", b =>
                {
                    b.HasOne("GameWeb.Models.Game", "Game")
                        .WithMany("CommentThreads")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GameWeb.Models.Requirement", b =>
                {
                    b.HasOne("GameWeb.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameWeb.Models.Game", b =>
                {
                    b.Navigation("CommentThreads");
                });

            modelBuilder.Entity("GameWeb.Models.GameCommentThread", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
