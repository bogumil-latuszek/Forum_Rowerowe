using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumRowerowe.Data.Migrations
{
    public partial class addAuthorID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "authorID",
                table: "Threads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "authorID",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "authorID",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "authorID",
                table: "Posts");
        }
    }
}
