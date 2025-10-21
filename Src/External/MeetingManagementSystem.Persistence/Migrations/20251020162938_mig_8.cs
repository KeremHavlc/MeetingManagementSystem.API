using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DecisionAssignments");

            migrationBuilder.AddColumn<int>(
                name: "DecisionStatus",
                table: "DecisionAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecisionStatus",
                table: "DecisionAssignments");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DecisionAssignments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
