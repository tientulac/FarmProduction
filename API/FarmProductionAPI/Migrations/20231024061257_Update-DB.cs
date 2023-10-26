using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmProductionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order_UserAccount_UserAccountId",
            //    table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Order_UserAccountId",
            //    table: "Orders",
            //    newName: "IX_Orders_UserAccountId");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusUser",
                table: "UserAccount",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProducerId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerAccountId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId1",
            //    table: "Orders",
            //    type: "uniqueidentifier",
            //    nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeModel = table.Column<int>(type: "int", nullable: true),
                    MainMarketing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearBorn = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSoftDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProducerId",
                table: "Product",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SellerAccountId",
                table: "Orders",
                column: "SellerAccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders_UserAccountId1",
            //    table: "Orders",
            //    column: "UserAccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAccount_SellerAccountId",
                table: "Orders",
                column: "SellerAccountId",
                principalTable: "UserAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAccount_UserAccountId",
                table: "Orders",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_UserAccount_UserAccountId1",
            //    table: "Orders",
            //    column: "UserAccountId1",
            //    principalTable: "UserAccount",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Producer_ProducerId",
                table: "Product",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAccount_SellerAccountId",
                table: "Orders");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_UserAccount_UserAccountId",
            //    table: "Orders");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_UserAccount_UserAccountId1",
            //    table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Producer_ProducerId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProducerId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SellerAccountId",
                table: "Orders");

            //migrationBuilder.DropIndex(
            //    name: "IX_Orders_UserAccountId1",
            //    table: "Orders");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "StatusUser",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SellerAccountId",
                table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "UserAccountId1",
            //    table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Orders_UserAccountId",
            //    table: "Order",
            //    newName: "IX_Order_UserAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order_UserAccount_UserAccountId",
            //    table: "Order",
            //    column: "UserAccountId",
            //    principalTable: "UserAccount",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
