using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceBot.Db.Migrations
{
    public partial class OrdersToBeExecutedCalculatedAmountfieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CalculatedAmount",
                table: "OrdersToBeExecuteds",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatedAmount",
                table: "OrdersToBeExecuteds");
        }
    }
}
