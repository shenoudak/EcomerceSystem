using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class updateInSubPeoductStepOneAndItemModelsMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "MiniSubProducts");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "SubProductStepOnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemMiniDetailsAr",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemMiniDetailsEn",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "SubProductStepOnes");

            migrationBuilder.DropColumn(
                name: "ItemMiniDetailsAr",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemMiniDetailsEn",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "MiniSubProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
