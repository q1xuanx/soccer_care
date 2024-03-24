using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class addtablerole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_RoleModel_IDRole",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleModel",
                table: "RoleModel");

            migrationBuilder.RenameTable(
                name: "RoleModel",
                newName: "Role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "IDRole");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_IDRole",
                table: "User",
                column: "IDRole",
                principalTable: "Role",
                principalColumn: "IDRole",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_IDRole",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "RoleModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleModel",
                table: "RoleModel",
                column: "IDRole");

            migrationBuilder.AddForeignKey(
                name: "FK_User_RoleModel_IDRole",
                table: "User",
                column: "IDRole",
                principalTable: "RoleModel",
                principalColumn: "IDRole",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
