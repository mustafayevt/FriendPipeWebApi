using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendPipeApi.Migrations
{
    public partial class following7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "FollowedUserId",
                table: "UserFollows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceUserId",
                table: "UserFollows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FollowedUserId",
                table: "UserFollows",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_SourceUserId",
                table: "UserFollows",
                column: "SourceUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowedUserId",
                table: "UserFollows",
                column: "FollowedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_SourceUserId",
                table: "UserFollows",
                column: "SourceUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowedUserId",
                table: "UserFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_SourceUserId",
                table: "UserFollows");

            migrationBuilder.DropIndex(
                name: "IX_UserFollows_FollowedUserId",
                table: "UserFollows");

            migrationBuilder.DropIndex(
                name: "IX_UserFollows_SourceUserId",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "FollowedUserId",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "SourceUserId",
                table: "UserFollows");

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
    }
}
