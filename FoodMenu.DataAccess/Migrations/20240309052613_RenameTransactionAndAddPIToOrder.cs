using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodMenu.DataAccess.Migrations
{
    public partial class RenameTransactionAndAddPIToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "OrderHeader");

            migrationBuilder.AlterColumn<string>(
                name: "PickupName",
                table: "OrderHeader",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeader",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentID",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionID",
                table: "OrderHeader",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentID",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "OrderHeader");

            migrationBuilder.AlterColumn<string>(
                name: "PickupName",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "TransactionID",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
