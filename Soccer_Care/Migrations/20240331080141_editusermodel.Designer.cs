﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Soccer_Care.Models;

#nullable disable

namespace Soccer_Care.Migrations
{
    [DbContext(typeof(SoccerCareDbContext))]
    [Migration("20240331080141_editusermodel")]
    partial class editusermodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Soccer_Care.Models.BillModel", b =>
                {
                    b.Property<string>("IDBill")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IDOrderDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("TotalPay")
                        .HasColumnType("real");

                    b.Property<float>("TotalTimeUse")
                        .HasColumnType("real");

                    b.HasKey("IDBill");

                    b.HasIndex("IDOrderDetails");

                    b.ToTable("BillSet");
                });

            modelBuilder.Entity("Soccer_Care.Models.DetailsOrderModel", b =>
                {
                    b.Property<string>("IDDetails")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IDOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDDetails");

                    b.HasIndex("IDOrder");

                    b.ToTable("DetailsOrder");
                });

            modelBuilder.Entity("Soccer_Care.Models.FieldLikeModel", b =>
                {
                    b.Property<string>("IDFieldLike")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IDFootballField")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IDFieldLike");

                    b.HasIndex("IDFootballField");

                    b.HasIndex("Username");

                    b.ToTable("FieldLike");
                });

            modelBuilder.Entity("Soccer_Care.Models.FootBallFieldModel", b =>
                {
                    b.Property<string>("IDFootBallField")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Gia")
                        .HasColumnType("real");

                    b.Property<string>("HinhAnhDaiDien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IDFootBallField");

                    b.HasIndex("Username");

                    b.ToTable("FootBallFields");
                });

            modelBuilder.Entity("Soccer_Care.Models.HistoryOrderModel", b =>
                {
                    b.Property<string>("IDHistory")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IDFootballField")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IDHistory");

                    b.HasIndex("IDFootballField");

                    b.HasIndex("Username");

                    b.ToTable("HistoryOrders");
                });

            modelBuilder.Entity("Soccer_Care.Models.ListFieldModel", b =>
                {
                    b.Property<string>("IDField")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DescriptionField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Gia")
                        .HasColumnType("real");

                    b.Property<string>("HinhAnhSanBong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDFootballField")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IDType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDField");

                    b.HasIndex("IDFootballField");

                    b.HasIndex("IDType");

                    b.ToTable("listFields");
                });

            modelBuilder.Entity("Soccer_Care.Models.OrderFieldModel", b =>
                {
                    b.Property<string>("IDOrder")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IDFootballField")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IDOrder");

                    b.HasIndex("IDFootballField");

                    b.HasIndex("Username");

                    b.ToTable("OrderField");
                });

            modelBuilder.Entity("Soccer_Care.Models.RatingModel", b =>
                {
                    b.Property<string>("IDDanhGia")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Diem")
                        .HasColumnType("int");

                    b.Property<string>("IDField")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IDDanhGia");

                    b.HasIndex("IDField");

                    b.HasIndex("Username");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Soccer_Care.Models.RoleModel", b =>
                {
                    b.Property<string>("IDRole")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDRole");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Soccer_Care.Models.TypeFieldModel", b =>
                {
                    b.Property<string>("IDType")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDType");

                    b.ToTable("TypeFields");
                });

            modelBuilder.Entity("Soccer_Care.Models.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsBlock")
                        .HasColumnType("int");

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
                        .IsRequired()
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

                    b.ToTable("AspNetUsers", (string)null);
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
                    b.HasOne("Soccer_Care.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Soccer_Care.Models.UserModel", null)
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

                    b.HasOne("Soccer_Care.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Soccer_Care.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Soccer_Care.Models.BillModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.DetailsOrderModel", "DetailsOrder")
                        .WithMany()
                        .HasForeignKey("IDOrderDetails")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetailsOrder");
                });

            modelBuilder.Entity("Soccer_Care.Models.DetailsOrderModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.OrderFieldModel", "Order")
                        .WithMany()
                        .HasForeignKey("IDOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Soccer_Care.Models.FieldLikeModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.FootBallFieldModel", "Football")
                        .WithMany()
                        .HasForeignKey("IDFootballField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soccer_Care.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Football");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soccer_Care.Models.FootBallFieldModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soccer_Care.Models.HistoryOrderModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.FootBallFieldModel", "FootBall")
                        .WithMany()
                        .HasForeignKey("IDFootballField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soccer_Care.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootBall");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soccer_Care.Models.ListFieldModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.FootBallFieldModel", "FootBall")
                        .WithMany("ListField")
                        .HasForeignKey("IDFootballField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soccer_Care.Models.TypeFieldModel", "Type")
                        .WithMany()
                        .HasForeignKey("IDType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootBall");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Soccer_Care.Models.OrderFieldModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.FootBallFieldModel", "FootBall")
                        .WithMany()
                        .HasForeignKey("IDFootballField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soccer_Care.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootBall");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soccer_Care.Models.RatingModel", b =>
                {
                    b.HasOne("Soccer_Care.Models.FootBallFieldModel", "FootBall")
                        .WithMany()
                        .HasForeignKey("IDField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Soccer_Care.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootBall");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Soccer_Care.Models.FootBallFieldModel", b =>
                {
                    b.Navigation("ListField");
                });
#pragma warning restore 612, 618
        }
    }
}
