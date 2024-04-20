using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class updatedetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DetailsOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DetailsOrder");
        }
    }
}
