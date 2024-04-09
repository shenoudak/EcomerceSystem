using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class addUpdateStoreProfileStatusMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreProfileStatuses_Stores_StoreId",
                table: "StoreProfileStatuses");

            migrationBuilder.DropIndex(
                name: "IX_StoreProfileStatuses_StoreId",
                table: "StoreProfileStatuses");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "StoreProfileStatuses");

            migrationBuilder.AddColumn<string>(
                name: "RejectProfileReason",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreProfileStatusId",
                table: "Stores",
                column: "StoreProfileStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_StoreProfileStatuses_StoreProfileStatusId",
                table: "Stores",
                column: "StoreProfileStatusId",
                principalTable: "StoreProfileStatuses",
                principalColumn: "StoreProfileStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StoreProfileStatuses_StoreProfileStatusId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_StoreProfileStatusId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "RejectProfileReason",
                table: "Stores");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "StoreProfileStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoreProfileStatuses_StoreId",
                table: "StoreProfileStatuses",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProfileStatuses_Stores_StoreId",
                table: "StoreProfileStatuses",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
