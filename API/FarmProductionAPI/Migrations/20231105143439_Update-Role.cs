using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProductionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Role",
                newName: "IsAdmin");


            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Role",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "Role",
                newName: "Active");
        }
    }
}
