using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "drivers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "drivers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "drivers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "drivers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_at_utc",
                table: "drivers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverOtpChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Contact = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Channel = table.Column<int>(type: "integer", nullable: false),
                    Purpose = table.Column<int>(type: "integer", nullable: false),
                    CodeHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConsumedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSentAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    MaxAttempts = table.Column<int>(type: "integer", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverOtpChallenges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_drivers_created_at_utc",
                table: "drivers",
                column: "created_at_utc");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_email",
                table: "drivers",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_phone",
                table: "drivers",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverOtpChallenges_Contact",
                table: "DriverOtpChallenges",
                column: "Contact");

            migrationBuilder.CreateIndex(
                name: "IX_DriverOtpChallenges_Contact_Purpose",
                table: "DriverOtpChallenges",
                columns: new[] { "Contact", "Purpose" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverOtpChallenges_Contact_Purpose_CreatedAtUtc",
                table: "DriverOtpChallenges",
                columns: new[] { "Contact", "Purpose", "CreatedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverOtpChallenges_CreatedAtUtc",
                table: "DriverOtpChallenges",
                column: "CreatedAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverOtpChallenges");

            migrationBuilder.DropIndex(
                name: "IX_drivers_created_at_utc",
                table: "drivers");

            migrationBuilder.DropIndex(
                name: "IX_drivers_email",
                table: "drivers");

            migrationBuilder.DropIndex(
                name: "IX_drivers_phone",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "last_login_at_utc",
                table: "drivers");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "drivers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "drivers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "drivers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
