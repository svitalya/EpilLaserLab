using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "Name",
                value: "root");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "Name",
                value: "admin");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { 3, "client" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$U4M8uuzgXun3lddN/kYD..HWw69IOlC5CxaikyGntVU.MbwYhkfVe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$RSkL9E6q2DLZqM91aOXhGuPrfxp4TvCdoxmzCDIdDbpal4I1EG7vm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "Name",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "Name",
                value: "user");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$xSKKzX7PuUkXGsVZ944NXehnbdzAZyQs0luuQe4ntCt7M5UV34lQu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$SDpy7Gr9AtO8BIiEOwyvpup8gHBiZbC4gyCSvW3qRJqcSkJwdDbtm");
        }
    }
}
