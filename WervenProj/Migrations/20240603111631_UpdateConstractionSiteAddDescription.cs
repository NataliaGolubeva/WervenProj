using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WervenProj.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConstractionSiteAddDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ConstractionSites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ConstractionSites");
        }
    }
}
