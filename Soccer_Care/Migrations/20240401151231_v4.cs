using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isThanhToan",
                table: "DetailsOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isThanhToan",
                table: "DetailsOrder");
        }
    }
}
