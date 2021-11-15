﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Staff_Service.Migrations.Production
{
    public partial class ProductionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "production");

            migrationBuilder.CreateTable(
                name: "production_db",
                schema: "production",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_db", x => x.StaffID);
                });

            migrationBuilder.InsertData(
                schema: "production",
                table: "production_db",
                columns: new[] { "StaffID", "StaffEmailAddress", "StaffFirstName", "StaffLastName" },
                values: new object[] { 1, "Jacob-Jardine@ThAmCo.co.uk", "Jacob", "Jardine" });

            migrationBuilder.InsertData(
                schema: "production",
                table: "production_db",
                columns: new[] { "StaffID", "StaffEmailAddress", "StaffFirstName", "StaffLastName" },
                values: new object[] { 2, "Ben-Souch@ThAmCo.co.uk", "Ben", "Souch" });

            migrationBuilder.InsertData(
                schema: "production",
                table: "production_db",
                columns: new[] { "StaffID", "StaffEmailAddress", "StaffFirstName", "StaffLastName" },
                values: new object[] { 3, "Joseph-Stavers@ThAmCo.co.uk", "Joseph", "Stavers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "production_db",
                schema: "production");
        }
    }
}
