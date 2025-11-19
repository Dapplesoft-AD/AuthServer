using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable IDE0053

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateNew : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "email_verifications",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_email_verifications", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "password_reset",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                token = table.Column<string>(type: "text", nullable: false),
                expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                used = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_password_reset", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "tokens",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                app_id = table.Column<Guid>(type: "uuid", nullable: false),
                acess_token = table.Column<string>(type: "text", nullable: false),
                refresh_token = table.Column<string>(type: "text", nullable: false),
                issued_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tokens", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "email_verifications",
            schema: "public");

        migrationBuilder.DropTable(
            name: "password_reset",
            schema: "public");

        migrationBuilder.DropTable(
            name: "tokens",
            schema: "public");
    }
}
#pragma warning restore IDE0053
