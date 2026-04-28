using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDevice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inbox_messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Consumer = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inbox_messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user-devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DeviceId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    DeviceType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FirebaseToken = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IsTrusted = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user-devices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ux_inbox_message_consumer",
                table: "inbox_messages",
                columns: new[] { "MessageId", "Consumer" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_devices_user_id",
                table: "user-devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ux_user_devices_user_device",
                table: "user-devices",
                columns: new[] { "UserId", "DeviceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_messages");

            migrationBuilder.DropTable(
                name: "user-devices");
        }
    }
}
