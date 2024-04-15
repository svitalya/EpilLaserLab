using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeasonTicketsPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeasonTicketsPrice",
                columns: table => new
                {
                    SeasonTicketPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SeasonTicketId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonTicketsPrice", x => x.SeasonTicketPriceId);
                    table.ForeignKey(
                        name: "FK_SeasonTicketsPrice_SeasonTickets_SeasonTicketId",
                        column: x => x.SeasonTicketId,
                        principalTable: "SeasonTickets",
                        principalColumn: "SeasonTicketId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$QSN8ew7ZfJ5fdJhae8s53.74.BDnU4/8YiWsOpW5ctJbcYvCaicRu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$/AM1YvJwLDWzJtpbbzdWveEMrHehAkZLI2/MFEhsRND.e68OKlHO2");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTicketsPrice_SeasonTicketId",
                table: "SeasonTicketsPrice",
                column: "SeasonTicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeasonTicketsPrice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$CWU.I/jndl7YWQNQcZRFKOlvkUogqJFLRHKSs17LbFcoWQ7ZigX6S");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$MpAKwe5kJ07AjVQKnpOkZORnGlGz4cu3OAfkLPMPVyJ2iPxUpEPV.");
        }
    }
}
