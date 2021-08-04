using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftProject.Data.Migrations
{
    public partial class changeProductVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "ProductVotes");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextVoteDate",
                table: "ProductVotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "ProductVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextVoteDate",
                table: "ProductVotes");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "ProductVotes");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "ProductVotes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
