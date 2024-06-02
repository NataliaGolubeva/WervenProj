using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WervenProj.Migrations
{
    /// <inheritdoc />
    public partial class addEnrollmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Werven");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.CreateTable(
                name: "ConstractionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusNr = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstractionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNr = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstractionSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstractionSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstractionSites_ConstractionStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ConstractionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstractionSiteEnrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ConstractionSiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstractionSiteEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstractionSiteEnrollments_ConstractionSites_ConstractionSiteId",
                        column: x => x.ConstractionSiteId,
                        principalTable: "ConstractionSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstractionSiteEnrollments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConstractionStatuses",
                columns: new[] { "Id", "StatusName", "StatusNr" },
                values: new object[,]
                {
                    { 1, "Aangemakt", 0 },
                    { 2, "Goedgekeurd", 1 },
                    { 3, "In Werking", 2 },
                    { 4, "Afgerond", 3 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "Id", "RoleName", "RoleNr" },
                values: new object[,]
                {
                    { 1, "Metselaar", 0 },
                    { 2, "Schrijnwerker", 1 },
                    { 3, "Administratie", 2 },
                    { 4, "Manager", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstractionSiteEnrollments_ConstractionSiteId",
                table: "ConstractionSiteEnrollments",
                column: "ConstractionSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstractionSiteEnrollments_EmployeeId",
                table: "ConstractionSiteEnrollments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstractionSites_StatusId",
                table: "ConstractionSites",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "EmployeeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ConstractionSiteEnrollments");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "ConstractionSites");

            migrationBuilder.DropTable(
                name: "ConstractionStatuses");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Werven",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Werven", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Werven_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleNr" },
                values: new object[,]
                {
                    { 1, "Metselaar", 0 },
                    { 2, "Schrijnwerker", 1 },
                    { 3, "Administratie", 2 },
                    { 4, "Manager", 3 }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName", "StatusNr" },
                values: new object[,]
                {
                    { 1, "Aangemakt", 0 },
                    { 2, "Goedgekeurd", 1 },
                    { 3, "In Werking", 2 },
                    { 4, "Afgerond", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Werven_StatusId",
                table: "Werven",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
