using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendPipeApi.Migrations
{
    public partial class initialize4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
