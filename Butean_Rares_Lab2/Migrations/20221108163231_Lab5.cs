using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Butean_Rares_Lab2.Migrations
{
    public partial class Lab5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Publisher",
                newName: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublisherId",
                table: "Publisher",
                newName: "ID");
        }
    }
}
