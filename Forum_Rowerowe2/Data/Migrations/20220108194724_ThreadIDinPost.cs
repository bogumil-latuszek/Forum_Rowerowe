using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumRowerowe.Data.Migrations
{
    public partial class ThreadIDinPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadID",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadID",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadID",
                table: "Posts",
                column: "ThreadID",
                principalTable: "Threads",
                principalColumn: "ThreadID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadID",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadID",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadID",
                table: "Posts",
                column: "ThreadID",
                principalTable: "Threads",
                principalColumn: "ThreadID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
