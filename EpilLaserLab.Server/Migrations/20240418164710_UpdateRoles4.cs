using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Roles",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "Title",
                value: "Главный");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "Title",
                value: "Администратор");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "Title",
                value: "Клиент");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$RBUy8irkIh.mbEwkiz4JNezFxKOO/P7uuVhaMk.9lWoUN7zzsvm.2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$r8s1E7sLsXbBIuFFU2XHFeR5zChRtRXBFelClYumlw6q0P.Rrhwau");
        }
    }
}
