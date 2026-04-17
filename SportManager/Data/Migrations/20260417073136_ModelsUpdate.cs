using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endurance",
                table: "Joueurs",
                newName: "EquipeId");

            migrationBuilder.AddColumn<int>(
                name: "Endurence",
                table: "Joueurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_EquipeId",
                table: "Joueurs",
                column: "EquipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joueurs_Equipes_EquipeId",
                table: "Joueurs",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joueurs_Equipes_EquipeId",
                table: "Joueurs");

            migrationBuilder.DropIndex(
                name: "IX_Joueurs_EquipeId",
                table: "Joueurs");

            migrationBuilder.DropColumn(
                name: "Endurence",
                table: "Joueurs");

            migrationBuilder.RenameColumn(
                name: "EquipeId",
                table: "Joueurs",
                newName: "Endurance");
        }
    }
}
