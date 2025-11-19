using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable IDE0053
namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "applications",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                client_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                client_secret = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                redirect_url = table.Column<string>(type: "text", nullable: false),
                api_base_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                application_status = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_applications", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "customers",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_customers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "users",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                first_name = table.Column<string>(type: "text", nullable: false),
                last_name = table.Column<string>(type: "text", nullable: false),
                password_hash = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "todo_items",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                labels = table.Column<List<string>>(type: "text[]", nullable: false),
                is_completed = table.Column<bool>(type: "boolean", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                priority = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_todo_items", x => x.id);
                table.ForeignKey(
                    name: "fk_todo_items_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_customers_email",
            schema: "public",
            table: "customers",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_todo_items_user_id",
            schema: "public",
            table: "todo_items",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            schema: "public",
            table: "users",
            column: "email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "applications",
            schema: "public");

        migrationBuilder.DropTable(
            name: "customers",
            schema: "public");

        migrationBuilder.DropTable(
            name: "todo_items",
            schema: "public");

        migrationBuilder.DropTable(
            name: "users",
            schema: "public");
    }
}
#pragma warning restore IDE0053
