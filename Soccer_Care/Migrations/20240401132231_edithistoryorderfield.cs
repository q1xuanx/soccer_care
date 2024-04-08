using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class edithistoryorderfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOrders_FootBallFields_IDFootballField",
                table: "HistoryOrders");

            migrationBuilder.RenameColumn(
                name: "IDFootballField",
                table: "HistoryOrders",
                newName: "IDDetails");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOrders_IDFootballField",
                table: "HistoryOrders",
                newName: "IX_HistoryOrders_IDDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOrders_DetailsOrder_IDDetails",
                table: "HistoryOrders",
                column: "IDDetails",
                principalTable: "DetailsOrder",
                principalColumn: "IDDetails",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOrders_DetailsOrder_IDDetails",
                table: "HistoryOrders");

            migrationBuilder.RenameColumn(
                name: "IDDetails",
                table: "HistoryOrders",
                newName: "IDFootballField");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOrders_IDDetails",
                table: "HistoryOrders",
                newName: "IX_HistoryOrders_IDFootballField");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOrders_FootBallFields_IDFootballField",
                table: "HistoryOrders",
                column: "IDFootballField",
                principalTable: "FootBallFields",
                principalColumn: "IDFootBallField",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
