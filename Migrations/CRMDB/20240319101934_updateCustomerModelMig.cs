using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class updateCustomerModelMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Customers");
        }
    }
}
