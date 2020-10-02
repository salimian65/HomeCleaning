using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class OrderAddCleaningExtraOptionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_CleaningExtraOptionId",
                table: "Order",
                column: "CleaningExtraOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_CleaningExtraOption_CleaningExtraOptionId",
                table: "Order",
                column: "CleaningExtraOptionId",
                principalTable: "CleaningExtraOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_CleaningExtraOption_CleaningExtraOptionId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CleaningExtraOptionId",
                table: "Order");
        }
    }
}
