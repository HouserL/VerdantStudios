using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerdantAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonsterId",
                table: "DamageTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DamageTypes_MonsterId",
                table: "DamageTypes",
                column: "MonsterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageTypes_Monsters_MonsterId",
                table: "DamageTypes",
                column: "MonsterId",
                principalTable: "Monsters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageTypes_Monsters_MonsterId",
                table: "DamageTypes");

            migrationBuilder.DropIndex(
                name: "IX_DamageTypes_MonsterId",
                table: "DamageTypes");

            migrationBuilder.DropColumn(
                name: "MonsterId",
                table: "DamageTypes");
        }
    }
}
