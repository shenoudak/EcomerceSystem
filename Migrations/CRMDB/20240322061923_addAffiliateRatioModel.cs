using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class addAffiliateRatioModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffiliateRatios",
                columns: table => new
                {
                    AffiliateRatioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ratioInPrecentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateRatios", x => x.AffiliateRatioId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliateRatios");
        }
    }
}
