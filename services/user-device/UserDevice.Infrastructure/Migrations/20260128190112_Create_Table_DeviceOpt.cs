using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDevice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_DeviceOpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "device_otps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserDeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_otps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_device_otps_user-devices_UserDeviceId",
                        column: x => x.UserDeviceId,
                        principalTable: "user-devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_device_otps_UserDeviceId",
                table: "device_otps",
                column: "UserDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_device_otps_UserDeviceId_IsUsed_ExpiredAt",
                table: "device_otps",
                columns: new[] { "UserDeviceId", "IsUsed", "ExpiredAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device_otps");
        }
    }
}
