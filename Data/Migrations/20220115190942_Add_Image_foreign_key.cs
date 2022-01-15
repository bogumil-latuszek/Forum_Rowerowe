using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumRowerowe.Data.Migrations
{
    public partial class Add_Image_foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImageID",
                table: "Posts",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_ImageID",
                table: "Posts",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Images_ImageID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ImageID",
                table: "Posts");
        }
    }
}
