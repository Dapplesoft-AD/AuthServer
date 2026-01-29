using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Stable_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_otp",
                schema: "public",
                table: "otp");

            migrationBuilder.DropColumn(
                name: "sms_token",
                schema: "public",
                table: "sms_config");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "public",
                table: "otp");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "public",
                table: "otp");

            migrationBuilder.RenameTable(
                name: "otp",
                schema: "public",
                newName: "Otps",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "is_email_verified",
                schema: "public",
                table: "users",
                newName: "is_verified");

            migrationBuilder.RenameColumn(
                name: "sms_id",
                schema: "public",
                table: "sms_config",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "otp_id",
                schema: "public",
                table: "Otps",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "api_url",
                schema: "public",
                table: "sms_config",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "public",
                table: "sms_config",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "provider_name",
                schema: "public",
                table: "sms_config",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "provider_url",
                schema: "public",
                table: "sms_config",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "token",
                schema: "public",
                table: "sms_config",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "otp_token",
                schema: "public",
                table: "Otps",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<bool>(
                name: "is_expired",
                schema: "public",
                table: "Otps",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "destination",
                schema: "public",
                table: "Otps",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "expires_at",
                schema: "public",
                table: "Otps",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                schema: "public",
                table: "Otps",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "otp_type",
                schema: "public",
                table: "Otps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_otps",
                schema: "public",
                table: "Otps",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_otps_destination",
                schema: "public",
                table: "Otps",
                column: "destination");

            migrationBuilder.CreateIndex(
                name: "ix_otps_destination_otp_type_otp_token_expires_at",
                schema: "public",
                table: "Otps",
                columns: new[] { "destination", "otp_type", "otp_token", "expires_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_otps",
                schema: "public",
                table: "Otps");

            migrationBuilder.DropIndex(
                name: "ix_otps_destination",
                schema: "public",
                table: "Otps");

            migrationBuilder.DropIndex(
                name: "ix_otps_destination_otp_type_otp_token_expires_at",
                schema: "public",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "api_url",
                schema: "public",
                table: "sms_config");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "public",
                table: "sms_config");

            migrationBuilder.DropColumn(
                name: "provider_name",
                schema: "public",
                table: "sms_config");

            migrationBuilder.DropColumn(
                name: "provider_url",
                schema: "public",
                table: "sms_config");

            migrationBuilder.DropColumn(
                name: "token",
                schema: "public",
                table: "sms_config");

            migrationBuilder.DropColumn(
                name: "destination",
                schema: "public",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "expires_at",
                schema: "public",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "is_used",
                schema: "public",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "otp_type",
                schema: "public",
                table: "Otps");

            migrationBuilder.RenameTable(
                name: "Otps",
                schema: "public",
                newName: "otp",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "is_verified",
                schema: "public",
                table: "users",
                newName: "is_email_verified");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "public",
                table: "sms_config",
                newName: "sms_id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "public",
                table: "otp",
                newName: "otp_id");

            migrationBuilder.AddColumn<string>(
                name: "sms_token",
                schema: "public",
                table: "sms_config",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "otp_token",
                schema: "public",
                table: "otp",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "is_expired",
                schema: "public",
                table: "otp",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "public",
                table: "otp",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "public",
                table: "otp",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_otp",
                schema: "public",
                table: "otp",
                column: "otp_id");
        }
    }
}
