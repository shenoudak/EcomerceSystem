using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations
{
    public partial class addNewRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6759bc2-ffd2-43ed-943f-f1c971c80575");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67d8a139-2adb-4698-a84f-54f25b71f9fd", "274c8eb3-5e1b-4be5-89cc-2855e6216410", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d0dd427-c289-47b1-bc0a-25e49a0e5f84", "d1db4989-d7bc-4a98-af90-14958c95702a", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fac096d3-c93d-4bec-93d9-80baa4e743fe", "99446025-0c74-40ac-abc4-f27aff2f31fe", "Store", "STORE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67d8a139-2adb-4698-a84f-54f25b71f9fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d0dd427-c289-47b1-bc0a-25e49a0e5f84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fac096d3-c93d-4bec-93d9-80baa4e743fe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6759bc2-ffd2-43ed-943f-f1c971c80575", "17832190-61f5-4e33-bd8f-35ea7ee0caa4", "Admin", "ADMIN" });
        }
    }
}
