using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendPipeApi.Migrations
{
    public partial class addUserFollowUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollows_SourceUserId",
                table: "UserFollows");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_SourceUserId_FollowedUserId",
                table: "UserFollows",
                columns: new[] { "SourceUserId", "FollowedUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollows_SourceUserId_FollowedUserId",
                table: "UserFollows");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_SourceUserId",
                table: "UserFollows",
                column: "SourceUserId");
        }
    }
}
