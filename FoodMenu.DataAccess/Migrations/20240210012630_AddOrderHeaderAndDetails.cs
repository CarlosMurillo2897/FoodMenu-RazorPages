using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodMenu.DataAccess.Migrations
{
    public partial class AddOrderHeaderAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "ShoppingCart",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderTotal = table.Column<double>(nullable: false),
                    PickupTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    TransactionID = table.Column<string>(nullable: true),
                    PickupName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderHeader_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(nullable: false),
                    MenuItemID = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_MenuItem_MenuItemID",
                        column: x => x.MenuItemID,
                        principalTable: "MenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeader_OrderID",
                        column: x => x.OrderID,
                        principalTable: "OrderHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ApplicationUserID",
                table: "ShoppingCart",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MenuItemID",
                table: "OrderDetails",
                column: "MenuItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_UserID",
                table: "OrderHeader",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_AspNetUsers_ApplicationUserID",
                table: "ShoppingCart",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_AspNetUsers_ApplicationUserID",
                table: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ApplicationUserID",
                table: "ShoppingCart");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "ShoppingCart",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
