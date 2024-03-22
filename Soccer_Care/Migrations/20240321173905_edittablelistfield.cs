using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class edittablelistfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListFieldModel_FootBallFields_IDFootballField",
                table: "ListFieldModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ListFieldModel_TypeFields_IDType",
                table: "ListFieldModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListFieldModel",
                table: "ListFieldModel");

            migrationBuilder.RenameTable(
                name: "ListFieldModel",
                newName: "listFields");

            migrationBuilder.RenameIndex(
                name: "IX_ListFieldModel_IDType",
                table: "listFields",
                newName: "IX_listFields_IDType");

            migrationBuilder.RenameIndex(
                name: "IX_ListFieldModel_IDFootballField",
                table: "listFields",
                newName: "IX_listFields_IDFootballField");

            migrationBuilder.AddColumn<float>(
                name: "Gia",
                table: "listFields",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_listFields",
                table: "listFields",
                column: "IDField");

            migrationBuilder.AddForeignKey(
                name: "FK_listFields_FootBallFields_IDFootballField",
                table: "listFields",
                column: "IDFootballField",
                principalTable: "FootBallFields",
                principalColumn: "IDFootBallField",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_listFields_TypeFields_IDType",
                table: "listFields",
                column: "IDType",
                principalTable: "TypeFields",
                principalColumn: "IDType",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_listFields_FootBallFields_IDFootballField",
                table: "listFields");

            migrationBuilder.DropForeignKey(
                name: "FK_listFields_TypeFields_IDType",
                table: "listFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_listFields",
                table: "listFields");

            migrationBuilder.DropColumn(
                name: "Gia",
                table: "listFields");

            migrationBuilder.RenameTable(
                name: "listFields",
                newName: "ListFieldModel");

            migrationBuilder.RenameIndex(
                name: "IX_listFields_IDType",
                table: "ListFieldModel",
                newName: "IX_ListFieldModel_IDType");

            migrationBuilder.RenameIndex(
                name: "IX_listFields_IDFootballField",
                table: "ListFieldModel",
                newName: "IX_ListFieldModel_IDFootballField");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListFieldModel",
                table: "ListFieldModel",
                column: "IDField");

            migrationBuilder.AddForeignKey(
                name: "FK_ListFieldModel_FootBallFields_IDFootballField",
                table: "ListFieldModel",
                column: "IDFootballField",
                principalTable: "FootBallFields",
                principalColumn: "IDFootBallField",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListFieldModel_TypeFields_IDType",
                table: "ListFieldModel",
                column: "IDType",
                principalTable: "TypeFields",
                principalColumn: "IDType",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
