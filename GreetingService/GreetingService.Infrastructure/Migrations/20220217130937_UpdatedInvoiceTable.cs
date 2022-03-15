using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreetingService.Infrastructure.Migrations
{
    public partial class UpdatedInvoiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_UserEmail",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Invoices",
                newName: "SenderEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UserEmail",
                table: "Invoices",
                newName: "IX_Invoices_SenderEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_SenderEmail",
                table: "Invoices",
                column: "SenderEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_SenderEmail",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "Invoices",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SenderEmail",
                table: "Invoices",
                newName: "IX_Invoices_UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_UserEmail",
                table: "Invoices",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
