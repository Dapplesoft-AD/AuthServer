using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas", x => x.id);
                    table.ForeignKey(
                        name: "fk_areas_districts_district_id",
                        column: x => x.district_id,
                        principalSchema: "public",
                        principalTable: "districts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localities", x => x.id);
                    table.ForeignKey(
                        name: "fk_localities_areas_area_id",
                        column: x => x.area_id,
                        principalSchema: "public",
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_areas_country_id",
                schema: "public",
                table: "areas",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_areas_district_id",
                schema: "public",
                table: "areas",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_areas_name",
                schema: "public",
                table: "areas",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_areas_type",
                schema: "public",
                table: "areas",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_localities_area_id",
                schema: "public",
                table: "localities",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_localities_area_id_is_active",
                schema: "public",
                table: "localities",
                columns: ["area_id", "is_active"]);

            migrationBuilder.CreateIndex(
                name: "ix_localities_area_id_name",
                schema: "public",
                table: "localities",
                columns: ["area_id", "name"],
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_localities_area_id_type_is_active",
                schema: "public",
                table: "localities",
                columns: ["area_id", "type", "is_active"]);

            migrationBuilder.CreateIndex(
                name: "ix_localities_country_id",
                schema: "public",
                table: "localities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_localities_country_id_area_id_is_active",
                schema: "public",
                table: "localities",
                columns: ["country_id", "area_id", "is_active"]);

            migrationBuilder.CreateIndex(
                name: "ix_localities_is_active",
                schema: "public",
                table: "localities",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_localities_name",
                schema: "public",
                table: "localities",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_localities_type",
                schema: "public",
                table: "localities",
                column: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "areas",
                schema: "public");
        }
    }
}
