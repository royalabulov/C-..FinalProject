using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addd1231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companys_Industry_IndustryId",
                table: "Companys");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancy_Advertising_AdvertisingId1",
                table: "Vacancy");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropIndex(
                name: "IX_Vacancy_AdvertisingId1",
                table: "Vacancy");

            migrationBuilder.DropIndex(
                name: "IX_Companys_IndustryId",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "AdvertisingId1",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Companys");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "VacantProfiles",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "VacantProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "VacantProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising",
                column: "VacancyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertising_Vacancy_VacancyId",
                table: "Advertising",
                column: "VacancyId",
                principalTable: "Vacancy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertising_Vacancy_VacancyId",
                table: "Advertising");

            migrationBuilder.DropIndex(
                name: "IX_Advertising_VacancyId",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "About",
                table: "VacantProfiles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "VacantProfiles");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "VacantProfiles",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisingId1",
                table: "Vacancy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "Companys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_AdvertisingId1",
                table: "Vacancy",
                column: "AdvertisingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_IndustryId",
                table: "Companys",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companys_Industry_IndustryId",
                table: "Companys",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancy_Advertising_AdvertisingId1",
                table: "Vacancy",
                column: "AdvertisingId1",
                principalTable: "Advertising",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
