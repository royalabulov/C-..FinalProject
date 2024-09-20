using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adddddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "AdvertisingId",
                table: "Vacancy");

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Vacancy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Advertising",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Advertising",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Advertising",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Advertising");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisingId",
                table: "Vacancy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Advertising",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising",
                column: "VacancyId",
                unique: true);
        }
    }
}
