using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CleaningCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CleaningExtraOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CleaningCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningExtraOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningExtraOption_CleaningCategory_CleaningCategoryId",
                        column: x => x.CleaningCategoryId,
                        principalTable: "CleaningCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CleaningPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CleaningCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningPackage_CleaningCategory_CleaningCategoryId",
                        column: x => x.CleaningCategoryId,
                        principalTable: "CleaningCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SpaceSize",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CleaningCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceSize_CleaningCategory_CleaningCategoryId",
                        column: x => x.CleaningCategoryId,
                        principalTable: "CleaningCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_FirstName = table.Column<string>(nullable: true),
                    Customer_LastName = table.Column<string>(nullable: true),
                    SpaceSizeId = table.Column<int>(nullable: false),
                    CleaningPackageId = table.Column<int>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_Alley = table.Column<string>(nullable: true),
                    Address_Floor = table.Column<string>(nullable: true),
                    Address_Block = table.Column<string>(nullable: true),
                    ScheduledTime = table.Column<DateTime>(nullable: false),
                    RegisterTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_SpaceSize_SpaceSizeId",
                        column: x => x.SpaceSizeId,
                        principalTable: "SpaceSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "CleaningCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Home Cleaning" },
                    { 2, null, "Empty Home Cleaning" },
                    { 3, null, "Cleaning after construction" },
                    { 4, null, "Villa Cleaning" },
                    { 5, null, "Office Cleaning" }
                });

            migrationBuilder.InsertData(
                table: "CleaningExtraOption",
                columns: new[] { "Id", "CleaningCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, null, "ironing" },
                    { 7, 4, null, "wardrobe cleaning" },
                    { 5, 4, null, "ironing" },
                    { 4, 5, null, "interior glass cleaning" },
                    { 3, 1, null, "wardrobe cleaning" },
                    { 2, 1, null, "refrigerator cleaning" },
                    { 6, 4, null, "refrigerator cleaning" }
                });

            migrationBuilder.InsertData(
                table: "CleaningPackage",
                columns: new[] { "Id", "CleaningCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 2, 1, null, "Second Cleaning Package" },
                    { 3, 1, null, "Third Cleaning Package" },
                    { 6, 4, null, "Third Cleaning Package" },
                    { 5, 4, null, "Second Cleaning Package" },
                    { 4, 4, null, "First Cleaning Package" },
                    { 1, 1, null, "First Cleaning Package" }
                });

            migrationBuilder.InsertData(
                table: "SpaceSize",
                columns: new[] { "Id", "CleaningCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 15, 5, null, "150-200 m2" },
                    { 14, 5, null, "100-150 m2" },
                    { 13, 5, null, "100 m2 and below" },
                    { 21, 4, null, "425 m2 above" },
                    { 20, 4, null, "350-425 m2" },
                    { 19, 4, null, "275-350 m2" },
                    { 18, 4, null, "200-275 m2" },
                    { 11, 3, null, "3 + 1" },
                    { 16, 5, null, "200-250 m2" },
                    { 10, 3, null, "2 + 1" },
                    { 9, 3, null, "1 + 1" },
                    { 8, 2, null, "4 + 1" },
                    { 7, 2, null, "3 + 1" },
                    { 6, 2, null, "2 + 1" },
                    { 5, 2, null, "1 + 1" },
                    { 4, 1, null, "4 + 1" },
                    { 3, 1, null, "3 + 1" },
                    { 2, 1, null, "2 + 1" },
                    { 1, 1, null, "1 + 1" },
                    { 12, 3, null, "4 + 1" },
                    { 17, 5, null, "250 m2 above" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningExtraOption_CleaningCategoryId",
                table: "CleaningExtraOption",
                column: "CleaningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningPackage_CleaningCategoryId",
                table: "CleaningPackage",
                column: "CleaningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SpaceSizeId",
                table: "Order",
                column: "SpaceSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceSize_CleaningCategoryId",
                table: "SpaceSize",
                column: "CleaningCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningExtraOption");

            migrationBuilder.DropTable(
                name: "CleaningPackage");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SpaceSize");

            migrationBuilder.DropTable(
                name: "CleaningCategory");
        }
    }
}
