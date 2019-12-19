using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendPipeApi.Migrations
{
    public partial class following5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowingId",
                table: "UserFollows");

            migrationBuilder.DropIndex(
                name: "IX_UserFollows_FollowingId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "UserFollows",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "FollowingId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FollowingId",
                table: "AspNetUsers",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserFollows_FollowingId",
                table: "AspNetUsers",
                column: "FollowingId",
                principalTable: "UserFollows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserFollows_FollowingId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FollowingId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FollowingId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserFollows",
                newName: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FollowingId",
                table: "UserFollows",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowingId",
                table: "UserFollows",
                column: "FollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
