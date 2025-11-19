using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class BusinessBusinessMembersRoles : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "business_members",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                business_id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                role_id = table.Column<Guid>(type: "uuid", nullable: false),
                joined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_business_members", x => x.id));

        migrationBuilder.CreateTable(
            name: "businesses",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                owner_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                business_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                industry_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                logo_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_businesses", x => x.id);
                table.ForeignKey(
                    name: "fk_businesses_users_owner_user_id",
                    column: x => x.owner_user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "roles",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_roles", x => x.id));

        migrationBuilder.CreateIndex(
            name: "ix_businesses_owner_user_id",
            schema: "public",
            table: "businesses",
            column: "owner_user_id");

        migrationBuilder.CreateIndex(
            name: "ix_roles_role_name",
            schema: "public",
            table: "roles",
            column: "role_name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "business_members",
            schema: "public");

        migrationBuilder.DropTable(
            name: "businesses",
            schema: "public");

        migrationBuilder.DropTable(
            name: "roles",
            schema: "public");
    }
}
