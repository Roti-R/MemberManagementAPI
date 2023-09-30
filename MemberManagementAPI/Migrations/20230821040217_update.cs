using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountOfMember",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_accountOfMemberID",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "IDMember");

            migrationBuilder.RenameColumn(
                name: "accountOfMemberID",
                table: "Account",
                newName: "IDMember");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "IDMember");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "IDMember",
                table: "Members",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IDMember",
                table: "Account",
                newName: "accountOfMemberID");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

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
    }
}
