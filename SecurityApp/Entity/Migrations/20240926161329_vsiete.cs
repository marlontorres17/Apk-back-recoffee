using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class vsiete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lots_farms_FarmId",
                table: "lots");

            migrationBuilder.AlterColumn<string>(
                name: "NumberDocument",
                table: "persons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SizeMeters",
                table: "lots",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "lots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "lots",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_persons_NumberDocument",
                table: "persons",
                column: "NumberDocument",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lots_farms_FarmId",
                table: "lots",
                column: "FarmId",
                principalTable: "farms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lots_farms_FarmId",
                table: "lots");

            migrationBuilder.DropIndex(
                name: "IX_persons_NumberDocument",
                table: "persons");

            migrationBuilder.AlterColumn<string>(
                name: "NumberDocument",
                table: "persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SizeMeters",
                table: "lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "lots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lots_farms_FarmId",
                table: "lots",
                column: "FarmId",
                principalTable: "farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
