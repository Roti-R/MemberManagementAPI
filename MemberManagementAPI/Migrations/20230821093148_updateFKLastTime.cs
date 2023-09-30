using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagementAPI.Migrations
{
    public partial class updateFKLastTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentOrganizationID",
                table: "Member",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Member_CurrentOrganizationID",
                table: "Member",
                column: "CurrentOrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Organization_CurrentOrganizationID",
                table: "Member",
                column: "CurrentOrganizationID",
                principalTable: "Organization",
                principalColumn: "OrgID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Organization_CurrentOrganizationID",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_CurrentOrganizationID",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "CurrentOrganizationID",
                table: "Member");
        }
    }
}
