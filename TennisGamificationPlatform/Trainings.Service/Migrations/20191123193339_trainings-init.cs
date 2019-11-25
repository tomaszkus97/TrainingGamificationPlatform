using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainings.Service.Migrations
{
    public partial class trainingsinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdentityId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Day = table.Column<int>(nullable: false),
                    Hour = table.Column<string>(nullable: true),
                    CoachId = table.Column<Guid>(nullable: true),
                    LevelName = table.Column<string>(nullable: true),
                    CurrentOptionalChallenge = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingGroups_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_TrainingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    AttendanceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_TrainingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "IdentityId", "Name", "Surname" },
                values: new object[] { new Guid("76e03793-0a33-441e-94e7-dec64d1c9703"), new Guid("00000000-0000-0000-0000-000000000000"), "Test", "Coach" });

            migrationBuilder.InsertData(
                table: "TrainingGroups",
                columns: new[] { "Id", "CoachId", "CurrentOptionalChallenge", "Day", "Hour", "LevelName", "Name" },
                values: new object[] { new Guid("76e03793-0a33-441e-94e7-dec64d1c9703"), null, new Guid("00000000-0000-0000-0000-000000000000"), 3, "18:00", "Red", "Wednesday 18:00" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_GroupId",
                table: "Attendances",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_AttendanceId",
                table: "Players",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GroupId",
                table: "Players",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingGroups_CoachId",
                table: "TrainingGroups",
                column: "CoachId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "TrainingGroups");

            migrationBuilder.DropTable(
                name: "Coaches");
        }
    }
}
