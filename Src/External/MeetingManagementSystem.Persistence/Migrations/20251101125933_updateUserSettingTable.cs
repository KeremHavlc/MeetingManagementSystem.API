using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateUserSettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveChatNotifications",
                table: "UserSettings",
                newName: "ReceiveMeetingJoinNotifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveMeetingJoinNotifications",
                table: "UserSettings",
                newName: "ReceiveChatNotifications");
        }
    }
}
