using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PostalZipService.Migrations
{
    public partial class UserDistrictRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistictOfRepresentationRole",
                table: "AspNetUsers",
                newName: "DistrictRole");

            migrationBuilder.RenameColumn(
                name: "DistictClaim",
                table: "AspNetUsers",
                newName: "DistrictClaim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistrictRole",
                table: "AspNetUsers",
                newName: "DistictOfRepresentationRole");

            migrationBuilder.RenameColumn(
                name: "DistrictClaim",
                table: "AspNetUsers",
                newName: "DistictClaim");
        }
    }
}
