using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "PrepaymentPercentage",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$MZJELQSS6ghla1FO7fVSiuNxbfjj//KrjFpShiUKkwcZqqPRQNMxu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrepaymentPercentage",
                table: "Applications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$tecGI8IUsgJ//zo41/Ax0uRQusuMoKHeYsm6Uvgj/ScJCgkirNKmS");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Login", "PasswordHash", "RoleId" },
                values: new object[] { 2, null, "User", "$2a$11$iWyOWH5RTYzMo40cT5BoNeC6maqHKp.j9oBNwg4y8FttE5rm8mU4m", 2 });
        }
    }
}
