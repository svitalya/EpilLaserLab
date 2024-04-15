using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeasonTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeasonTickets",
                columns: table => new
                {
                    SeasonTicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Sessions = table.Column<int>(type: "int", nullable: false),
                    ValidityPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonTickets", x => x.SeasonTicketId);
                    table.ForeignKey(
                        name: "FK_SeasonTickets_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTickets_ServiceId",
                table: "SeasonTickets",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeasonTickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$wnqePrHMc.la9b3dRHH3GugqBSC1RJjezL1F8dwePBhdWTBkWlYs2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$p48CfxZGXwOW253f.OTk2eXtZ9tITssXUTNsCzdfd89ydrfVJlvNG");
        }
    }
}
