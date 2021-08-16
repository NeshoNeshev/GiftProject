using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftProject.Data.Migrations
{
    public partial class AddUpAndDownVoteEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "ProductVotes",
                newName: "Vote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "ProductVotes",
                newName: "Rate");
        }
    }
}
