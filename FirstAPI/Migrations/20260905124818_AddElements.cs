using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "ElementId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "ElementId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "ElementId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "ElementId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Elements",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Physical" },
                    { 2, "Fire" },
                    { 3, "Ice" },
                    { 4, "Lightning" },
                    { 5, "Wind" },
                    { 6, "Quantum" },
                    { 7, "Imaginary" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ElementId",
                table: "Characters",
                column: "ElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Elements_ElementId",
                table: "Characters",
                column: "ElementId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Elements_ElementId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Characters_ElementId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "Characters");
        }
    }
}
