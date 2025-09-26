using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipants_Roles_AppRoleId",
                table: "MeetingParticipants");

            migrationBuilder.DropIndex(
                name: "IX_MeetingParticipants_AppRoleId",
                table: "MeetingParticipants");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "MeetingParticipants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppRoleId",
                table: "MeetingParticipants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipants_AppRoleId",
                table: "MeetingParticipants",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_Roles_AppRoleId",
                table: "MeetingParticipants",
                column: "AppRoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
