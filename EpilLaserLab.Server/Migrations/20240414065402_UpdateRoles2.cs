using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelAccess",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$0O9Jt1CN.qNklBcvZ2MsVOxrnVwtZozYo4e1FJ4EUK83h7f7dQHRy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$X4978Q/r2uEvNu5xyN4CE.hNvXnf45C4tfjqudOvQkfyrzZz4Wd.K");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "LevelAccess",
                table: "Roles",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "LevelAccess",
                value: 0u);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "LevelAccess",
                value: 0u);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$.yq54P4LuavFwjwA6l4HQ.lgU8lMLhEEjOlIXSeoA/IharO9Zq4yG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$J7bSLeGi9STb0GVLXqeL8eoW/J/DB3HQg0BFGEtdhRw5dNth5d19S");
        }
    }
}
