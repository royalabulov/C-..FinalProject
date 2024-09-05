using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListVacant_VacantProfiles_VacantProfileId",
                table: "WishListVacant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListVacant",
                table: "WishListVacant");

            migrationBuilder.DropIndex(
                name: "IX_WishListVacant_VacantProfileId",
                table: "WishListVacant");

            migrationBuilder.RenameTable(
                name: "WishListVacant",
                newName: "WishListVacants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListVacants",
                table: "WishListVacants",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListVacants_VacantProfileId",
                table: "WishListVacants",
                column: "VacantProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListVacants_VacantProfiles_VacantProfileId",
                table: "WishListVacants",
                column: "VacantProfileId",
                principalTable: "VacantProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListVacants_VacantProfiles_VacantProfileId",
                table: "WishListVacants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListVacants",
                table: "WishListVacants");

            migrationBuilder.DropIndex(
                name: "IX_WishListVacants_VacantProfileId",
                table: "WishListVacants");

            migrationBuilder.RenameTable(
                name: "WishListVacants",
                newName: "WishListVacant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListVacant",
                table: "WishListVacant",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListVacant_VacantProfileId",
                table: "WishListVacant",
                column: "VacantProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListVacant_VacantProfiles_VacantProfileId",
                table: "WishListVacant",
                column: "VacantProfileId",
                principalTable: "VacantProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
