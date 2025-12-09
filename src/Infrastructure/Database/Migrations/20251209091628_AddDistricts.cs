using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDistricts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "regions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    region_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regions", x => x.id);
                    table.ForeignKey(
                        name: "fk_regions_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "public",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    region_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_districts", x => x.id);
                    table.ForeignKey(
                        name: "fk_districts_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "public",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_districts_regions_region_id",
                        column: x => x.region_id,
                        principalSchema: "public",
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_districts_country_id",
                schema: "public",
                table: "districts",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_districts_region_id",
                schema: "public",
                table: "districts",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "ix_regions_country_id",
                schema: "public",
                table: "regions",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "public");
        }
    }
}
