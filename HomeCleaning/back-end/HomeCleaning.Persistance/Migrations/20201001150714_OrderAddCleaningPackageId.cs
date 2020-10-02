using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class OrderAddCleaningPackageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CleaningExtraOptionId",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CleaningPackageId",
                table: "Order",
                column: "CleaningPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_CleaningPackage_CleaningPackageId",
                table: "Order",
                column: "CleaningPackageId",
                principalTable: "CleaningPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_CleaningPackage_CleaningPackageId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CleaningPackageId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CleaningExtraOptionId",
                table: "Order");
        }
    }
}
