using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainings.Service.Migrations
{
    public partial class timespan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: new Guid("76e03793-0a33-441e-94e7-dec64d1c9703"));

            migrationBuilder.DeleteData(
                table: "TrainingGroups",
                keyColumn: "Id",
                keyValue: new Guid("76e03793-0a33-441e-94e7-dec64d1c9703"));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Hour",
                table: "TrainingGroups",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "IdentityId", "Name", "Surname" },
                values: new object[] { new Guid("f9469660-3c22-4109-910e-982f59f8ba0d"), new Guid("00000000-0000-0000-0000-000000000000"), "Test", "Coach" });

            migrationBuilder.InsertData(
                table: "TrainingGroups",
                columns: new[] { "Id", "CoachId", "CurrentOptionalChallenge", "Day", "Hour", "LevelName", "Name" },
                values: new object[] { new Guid("db9444e6-3334-4e29-9676-03f8a0c10f91"), null, new Guid("00000000-0000-0000-0000-000000000000"), 3, new TimeSpan(0, 18, 0, 0, 0), "Red", "Wednesday 18:00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: new Guid("f9469660-3c22-4109-910e-982f59f8ba0d"));

            migrationBuilder.DeleteData(
                table: "TrainingGroups",
                keyColumn: "Id",
                keyValue: new Guid("db9444e6-3334-4e29-9676-03f8a0c10f91"));

            migrationBuilder.AlterColumn<string>(
                name: "Hour",
                table: "TrainingGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "IdentityId", "Name", "Surname" },
                values: new object[] { new Guid("76e03793-0a33-441e-94e7-dec64d1c9703"), new Guid("00000000-0000-0000-0000-000000000000"), "Test", "Coach" });

            migrationBuilder.InsertData(
                table: "TrainingGroups",
                columns: new[] { "Id", "CoachId", "CurrentOptionalChallenge", "Day", "Hour", "LevelName", "Name" },
                values: new object[] { new Guid("76e03793-0a33-441e-94e7-dec64d1c9703"), null, new Guid("00000000-0000-0000-0000-000000000000"), 3, "18:00", "Red", "Wednesday 18:00" });
        }
    }
}
