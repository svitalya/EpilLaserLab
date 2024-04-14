using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                columns: new[] { "LevelAccess", "Name" },
                values: new object[] { 0u, "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "LevelAccess", "Name" },
                values: new object[] { 2, 0u, "user" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$.yq54P4LuavFwjwA6l4HQ.lgU8lMLhEEjOlIXSeoA/IharO9Zq4yG");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Login", "PasswordHash", "RoleId" },
                values: new object[] { 2, null, "User", "$2a$11$J7bSLeGi9STb0GVLXqeL8eoW/J/DB3HQg0BFGEtdhRw5dNth5d19S", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                columns: new[] { "LevelAccess", "Name" },
                values: new object[] { 4294967295u, "Админ" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$UCG3fqgh.V.eacIQnt86m.hqqi9EZxT8bBRBITmHhfzfxAJk79dQu");
        }
    }
}
