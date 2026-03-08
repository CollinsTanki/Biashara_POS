using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biashara_POS.Migrations
{
    /// <inheritdoc />
    public partial class AddProductStockQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StockQuantity",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");
        }
    }
}
