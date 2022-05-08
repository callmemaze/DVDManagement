using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDManagement.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanModel",
                columns: table => new
                {
                    LoanNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoanTypeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturned = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanModel", x => x.LoanNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanModel");
        }
    }
}
