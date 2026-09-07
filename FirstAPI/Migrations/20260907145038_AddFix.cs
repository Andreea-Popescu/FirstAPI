using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Paths",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "Rememberance" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Paths",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
