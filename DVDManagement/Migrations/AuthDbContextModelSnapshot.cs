﻿// <auto-generated />
using System;
using DVDManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DVDManagement.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DVDManagement.Models.ActorModel", b =>
                {
                    b.Property<string>("ActorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActorFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActorSurname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorId");

                    b.ToTable("ActorModel");
                });

            modelBuilder.Entity("DVDManagement.Models.CastMemberModel", b =>
                {
                    b.Property<string>("CastMemberModelNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActorNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DVDNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CastMemberModelNo");

                    b.HasIndex("ActorNumber");

                    b.HasIndex("DVDNumber");

                    b.ToTable("CastMemberModel");
                });

            modelBuilder.Entity("DVDManagement.Models.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("DVDManagement.Models.DVDCategoryModel", b =>
                {
                    b.Property<string>("CategoryNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgeRestriction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryDescriptoin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryNumber");

                    b.ToTable("DVDCategoryModel");
                });

            modelBuilder.Entity("DVDManagement.Models.DVDCopyModel", b =>
                {
                    b.Property<string>("CopyNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DVDNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("datetime2");

                    b.HasKey("CopyNumber");

                    b.HasIndex("DVDNumber");

                    b.ToTable("DVDCopyModel");
                });

            modelBuilder.Entity("DVDManagement.Models.DVDTitleModel", b =>
                {
                    b.Property<string>("DVDNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DVDReleased")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DVDTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PenaltyCharge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProducerNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StandardCharge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudioNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DVDNumber");

                    b.HasIndex("CategoryNumber");

                    b.HasIndex("ProducerNumber");

                    b.HasIndex("StudioNumber");

                    b.ToTable("DVDTitleModel");
                });

            modelBuilder.Entity("DVDManagement.Models.LoanModel", b =>
                {
                    b.Property<string>("LoanNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CopyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoanTypeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanNumber");

                    b.ToTable("LoanModel");
                });

            modelBuilder.Entity("DVDManagement.Models.LoanTypeModel", b =>
                {
                    b.Property<string>("LoanTypeNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoanDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoanType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanTypeNumber");

                    b.ToTable("LoanTypeModel");
                });

            modelBuilder.Entity("DVDManagement.Models.MemberModel", b =>
                {
                    b.Property<string>("MemberNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MemberAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MemberDateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MembershipCategoryNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MemberNumber");

                    b.HasIndex("MembershipCategoryNumber");

                    b.ToTable("MemberModel");
                });

            modelBuilder.Entity("DVDManagement.Models.MembershipCategoryModel", b =>
                {
                    b.Property<string>("MembershipCategoryNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MembershipCategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MembershipCategoryTotalLoans")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MembershipCategoryNumber");

                    b.ToTable("MembershipCategoryModel");
                });

            modelBuilder.Entity("DVDManagement.Models.ProducerModel", b =>
                {
                    b.Property<string>("ProducerNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProducerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducerNumber");

                    b.ToTable("ProducerModel");
                });

            modelBuilder.Entity("DVDManagement.Models.StudioModel", b =>
                {
                    b.Property<string>("StudioNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudioName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudioNumber");

                    b.ToTable("StudioModel");
                });

            modelBuilder.Entity("DVDManagement.Models.User", b =>
                {
                    b.Property<int>("UserNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserNumber"), 1L, 1);

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserNumber");

                    b.ToTable("User");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
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

                    b.ToTable("AspNetUsers", (string)null);
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

            modelBuilder.Entity("DVDManagement.Models.CastMemberModel", b =>
                {
                    b.HasOne("DVDManagement.Models.ActorModel", "ActorNumberModel")
                        .WithMany()
                        .HasForeignKey("ActorNumber");

                    b.HasOne("DVDManagement.Models.DVDTitleModel", "DVDNumberModel")
                        .WithMany()
                        .HasForeignKey("DVDNumber");

                    b.Navigation("ActorNumberModel");

                    b.Navigation("DVDNumberModel");
                });

            modelBuilder.Entity("DVDManagement.Models.DVDCopyModel", b =>
                {
                    b.HasOne("DVDManagement.Models.DVDTitleModel", "DVDTitleModel")
                        .WithMany()
                        .HasForeignKey("DVDNumber");

                    b.Navigation("DVDTitleModel");
                });

            modelBuilder.Entity("DVDManagement.Models.DVDTitleModel", b =>
                {
                    b.HasOne("DVDManagement.Models.DVDCategoryModel", "DVDCategoryModel")
                        .WithMany()
                        .HasForeignKey("CategoryNumber");

                    b.HasOne("DVDManagement.Models.ProducerModel", "ProducerModel")
                        .WithMany()
                        .HasForeignKey("ProducerNumber");

                    b.HasOne("DVDManagement.Models.StudioModel", "StudioModel")
                        .WithMany()
                        .HasForeignKey("StudioNumber");

                    b.Navigation("DVDCategoryModel");

                    b.Navigation("ProducerModel");

                    b.Navigation("StudioModel");
                });

            modelBuilder.Entity("DVDManagement.Models.MemberModel", b =>
                {
                    b.HasOne("DVDManagement.Models.MembershipCategoryModel", "MembershipCategoryModel")
                        .WithMany()
                        .HasForeignKey("MembershipCategoryNumber");

                    b.Navigation("MembershipCategoryModel");
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
#pragma warning restore 612, 618
        }
    }
}
