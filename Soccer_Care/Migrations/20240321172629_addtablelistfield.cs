using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class addtablelistfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FootBallFields_TypeFields_IDType",
                table: "FootBallFields");

            migrationBuilder.DropIndex(
                name: "IX_FootBallFields_IDType",
                table: "FootBallFields");

            migrationBuilder.DropColumn(
                name: "HinhAnhSanBong",
                table: "FootBallFields");

            migrationBuilder.DropColumn(
                name: "IDType",
                table: "FootBallFields");

            migrationBuilder.CreateTable(
                name: "ListFieldModel",
                columns: table => new
                {
                    IDField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnhSanBong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDFootballField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListFieldModel", x => x.IDField);
                    table.ForeignKey(
                        name: "FK_ListFieldModel_FootBallFields_IDFootballField",
                        column: x => x.IDFootballField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListFieldModel_TypeFields_IDType",
                        column: x => x.IDType,
                        principalTable: "TypeFields",
                        principalColumn: "IDType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListFieldModel_IDFootballField",
                table: "ListFieldModel",
                column: "IDFootballField");

            migrationBuilder.CreateIndex(
                name: "IX_ListFieldModel_IDType",
                table: "ListFieldModel",
                column: "IDType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListFieldModel");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnhSanBong",
                table: "FootBallFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IDType",
                table: "FootBallFields",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FootBallFields_IDType",
                table: "FootBallFields",
                column: "IDType");

            migrationBuilder.AddForeignKey(
                name: "FK_FootBallFields_TypeFields_IDType",
                table: "FootBallFields",
                column: "IDType",
                principalTable: "TypeFields",
                principalColumn: "IDType",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
