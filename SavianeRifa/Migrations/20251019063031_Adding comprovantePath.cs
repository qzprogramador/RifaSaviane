using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavianeRifa.Migrations
{
    /// <inheritdoc />
    public partial class AddingcomprovantePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComprovantePath",
                table: "PaymentInformations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComprovantePath",
                table: "PaymentInformations");
        }
    }
}
