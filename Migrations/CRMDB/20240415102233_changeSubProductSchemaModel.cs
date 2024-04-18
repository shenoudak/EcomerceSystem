using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class changeSubProductSchemaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Colors_ColorId",
                table: "SubProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Sizes_SizeId",
                table: "SubProducts");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "ProductFavourites");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "SubProducts",
                newName: "StepTwoId");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "SubProducts",
                newName: "StepOneId");

            migrationBuilder.RenameIndex(
                name: "IX_SubProducts_SizeId",
                table: "SubProducts",
                newName: "IX_SubProducts_StepTwoId");

            migrationBuilder.RenameIndex(
                name: "IX_SubProducts_ColorId",
                table: "SubProducts",
                newName: "IX_SubProducts_StepOneId");

            migrationBuilder.CreateTable(
                name: "StepOnes",
                columns: table => new
                {
                    StepOneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepOneTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StepOneTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepOnes", x => x.StepOneId);
                });

            migrationBuilder.CreateTable(
                name: "StepTwos",
                columns: table => new
                {
                    StepTwoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepTwoTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StepTwoTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StepOneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepTwos", x => x.StepTwoId);
                    table.ForeignKey(
                        name: "FK_StepTwos_StepOnes_StepOneId",
                        column: x => x.StepOneId,
                        principalTable: "StepOnes",
                        principalColumn: "StepOneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubProductStepOnes",
                columns: table => new
                {
                    SubProductStepOneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepOneId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProductStepOnes", x => x.SubProductStepOneId);
                    table.ForeignKey(
                        name: "FK_SubProductStepOnes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubProductStepOnes_StepOnes_StepOneId",
                        column: x => x.StepOneId,
                        principalTable: "StepOnes",
                        principalColumn: "StepOneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MiniSubProducts",
                columns: table => new
                {
                    MiniSubProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepTwoId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ItemQRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubProductStepOneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniSubProducts", x => x.MiniSubProductId);
                    table.ForeignKey(
                        name: "FK_MiniSubProducts_StepTwos_StepTwoId",
                        column: x => x.StepTwoId,
                        principalTable: "StepTwos",
                        principalColumn: "StepTwoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MiniSubProducts_SubProductStepOnes_SubProductStepOneId",
                        column: x => x.SubProductStepOneId,
                        principalTable: "SubProductStepOnes",
                        principalColumn: "SubProductStepOneId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MiniSubProducts_StepTwoId",
                table: "MiniSubProducts",
                column: "StepTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_MiniSubProducts_SubProductStepOneId",
                table: "MiniSubProducts",
                column: "SubProductStepOneId");

            migrationBuilder.CreateIndex(
                name: "IX_StepTwos_StepOneId",
                table: "StepTwos",
                column: "StepOneId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductStepOnes_ItemId",
                table: "SubProductStepOnes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductStepOnes_StepOneId",
                table: "SubProductStepOnes",
                column: "StepOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_StepOnes_StepOneId",
                table: "SubProducts",
                column: "StepOneId",
                principalTable: "StepOnes",
                principalColumn: "StepOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_StepTwos_StepTwoId",
                table: "SubProducts",
                column: "StepTwoId",
                principalTable: "StepTwos",
                principalColumn: "StepTwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_StepOnes_StepOneId",
                table: "SubProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_StepTwos_StepTwoId",
                table: "SubProducts");

            migrationBuilder.DropTable(
                name: "MiniSubProducts");

            migrationBuilder.DropTable(
                name: "StepTwos");

            migrationBuilder.DropTable(
                name: "SubProductStepOnes");

            migrationBuilder.DropTable(
                name: "StepOnes");

            migrationBuilder.RenameColumn(
                name: "StepTwoId",
                table: "SubProducts",
                newName: "SizeId");

            migrationBuilder.RenameColumn(
                name: "StepOneId",
                table: "SubProducts",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_SubProducts_StepTwoId",
                table: "SubProducts",
                newName: "IX_SubProducts_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubProducts_StepOneId",
                table: "SubProducts",
                newName: "IX_SubProducts_ColorId");

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "ProductFavourites",
                columns: table => new
                {
                    ProductFavouriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFavourites", x => x.ProductFavouriteId);
                    table.ForeignKey(
                        name: "FK_ProductFavourites_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFavourites_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    ProductReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.ProductReviewId);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SizeTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFavourites_CustomerId",
                table: "ProductFavourites",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFavourites_ItemId",
                table: "ProductFavourites",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_CustomerId",
                table: "ProductReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ItemId",
                table: "ProductReviews",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Colors_ColorId",
                table: "SubProducts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Sizes_SizeId",
                table: "SubProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId");
        }
    }
}
