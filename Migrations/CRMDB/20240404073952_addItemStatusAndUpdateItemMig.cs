using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class addItemStatusAndUpdateItemMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountRatio",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemStatusId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "OldPrice",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OurSellingPrice",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SellingPriceForCustomer",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ItemStatuses",
                columns: table => new
                {
                    ItemStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusArabicTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEnglishTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatuses", x => x.ItemStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemStatusId",
                table: "Items",
                column: "ItemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemStatuses_ItemStatusId",
                table: "Items",
                column: "ItemStatusId",
                principalTable: "ItemStatuses",
                principalColumn: "ItemStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemStatuses_ItemStatusId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemStatusId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DiscountRatio",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemStatusId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OurSellingPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SellingPriceForCustomer",
                table: "Items");
        }
    }
}
