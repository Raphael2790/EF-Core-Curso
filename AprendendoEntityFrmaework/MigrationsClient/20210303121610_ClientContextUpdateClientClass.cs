using Microsoft.EntityFrameworkCore.Migrations;

namespace AprendendoEntityFrmaework.MigrationsClient
{
    public partial class ClientContextUpdateClientClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Adresses_ClientID",
                schema: "Admin",
                table: "Adresses");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_ClientID",
                schema: "Admin",
                table: "Adresses",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Adresses_ClientID",
                schema: "Admin",
                table: "Adresses");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_ClientID",
                schema: "Admin",
                table: "Adresses",
                column: "ClientID",
                unique: true);
        }
    }
}
