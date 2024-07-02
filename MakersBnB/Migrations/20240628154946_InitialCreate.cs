using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakersBnB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Spaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserSpaces",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SpaceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpaces", x => new { x.UserId, x.SpaceId });
                    table.ForeignKey(
                        name: "FK_UserSpaces_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSpaces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_UserId",
                table: "Spaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpaces_SpaceId",
                table: "UserSpaces",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Users_UserId",
                table: "Spaces",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Users_UserId",
                table: "Spaces");

            migrationBuilder.DropTable(
                name: "UserSpaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_UserId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Spaces");
        }
    }
}
