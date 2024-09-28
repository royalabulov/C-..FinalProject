using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class num1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertising_Vacancy_VacancyId",
                table: "Advertising");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Companys_CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpireTime",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Advertising");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "Advertising",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising",
                newName: "IX_Advertising_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisingId",
                table: "Vacancy",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_AdvertisingId",
                table: "Vacancy",
                column: "AdvertisingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertising_Companys_CompanyId",
                table: "Advertising",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Companys_CompanyId",
                table: "Subscriptions",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancy_Advertising_AdvertisingId",
                table: "Vacancy",
                column: "AdvertisingId",
                principalTable: "Advertising",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertising_Companys_CompanyId",
                table: "Advertising");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Companys_CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancy_Advertising_AdvertisingId",
                table: "Vacancy");

            migrationBuilder.DropIndex(
                name: "IX_Vacancy_AdvertisingId",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "AdvertisingId",
                table: "Vacancy");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Advertising",
                newName: "VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertising_CompanyId",
                table: "Advertising",
                newName: "IX_Advertising_VacancyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Vacancy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpireTime",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Advertising",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertising_Vacancy_VacancyId",
                table: "Advertising",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Companys_CompanyId",
                table: "Subscriptions",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
