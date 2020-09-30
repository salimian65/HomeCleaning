using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCleaning.Persistance.Migrations
{
    public partial class AddCleaningCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceSize_CleaningCategory_CleaningCategoryId",
                table: "SpaceSize");

            migrationBuilder.AlterColumn<int>(
                name: "CleaningCategoryId",
                table: "SpaceSize",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CleaningCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Home Cleaning" },
                    { 2, "Empty Home Cleaning" },
                    { 3, "Cleaning after construction" },
                    { 4, "Villa Cleaning" },
                    { 5, "Office Cleaning" }
                });

            migrationBuilder.InsertData(
                table: "SpaceSize",
                columns: new[] { "Id", "CleaningCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "1 + 1" },
                    { 2, 1, "2 + 1" },
                    { 3, 1, "3 + 1" },
                    { 4, 1, "4 + 1" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceSize_CleaningCategory_CleaningCategoryId",
                table: "SpaceSize",
                column: "CleaningCategoryId",
                principalTable: "CleaningCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceSize_CleaningCategory_CleaningCategoryId",
                table: "SpaceSize");

            migrationBuilder.DeleteData(
                table: "CleaningCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CleaningCategory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CleaningCategory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CleaningCategory",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SpaceSize",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SpaceSize",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SpaceSize",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SpaceSize",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CleaningCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "CleaningCategoryId",
                table: "SpaceSize",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceSize_CleaningCategory_CleaningCategoryId",
                table: "SpaceSize",
                column: "CleaningCategoryId",
                principalTable: "CleaningCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
