using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soccer_Care.Migrations
{
    public partial class InitWebDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBlock = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    IDRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IDRole);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootBallFields",
                columns: table => new
                {
                    IDFootBallField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDUserOwner = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootBallFields", x => x.IDFootBallField);
                    table.ForeignKey(
                        name: "FK_FootBallFields_AspNetUsers_IDUserOwner",
                        column: x => x.IDUserOwner,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldLike",
                columns: table => new
                {
                    IDFieldLike = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDFootballField = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldLike", x => x.IDFieldLike);
                    table.ForeignKey(
                        name: "FK_FieldLike_AspNetUsers_Username",
                        column: x => x.Username,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FieldLike_FootBallFields_IDFootballField",
                        column: x => x.IDFootballField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "listFields",
                columns: table => new
                {
                    IDField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnhSanBong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    IDFootballField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listFields", x => x.IDField);
                    table.ForeignKey(
                        name: "FK_listFields_FootBallFields_IDFootballField",
                        column: x => x.IDFootballField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_listFields_TypeFields_IDType",
                        column: x => x.IDType,
                        principalTable: "TypeFields",
                        principalColumn: "IDType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    IDDanhGia = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.IDDanhGia);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_Username",
                        column: x => x.Username,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Ratings_FootBallFields_IDField",
                        column: x => x.IDField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderField",
                columns: table => new
                {
                    IDOrder = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDFootballField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDChildField = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderField", x => x.IDOrder);
                    table.ForeignKey(
                        name: "FK_OrderField_AspNetUsers_IDUser",
                        column: x => x.IDUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderField_FootBallFields_IDFootballField",
                        column: x => x.IDFootballField,
                        principalTable: "FootBallFields",
                        principalColumn: "IDFootBallField",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderField_listFields_IDChildField",
                        column: x => x.IDChildField,
                        principalTable: "listFields",
                        principalColumn: "IDField",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DetailsOrder",
                columns: table => new
                {
                    IDDetails = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isThanhToan = table.Column<int>(type: "int", nullable: false),
                    IDOrder = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HistoryOrders",
                columns: table => new
                {
                    IDHistory = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDDetails = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOrders", x => x.IDHistory);
                    table.ForeignKey(
                        name: "FK_HistoryOrders_AspNetUsers_IDUser",
                        column: x => x.IDUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HistoryOrders_DetailsOrder_IDDetails",
                        column: x => x.IDDetails,
                        principalTable: "DetailsOrder",
                        principalColumn: "IDDetails",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_FootBallFields_IDUserOwner",
                table: "FootBallFields",
                column: "IDUserOwner");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOrders_IDDetails",
                table: "HistoryOrders",
                column: "IDDetails");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOrders_IDUser",
                table: "HistoryOrders",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_listFields_IDFootballField",
                table: "listFields",
                column: "IDFootballField");

            migrationBuilder.CreateIndex(
                name: "IX_listFields_IDType",
                table: "listFields",
                column: "IDType");

            migrationBuilder.CreateIndex(
                name: "IX_OrderField_IDChildField",
                table: "OrderField",
                column: "IDChildField");

            migrationBuilder.CreateIndex(
                name: "IX_OrderField_IDFootballField",
                table: "OrderField",
                column: "IDFootballField");

            migrationBuilder.CreateIndex(
                name: "IX_OrderField_IDUser",
                table: "OrderField",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_IDField",
                table: "Ratings",
                column: "IDField");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Username",
                table: "Ratings",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BillSet");

            migrationBuilder.DropTable(
                name: "FieldLike");

            migrationBuilder.DropTable(
                name: "HistoryOrders");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DetailsOrder");

            migrationBuilder.DropTable(
                name: "OrderField");

            migrationBuilder.DropTable(
                name: "listFields");

            migrationBuilder.DropTable(
                name: "FootBallFields");

            migrationBuilder.DropTable(
                name: "TypeFields");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
