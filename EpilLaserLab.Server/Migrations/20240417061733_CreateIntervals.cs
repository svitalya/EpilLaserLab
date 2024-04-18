using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EpilLaserLab.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateIntervals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Intervals",
                columns: table => new
                {
                    IntervalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<int>(type: "int", nullable: false),
                    TimeEnd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervals", x => x.IntervalId);
                    table.ForeignKey(
                        name: "FK_Intervals_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Q.rgaI7yqtxIwQg60XRzk./k2qSX02UTEWBKup04ibtP5QJzReB2S");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$gH1OZ.RtegwMYLQrfS2tXuGgShRC/G57hWF5cFVvLVdXVdxkiH.9i");

            migrationBuilder.CreateIndex(
                name: "IX_Intervals_ScheduleId",
                table: "Intervals",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intervals");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$JXzI1Yk/k.EinQ04j0zqMemWqs4HanYeVO7wLckdu3BIDS1osvGWm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$t82vqNtHAvczyDmc336BvufzCFRJ9FArCNapYRuAAwfW6HSj7jN2S");
        }
    }
}
