using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class Teoteoteo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Account",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Account",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Account",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Account",
                newName: "password");
        }
    }
}
