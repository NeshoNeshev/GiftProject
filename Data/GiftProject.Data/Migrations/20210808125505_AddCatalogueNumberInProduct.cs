using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftProject.Data.Migrations
{
    public partial class AddCatalogueNumberInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatalogueNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogueNumber",
                table: "Products");
        }
    }
}
