using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuizBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAttemptId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttemptId",
                table: "UserQuizAttendee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttemptId",
                table: "UserQuizAttendee",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
