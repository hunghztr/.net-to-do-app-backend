using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class New5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c51208c5-6eb8-419e-83d8-b3ad0d175e80", "AQAAAAIAAYagAAAAEADedjdK/IwxR8vix1x5QkpyvRlHz7Y2eUbr/5Pw14SxR4xpEH9zKzOmxDMivPLynA==", "78a06099-da93-4236-85d9-ac0cd0ec249f" });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Path",
                value: "/api/uploadfiles/download/{id}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "455e6793-dde9-4a64-87b9-c950cfc72e95", "AQAAAAIAAYagAAAAEONg2BZlSoPWYrzH/fCW0BoXlsMn/65oM8sJMM5GcrrGuElcsbcteWysb9zYe/Vp5Q==", "90895813-b35c-46c6-a1cc-78f1fec9c574" });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Path",
                value: "/api/uploadfiles/download{id}");
        }
    }
}
