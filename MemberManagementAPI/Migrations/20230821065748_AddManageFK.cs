using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class AddManageFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Members_IDMember",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Organization",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "IDMember",
                table: "Members",
                newName: "MemberID");

            migrationBuilder.RenameColumn(
                name: "IDMember",
                table: "Account",
                newName: "MemberID");

            migrationBuilder.CreateTable(
                name: "Management",
                columns: table => new
                {
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Management", x => new { x.ManagerID, x.OrgID });
                    table.ForeignKey(
                        name: "FK_Management_Members_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Management_Organization_OrgID",
                        column: x => x.OrgID,
                        principalTable: "Organization",
                        principalColumn: "OrgID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Management_OrgID",
                table: "Management",
                column: "OrgID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Members_MemberID",
                table: "Account",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Members_MemberID",
                table: "Account");

            migrationBuilder.DropTable(
                name: "Management");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "Organization",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "Members",
                newName: "IDMember");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "Account",
                newName: "IDMember");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Members_IDMember",
                table: "Account",
                column: "IDMember",
                principalTable: "Members",
                principalColumn: "IDMember",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
