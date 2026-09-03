using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanetsAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Planet",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "PlanetId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanetId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "PlanetId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "PlanetId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "PlanetId",
                value: 4);

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Eternal Land", "Amphoreus" },
                    { 2, "Planet of Festivities", "Penacony" },
                    { 3, "Vibrant Dreamscape", "Planarcadia" },
                    { 4, "Alliance Flagship", "Xianzhou" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlanetId",
                table: "Characters",
                column: "PlanetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Planets_PlanetId",
                table: "Characters",
                column: "PlanetId",
                principalTable: "Planets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Planets_PlanetId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PlanetId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PlanetId",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "Planet",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Planet",
                value: "Amphoreus");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Planet",
                value: "Penacony");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Planet",
                value: "Planarcadia");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "Planet",
                value: "Xianzhou");
        }
    }
}
