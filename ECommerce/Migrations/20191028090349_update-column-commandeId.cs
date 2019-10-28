using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Migrations
{
    public partial class updatecolumncommandeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCommandes_Commandes_CommandId",
                table: "ProductsCommandes");

            migrationBuilder.DropIndex(
                name: "IX_ProductsCommandes_CommandId",
                table: "ProductsCommandes");

            migrationBuilder.DropColumn(
                name: "CommandId",
                table: "ProductsCommandes");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCommandes_CommandeId",
                table: "ProductsCommandes",
                column: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCommandes_Commandes_CommandeId",
                table: "ProductsCommandes",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCommandes_Commandes_CommandeId",
                table: "ProductsCommandes");

            migrationBuilder.DropIndex(
                name: "IX_ProductsCommandes_CommandeId",
                table: "ProductsCommandes");

            migrationBuilder.AddColumn<int>(
                name: "CommandId",
                table: "ProductsCommandes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCommandes_CommandId",
                table: "ProductsCommandes",
                column: "CommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCommandes_Commandes_CommandId",
                table: "ProductsCommandes",
                column: "CommandId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
