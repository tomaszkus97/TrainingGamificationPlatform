using Microsoft.EntityFrameworkCore.Migrations;

namespace Players.Service.Migrations
{
    public partial class playersgroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGroup_Players_PlayerId",
                table: "PlayerGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGroup",
                table: "PlayerGroup");

            migrationBuilder.RenameTable(
                name: "PlayerGroup",
                newName: "PlayersGroups");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGroup_PlayerId",
                table: "PlayersGroups",
                newName: "IX_PlayersGroups_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayersGroups",
                table: "PlayersGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersGroups_Players_PlayerId",
                table: "PlayersGroups",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersGroups_Players_PlayerId",
                table: "PlayersGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayersGroups",
                table: "PlayersGroups");

            migrationBuilder.RenameTable(
                name: "PlayersGroups",
                newName: "PlayerGroup");

            migrationBuilder.RenameIndex(
                name: "IX_PlayersGroups_PlayerId",
                table: "PlayerGroup",
                newName: "IX_PlayerGroup_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGroup",
                table: "PlayerGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGroup_Players_PlayerId",
                table: "PlayerGroup",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
