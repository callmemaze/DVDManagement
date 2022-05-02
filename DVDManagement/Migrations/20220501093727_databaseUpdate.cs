using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDManagement.Migrations
{
    public partial class databaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMemberModel_ActorModel_ActorNumberActorId",
                table: "CastMemberModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CastMemberModel_DVDTitleModel_DVDNumber1",
                table: "CastMemberModel");

            migrationBuilder.RenameColumn(
                name: "DVDNumber1",
                table: "CastMemberModel",
                newName: "DVDNumber");

            migrationBuilder.RenameColumn(
                name: "ActorNumberActorId",
                table: "CastMemberModel",
                newName: "ActorNumber");

            migrationBuilder.RenameIndex(
                name: "IX_CastMemberModel_DVDNumber1",
                table: "CastMemberModel",
                newName: "IX_CastMemberModel_DVDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_CastMemberModel_ActorNumberActorId",
                table: "CastMemberModel",
                newName: "IX_CastMemberModel_ActorNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMemberModel_ActorModel_ActorNumber",
                table: "CastMemberModel",
                column: "ActorNumber",
                principalTable: "ActorModel",
                principalColumn: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMemberModel_DVDTitleModel_DVDNumber",
                table: "CastMemberModel",
                column: "DVDNumber",
                principalTable: "DVDTitleModel",
                principalColumn: "DVDNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMemberModel_ActorModel_ActorNumber",
                table: "CastMemberModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CastMemberModel_DVDTitleModel_DVDNumber",
                table: "CastMemberModel");

            migrationBuilder.RenameColumn(
                name: "DVDNumber",
                table: "CastMemberModel",
                newName: "DVDNumber1");

            migrationBuilder.RenameColumn(
                name: "ActorNumber",
                table: "CastMemberModel",
                newName: "ActorNumberActorId");

            migrationBuilder.RenameIndex(
                name: "IX_CastMemberModel_DVDNumber",
                table: "CastMemberModel",
                newName: "IX_CastMemberModel_DVDNumber1");

            migrationBuilder.RenameIndex(
                name: "IX_CastMemberModel_ActorNumber",
                table: "CastMemberModel",
                newName: "IX_CastMemberModel_ActorNumberActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMemberModel_ActorModel_ActorNumberActorId",
                table: "CastMemberModel",
                column: "ActorNumberActorId",
                principalTable: "ActorModel",
                principalColumn: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMemberModel_DVDTitleModel_DVDNumber1",
                table: "CastMemberModel",
                column: "DVDNumber1",
                principalTable: "DVDTitleModel",
                principalColumn: "DVDNumber");
        }
    }
}
