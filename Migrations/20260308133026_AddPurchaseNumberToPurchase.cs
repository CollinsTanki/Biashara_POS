using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biashara_POS.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseNumberToPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchaseNumber",
                table: "Purchases",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseNumber",
                table: "Purchases");
        }
    }
}
