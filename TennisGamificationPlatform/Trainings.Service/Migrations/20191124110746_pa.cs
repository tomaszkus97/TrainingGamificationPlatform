using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainings.Service.Migrations
{
    public partial class pa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Attendances_AttendanceId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_AttendanceId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "PlayerAttendance",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(nullable: false),
                    AttendanceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAttendance", x => new { x.AttendanceId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerAttendance_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAttendance_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAttendance_PlayerId",
                table: "PlayerAttendance",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerAttendance");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_AttendanceId",
                table: "Players",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Attendances_AttendanceId",
                table: "Players",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
