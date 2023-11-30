using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.PracticeTask_1.Migrations
{
    public partial class isNewisFeaturedisBestsellerColumnsAddedtoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBestseller",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isFeatured",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isNew",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBestseller",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "isFeatured",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "isNew",
                table: "Books");
        }
    }
}
