using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementApp.Migrations
{
    public partial class imageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Posts");
        }
    }
}
