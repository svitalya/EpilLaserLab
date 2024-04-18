using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdmin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$lXT5Lthl7bqUomEi8qI.Ku6ZiHWOT3X.xxqMWi9OuNHsAhtxEQibq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$RGalnQiIxBsjPnyoVha0cezIbwMvmrFbIDPhTHGORla.0n8WsbEKu");
        }
    }
}
