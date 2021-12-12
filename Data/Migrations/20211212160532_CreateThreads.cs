using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumRowerowe.Data.Migrations
{
    public partial class CreateThreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThreadID",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    ThreadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.ThreadID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThreadID",
                table: "Posts",
                column: "ThreadID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadID",
                table: "Posts",
                column: "ThreadID",
                principalTable: "Threads",
                principalColumn: "ThreadID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadID",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ThreadID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ThreadID",
                table: "Posts");
        }
    }
}
