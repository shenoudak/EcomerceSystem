using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class addStoreProfileStatusMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreProfileStatusId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoreProfileStatuses",
                columns: table => new
                {
                    StoreProfileStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusArabicTitle = table.Column<int>(type: "int", nullable: false),
                    StatusEnglishTitle = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProfileStatuses", x => x.StoreProfileStatusId);
                    table.ForeignKey(
                        name: "FK_StoreProfileStatuses_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProfileStatuses_StoreId",
                table: "StoreProfileStatuses",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProfileStatuses");

            migrationBuilder.DropColumn(
                name: "StoreProfileStatusId",
                table: "Stores");
        }
    }
}
