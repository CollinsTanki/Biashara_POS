using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biashara_POS.Migrations
{
    /// <inheritdoc />
    public partial class AddVatIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VatSetups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VatSetups");
        }
    }
}
