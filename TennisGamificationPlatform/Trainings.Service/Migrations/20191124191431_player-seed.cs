using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainings.Service.Migrations
{
    public partial class playerseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: new Guid("f9469660-3c22-4109-910e-982f59f8ba0d"));

            migrationBuilder.DeleteData(
                table: "TrainingGroups",
                keyColumn: "Id",
                keyValue: new Guid("db9444e6-3334-4e29-9676-03f8a0c10f91"));

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "IdentityId", "Name", "Surname" },
                values: new object[] { new Guid("cb07d988-5693-4544-a33f-cc80ecb2cea9"), new Guid("00000000-0000-0000-0000-000000000000"), "Test", "Coach" });

            migrationBuilder.InsertData(
                table: "Players",
                column: "Id",
                value: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.InsertData(
                table: "TrainingGroups",
                columns: new[] { "Id", "CoachId", "CurrentOptionalChallenge", "Day", "Hour", "LevelName", "Name" },
                values: new object[] { new Guid("1faa9596-5427-4bac-8cab-a3fd951c1774"), null, new Guid("00000000-0000-0000-0000-000000000000"), 3, new TimeSpan(0, 18, 0, 0, 0), "Red", "Wednesday 18:00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: new Guid("cb07d988-5693-4544-a33f-cc80ecb2cea9"));

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "TrainingGroups",
                keyColumn: "Id",
                keyValue: new Guid("1faa9596-5427-4bac-8cab-a3fd951c1774"));

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "IdentityId", "Name", "Surname" },
                values: new object[] { new Guid("f9469660-3c22-4109-910e-982f59f8ba0d"), new Guid("00000000-0000-0000-0000-000000000000"), "Test", "Coach" });

            migrationBuilder.InsertData(
                table: "TrainingGroups",
                columns: new[] { "Id", "CoachId", "CurrentOptionalChallenge", "Day", "Hour", "LevelName", "Name" },
                values: new object[] { new Guid("db9444e6-3334-4e29-9676-03f8a0c10f91"), null, new Guid("00000000-0000-0000-0000-000000000000"), 3, new TimeSpan(0, 18, 0, 0, 0), "Red", "Wednesday 18:00" });
        }
    }
}
