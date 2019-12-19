using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendPipeApi.Migrations
{
    public partial class following6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFollows");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserFollows",
                nullable: false,
                defaultValue: 0);
        }
    }
}
