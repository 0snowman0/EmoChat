using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analys.api.Migrations
{
    /// <inheritdoc />
    public partial class change_unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_userEmojiUsage_UserId_Emoji",
                table: "userEmojiUsage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_userEmojiUsage_UserId_Emoji",
                table: "userEmojiUsage",
                columns: new[] { "UserId", "Emoji" },
                unique: true);
        }
    }
}
