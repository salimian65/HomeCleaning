using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class AddforeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ClientUserId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientUserId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ClientUserId1",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "ClientUserId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientUserId",
                table: "Order",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ClientUserId",
                table: "Order",
                column: "ClientUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ClientUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientUserId",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientUserId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientUserId1",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientUserId1",
                table: "Order",
                column: "ClientUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ClientUserId1",
                table: "Order",
                column: "ClientUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
