using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_FirstName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Customer_LastName",
                table: "Order");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true),
                    Cellphone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Customer_FirstName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_LastName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
