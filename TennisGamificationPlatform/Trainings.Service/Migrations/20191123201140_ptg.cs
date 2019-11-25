using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainings.Service.Migrations
{
    public partial class ptg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_TrainingGroups_GroupId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GroupId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "PlayerTrainingGroup",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(nullable: false),
                    TrainingGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTrainingGroup", x => new { x.TrainingGroupId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerTrainingGroup_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTrainingGroup_TrainingGroups_TrainingGroupId",
                        column: x => x.TrainingGroupId,
                        principalTable: "TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTrainingGroup_PlayerId",
                table: "PlayerTrainingGroup",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerTrainingGroup");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Players_GroupId",
                table: "Players",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TrainingGroups_GroupId",
                table: "Players",
                column: "GroupId",
                principalTable: "TrainingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
