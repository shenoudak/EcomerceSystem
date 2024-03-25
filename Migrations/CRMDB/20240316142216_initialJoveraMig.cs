using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    public partial class initialJoveraMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    FAQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.FAQId);
                });

            migrationBuilder.CreateTable(
                name: "PageContents",
                columns: table => new
                {
                    PageContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContents", x => x.PageContentId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMehods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodPic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMehods", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "SoicialMidiaLinks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    facebooklink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApplink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInlink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instgramlink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoicialMidiaLinks", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentAr", "ContentEn", "PageTitleAr", "PageTitleEn" },
                values: new object[] { 1, "من نحن", "About Page", "من نحن", "About" });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentAr", "ContentEn", "PageTitleAr", "PageTitleEn" },
                values: new object[] { 2, "الشروط والاحكام", "Condition and Terms Page", "الشروط والاحكام", "Condition and Terms" });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentAr", "ContentEn", "PageTitleAr", "PageTitleEn" },
                values: new object[] { 3, "سياسة الخصوصية", "Privacy Policy Page", "سياسة الخصوصية", "Privacy Policy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "PageContents");

            migrationBuilder.DropTable(
                name: "PaymentMehods");

            migrationBuilder.DropTable(
                name: "SoicialMidiaLinks");
        }
    }
}
