using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WervenProj.Migrations
{
    /// <inheritdoc />
    public partial class AddFkStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Werven",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Werven_StatusId",
                table: "Werven",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Werven_Statuses_StatusId",
                table: "Werven",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Werven_Statuses_StatusId",
                table: "Werven");

            migrationBuilder.DropIndex(
                name: "IX_Werven_StatusId",
                table: "Werven");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Werven");
        }
    }
}
