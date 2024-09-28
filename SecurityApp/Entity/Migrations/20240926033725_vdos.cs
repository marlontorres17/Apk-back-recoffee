using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class vdos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codeUnique",
                table: "farms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "collectorFarms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectorFarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_collectorFarms_farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collectorFarms_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_farms_codeUnique",
                table: "farms",
                column: "codeUnique",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_collectorFarms_FarmId",
                table: "collectorFarms",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_collectorFarms_PersonId",
                table: "collectorFarms",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collectorFarms");

            migrationBuilder.DropIndex(
                name: "IX_farms_codeUnique",
                table: "farms");

            migrationBuilder.DropColumn(
                name: "codeUnique",
                table: "farms");
        }
    }
}
