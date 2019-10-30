using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursAPI.Migrations
{
    public partial class ajoutchampimagedansclasscontact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Contacts");
        }
    }
}
