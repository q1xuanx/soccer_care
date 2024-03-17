using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleModel",
                columns: table => new
                {
                    IDRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModel", x => x.IDRole);
                });

            migrationBuilder.CreateTable(
                name: "TypeFields",
                columns: table => new
                {
                    IDType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeFields", x => x.IDType);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlock = table.Column<int>(type: "int", nullable: false),
                    IDRole = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                    table.ForeignKey(
                        name: "FK_User_RoleModel_IDRole",
                        column: x => x.IDRole,
                        principalTable: "RoleModel",
                        principalColumn: "IDRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootBallFields",
                columns: table => new
                {
                    IDFootBallField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    HinhAnhSanBong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IDType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootBallFields", x => x.IDFootBallField);
                    table.ForeignKey(
                        name: "FK_FootBallFields_TypeFields_IDType",
                        column: x => x.IDType,
                        principalTable: "TypeFields",
                        principalColumn: "IDType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FootBallFields_User_Username",
                        column: x => x.Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldLike",
                columns: table => new
                {
                    IDFieldLike = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IDFootballField = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldLike", x => x.IDFieldLike);
                    table.ForeignKey(
                        name: "FK_FieldLike_FootBallFields_IDFootballField",
                        column: x => x.IDFootballField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldLike_User_Username",
                        column: x => x.Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderField",
                columns: table => new
                {
                    IDOrder = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDFootballField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderField", x => x.IDOrder);
                    table.ForeignKey(
                        name: "FK_OrderField_FootBallFields_IDFootballField",
                        column: x => x.IDFootballField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderField_User_Username",
                        column: x => x.Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    IDDanhGia = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.IDDanhGia);
                    table.ForeignKey(
                        name: "FK_Ratings_FootBallFields_IDField",
                        column: x => x.IDField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_User_Username",
                        column: x => x.Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DetailsOrder",
                columns: table => new
                {
                    IDDetails = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDOrder = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsOrder", x => x.IDDetails);
                    table.ForeignKey(
                        name: "FK_DetailsOrder_OrderField_IDOrder",
                        column: x => x.IDOrder,
                        principalTable: "OrderField",
                        principalColumn: "IDOrder",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillSet",
                columns: table => new
                {
                    IDBill = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPay = table.Column<float>(type: "real", nullable: false),
                    TotalTimeUse = table.Column<float>(type: "real", nullable: false),
                    IDOrderDetails = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSet", x => x.IDBill);
                    table.ForeignKey(
                        name: "FK_BillSet_DetailsOrder_IDOrderDetails",
                        column: x => x.IDOrderDetails,
                        principalTable: "DetailsOrder",
                        principalColumn: "IDDetails",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryOrders",
                columns: table => new
                {
                    IDHistory = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IDBill = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOrders", x => x.IDHistory);
                    table.ForeignKey(
                        name: "FK_HistoryOrders_BillSet_IDBill",
                        column: x => x.IDBill,
                        principalTable: "BillSet",
                        principalColumn: "IDBill",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryOrders_User_Username",
                        column: x => x.Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillSet_IDOrderDetails",
                table: "BillSet",
                column: "IDOrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOrder_IDOrder",
                table: "DetailsOrder",
                column: "IDOrder");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLike_IDFootballField",
                table: "FieldLike",
                column: "IDFootballField");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLike_Username",
                table: "FieldLike",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_FootBallFields_IDType",
                table: "FootBallFields",
                column: "IDType");

            migrationBuilder.CreateIndex(
                name: "IX_FootBallFields_Username",
                table: "FootBallFields",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOrders_IDBill",
                table: "HistoryOrders",
                column: "IDBill");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOrders_Username",
                table: "HistoryOrders",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_OrderField_IDFootballField",
                table: "OrderField",
                column: "IDFootballField");

            migrationBuilder.CreateIndex(
                name: "IX_OrderField_Username",
                table: "OrderField",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_IDField",
                table: "Ratings",
                column: "IDField");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Username",
                table: "Ratings",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_User_IDRole",
                table: "User",
                column: "IDRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldLike");

            migrationBuilder.DropTable(
                name: "HistoryOrders");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "BillSet");

            migrationBuilder.DropTable(
                name: "DetailsOrder");

            migrationBuilder.DropTable(
                name: "OrderField");

            migrationBuilder.DropTable(
                name: "FootBallFields");

            migrationBuilder.DropTable(
                name: "TypeFields");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RoleModel");
        }
    }
}
