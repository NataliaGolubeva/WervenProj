using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WervenProj.Migrations
{
    /// <inheritdoc />
    public partial class updateSites2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstractionSites_ConstractionStatuses_StatusId",
                table: "ConstractionSites");

            migrationBuilder.DropIndex(
                name: "IX_ConstractionSites_StatusId",
                table: "ConstractionSites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ConstractionSites_StatusId",
                table: "ConstractionSites",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstractionSites_ConstractionStatuses_StatusId",
                table: "ConstractionSites",
                column: "StatusId",
                principalTable: "ConstractionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
