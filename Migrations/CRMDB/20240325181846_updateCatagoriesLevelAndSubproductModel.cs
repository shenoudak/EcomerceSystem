using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class updateCatagoriesLevelAndSubproductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MiniSubCategory_MiniSubCategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_MiniSubCategory_SubCategory_SubCategoryId",
                table: "MiniSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Categories_CategoryId",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniSubCategory",
                table: "MiniSubCategory");

            migrationBuilder.RenameTable(
                name: "SubCategory",
                newName: "SubCategories");

            migrationBuilder.RenameTable(
                name: "MiniSubCategory",
                newName: "MiniSubCategories");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategories",
                newName: "IX_SubCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MiniSubCategory_SubCategoryId",
                table: "MiniSubCategories",
                newName: "IX_MiniSubCategories_SubCategoryId");

            migrationBuilder.AddColumn<bool>(
                name: "HasSubProduct",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories",
                column: "SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniSubCategories",
                table: "MiniSubCategories",
                column: "MiniSubCategoryId");

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "SubProducts",
                columns: table => new
                {
                    SubProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ItemQRCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProducts", x => x.SubProductId);
                    table.ForeignKey(
                        name: "FK_SubProducts_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId");
                    table.ForeignKey(
                        name: "FK_SubProducts_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubProducts_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_ColorId",
                table: "SubProducts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_ItemId",
                table: "SubProducts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_SizeId",
                table: "SubProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MiniSubCategories_MiniSubCategoryId",
                table: "Items",
                column: "MiniSubCategoryId",
                principalTable: "MiniSubCategories",
                principalColumn: "MiniSubCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MiniSubCategories_SubCategories_SubCategoryId",
                table: "MiniSubCategories",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "SubCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MiniSubCategories_MiniSubCategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_MiniSubCategories_SubCategories_SubCategoryId",
                table: "MiniSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropTable(
                name: "SubProducts");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniSubCategories",
                table: "MiniSubCategories");

            migrationBuilder.DropColumn(
                name: "HasSubProduct",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "SubCategories",
                newName: "SubCategory");

            migrationBuilder.RenameTable(
                name: "MiniSubCategories",
                newName: "MiniSubCategory");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategory",
                newName: "IX_SubCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MiniSubCategories_SubCategoryId",
                table: "MiniSubCategory",
                newName: "IX_MiniSubCategory_SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory",
                column: "SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniSubCategory",
                table: "MiniSubCategory",
                column: "MiniSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MiniSubCategory_MiniSubCategoryId",
                table: "Items",
                column: "MiniSubCategoryId",
                principalTable: "MiniSubCategory",
                principalColumn: "MiniSubCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MiniSubCategory_SubCategory_SubCategoryId",
                table: "MiniSubCategory",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Categories_CategoryId",
                table: "SubCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
