using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPathsAndLightCones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PathId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PathhId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Paths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lightcones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Rarity = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseAtk = table.Column<int>(type: "INTEGER", nullable: false),
                    PathId = table.Column<int>(type: "INTEGER", nullable: false),
                    PathhId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lightcones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lightcones_Paths_PathhId",
                        column: x => x.PathhId,
                        principalTable: "Paths",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PathId", "PathhId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathId", "PathhId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathId", "PathhId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathId", "PathhId" },
                values: new object[] { 0, null });

            migrationBuilder.InsertData(
                table: "Lightcones",
                columns: new[] { "Id", "BaseAtk", "Name", "PathId", "PathhId", "Rarity" },
                values: new object[,]
                {
                    { 1, 687, "Thus Burns The Dawn", 1, null, 5 },
                    { 2, 476, "A Grounded Ascent", 4, null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Paths",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Destruction" },
                    { 2, "Hunt" },
                    { 3, "Erudition" },
                    { 4, "Harmony" },
                    { 5, "Nihility" },
                    { 6, "Preservation" },
                    { 7, "Abundance" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PathhId",
                table: "Characters",
                column: "PathhId");

            migrationBuilder.CreateIndex(
                name: "IX_Lightcones_PathhId",
                table: "Lightcones",
                column: "PathhId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Paths_PathhId",
                table: "Characters",
                column: "PathhId",
                principalTable: "Paths",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Paths_PathhId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Lightcones");

            migrationBuilder.DropTable(
                name: "Paths");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PathhId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PathId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PathhId",
                table: "Characters");
        }
    }
}
