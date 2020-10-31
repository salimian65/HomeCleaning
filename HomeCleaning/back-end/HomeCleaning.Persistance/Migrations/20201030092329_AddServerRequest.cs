using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class AddServerRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerUserId = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    ServerRequestStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerRequest_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerRequest_AspNetUsers_ServerUserId",
                        column: x => x.ServerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerRequest_OrderId",
                table: "ServerRequest",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerRequest_ServerUserId",
                table: "ServerRequest",
                column: "ServerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerRequest");
        }
    }
}
