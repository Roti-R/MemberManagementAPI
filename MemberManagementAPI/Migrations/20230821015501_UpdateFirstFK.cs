using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class UpdateFirstFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "accountOfMemberID",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_accountOfMemberID",
                table: "Account",
                column: "accountOfMemberID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountOfMember",
                table: "Account",
                column: "accountOfMemberID",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountOfMember",
                table: "Account");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_accountOfMemberID",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "accountOfMemberID",
                table: "Account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "username");
        }
    }
}
