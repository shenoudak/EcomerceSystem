﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class addNewModelsFavAndRev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiniSubProducts_SubProductStepOnes_SubProductStepOneId",
                table: "MiniSubProducts");

            migrationBuilder.AlterColumn<int>(
                name: "SubProductStepOneId",
                table: "MiniSubProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductFavourites",
                columns: table => new
                {
                    ProductFavouriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
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
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false)
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
                name: "FK_MiniSubProducts_SubProductStepOnes_SubProductStepOneId",
                table: "MiniSubProducts",
                column: "SubProductStepOneId",
                principalTable: "SubProductStepOnes",
                principalColumn: "SubProductStepOneId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiniSubProducts_SubProductStepOnes_SubProductStepOneId",
                table: "MiniSubProducts");

            migrationBuilder.DropTable(
                name: "ProductFavourites");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.AlterColumn<int>(
                name: "SubProductStepOneId",
                table: "MiniSubProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MiniSubProducts_SubProductStepOnes_SubProductStepOneId",
                table: "MiniSubProducts",
                column: "SubProductStepOneId",
                principalTable: "SubProductStepOnes",
                principalColumn: "SubProductStepOneId");
        }
    }
}
