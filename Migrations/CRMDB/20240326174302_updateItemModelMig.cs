using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class updateItemModelMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_StoreId",
                table: "SubProducts",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Stores_StoreId",
                table: "SubProducts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Stores_StoreId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_StoreId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Items");
        }
    }
}
