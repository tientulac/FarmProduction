using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProductionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProductAttribute",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProductAttribute");
        }
    }
}
