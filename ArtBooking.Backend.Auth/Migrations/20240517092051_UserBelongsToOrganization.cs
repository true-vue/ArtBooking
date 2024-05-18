using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtBooking.Auth.Migrations
{
    /// <inheritdoc />
    public partial class UserBelongsToOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BelongsToOrganizationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BelongsToOrganizationId",
                table: "Users",
                column: "BelongsToOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_BelongsToOrganizationId",
                table: "Users",
                column: "BelongsToOrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_BelongsToOrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Users_BelongsToOrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BelongsToOrganizationId",
                table: "Users");
        }
    }
}
