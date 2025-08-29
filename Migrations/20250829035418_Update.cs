using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "AspNetRoles",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "Description",
                value: "Default role for new users");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "Description",
                value: "Administrator role with full permissions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "187f44e5-86e5-455b-8e92-f76a84831b31", "AQAAAAIAAYagAAAAEE+gc40SYjkAHV068hLyqndYRGPccq257GO0gp2jn56GriF5Yitj8B0b06X9WR3p0A==", "6ed6b49c-140f-4713-b3c6-a943efae4f38" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AspNetRoles",
                newName: "description");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9ff1ef6-6d5d-4610-8eac-0473f293f568", "AQAAAAIAAYagAAAAEJueN8z8I4nWjRdUIcwG1wJopPSgoRCaFMs1DIxv+sUhRR6kjMYRziEpKbofAyvjdA==", "c9d4b8f2-baed-40a9-af2e-7bd6e821b3eb" });
        }
    }
}
