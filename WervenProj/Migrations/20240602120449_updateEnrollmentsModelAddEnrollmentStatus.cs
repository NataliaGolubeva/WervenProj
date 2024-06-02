using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WervenProj.Migrations
{
    /// <inheritdoc />
    public partial class updateEnrollmentsModelAddEnrollmentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "ConstractionSiteEnrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ConstractionSiteEnrollments");
        }
    }
}
