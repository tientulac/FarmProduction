using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProductionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProductAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "ProductAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ManufactureDate",
                table: "ProductAttribute",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "ProductAttribute");

            migrationBuilder.DropColumn(
                name: "ManufactureDate",
                table: "ProductAttribute");
        }
    }
}
