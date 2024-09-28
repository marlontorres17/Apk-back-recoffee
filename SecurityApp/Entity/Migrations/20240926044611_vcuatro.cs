using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class vcuatro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_farms_codeUnique",
                table: "farms");

            migrationBuilder.AlterColumn<string>(
                name: "codeUnique",
                table: "farms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_farms_codeUnique",
                table: "farms",
                column: "codeUnique",
                unique: true,
                filter: "[codeUnique] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_farms_codeUnique",
                table: "farms");

            migrationBuilder.AlterColumn<string>(
                name: "codeUnique",
                table: "farms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_farms_codeUnique",
                table: "farms",
                column: "codeUnique",
                unique: true);
        }
    }
}
