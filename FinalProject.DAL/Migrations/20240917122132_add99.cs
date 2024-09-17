using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add99 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListVacancies_Vacancy_VacancyId",
                table: "WishListVacancies");

            migrationBuilder.DropIndex(
                name: "IX_WishListVacancies_VacancyId",
                table: "WishListVacancies");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "WishListVacancies");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpireTime",
                table: "Companys");

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "WishListVacants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpireTime",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionLevel",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VacancyWishListVacancy",
                columns: table => new
                {
                    VacancyId = table.Column<int>(type: "int", nullable: false),
                    WishListVacancyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyWishListVacancy", x => new { x.VacancyId, x.WishListVacancyId });
                    table.ForeignKey(
                        name: "FK_VacancyWishListVacancy_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancyWishListVacancy_WishListVacancies_WishListVacancyId",
                        column: x => x.WishListVacancyId,
                        principalTable: "WishListVacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishListVacants_VacancyId",
                table: "WishListVacants",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CompanyId",
                table: "Subscriptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyWishListVacancy_WishListVacancyId",
                table: "VacancyWishListVacancy",
                column: "WishListVacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Companys_CompanyId",
                table: "Subscriptions",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListVacants_Vacancy_VacancyId",
                table: "WishListVacants",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Companys_CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListVacants_Vacancy_VacancyId",
                table: "WishListVacants");

            migrationBuilder.DropTable(
                name: "VacancyWishListVacancy");

            migrationBuilder.DropIndex(
                name: "IX_WishListVacants_VacancyId",
                table: "WishListVacants");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "WishListVacants");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpireTime",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionLevel",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "WishListVacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpireTime",
                table: "Companys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_WishListVacancies_VacancyId",
                table: "WishListVacancies",
                column: "VacancyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListVacancies_Vacancy_VacancyId",
                table: "WishListVacancies",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
