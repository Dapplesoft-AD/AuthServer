using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSMSOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "public",
                table: "otp",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sms_config",
                schema: "public",
                columns: table => new
                {
                    sms_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sms_token = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sms_config", x => x.sms_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sms_config",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "public",
                table: "otp");
        }
    }
}
