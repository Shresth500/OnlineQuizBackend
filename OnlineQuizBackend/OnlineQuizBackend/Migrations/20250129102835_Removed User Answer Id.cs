using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuizBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserAnswerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAnswerId",
                table: "UserAnswer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAnswerId",
                table: "UserAnswer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
