using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class UPDATEFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Members_MemberID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Management_Members_ManagerID",
                table: "Management");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentID",
                table: "Organization",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentID",
                table: "Organization",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Member_MemberID",
                table: "Account",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Management_Member_ManagerID",
                table: "Management",
                column: "ManagerID",
                principalTable: "Member",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_Organization_ParentID",
                table: "Organization",
                column: "ParentID",
                principalTable: "Organization",
                principalColumn: "OrgID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Member_MemberID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Management_Member_ManagerID",
                table: "Management");

            migrationBuilder.DropForeignKey(
                name: "FK_Organization_Organization_ParentID",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_ParentID",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Organization");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Members_MemberID",
                table: "Account",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Management_Members_ManagerID",
                table: "Management",
                column: "ManagerID",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
