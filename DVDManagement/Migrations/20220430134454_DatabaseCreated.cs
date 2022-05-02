using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDManagement.Migrations
{
    public partial class DatabaseCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorModel",
                columns: table => new
                {
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActorSurname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorModel", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "DVDCategoryModel",
                columns: table => new
                {
                    CategoryNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryDescriptoin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeRestriction = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDCategoryModel", x => x.CategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "LoanTypeModel",
                columns: table => new
                {
                    LoanTypeNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanDuration = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypeModel", x => x.LoanTypeNumber);
                });

            migrationBuilder.CreateTable(
                name: "MembershipCategoryModel",
                columns: table => new
                {
                    MembershipCategoryNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MembershipCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipCategoryTotalLoans = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCategoryModel", x => x.MembershipCategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "ProducerModel",
                columns: table => new
                {
                    ProducerNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProducerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducerModel", x => x.ProducerNumber);
                });

            migrationBuilder.CreateTable(
                name: "StudioModel",
                columns: table => new
                {
                    StudioNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudioName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioModel", x => x.StudioNumber);
                });

            migrationBuilder.CreateTable(
                name: "MemberModel",
                columns: table => new
                {
                    MemberNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MembershipCategoryNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MemberLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberModel", x => x.MemberNumber);
                    table.ForeignKey(
                        name: "FK_MemberModel_MembershipCategoryModel_MembershipCategoryNumber",
                        column: x => x.MembershipCategoryNumber,
                        principalTable: "MembershipCategoryModel",
                        principalColumn: "MembershipCategoryNumber");
                });

            migrationBuilder.CreateTable(
                name: "DVDTitleModel",
                columns: table => new
                {
                    DVDNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudioNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProducerNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DVDTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DVDReleased = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenaltyCharge = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDTitleModel", x => x.DVDNumber);
                    table.ForeignKey(
                        name: "FK_DVDTitleModel_DVDCategoryModel_CategoryNumber",
                        column: x => x.CategoryNumber,
                        principalTable: "DVDCategoryModel",
                        principalColumn: "CategoryNumber");
                    table.ForeignKey(
                        name: "FK_DVDTitleModel_ProducerModel_ProducerNumber",
                        column: x => x.ProducerNumber,
                        principalTable: "ProducerModel",
                        principalColumn: "ProducerNumber");
                    table.ForeignKey(
                        name: "FK_DVDTitleModel_StudioModel_StudioNumber",
                        column: x => x.StudioNumber,
                        principalTable: "StudioModel",
                        principalColumn: "StudioNumber");
                });

            migrationBuilder.CreateTable(
                name: "CastMemberModel",
                columns: table => new
                {
                    CastMemberModelNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DVDNumber1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActorNumberActorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMemberModel", x => x.CastMemberModelNo);
                    table.ForeignKey(
                        name: "FK_CastMemberModel_ActorModel_ActorNumberActorId",
                        column: x => x.ActorNumberActorId,
                        principalTable: "ActorModel",
                        principalColumn: "ActorId");
                    table.ForeignKey(
                        name: "FK_CastMemberModel_DVDTitleModel_DVDNumber1",
                        column: x => x.DVDNumber1,
                        principalTable: "DVDTitleModel",
                        principalColumn: "DVDNumber");
                });

            migrationBuilder.CreateTable(
                name: "DVDCopyModel",
                columns: table => new
                {
                    CopyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DVDNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DatePurchased = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDCopyModel", x => x.CopyNumber);
                    table.ForeignKey(
                        name: "FK_DVDCopyModel_DVDTitleModel_DVDNumber",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitleModel",
                        principalColumn: "DVDNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMemberModel_ActorNumberActorId",
                table: "CastMemberModel",
                column: "ActorNumberActorId");

            migrationBuilder.CreateIndex(
                name: "IX_CastMemberModel_DVDNumber1",
                table: "CastMemberModel",
                column: "DVDNumber1");

            migrationBuilder.CreateIndex(
                name: "IX_DVDCopyModel_DVDNumber",
                table: "DVDCopyModel",
                column: "DVDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitleModel_CategoryNumber",
                table: "DVDTitleModel",
                column: "CategoryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitleModel_ProducerNumber",
                table: "DVDTitleModel",
                column: "ProducerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitleModel_StudioNumber",
                table: "DVDTitleModel",
                column: "StudioNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MemberModel_MembershipCategoryNumber",
                table: "MemberModel",
                column: "MembershipCategoryNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastMemberModel");

            migrationBuilder.DropTable(
                name: "DVDCopyModel");

            migrationBuilder.DropTable(
                name: "LoanTypeModel");

            migrationBuilder.DropTable(
                name: "MemberModel");

            migrationBuilder.DropTable(
                name: "ActorModel");

            migrationBuilder.DropTable(
                name: "DVDTitleModel");

            migrationBuilder.DropTable(
                name: "MembershipCategoryModel");

            migrationBuilder.DropTable(
                name: "DVDCategoryModel");

            migrationBuilder.DropTable(
                name: "ProducerModel");

            migrationBuilder.DropTable(
                name: "StudioModel");
        }
    }
}
