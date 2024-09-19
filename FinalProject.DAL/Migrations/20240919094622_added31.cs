using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListVacants_Vacancy_VacancyId",
                table: "WishListVacants");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListVacants_VacantProfiles_VacantProfileId",
                table: "WishListVacants");

            migrationBuilder.DropIndex(
                name: "IX_WishListVacants_VacancyId",
                table: "WishListVacants");

            migrationBuilder.DropIndex(
                name: "IX_WishListVacants_VacantProfileId",
                table: "WishListVacants");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "WishListVacants");

            migrationBuilder.DropColumn(
                name: "VacantProfileId",
                table: "WishListVacants");

            migrationBuilder.CreateTable(
                name: "VacantProfileWishListVacant",
                columns: table => new
                {
                    VacantProfilesId = table.Column<int>(type: "int", nullable: false),
                    WishListVacantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacantProfileWishListVacant", x => new { x.VacantProfilesId, x.WishListVacantId });
                    table.ForeignKey(
                        name: "FK_VacantProfileWishListVacant_VacantProfiles_VacantProfilesId",
                        column: x => x.VacantProfilesId,
                        principalTable: "VacantProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacantProfileWishListVacant_WishListVacants_WishListVacantId",
                        column: x => x.WishListVacantId,
                        principalTable: "WishListVacants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacantProfileWishListVacant_WishListVacantId",
                table: "VacantProfileWishListVacant",
                column: "WishListVacantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacantProfileWishListVacant");

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "WishListVacants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VacantProfileId",
                table: "WishListVacants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WishListVacants_VacancyId",
                table: "WishListVacants",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListVacants_VacantProfileId",
                table: "WishListVacants",
                column: "VacantProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListVacants_Vacancy_VacancyId",
                table: "WishListVacants",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListVacants_VacantProfiles_VacantProfileId",
                table: "WishListVacants",
                column: "VacantProfileId",
                principalTable: "VacantProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
