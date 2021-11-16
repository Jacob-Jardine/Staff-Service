﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Staff_Service.Migrations
{
    public partial class AddedMoreStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "staff",
                table: "_staff",
                columns: new[] { "StaffID", "StaffEmailAddress", "StaffFirstName", "StaffLastName" },
                values: new object[] { 2, "Ben-Souch@ThAmCo.co.uk", "Ben", "Souch" });

            migrationBuilder.InsertData(
                schema: "staff",
                table: "_staff",
                columns: new[] { "StaffID", "StaffEmailAddress", "StaffFirstName", "StaffLastName" },
                values: new object[] { 3, "Joseph-Stavers@ThAmCo.co.uk", "Joseph", "Stavers" });

            migrationBuilder.InsertData(
                schema: "staff",
                table: "_staff",
                columns: new[] { "StaffID", "StaffEmailAddress", "StaffFirstName", "StaffLastName" },
                values: new object[] { 4, "Teddy-Teasdale@ThAmCo.co.uk", "Teddy", "Teasdale" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "staff",
                table: "_staff",
                keyColumn: "StaffID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "staff",
                table: "_staff",
                keyColumn: "StaffID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "staff",
                table: "_staff",
                keyColumn: "StaffID",
                keyValue: 4);
        }
    }
}