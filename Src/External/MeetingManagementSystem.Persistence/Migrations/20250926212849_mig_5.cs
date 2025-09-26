using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipants_Roles_RoleId",
                table: "MeetingParticipants");

            migrationBuilder.AddColumn<Guid>(
                name: "AppRoleId",
                table: "MeetingParticipants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeetingRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipants_AppRoleId",
                table: "MeetingParticipants",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_MeetingRole_RoleId",
                table: "MeetingParticipants",
                column: "RoleId",
                principalTable: "MeetingRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_Roles_AppRoleId",
                table: "MeetingParticipants",
                column: "AppRoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipants_MeetingRole_RoleId",
                table: "MeetingParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipants_Roles_AppRoleId",
                table: "MeetingParticipants");

            migrationBuilder.DropTable(
                name: "MeetingRole");

            migrationBuilder.DropIndex(
                name: "IX_MeetingParticipants_AppRoleId",
                table: "MeetingParticipants");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "MeetingParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_Roles_RoleId",
                table: "MeetingParticipants",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
