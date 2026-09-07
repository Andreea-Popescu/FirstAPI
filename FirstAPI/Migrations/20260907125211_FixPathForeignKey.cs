using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixPathForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Paths_PathhId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Lightcones_Paths_PathhId",
                table: "Lightcones");

            migrationBuilder.DropIndex(
                name: "IX_Lightcones_PathhId",
                table: "Lightcones");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PathhId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PathhId",
                table: "Lightcones");

            migrationBuilder.DropColumn(
                name: "PathhId",
                table: "Characters");

            migrationBuilder.CreateIndex(
                name: "IX_Lightcones_PathId",
                table: "Lightcones",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PathId",
                table: "Characters",
                column: "PathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Paths_PathId",
                table: "Characters",
                column: "PathId",
                principalTable: "Paths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lightcones_Paths_PathId",
                table: "Lightcones",
                column: "PathId",
                principalTable: "Paths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Paths_PathId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Lightcones_Paths_PathId",
                table: "Lightcones");

            migrationBuilder.DropIndex(
                name: "IX_Lightcones_PathId",
                table: "Lightcones");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PathId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "PathhId",
                table: "Lightcones",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PathhId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "PathhId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "PathhId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "PathhId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "PathhId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Lightcones",
                keyColumn: "Id",
                keyValue: 1,
                column: "PathhId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Lightcones",
                keyColumn: "Id",
                keyValue: 2,
                column: "PathhId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Lightcones_PathhId",
                table: "Lightcones",
                column: "PathhId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PathhId",
                table: "Characters",
                column: "PathhId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Paths_PathhId",
                table: "Characters",
                column: "PathhId",
                principalTable: "Paths",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lightcones_Paths_PathhId",
                table: "Lightcones",
                column: "PathhId",
                principalTable: "Paths",
                principalColumn: "Id");
        }
    }
}
