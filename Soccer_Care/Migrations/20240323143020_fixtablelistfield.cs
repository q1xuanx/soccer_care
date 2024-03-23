using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class fixtablelistfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeFields_listFields_ListFieldModelIDField",
                table: "TypeFields");

            migrationBuilder.DropIndex(
                name: "IX_TypeFields_ListFieldModelIDField",
                table: "TypeFields");

            migrationBuilder.DropColumn(
                name: "ListFieldModelIDField",
                table: "TypeFields");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListFieldModelIDField",
                table: "TypeFields",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeFields_ListFieldModelIDField",
                table: "TypeFields",
                column: "ListFieldModelIDField");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeFields_listFields_ListFieldModelIDField",
                table: "TypeFields",
                column: "ListFieldModelIDField",
                principalTable: "listFields",
                principalColumn: "IDField");
        }
    }
}
