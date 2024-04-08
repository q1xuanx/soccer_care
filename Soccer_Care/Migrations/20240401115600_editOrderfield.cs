using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class editOrderfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderField_AspNetUsers_IDOwner",
                table: "OrderField");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "OrderField");

            migrationBuilder.RenameColumn(
                name: "IDOwner",
                table: "OrderField",
                newName: "IDUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrderField_IDOwner",
                table: "OrderField",
                newName: "IX_OrderField_IDUser");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderField_AspNetUsers_IDUser",
                table: "OrderField",
                column: "IDUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderField_AspNetUsers_IDUser",
                table: "OrderField");

            migrationBuilder.RenameColumn(
                name: "IDUser",
                table: "OrderField",
                newName: "IDOwner");

            migrationBuilder.RenameIndex(
                name: "IX_OrderField_IDUser",
                table: "OrderField",
                newName: "IX_OrderField_IDOwner");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "OrderField",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderField_AspNetUsers_IDOwner",
                table: "OrderField",
                column: "IDOwner",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
