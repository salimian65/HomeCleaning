using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Cellphone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ClientUserId = table.Column<string>(nullable: true),
                    SpaceSizeId = table.Column<int>(nullable: false),
                    CleaningPackageId = table.Column<int>(nullable: false),
                    CleaningExtraOptionId = table.Column<int>(nullable: false),
                    Address_AddressStr = table.Column<string>(nullable: true),
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
                        name: "FK_Order_CleaningExtraOption_CleaningExtraOptionId",
                        column: x => x.CleaningExtraOptionId,
                        principalTable: "CleaningExtraOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_CleaningPackage_CleaningPackageId",
                        column: x => x.CleaningPackageId,
                        principalTable: "CleaningPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_SpaceSize_SpaceSizeId",
                        column: x => x.SpaceSizeId,
                        principalTable: "SpaceSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningExtraOption_CleaningCategoryId",
                table: "CleaningExtraOption",
                column: "CleaningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningPackage_CleaningCategoryId",
                table: "CleaningPackage",
                column: "CleaningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CleaningExtraOptionId",
                table: "Order",
                column: "CleaningExtraOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CleaningPackageId",
                table: "Order",
                column: "CleaningPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientUserId",
                table: "Order",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SpaceSizeId",
                table: "Order",
                column: "SpaceSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerRequest_OrderId",
                table: "ServerRequest",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerRequest_ServerUserId",
                table: "ServerRequest",
                column: "ServerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceSize_CleaningCategoryId",
                table: "SpaceSize",
                column: "CleaningCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ServerRequest");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "CleaningExtraOption");

            migrationBuilder.DropTable(
                name: "CleaningPackage");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SpaceSize");

            migrationBuilder.DropTable(
                name: "CleaningCategory");
        }
    }
}
