using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Players.Service.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    PointsToAdvance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdentityId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    LevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "LevelId", "Name", "PointsToAdvance" },
                values: new object[] { 1, 0, 250 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "IdentityId", "LevelId", "Name", "Points", "Surname" },
                values: new object[] { new Guid("44420a8f-ec2c-4ad1-aa2c-57a8624c7b3f"), 8, new Guid("3cb57a49-a626-41d6-5f19-08d760866da9"), 1, "Player", 0, "One" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_LevelId",
                table: "Players",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
