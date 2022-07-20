using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Indexes.Migrations
{
    public partial class checkConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "PriceDiscountPriceCheck",
                table: "Products",
                sql: "[Price]>[DiscountPrice]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "PriceDiscountPriceCheck",
                table: "Products");
        }
    }
}
