using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CarClassDuzenlendi1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotorGücü",
                table: "Cars",
                newName: "MotorGucu");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 5, 16, 0, 33, 48, 934, DateTimeKind.Local).AddTicks(670), "ed1c214e-30bf-4652-b536-ae4d2b94df6d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotorGucu",
                table: "Cars",
                newName: "MotorGücü");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 5, 16, 0, 31, 51, 520, DateTimeKind.Local).AddTicks(8741), "ee839de6-6c63-4ef7-bcea-f372bfe58379" });
        }
    }
}
