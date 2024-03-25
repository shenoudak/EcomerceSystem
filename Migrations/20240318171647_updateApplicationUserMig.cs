using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations
{
    public partial class updateApplicationUserMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "170912f0-e8b3-424d-842a-0e0a2e5e7f55");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6759bc2-ffd2-43ed-943f-f1c971c80575", "17832190-61f5-4e33-bd8f-35ea7ee0caa4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6759bc2-ffd2-43ed-943f-f1c971c80575");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "170912f0-e8b3-424d-842a-0e0a2e5e7f55", "b3a0ac3c-168d-42a7-a177-9658c2d35862", "Admin", "ADMIN" });
        }
    }
}
