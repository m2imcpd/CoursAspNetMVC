using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursAPI.Migrations
{
    public partial class addUserModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersContacts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersContacts",
                table: "UsersContacts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersContacts",
                table: "UsersContacts");

            migrationBuilder.RenameTable(
                name: "UsersContacts",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
