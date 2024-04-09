using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class addStoreProfileImagesMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddingTax",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Stores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CatagoriesTypes",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "Stores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Depit",
                table: "Stores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IPan",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdPhoto",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LicensePhoto",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleForSupply",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ShareRatio",
                table: "Stores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TradeName",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoreProfileImages",
                columns: table => new
                {
                    StoreProfileImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProfileImages", x => x.StoreProfileImageId);
                    table.ForeignKey(
                        name: "FK_StoreProfileImages_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProfileImages_StoreId",
                table: "StoreProfileImages",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProfileImages");

            migrationBuilder.DropColumn(
                name: "AddingTax",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CatagoriesTypes",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Depit",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IPan",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IdPhoto",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "LicensePhoto",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ResponsibleForSupply",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ShareRatio",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "TradeName",
                table: "Stores");
        }
    }
}
