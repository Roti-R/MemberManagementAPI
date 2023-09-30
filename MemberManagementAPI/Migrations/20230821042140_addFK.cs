using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class addFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Account_Members_IDMember",
                table: "Account",
                column: "IDMember",
                principalTable: "Members",
                principalColumn: "IDMember",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Members_IDMember",
                table: "Account");
        }
    }
}
