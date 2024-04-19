using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreatePurchasedSeasonTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchasedSeasonTickets",
                columns: table => new
                {
                    PurchasedSeasonTicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SeasonTicketPriceId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Remains = table.Column<int>(type: "int", nullable: false),
                    DateOfPurchased = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateOfCombustion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedSeasonTickets", x => x.PurchasedSeasonTicketId);
                    table.ForeignKey(
                        name: "FK_PurchasedSeasonTickets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedSeasonTickets_SeasonTicketsPrice_SeasonTicketPriceId",
                        column: x => x.SeasonTicketPriceId,
                        principalTable: "SeasonTicketsPrice",
                        principalColumn: "SeasonTicketPriceId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IntervalId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ServicePriceId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PurchasedSeasonTicketId = table.Column<int>(type: "int", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateTimeClosed = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Intervals_IntervalId",
                        column: x => x.IntervalId,
                        principalTable: "Intervals",
                        principalColumn: "IntervalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_PurchasedSeasonTickets_PurchasedSeasonTicketId",
                        column: x => x.PurchasedSeasonTicketId,
                        principalTable: "PurchasedSeasonTickets",
                        principalColumn: "PurchasedSeasonTicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_ServicePrices_ServicePriceId",
                        column: x => x.ServicePriceId,
                        principalTable: "ServicePrices",
                        principalColumn: "ServicePriceId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$tecGI8IUsgJ//zo41/Ax0uRQusuMoKHeYsm6Uvgj/ScJCgkirNKmS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$iWyOWH5RTYzMo40cT5BoNeC6maqHKp.j9oBNwg4y8FttE5rm8mU4m");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ClientId",
                table: "Applications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_IntervalId",
                table: "Applications",
                column: "IntervalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PurchasedSeasonTicketId",
                table: "Applications",
                column: "PurchasedSeasonTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ServicePriceId",
                table: "Applications",
                column: "ServicePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSeasonTickets_ClientId",
                table: "PurchasedSeasonTickets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSeasonTickets_SeasonTicketPriceId",
                table: "PurchasedSeasonTickets",
                column: "SeasonTicketPriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "PurchasedSeasonTickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5T5wJZt8TJMGFFAuyvJxgeaANiGkR.e7k.AxKcjardIsdhUYW5sCm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$v8aiaOCTQLHkmlS03Ct.JO3moSmNYlo5XG1MgQOCstQrAgI/Ps5g6");
        }
    }
}
