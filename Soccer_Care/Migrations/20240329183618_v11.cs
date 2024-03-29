using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOrders_BillSet_IDBill",
                table: "HistoryOrders");

            migrationBuilder.RenameColumn(
                name: "IDBill",
                table: "HistoryOrders",
                newName: "IDFootballField");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOrders_IDBill",
                table: "HistoryOrders",
                newName: "IX_HistoryOrders_IDFootballField");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "DetailsOrder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOrders_FootBallFields_IDFootballField",
                table: "HistoryOrders",
                column: "IDFootballField",
                principalTable: "FootBallFields",
                principalColumn: "IDFootBallField",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOrders_FootBallFields_IDFootballField",
                table: "HistoryOrders");

            migrationBuilder.RenameColumn(
                name: "IDFootballField",
                table: "HistoryOrders",
                newName: "IDBill");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOrders_IDFootballField",
                table: "HistoryOrders",
                newName: "IX_HistoryOrders_IDBill");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "DetailsOrder",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOrders_BillSet_IDBill",
                table: "HistoryOrders",
                column: "IDBill",
                principalTable: "BillSet",
                principalColumn: "IDBill",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
