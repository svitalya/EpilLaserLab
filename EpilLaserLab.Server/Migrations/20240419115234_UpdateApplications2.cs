using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplications2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PurchasedSeasonTickets_PurchasedSeasonTicketId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "PurchasedSeasonTicketId",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrepaymentPercentage",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeClosed",
                table: "Applications",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$NG/vnlrP2DBM9W6PAFbx3.A/rEpkE/i.hjWsil89O6Fxf0sXO8ClS");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PurchasedSeasonTickets_PurchasedSeasonTicketId",
                table: "Applications",
                column: "PurchasedSeasonTicketId",
                principalTable: "PurchasedSeasonTickets",
                principalColumn: "PurchasedSeasonTicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PurchasedSeasonTickets_PurchasedSeasonTicketId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "PurchasedSeasonTicketId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrepaymentPercentage",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeClosed",
                table: "Applications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$MZJELQSS6ghla1FO7fVSiuNxbfjj//KrjFpShiUKkwcZqqPRQNMxu");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PurchasedSeasonTickets_PurchasedSeasonTicketId",
                table: "Applications",
                column: "PurchasedSeasonTicketId",
                principalTable: "PurchasedSeasonTickets",
                principalColumn: "PurchasedSeasonTicketId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
