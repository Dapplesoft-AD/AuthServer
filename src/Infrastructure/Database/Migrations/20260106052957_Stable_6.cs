using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Stable_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_districts_countries_country_id",
                schema: "public",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "fk_localities_areas_area_id",
                schema: "public",
                table: "localities");

            migrationBuilder.DropTable(
                name: "areas",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "ix_districts_country_id",
                schema: "public",
                table: "districts");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "public",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "public",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "region_type",
                schema: "public",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "public",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "country_id",
                schema: "public",
                table: "districts");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "public",
                table: "districts");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "public",
                table: "districts");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "public",
                table: "districts");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "public",
                table: "countries");

            migrationBuilder.AlterColumn<bool>(
                name: "is_email_verified",
                schema: "public",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "public",
                table: "users",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "country_id",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "district_id",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "region_id",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "sub_district_id",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role_code",
                schema: "public",
                table: "roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_new",
                schema: "public",
                table: "regions",
                type: "boolean",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_new",
                schema: "public",
                table: "districts",
                type: "boolean",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_new",
                schema: "public",
                table: "countries",
                type: "boolean",
                nullable: true,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "sub_districts",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_new = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sub_districts", x => x.id);
                    table.ForeignKey(
                        name: "fk_sub_districts_districts_district_id",
                        column: x => x.district_id,
                        principalSchema: "public",
                        principalTable: "districts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "countries",
                columns: new[] { "id", "capital", "is_new", "name", "phone_code" },
                values: new object[] { new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), "", false, "Bangladesh", "" });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "role_code",
                value: "_ADMIN_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "role_code",
                value: "_SUPPORT_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "role_code",
                value: "_ANALYTICS_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "role_code",
                value: "_PAYMENT_ADMIN_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "description", "role_code", "role_name" },
                values: new object[] { "Common/Normal User", "_PUBLIC_USER_", "Common Usser" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "address", "country_id", "created_at", "district_id", "email", "full_name", "is_mfa_enabled", "password_hash", "phone", "region_id", "status", "sub_district_id", "updated_at" },
                values: new object[] { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, null, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@auth.dapplesoft.com", "Default Admin", false, "60358AD3245A0E1D8FC2CA0B0914E45C5F87143DDB2C9E81E09B4E41676F30B8-99D093AF2C44DB8DDCA9FE77BDE4A9F2", null, null, 0, null, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                schema: "public",
                table: "regions",
                columns: new[] { "id", "country_id", "is_new", "name" },
                values: new object[,]
                {
                    { new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Rajshahi" },
                    { new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Rangpur" },
                    { new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Sylhet" },
                    { new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Barisal" },
                    { new Guid("3376f1ac-f65e-403f-67ca-b303f202901f"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Dhaka" },
                    { new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Mymensingh" },
                    { new Guid("b926ac14-8560-b45b-84d6-31851ba90b22"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Khulna" },
                    { new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Chattagram" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "user_roles",
                columns: new[] { "id", "role_id", "user_id" },
                values: new object[] { new Guid("aaaaaaaa-eeee-ffff-ffff-ffffffffffff"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") });

            migrationBuilder.InsertData(
                schema: "public",
                table: "districts",
                columns: new[] { "id", "is_new", "name", "region_id" },
                values: new object[,]
                {
                    { new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Faridpur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Kushtia", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Bogura", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), false, "Thakurgaon", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Patuakhali", new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d") },
                    { new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Chandpur", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Khulna", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Bagerhat", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Jashore", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), false, "Narail", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Gazipur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Habiganj", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Pabna", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Manikganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Netrokona", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Khagrachhari", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), false, "Chuadanga", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Sylhet", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Rangamati", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Coxsbazar", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Noakhali", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), false, "Magura", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Kurigram", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Barguna", new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d") },
                    { new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Rajshahi", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Satkhira", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Shariatpur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Narsingdi", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Pirojpur", new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d") },
                    { new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Nilphamari", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Kishoreganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Joypurhat", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), false, "Meherpur", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Jhenaidah", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Mymensingh", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"), false, "Lakshmipur", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Jamalpur", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Bandarban", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"), false, "Chapainawabganj", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Dhaka", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Barisal", new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d") },
                    { new Guid("ac9ddfec-4168-50ad-1e4f-85213d6165cc"), false, "Jhalakathi", new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d") },
                    { new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Sunamganj", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Gaibandha", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Moulvibazar", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Madaripur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Sherpur", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Tangail", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Natore", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Dinajpur", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Sirajganj", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Rangpur", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Panchagarh", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Munshiganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Naogaon", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Comilla", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Feni", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Bhola", new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d") },
                    { new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Brahmanbaria", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Rajbari", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Lalmonirhat", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Gopalganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Chattogram", new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3") },
                    { new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Narayanganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "sub_districts",
                columns: new[] { "id", "district_id", "is_new", "name" },
                values: new object[,]
                {
                    { new Guid("000a5548-dd43-7183-57f3-0921bee7f624"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Baliakandi" },
                    { new Guid("0041deb6-09fe-0839-6e3f-605e313ce35b"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), false, "Thakurgaon Sadar" },
                    { new Guid("004f7c4b-81a1-6507-7e1e-bdf500162c56"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Mohonpur" },
                    { new Guid("00b401d4-7bd4-a406-93f2-3d9ac6768298"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Taragonj" },
                    { new Guid("0164eedf-2983-baf7-d080-a59569838206"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Nagarpur" },
                    { new Guid("01730eb0-590c-6fb8-218f-c99bc7a71484"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Shariatpur Sadar" },
                    { new Guid("0290a7e7-ef74-45a6-abe2-6f788b4377d8"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Patiya" },
                    { new Guid("0298769f-b0af-477a-54bd-e5d9bfd6a528"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Dakshinsurma" },
                    { new Guid("02b67204-8c9d-3f47-660c-104e4acf137c"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Bishwambarpur" },
                    { new Guid("030a266b-f43b-2dc4-4aa8-ca54b3ae4d41"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Paikgasa" },
                    { new Guid("0329ab85-5358-ac58-2535-524128fa102c"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Koyra" },
                    { new Guid("03afed44-0510-9c7d-5a57-cc0d32873bd6"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Sadullapur" },
                    { new Guid("03cf2558-40ed-f57b-0b60-8bbcac37aece"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Baraigram" },
                    { new Guid("045b40dc-b2ac-719f-2537-87d6fa1ccf2b"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Chauddagram" },
                    { new Guid("04b3af02-a81e-896c-866a-c03a55bc858f"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Hatibandha" },
                    { new Guid("04de85b0-d16a-cb09-c45a-268d54513f9f"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Hizla" },
                    { new Guid("05aa8303-92a0-dd6c-bbe6-e0827626bf38"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), false, "Damurhuda" },
                    { new Guid("05f02073-ec03-eb14-5916-adf513e88b17"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Fulbari" },
                    { new Guid("0635be1c-fdbb-ad14-75d2-10d7f78eb674"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Debhata" },
                    { new Guid("068d9504-a6cb-5f98-3de8-184feebdeecb"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Babuganj" },
                    { new Guid("06b91699-7c3e-3b38-0ef2-b0b84f73a5aa"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Satkania" },
                    { new Guid("070a4664-bb89-2ae8-615d-ac4158a3c9a3"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Tarail" },
                    { new Guid("083ed979-2769-159b-a10e-07edf1722639"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Manikchari" },
                    { new Guid("08fa3a5c-fe47-0257-0908-83af6e1a24ff"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Jhikargacha" },
                    { new Guid("0a25a35c-511c-a55c-654e-e11328e6bdf2"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Dowarabazar" },
                    { new Guid("0ad6ae1b-60ee-50a6-280a-c9147df10599"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Akhaura" },
                    { new Guid("0bafe76e-3511-2d4f-f716-cd9d356d449f"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Saghata" },
                    { new Guid("0d132a82-4f9a-2236-9e5d-9fe5125d10f9"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Birganj" },
                    { new Guid("0d1c09cf-9401-9d69-6e97-2b74af9a7655"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Sreemangal" },
                    { new Guid("0ea0d040-300a-c87c-7c7f-2aa566325a0c"), new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), false, "Mujibnagar" },
                    { new Guid("1029cc68-4d1b-3c57-a25d-ec49caa49bcf"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Companiganj" },
                    { new Guid("1035b5dc-6677-2caf-30f1-ceecd15cccf7"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Madhupur" },
                    { new Guid("10711dcb-cc39-e3cc-be57-17f2e3d27c50"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Kaharol" },
                    { new Guid("11516a0a-c115-e33f-878a-73b2ba38a883"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Dumuria" },
                    { new Guid("118a6e51-9af6-5fd4-6c46-f71630d3681a"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Kaliganj" },
                    { new Guid("1205bc03-b525-4a8d-64db-3484f6d9e137"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Rajarhat" },
                    { new Guid("121a07d7-04bd-23a6-fe1a-b44b6921fb91"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Zajira" },
                    { new Guid("1274688e-baa4-ca72-6e2f-50c622427431"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Harirampur" },
                    { new Guid("1364ce6c-0f2f-4e6a-fb35-15b07a29bc47"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Mirpur" },
                    { new Guid("13827ed1-3ea6-e2b6-a553-660919035950"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Rajoir" },
                    { new Guid("13b8a705-f677-7aa3-df5a-d8a29f78768e"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Rangabali" },
                    { new Guid("13e6c95a-e1b5-7a07-9c60-93bafc8f699a"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Rupganj" },
                    { new Guid("144ea8c8-a90b-9612-4145-1b732beec355"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Itna" },
                    { new Guid("15136a50-3af4-490c-b9f6-669b3ba8217f"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Trishal" },
                    { new Guid("155b9e52-0b45-000f-bb05-e68f60ed24fb"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Naldanga" },
                    { new Guid("1650833e-3f03-b998-68d8-b0590690e068"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Chatmohar" },
                    { new Guid("16e2741d-703c-9169-7efe-20969c47d158"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Bancharampur" },
                    { new Guid("17a70709-6819-dd7b-433f-86b1de38393a"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Brahmanbaria Sadar" },
                    { new Guid("199eb594-f198-5ea8-6215-31240d91e486"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Anwara" },
                    { new Guid("1a50e26f-88ae-6ceb-2c47-02bdb07e5b22"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Langadu" },
                    { new Guid("1b32351e-6a8e-4bc6-57ea-6b7b8f444d26"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Chitalmari" },
                    { new Guid("1c8de1ab-bee9-98d8-c5bc-bb83d4cd5ba1"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Moheshkhali" },
                    { new Guid("1dec672c-6fa7-d5ca-600d-8ef0f81aa3ca"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Gosairhat" },
                    { new Guid("1e29ebea-b552-8704-c511-913b88040e5b"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Goalanda" },
                    { new Guid("1e8e9d62-2018-2fac-e316-6fffb8322299"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Dhamrai" },
                    { new Guid("2014055b-ac00-4bc2-e718-5e6dcdca4aeb"), new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"), false, "Bholahat" },
                    { new Guid("213ddbe7-3aa6-077e-5082-a400c1629136"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Kishoreganj Sadar" },
                    { new Guid("21f9e1cb-466e-fa3b-8b10-9cd189aee252"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Sarankhola" },
                    { new Guid("221dd88a-c033-1dc5-8e04-f830b64dead6"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Godagari" },
                    { new Guid("2277004e-6475-fa6f-2166-f8169c7d833f"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Jamalpur Sadar" },
                    { new Guid("22e7c60e-28c3-2890-895e-ccf2910851c2"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Shailkupa" },
                    { new Guid("2376d4a3-58aa-5f33-dba6-8ce63d4a3218"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Kishorganj" },
                    { new Guid("23fd83bb-aae2-2b52-7cad-94e3f62f6b4c"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Kalapara" },
                    { new Guid("2429859c-04f7-cfdb-cb9e-03e777d8e6d4"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Gournadi" },
                    { new Guid("2452584a-59e8-7306-80eb-1746a714aa58"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Satkhira Sadar" },
                    { new Guid("24f5257c-5832-1895-3a84-027e4ef2b89b"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Rangamati Sadar" },
                    { new Guid("28218d4c-121b-4e4b-c329-1ea8ff836d84"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Panchbibi" },
                    { new Guid("285c8cb3-d7b2-ec3b-fc5c-5e8195ac2c2b"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Debidwar" },
                    { new Guid("29251309-9b7e-5a28-8b02-a3059511a927"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Madhabpur" },
                    { new Guid("297ad6e0-10d7-8cf2-bf46-e471d296d70e"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Paba" },
                    { new Guid("299415f3-b319-3015-dcab-5bc3487c8520"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Lama" },
                    { new Guid("299d5ffb-7605-c408-a567-cf477b034cce"), new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Bamna" },
                    { new Guid("2a7884dd-7d0b-560b-2787-d390a4a553d6"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Lalpur" },
                    { new Guid("2b5b4bd6-8b9b-69a1-442a-1305e1733b67"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Naria" },
                    { new Guid("2bc9c0e4-7e3f-44bf-ebac-a297ffbf5c03"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Shariakandi" },
                    { new Guid("2c0b7487-c0e4-abd5-ca67-b26d8a4094ac"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Panchagarh Sadar" },
                    { new Guid("2c622a70-1d54-8c7b-a0cb-08f886c31b92"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Kumarkhali" },
                    { new Guid("2c72faa7-a53c-9ce3-0646-041bd1ac7b83"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Ullapara" },
                    { new Guid("2c890bca-c1e1-5c55-e500-73aaec3f8699"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Raigonj" },
                    { new Guid("2d9a9d0f-5c57-b6d2-0e5d-6c5aaf68d149"), new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"), false, "Lakshmipur Sadar" },
                    { new Guid("2e279f52-086a-f6d3-416d-6692afdfc51d"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Kalaroa" },
                    { new Guid("2ed65972-7cc5-54d6-18a4-63b8282132e7"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Chhatak" },
                    { new Guid("2ef1d722-4d99-35a7-22da-623e640c102d"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Jaldhaka" },
                    { new Guid("2f252e97-c11f-bd8c-f7a0-0291c0f8040e"), new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), false, "Kalia" },
                    { new Guid("2f34899a-f6ef-f4d1-d278-e20c4f4e4793"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Mirzaganj" },
                    { new Guid("30356f5e-d54a-de15-2e61-273042506330"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Pirgacha" },
                    { new Guid("303b8a89-df16-15c1-33eb-d870f604dcea"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Nilphamari Sadar" },
                    { new Guid("305e55a4-5f18-0b99-ecac-06076d9c9edc"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Atrai" },
                    { new Guid("3064a5b1-22df-16ed-941d-dee07ebefeed"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Sonaimori" },
                    { new Guid("30d1ddf4-3448-96e7-206b-cbdad81d8635"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Naniarchar" },
                    { new Guid("31350fcd-5a0d-dafb-8d3f-2440fca33bd1"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Chandpur Sadar" },
                    { new Guid("31bc90fc-122d-c320-0b1f-ba77920a6366"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Dharmapasha" },
                    { new Guid("32c171a4-07e8-7058-cb6c-57dd47401630"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Boalmari" },
                    { new Guid("32e771ed-3b2f-e69f-a511-4f9c2baa788d"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Monohardi" },
                    { new Guid("344bd5b4-80a2-d406-3089-2e116239c713"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Tongibari" },
                    { new Guid("346cb411-d665-ce81-3a9c-2aeadb0c2a67"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Dhunot" },
                    { new Guid("3475ea4e-4d45-89bd-fd75-281495dabe44"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Kaunia" },
                    { new Guid("354cc051-2f3e-36a2-64e5-6cf9c0af61fd"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Gior" },
                    { new Guid("358f6915-ef74-8bf4-0ce0-7a21442caabd"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Nangalkot" },
                    { new Guid("35c0b4a0-ae64-b3c3-bdf4-fe400e66c568"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Rajnagar" },
                    { new Guid("36029ac1-af0f-0554-c68f-3ee9a19978e9"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Dhanbari" },
                    { new Guid("36083e3b-9a8f-ae35-d1c9-b129995fd7f3"), new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"), false, "Ramgati" },
                    { new Guid("364aed36-a0a1-f5f2-549e-ca8d1a15605d"), new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Amtali" },
                    { new Guid("36e284c8-19e1-ad7e-4a88-76af4ea34208"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Kalukhali" },
                    { new Guid("37278ba2-d9c4-2e3f-2885-d69892583e31"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Daulatpur" },
                    { new Guid("380026a6-6958-2e13-813a-2d2700045596"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Fultola" },
                    { new Guid("384b34fa-9f14-00ea-7a9b-89d88390f54b"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Dhamoirhat" },
                    { new Guid("384fa8e5-ac6a-c21b-26b6-70fa3460ae14"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Pakundia" },
                    { new Guid("39e6b9cb-e3f8-4bd2-acee-8d8a5c3c7712"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Palash" },
                    { new Guid("3a4c90d6-a2a8-b721-33c8-c8fe6ee470eb"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Puthia" },
                    { new Guid("3ab078a1-a6ee-16f0-6511-7a369f6e016c"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Chauhali" },
                    { new Guid("3c1afb99-0312-b99b-f9da-49e81eab1e48"), new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), false, "Gangni" },
                    { new Guid("3c33936e-cb3b-b642-5929-ed7f780d7423"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Digholia" },
                    { new Guid("3cf1a6ab-54c9-c326-7c56-9400741707f7"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Ramgarh" },
                    { new Guid("3d5cd29d-47fe-44d0-6430-5e3b114d74e5"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Matlab South" },
                    { new Guid("3eef0c18-86b2-df2a-19ce-e4c9a6086e16"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Kabirhat" },
                    { new Guid("3f8438ee-6743-b25d-b967-9673490fc906"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Gazipur Sadar" },
                    { new Guid("3f926ba6-df22-411f-5ac5-0d9a9918fd84"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Delduar" },
                    { new Guid("3fa1e2f0-44cf-23c9-4571-ff016b3c6612"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Atwari" },
                    { new Guid("3fbef265-fcab-1ade-bd5e-d37dc8e5237c"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Doulatpur" },
                    { new Guid("3fc1c056-dae6-685f-d074-8aa55c6078bc"), new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), false, "Lohagara" },
                    { new Guid("3fc7d060-ad80-2c55-3e16-8a585788f12e"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Sreenagar" },
                    { new Guid("404a0c42-2d26-c6c3-6b9a-872934203380"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Parbatipur" },
                    { new Guid("406a175d-f78f-6304-7f2d-36e583452de2"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Lalmai" },
                    { new Guid("406f8951-1f3b-b167-13f8-70ed5876bc42"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Iswarganj" },
                    { new Guid("411f5052-8f70-2c94-108d-0eb37a679414"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Badargonj" },
                    { new Guid("41a50992-d7cf-57e2-fc4f-c3c4b1b1a0c3"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Chirirbandar" },
                    { new Guid("41b042c3-6e56-e59b-c62f-fc4bc2880e16"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Narsingdi Sadar" },
                    { new Guid("42340696-180a-fb2e-4a26-a1b50a7105e6"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Hathazari" },
                    { new Guid("42d594cb-d004-38e9-7531-7ad62a433c9f"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Belaichari" },
                    { new Guid("439f0ab7-0cf1-3453-2f82-96804a0366e0"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Badalgachi" },
                    { new Guid("43c1359b-a452-d95a-a825-4193efbec975"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Nabiganj" },
                    { new Guid("44a22ced-fe64-5fb1-d80b-6c133708f508"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Khansama" },
                    { new Guid("45392fe1-c7ed-6009-72c0-f9d7d11a9c5c"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Mirsharai" },
                    { new Guid("45849de6-65dd-171a-5f3d-5d3bb8647978"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Mehendiganj" },
                    { new Guid("472aa8ad-71a2-07f8-e788-0fd2225484c4"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Gangachara" },
                    { new Guid("47ecd87b-15cf-76b0-df07-7809b6caab74"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Birol" },
                    { new Guid("4835affe-883d-c79c-abdd-c9a159c12d5d"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Savar" },
                    { new Guid("494020da-4d9f-eca2-24b4-0f80d1323b8f"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Lakhai" },
                    { new Guid("4953b997-2173-d934-ceb6-1f3a6fbc72b9"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Sonargaon" },
                    { new Guid("495b4f5a-9933-059a-233c-96503c68ba32"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Durgapur" },
                    { new Guid("496ae137-2977-7a81-58f8-19faf0a3d33b"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Bhairab" },
                    { new Guid("49acd299-2bf1-9a29-ddd3-97754584e434"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Syedpur" },
                    { new Guid("49dcb88e-f7dc-2d77-e0bf-285443d7668b"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Bera" },
                    { new Guid("4a4bc88d-89f9-e0c0-ebfb-861851b7defa"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Harinakundu" },
                    { new Guid("4a6c32e8-a87a-5715-b620-4cdba19444a4"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Gowainghat" },
                    { new Guid("4af87a66-5a13-b0c2-b498-33b178ea33fb"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Akkelpur" },
                    { new Guid("4cff3a59-8128-5ce4-018f-95d1162455b2"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Tala" },
                    { new Guid("4d84a7f6-bc04-3299-8814-26fafe0968e7"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Nazirpur" },
                    { new Guid("4d8dbdc5-db8d-ac02-1508-a4ce1bbc3dda"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Kasba" },
                    { new Guid("4e5ad53d-6217-e342-7412-fff148d398f2"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Monohargonj" },
                    { new Guid("4f2a65a4-a72f-0692-06f1-0f4c8fbbd6e3"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Gurudaspur" },
                    { new Guid("4f724266-b32c-2be0-39ee-0db426ac557a"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Mithapukur" },
                    { new Guid("4fb93e2c-e1f5-d8c0-2796-7c9dd08cebbc"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Shyamnagar" },
                    { new Guid("4fdec0c6-e8df-3e71-6e7c-84a6b76e5e11"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Kotchandpur" },
                    { new Guid("50286e46-37be-ecfe-e056-8a9e65208d59"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), false, "Alamdanga" },
                    { new Guid("51f365fa-917d-e970-f26a-4f2965095a0c"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Bagatipara" },
                    { new Guid("523b7ec8-815a-f80e-be43-c68d96d014d7"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Charghat" },
                    { new Guid("5243b5ff-42d5-4447-0f27-aae651e9b3ba"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Panchari" },
                    { new Guid("529fb675-38ea-4b53-0ca4-f5768ae5b73a"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Boalkhali" },
                    { new Guid("52abf885-c7fd-daec-436d-4c50bc737b82"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Madarganj" },
                    { new Guid("536ea8c7-5874-5bd2-69d0-d4402e20496f"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Bhurungamari" },
                    { new Guid("540f7adf-74ad-fb84-db68-5c3f929d2e72"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Jessore Sadar" },
                    { new Guid("55752638-a10f-7d63-ab71-f1f03da33c61"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Pirgonj" },
                    { new Guid("563de8da-a571-f7e7-0392-24385aadf2f3"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Derai" },
                    { new Guid("5709be41-91fa-5dea-3152-832bc041bd62"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Karnafuli" },
                    { new Guid("570ebbdc-1da7-f080-a8a6-ba6dffde9a73"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Gafargaon" },
                    { new Guid("576e5d9f-04ad-5fc1-0ad3-c97a278c54ca"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Faridgonj" },
                    { new Guid("578cdb75-e7e6-7076-3f24-5338621b9346"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Rowmari" },
                    { new Guid("5839a385-ddfe-7580-b4e9-0f0b7b651846"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Pabna Sadar" },
                    { new Guid("5851f2f3-8500-fd3a-8ae5-5ff89d0b6385"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), false, "Shalikha" },
                    { new Guid("5a0cd9d9-7815-a297-9159-98e1eddf9bd1"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Baghaichari" },
                    { new Guid("5aa834d3-841b-6302-9419-f4a08672320f"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Sherpur Sadar" },
                    { new Guid("5afb28b6-663b-e3ab-71d5-8b62b28c7157"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Durgapur" },
                    { new Guid("5afd8225-1fa2-df7d-687e-9965b7ff8727"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Matlab North" },
                    { new Guid("5b7cacb8-2448-b47d-e908-2a93d549447b"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Haluaghat" },
                    { new Guid("5d864207-f73a-f97c-94d4-dce573d17f2d"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Sundarganj" },
                    { new Guid("5da34181-21b9-8036-57d5-ee506d85b653"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Bhandaria" },
                    { new Guid("5e6683c4-f66c-80c4-e3f8-8816c55436ec"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Tangail Sadar" },
                    { new Guid("5e90b580-ff51-791f-eb7a-53b1520eff74"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Bandar" },
                    { new Guid("5f608ced-a20b-de03-585d-7fc5af4bc351"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Nandail" },
                    { new Guid("60b4d12d-e9ad-ac11-019c-3e4ff8c6d830"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Sapahar" },
                    { new Guid("61609177-0b09-0abd-5d42-7ff3bcbe25f4"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Sakhipur" },
                    { new Guid("6262565b-c96f-1fee-572c-77407ce92a3a"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Nokla" },
                    { new Guid("64a08849-1cc6-dfc6-dc36-c259ba0601b5"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Kaliganj" },
                    { new Guid("654cf3f6-727b-cf46-9306-e1e0d6abf570"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Sadarpur" },
                    { new Guid("65ad6515-f0ab-beb3-71f7-a41531872a6c"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Kaliganj" },
                    { new Guid("65efd6c6-01db-a814-3e18-73df2d38e0a3"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Belkuchi" },
                    { new Guid("66130f23-cc0c-0464-cd29-35997c933f3e"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Singiar" },
                    { new Guid("6845285a-b929-5df3-e437-a99dd3c6ff36"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Kalkini" },
                    { new Guid("6849a559-6a39-ee8e-1df7-13dac42d5103"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Sunamganj Sadar" },
                    { new Guid("6866c585-9236-8f82-03da-f17bf508ff87"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Teknaf" },
                    { new Guid("686aed5d-61f0-868c-e03f-75e5957e5cbe"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Nawabganj" },
                    { new Guid("6881cb61-c1ca-77b2-5c76-d1a7bac16a29"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Lohagara" },
                    { new Guid("68bc83c8-7561-9ed9-f186-16c69cd5951f"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Gopalpur" },
                    { new Guid("69251b6e-923e-7995-1617-f813f1f5ae72"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Birampur" },
                    { new Guid("6925b81e-8de7-c492-0673-776c38ebf2f2"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Madaripur Sadar" },
                    { new Guid("693763a6-6b8b-e95e-77ee-747a99007cd7"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Agailjhara" },
                    { new Guid("69e6e534-7be8-7471-ea92-700a8b4d003f"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Dhobaura" },
                    { new Guid("6a69f133-48b4-7057-d93b-3b6534958eb0"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), false, "Magura Sadar" },
                    { new Guid("6aeee799-5c55-e838-ed9d-df1c2470efb7"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Dasar" },
                    { new Guid("6b199237-5a30-334b-a855-3954003bafc3"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Shaistaganj" },
                    { new Guid("6cb34d95-454d-8939-fad9-fc93a08de40b"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Katiadi" },
                    { new Guid("6cecf2ca-5fe8-8062-b7da-7149a7e13cb8"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Osmaninagar" },
                    { new Guid("6d14fa19-fd9c-cd98-19d2-a158f1707603"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Dumki" },
                    { new Guid("6d307b47-90c4-e334-7658-d79ca26512dd"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Rajasthali" },
                    { new Guid("6d9ff31e-4091-5bb4-28e6-92eaaec8398c"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Tazumuddin" },
                    { new Guid("6e435e2a-5171-b30d-0d2a-6faa40b410e5"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Sitakunda" },
                    { new Guid("6e5a31e4-7e46-34c1-9e9e-dd4497b1ad7c"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Naogaon Sadar" },
                    { new Guid("6e8560c8-8bdb-b501-530c-49dde11e91eb"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Indurkani" },
                    { new Guid("6fe37cd2-8ca8-52f9-150b-81aa9023d762"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Khaliajuri" },
                    { new Guid("70202e1e-9839-a593-b19b-9754faf94dea"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Bhola Sadar" },
                    { new Guid("70504a90-f0d1-09d0-0000-60da0b6c30db"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Kawkhali" },
                    { new Guid("7075778d-b991-dd31-5805-78e54e7a5a6c"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Mohongonj" },
                    { new Guid("7160f74f-5516-f9dc-5d12-16b4b8456063"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Debiganj" },
                    { new Guid("71be61ca-9f0e-a115-842e-cfd1c5ebc39f"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Brahmanpara" },
                    { new Guid("724b2933-2c88-0575-aa26-e5d9fe3042b9"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Kahaloo" },
                    { new Guid("727c026e-6b97-914d-ebd7-f2fde0009d59"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Matiranga" },
                    { new Guid("72a7b939-d3e4-9bff-eee6-9c63f086e392"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Madan" },
                    { new Guid("74e11064-786a-fb1f-c28a-6d88946133dd"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Ghatail" },
                    { new Guid("75339a37-be7b-cef2-2336-e2f32af89639"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Netrokona Sadar" },
                    { new Guid("756c5011-6f9a-9019-05c0-b756c5b57e60"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Lalmonirhat Sadar" },
                    { new Guid("761d4475-b90f-43e7-70b8-086762896663"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Bheramara" },
                    { new Guid("76ac7dd6-26ce-da9a-ab74-67cc82487c69"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Chilmari" },
                    { new Guid("76e9df66-4187-b69c-8ac9-8f820ee83044"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Alikadam" },
                    { new Guid("77f4bec1-0f57-fa56-de93-a2fb21f2b125"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Phulpur" },
                    { new Guid("785bd34d-8e7f-c282-3384-4fe164387c14"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Raozan" },
                    { new Guid("78c96147-df87-8926-e357-5382d65f6184"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Hossainpur" },
                    { new Guid("7993d9db-9e19-f307-9828-aa58bb06cb92"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Shibganj" },
                    { new Guid("79abb8d4-889d-1f35-266b-22b5784cdcce"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Bakerganj" },
                    { new Guid("7af2de48-f15a-fbdb-a27d-4a925824c3b1"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Jhenaidah Sadar" },
                    { new Guid("7b99a45f-e69e-dea1-f3e9-b286bdfe4dd5"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Phulbari" },
                    { new Guid("7b9cc486-650d-f82b-b23d-960bfdc34e09"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Mongla" },
                    { new Guid("7bb23d03-c694-7dc9-de51-1fd92a7c4687"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Morrelganj" },
                    { new Guid("7d28fc01-e3c1-1c53-28d1-6e8805d62a9d"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Kapasia" },
                    { new Guid("7d294f00-73e7-1b3e-58c1-801dec0b6848"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Monpura" },
                    { new Guid("7eb80e05-7d1c-3d7b-b8cd-d3ee16d77bc8"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Hatia" },
                    { new Guid("7f6e315e-3fda-00b7-dfad-b67fef10a8b5"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Mohalchari" },
                    { new Guid("80c2be28-f908-ad5d-a980-304354670665"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Kaptai" },
                    { new Guid("80da5f6d-b9f4-ec41-b321-5d86a027b449"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Tetulia" },
                    { new Guid("80f20d69-379d-0e87-b6fe-9bb6d70ba8bb"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Subarnachar" },
                    { new Guid("8123afdf-dff3-cd6d-4215-7864cc5bce9c"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Fatikchhari" },
                    { new Guid("81d50da4-30a7-b518-5435-3555b35d88fd"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Fulbaria" },
                    { new Guid("81dbe879-ada6-b167-1b9f-0fcee63581c0"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Sarail" },
                    { new Guid("821d1bbd-bcbb-8870-cc25-4455f64cfb1b"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Guimara" },
                    { new Guid("825baf08-bbb8-475b-5a0a-b2e18bbca786"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Munshiganj Sadar" },
                    { new Guid("844db2a8-437b-11f8-df16-a84e9b8789f8"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Terokhada" },
                    { new Guid("849d413f-629e-c6da-f343-4ade426a6b89"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Gaibandha Sadar" },
                    { new Guid("84f33576-5b7c-1feb-7a5b-f1b69a26a09a"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Kushtia Sadar" },
                    { new Guid("8508aa78-9c0b-3e8d-cca9-5bd65f60d873"), new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Fulgazi" },
                    { new Guid("85463bd5-3c48-c57a-77fc-4d07568866e1"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Sirajganj Sadar" },
                    { new Guid("856da358-784b-dcc0-0db1-86da37fb0b23"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Nagarkanda" },
                    { new Guid("866617f3-038d-ab81-ef02-30bac25c343a"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Coxsbazar Sadar" },
                    { new Guid("8674a2b8-873c-efb1-9688-b840dd0e2df0"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Sandwip" },
                    { new Guid("86bf941a-9ed2-24a2-af8d-64dc3c200972"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Nalitabari" },
                    { new Guid("874dc821-f638-bbfc-7024-b2a65e7c0ba1"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Assasuni" },
                    { new Guid("8768d540-b213-a83d-3887-f836b133bfb1"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), false, "Pirganj" },
                    { new Guid("87b4ce03-7038-798d-0083-364cf447e4b8"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Shibchar" },
                    { new Guid("8816e411-31d1-5f4f-e9d9-caf77c9f6895"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Dupchanchia" },
                    { new Guid("88479360-dbc3-b530-0404-9c316979d125"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Noakhali Sadar" },
                    { new Guid("89082d3f-eae9-a490-8c54-207bc01eb48e"), new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"), false, "Ramganj" },
                    { new Guid("89591109-32d2-0850-53d8-d94c58dbb59c"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Senbug" },
                    { new Guid("89dd743c-dfd5-c6fa-a5bb-1bc85209f28d"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Doulatkhan" },
                    { new Guid("89f344c1-7662-f96b-d7a9-60368d22386f"), new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Feni Sadar" },
                    { new Guid("8a0386ca-e4b3-54c2-aeb3-8cc71e742367"), new Guid("ac9ddfec-4168-50ad-1e4f-85213d6165cc"), false, "Jhalakathi Sadar" },
                    { new Guid("8a5aa27f-32af-0069-c0ec-fbb522514d9d"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Habiganj Sadar" },
                    { new Guid("8b024a7c-1ec1-f74a-7303-ffade50e88ff"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Shalla" },
                    { new Guid("8bdd2f97-992d-d4ec-4090-0a292481c264"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Juraichari" },
                    { new Guid("8c6fbabe-13d7-e7cc-1ae3-0708c9f4e028"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Louhajanj" },
                    { new Guid("8cd8a456-b2d1-9bd7-bd44-c4645c2de203"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Dakop" },
                    { new Guid("8ec59be3-21ce-80cc-a75b-64804240f39f"), new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"), false, "Kamalnagar" },
                    { new Guid("900c4601-c129-57cc-4dc1-95d50c915dc0"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Rajbari Sadar" },
                    { new Guid("906bfeaf-93b6-c8ab-1737-f176600ab204"), new Guid("ac9ddfec-4168-50ad-1e4f-85213d6165cc"), false, "Rajapur" },
                    { new Guid("9127f7c4-63eb-ccf9-b6a9-9ad0fa29a50f"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Bogra Sadar" },
                    { new Guid("9163e163-54f1-116a-9fe1-6bf91a3b9aaf"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Tahirpur" },
                    { new Guid("916cb24f-880b-55ad-ea15-031e848f45bb"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Keshabpur" },
                    { new Guid("91b40663-9e66-5707-a7fa-ae75f88c8503"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Rangpur Sadar" },
                    { new Guid("91b8afb8-75d9-0bcd-0c5f-5da929ea0680"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Muladi" },
                    { new Guid("92b00402-65c3-7b0f-ccf6-e19d79ad3b2f"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Porsha" },
                    { new Guid("92c21b97-9fcf-23ea-4590-f9fcb72b85ed"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Charfesson" },
                    { new Guid("935c53ea-2d1a-109a-8f43-793a36f74863"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Boda" },
                    { new Guid("9446468f-ce69-2c4c-193d-9397805a9182"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Juri" },
                    { new Guid("94507861-c3c1-7608-660e-e415e69b7f28"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Jagannathpur" },
                    { new Guid("9488185d-3046-8094-7760-548a8ff98817"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Kurigram Sadar" },
                    { new Guid("94f28d7a-b2c1-b265-d305-0c53a9cc6a65"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Zakiganj" },
                    { new Guid("954dc63c-e65b-bd53-6000-22b62f89148b"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Islampur" },
                    { new Guid("95a8cf7f-7ec5-2eb9-5ce2-724b1de8c530"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Aditmari" },
                    { new Guid("96687724-b497-752a-cdd2-80daf0121a32"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Kanaighat" },
                    { new Guid("9759d391-bd58-b5d2-9e4e-cb372061c007"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Kulaura" },
                    { new Guid("990e8ccd-2c8b-0e7e-b644-2036adf7af12"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Bhangura" },
                    { new Guid("9961520c-2dff-879a-9ee7-c09218d9a285"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Balaganj" },
                    { new Guid("9992079c-e9f6-8034-b6d3-43104c8daa77"), new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), false, "Narail Sadar" },
                    { new Guid("9a0a698b-6193-6261-9c95-a84066eedfc8"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), false, "Chuadanga Sadar" },
                    { new Guid("9ac73591-f6e3-5868-98ad-bafefcf21316"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Raninagar" },
                    { new Guid("9ac9875a-1baa-06a7-79aa-9b7a4a44f766"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Rangunia" },
                    { new Guid("9b830c9d-8c19-88af-f147-5d5c61158e1b"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Mollahat" },
                    { new Guid("9bd9a0a9-cf95-35ca-a8c3-7308012db629"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Baniachong" },
                    { new Guid("9c39cb1b-16be-1b28-536f-b395a3c9006e"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Bhuapur" },
                    { new Guid("9c677a01-d670-364e-ae3e-704139f76901"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Adamdighi" },
                    { new Guid("9d55936c-64d0-3de4-46de-9fb5a6665480"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Fenchuganj" },
                    { new Guid("9d600d31-0d2d-defb-ce7b-5954be09f1c8"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Kachua" },
                    { new Guid("9debb95d-fa30-4730-391c-60f64da24a63"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Burichang" },
                    { new Guid("9e7dc79d-7341-b4b1-1461-3682bd544cdb"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Galachipa" },
                    { new Guid("9e9cf616-8f48-85f5-63a2-586a2c90a8be"), new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Pathorghata" },
                    { new Guid("9eb6c063-471b-6e61-76bc-2ffa0b0ec92e"), new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Daganbhuiyan" },
                    { new Guid("9eec4858-a83b-1daf-b04b-f742fca5878b"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), false, "Khoksa" },
                    { new Guid("a007c6b3-f680-9ce9-08e7-7f273abce6ea"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), false, "Jibannagar" },
                    { new Guid("a0148de2-970c-f845-19f7-64f12b72eaca"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Bagherpara" },
                    { new Guid("a059f7b3-3070-9964-5932-1312e1f90d03"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Kotalipara" },
                    { new Guid("a11df1ec-7c0d-d5f3-0791-4244c6d93b7f"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Patnitala" },
                    { new Guid("a16e578b-7d8a-73c2-4f9d-e915f86ecc26"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Narayanganj Sadar" },
                    { new Guid("a1ca3029-00a7-8627-1df9-d26b0624e749"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Belabo" },
                    { new Guid("a221cdd3-7a26-d5ce-e85d-fcbcffbd743c"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Kalai" },
                    { new Guid("a264bfe4-476f-442d-70a8-0b9513614c62"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Jamalganj" },
                    { new Guid("a2b91187-e9fb-d9c7-4f0b-e3e479b05549"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Patgram" },
                    { new Guid("a385dd39-de72-d302-3243-62ec2c69651a"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Rowangchhari" },
                    { new Guid("a3927475-2916-6eb9-c8c6-8d1ffeba485e"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Sarishabari" },
                    { new Guid("a3b221ac-fcb2-74bd-9aff-cb4f84491165"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Companiganj" },
                    { new Guid("a3bf18a9-af10-5978-f276-5a2352d80a71"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Borhanuddin" },
                    { new Guid("a443a1f4-5fe7-b02a-ade4-037740efa645"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Gobindaganj" },
                    { new Guid("a448fe20-5a06-399d-f7bd-4aece108c720"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Sherpur" },
                    { new Guid("a49a5b4e-f197-8286-c96b-7104837fd767"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Sharsha" },
                    { new Guid("a4cda7e3-a9c4-0b06-7197-eab81332a19f"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Muradnagar" },
                    { new Guid("a560f502-8af4-648d-b11f-33dc20ca59c7"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Patuakhali Sadar" },
                    { new Guid("a59efe22-a120-b183-73d9-2dddd1d3a9b1"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Hajiganj" },
                    { new Guid("a5b849db-a819-7d12-3d3e-1b49ec79b1a1"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Tungipara" },
                    { new Guid("a60b8890-77b3-4839-9255-2bd7f64b5ace"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Natore Sadar" },
                    { new Guid("a618ecd6-82fa-2e8f-3a8d-91fc984daaa2"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Meghna" },
                    { new Guid("a66e732a-9d34-9f1d-e68c-4396f0f08a29"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Pirojpur Sadar" },
                    { new Guid("a66fdac7-241b-204a-f7b6-21ac00d88486"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Charrajibpur" },
                    { new Guid("a6ee814b-134b-0bff-29fa-cf4f65e37c22"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), false, "Ranisankail" },
                    { new Guid("a7483250-9b3f-6203-e04b-59434b7a4f61"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Atghoria" },
                    { new Guid("a74a1c32-f6e0-b618-6d56-49bf2c4bd813"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Niamatpur" },
                    { new Guid("a7ed4347-bda3-ef29-94c6-aa04927d643c"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), false, "Sreepur" },
                    { new Guid("a80e5621-66d9-5af5-1de5-f17c3df5c53b"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Basail" },
                    { new Guid("a8ec9478-0914-fb80-04aa-b84f43fe8dc0"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Jhenaigati" },
                    { new Guid("abc6511b-199a-c63e-465d-6c9b7dd96cd5"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Moulvibazar Sadar" },
                    { new Guid("add3617a-3114-a9a1-80df-7d8eec20bfc0"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Kaliganj" },
                    { new Guid("adec1410-d861-cff6-c955-6bef3b504070"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Barhatta" },
                    { new Guid("af845a80-e7d4-b5a8-4e4d-f7961b6be029"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Bagha" },
                    { new Guid("afb7a893-475d-a319-f6d9-a5e9ab8570df"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Santhia" },
                    { new Guid("b06743e4-7a94-51f0-67a1-e99bfb0ec8b1"), new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"), false, "Gomostapur" },
                    { new Guid("b094296a-a196-e86f-bf63-4c79ec5e7627"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Raipura" },
                    { new Guid("b09dac86-a724-e845-ba50-98488d7dda93"), new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"), false, "Chapainawabganj Sadar" },
                    { new Guid("b0b8325b-208a-a10c-9c6c-3206e8574a07"), new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"), false, "Raipur" },
                    { new Guid("b128aff0-7a6d-599e-2fd2-37dc213315d7"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Laksam" },
                    { new Guid("b2bb7626-2151-5ecf-105f-7d852968eb5b"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Barkal" },
                    { new Guid("b2dc91c7-058f-b2a4-cf61-671647ac288e"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Haimchar" },
                    { new Guid("b3002add-4e49-98f6-e905-545cec7fde66"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Keraniganj" },
                    { new Guid("b3223dda-99df-8bf9-e331-a7cd6f671f92"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Sylhet Sadar" },
                    { new Guid("b3726244-f965-b3d0-8f53-e05a0f3a19fb"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Muksudpur" },
                    { new Guid("b3fc068b-ea28-15ff-ac75-255a3909e329"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Manirampur" },
                    { new Guid("b472b664-1132-fe7f-02b9-3a7791b802b7"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Sadarsouth" },
                    { new Guid("b49e5678-358c-560f-a4b8-1ad7c937f2ea"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Charbhadrasan" },
                    { new Guid("b4c36f4d-05bd-99e7-d7a7-48b6f9c496eb"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Abhaynagar" },
                    { new Guid("b52be133-d6fb-2ca8-0bf2-439668e3fe46"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Jaintiapur" },
                    { new Guid("b66a341b-99cb-8091-726f-69bac2d267c8"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Nikli" },
                    { new Guid("b69f8e9d-9adf-84df-63dd-64dd12f60e9f"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Kamarkhand" },
                    { new Guid("b6f8b375-57e1-2beb-27ab-dc90aed20b46"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Kamolganj" },
                    { new Guid("b6ff3efc-a3da-dfe5-3084-90b515d595b5"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Bagerhat Sadar" },
                    { new Guid("b7010474-5034-6eca-95b1-8c8fb8cae353"), new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"), false, "Lalmohan" },
                    { new Guid("b777f37b-13c9-05cd-3f04-0ae2d62dcbb0"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Ashuganj" },
                    { new Guid("b7c18501-e2aa-1a08-be28-83c0bde45e86"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Tanore" },
                    { new Guid("b998456b-fff5-b777-c393-5e5a9ac86464"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Saltha" },
                    { new Guid("b9d7cdf2-d245-630a-18fb-95178d8a206d"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Joypurhat Sadar" },
                    { new Guid("ba2682e4-8641-2b39-3feb-f5164b7e434d"), new Guid("ac9ddfec-4168-50ad-1e4f-85213d6165cc"), false, "Nalchity" },
                    { new Guid("ba76505c-905d-9fa1-856a-3269b037a71b"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Manda" },
                    { new Guid("bb827458-2417-0822-f475-dfbbbb5fa7ed"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Madhukhali" },
                    { new Guid("bd21293f-dba7-a285-cc01-fcc84c8ae905"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Wazirpur" },
                    { new Guid("bd4b3441-7be0-bc0a-fd39-20e8c0a90bc5"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Khagrachhari Sadar" },
                    { new Guid("bd74745f-5d94-e9c8-ec4a-4f9e5b431aeb"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Araihazar" },
                    { new Guid("bd9a57f6-5fc0-dbe0-5d05-c89b3d2aafe5"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), false, "Baliadangi" },
                    { new Guid("bf54368d-5ec1-604b-8c3d-0c4708050893"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Dewangonj" },
                    { new Guid("bf72f4b4-3c76-a1e4-30a9-91d0aa7808f8"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Laxmichhari" },
                    { new Guid("bf8d46b7-daba-0078-8911-4b90ea53afdc"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Karimgonj" },
                    { new Guid("bfa59874-4ba4-5d31-0416-438747982e6d"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Bhanga" },
                    { new Guid("c04e8d50-71f0-601a-abaf-c46b222f5c45"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Naikhongchhari" },
                    { new Guid("c165789e-716f-0487-dc14-11651144c404"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Beanibazar" },
                    { new Guid("c1c5756a-2f42-7473-c92d-e1fa7cc91222"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Gabtali" },
                    { new Guid("c1eb1c03-2255-9b7a-0887-2fc077a807a7"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Faridpur Sadar" },
                    { new Guid("c1f684a9-5a71-3a23-9231-760607172251"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Nasirnagar" },
                    { new Guid("c33e07af-1106-cc82-1181-ad46217afd37"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Purbadhala" },
                    { new Guid("c421ad55-d8e9-9d44-dd93-f94afd76ab6d"), new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Sonagazi" },
                    { new Guid("c4bb31c6-7dbb-8f72-f9f8-3474ffbabe57"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Palashbari" },
                    { new Guid("c4edc3c6-4320-5d97-8c8e-093c6a935ed2"), new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), false, "Meherpur Sadar" },
                    { new Guid("c5041ce6-923c-f0ca-96de-eebdb18a4b42"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Kutubdia" },
                    { new Guid("c579174a-c53f-3e8f-9da9-f62c028f2654"), new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"), false, "Dighinala" },
                    { new Guid("c5edb94a-9668-1a8a-f37b-ec7d9231d81b"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Nawabganj" },
                    { new Guid("c644cc88-7f96-6b88-7561-56a8dd72f12c"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Bhedarganj" },
                    { new Guid("c66e35a0-23b2-a4f3-fb3a-fea5daf20dcc"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Muktagacha" },
                    { new Guid("c7105d82-443a-c0c9-1779-0abb3f01b6be"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Bagmara" },
                    { new Guid("c77cdddb-0e20-3a0c-236f-915ab05cb89e"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Dimla" },
                    { new Guid("c7f5eb2c-e13a-43bf-4109-cb12b57d6984"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Barisal Sadar" },
                    { new Guid("c84d0dd9-5ce3-9a08-7838-e9e66118ce3a"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Mohadevpur" },
                    { new Guid("c8ad1f35-5e8c-9213-2c83-fda0a93cac5d"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Gajaria" },
                    { new Guid("cbb7dc6f-3903-5a51-7ffc-25f6a8a59189"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Ishurdi" },
                    { new Guid("cd7691e3-c57b-5f67-7a37-615aed40435a"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Barura" },
                    { new Guid("ce1a7b03-f68d-07ae-b57a-cebb81bac738"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Shahjadpur" },
                    { new Guid("ce8cd570-43bd-30df-5c23-c0f0c024ecba"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Bishwanath" },
                    { new Guid("cece2116-31c9-8edb-7c45-6f24f7a34d95"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Kalmakanda" },
                    { new Guid("cf7ca879-3718-1f4b-d60d-b895ee4e3077"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Hakimpur" },
                    { new Guid("cf9cc290-ef62-9911-2c66-a4c6a57d2fbf"), new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Chhagalnaiya" },
                    { new Guid("d119b788-b229-6575-7124-37371d6a1492"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Eidgaon" },
                    { new Guid("d17a5a59-7a19-5951-9369-fcd05fd5497b"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Bandarban Sadar" },
                    { new Guid("d210419b-7322-b293-db20-4de56109a7a8"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Pekua" },
                    { new Guid("d35a4e43-ab1a-1c7d-232b-a3997b1924ee"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Domar" },
                    { new Guid("d3d85da0-2c4e-81e7-c75a-16de86f2a5ce"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Fakirhat" },
                    { new Guid("d408422a-f86e-8a7f-f643-2124074800e5"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Austagram" },
                    { new Guid("d438b671-31a5-a99f-38f1-ad3b784a68c9"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Khetlal" },
                    { new Guid("d4801fe0-a413-cbc5-46b5-c9dfb95e4902"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Golapganj" },
                    { new Guid("d4e15c46-4fbb-341f-b71f-ee639288c745"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Comilla Sadar" },
                    { new Guid("d54fa408-4f2e-28e7-c142-1e3384cae727"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Dohar" },
                    { new Guid("d5734372-fe4b-22a0-8aa2-3f4a7d54948a"), new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"), false, "Nachol" },
                    { new Guid("d675be0e-4f12-1bb8-6f33-0ab8a61257fd"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Shahrasti" },
                    { new Guid("d686d630-c19d-1ea4-8ec7-723634054019"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Chatkhil" },
                    { new Guid("d77136bb-ebcc-dbbd-3573-66cef8aa9bf0"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Kendua" },
                    { new Guid("d8d5c74d-f547-84d9-a1cb-e5eb970adf81"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Tarash" },
                    { new Guid("d94024da-2272-f3d7-20f7-0e8010c4b378"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Sirajdikhan" },
                    { new Guid("d9be4719-39a5-167d-f544-acbb6a8ccfe8"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Manikganj Sadar" },
                    { new Guid("dabf22a1-43d8-2a88-4949-1edabb86a40f"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Kalihati" },
                    { new Guid("dacaa98c-70c7-6c37-23de-6f166932a196"), new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Barguna Sadar" },
                    { new Guid("db22c67a-c186-afaf-ccd0-8cac92e53d49"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Ramu" },
                    { new Guid("db4a0888-fb44-9e1f-db2d-938c23f91cb5"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Sreepur" },
                    { new Guid("dbafa463-f875-e0bb-877e-0b14b6881d40"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Barlekha" },
                    { new Guid("dbf7d56b-aff0-75fc-d4dd-3da4b67f5ed4"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Nageshwari" },
                    { new Guid("dc3637d5-8760-1380-0f9d-2204f8ba6d29"), new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"), false, "Parshuram" },
                    { new Guid("dc7e5cd4-8403-47fa-0d05-c424f9e35d0d"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Phulchari" },
                    { new Guid("dd014f4f-1bee-dbdf-4640-54160a0668be"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Chandina" },
                    { new Guid("dd17c986-400f-b305-d481-7408ec0e1945"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Bauphal" },
                    { new Guid("dd27cd1b-e546-e938-2a95-1404b0101956"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Tarakanda" },
                    { new Guid("dd53cf86-2959-079c-c7d6-bd5c78048d2d"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Sonatala" },
                    { new Guid("dd618d14-a19f-64b2-a824-e6ce5970eb91"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Ulipur" },
                    { new Guid("de1d991f-160b-b41d-4e5e-608d167781bc"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Daudkandi" },
                    { new Guid("de6aa7ef-1603-2db7-af2b-eef66a2505fb"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Kuliarchar" },
                    { new Guid("de84db3e-b608-7098-4e7e-a2a59553746c"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), false, "Mohammadpur" },
                    { new Guid("df0f6046-3895-1a35-1d42-95948c395b6d"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Sujanagar" },
                    { new Guid("df6becbb-2bb5-b1cf-e30d-8dc58f4cdec9"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Nesarabad" },
                    { new Guid("df8fb7e8-48dc-41ba-88cf-6df518492a71"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Shibpur" },
                    { new Guid("e1538f4c-b30b-5726-4790-7b5ffc963e03"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Gopalganj Sadar" },
                    { new Guid("e1edafad-b02b-d554-867f-993b2e3968c4"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Shibaloy" },
                    { new Guid("e511a15e-9654-6112-d018-a48effefbfd1"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Bijoynagar" },
                    { new Guid("e66b18e1-5b22-d88d-5cd5-04e0801ac819"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Nondigram" },
                    { new Guid("e66f4370-c4ba-b3cd-c688-a363b8a6d53d"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Faridpur" },
                    { new Guid("e6d9dd81-6cd0-59a6-859f-f6f86c120fc8"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Mirzapur" },
                    { new Guid("e6e894f8-5a61-f55d-aa91-18112652bcc2"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Ghoraghat" },
                    { new Guid("e77ec0a9-a325-d617-3b20-c6772243d380"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Gouripur" },
                    { new Guid("e8243ef3-0d60-dbae-55d3-bef2b6434054"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Sreebordi" },
                    { new Guid("e8535b6e-d135-8baf-ade3-3bd77d1bd0b8"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Bajitpur" },
                    { new Guid("e85edc42-5323-1c31-6561-58ee282fa8e4"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Singra" },
                    { new Guid("e906d64a-042b-4c52-1887-7057cb80066c"), new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"), false, "Dashmina" },
                    { new Guid("e99c7c8f-b8c5-e992-3383-2cea0afb641e"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Dinajpur Sadar" },
                    { new Guid("ea26e76f-f413-4067-eb63-b5639243934e"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Botiaghata" },
                    { new Guid("eb115ac6-0a8a-1634-7b38-0418a133644e"), new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"), false, "Shibganj" },
                    { new Guid("ec0cfea4-fd91-23ed-c0fc-e9be23f53f3c"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Saturia" },
                    { new Guid("ec2a4e14-2378-22e9-feae-7d040c10c98f"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Thanchi" },
                    { new Guid("ed10f0b7-8e14-d5a1-6029-e518cbaacd7f"), new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"), false, "Kawkhali" },
                    { new Guid("ed5278fc-7073-25b3-b840-ac8b59361679"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Damudya" },
                    { new Guid("ed6392c3-68be-9dd3-e42d-4ab65b1e7099"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Kashiani" },
                    { new Guid("ed6501bf-1f3a-119d-3574-a1a47025ae58"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Mymensingh Sadar" },
                    { new Guid("ed89c1f0-5e43-d6b8-08fd-067cfea42078"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Madhyanagar" },
                    { new Guid("eec9ff7e-a2f1-8799-b82d-467914da30bf"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Rampal" },
                    { new Guid("efde6f2d-8637-0a2b-7032-b5697a181985"), new Guid("aa41bc76-0294-1770-50e1-5094947998e5"), false, "Banaripara" },
                    { new Guid("efe6a776-9d9b-4142-68ea-134c94830e93"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Chougachha" },
                    { new Guid("f02d9078-c3cd-8c73-a749-0b83b9b7caad"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Ajmiriganj" },
                    { new Guid("f1614393-f87a-2443-5247-8547f9b7ecd8"), new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"), false, "Mathbaria" },
                    { new Guid("f27f6eb8-92e0-1f72-28b7-3907c8ae275b"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Homna" },
                    { new Guid("f351286b-0805-a0a2-1bb3-64bfa54ee4f1"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Atpara" },
                    { new Guid("f36ad097-a83c-a713-959d-d3f2084452df"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Banshkhali" },
                    { new Guid("f42c37d6-54f2-8cb0-780e-60f7374b6b8c"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Chakaria" },
                    { new Guid("f4c4b0ad-9174-00f5-ffb7-c027d85594f9"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Rupsha" },
                    { new Guid("f4d16298-90ca-50db-fc6c-d5cce3bdede9"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), false, "Haripur" },
                    { new Guid("f582f5fc-1b84-44cf-4715-4b88aa27abef"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Bokshiganj" },
                    { new Guid("f595f726-773b-ab11-2993-7690a5d587dd"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Moheshpur" },
                    { new Guid("f686ed1e-bfb5-3b23-ad92-39190806452d"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Bahubal" },
                    { new Guid("f6902e9e-f81e-78d7-ab40-3ed2df29c0cf"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Mithamoin" },
                    { new Guid("f6d044cf-f992-376a-ef22-3e988723b61c"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Chunarughat" },
                    { new Guid("f7239828-56ee-3f5a-1891-93e046477873"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), false, "Shajahanpur" },
                    { new Guid("f723a4ba-7257-9155-8c4a-665ecdd9030a"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Shantiganj" },
                    { new Guid("f7650516-e35d-1909-c54e-6a6601bb31fb"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), false, "Alfadanga" },
                    { new Guid("f7a04657-b77d-6c24-ac83-212957a2be2b"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Kazipur" },
                    { new Guid("f7a11a44-560b-59a9-5983-a93e31b24d8a"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Bhaluka" },
                    { new Guid("f7e59603-1d6e-e015-8729-76d17cadba85"), new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Betagi" },
                    { new Guid("f7f7f0ee-d7f0-3a2f-8008-94254532017f"), new Guid("fc538336-0267-537b-b467-6e83bef08d62"), false, "Chandanaish" },
                    { new Guid("fab724bf-0ffa-a920-f878-4d4312bf0d19"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Kaliakair" },
                    { new Guid("fb20c88f-eb6c-d2a9-a19e-7e1f2c58c298"), new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"), false, "Nabinagar" },
                    { new Guid("fb7ff719-e726-f43f-b5e4-d31ad6a88d7c"), new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"), false, "Begumganj" },
                    { new Guid("fbdbcaf6-cbf0-a396-40f0-eb43a5b58d98"), new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"), false, "Ukhiya" },
                    { new Guid("fc6bd80c-9280-5f06-1e4f-88680ac7fb8e"), new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"), false, "Ruma" },
                    { new Guid("fd0de48d-ffd7-8a40-26cc-8a14a684c5d1"), new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"), false, "Kachua" },
                    { new Guid("fd4ede89-2ef6-8bfc-4dbb-8200a62aeea9"), new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"), false, "Titas" },
                    { new Guid("fde4f919-9fb8-cc3e-a6e1-eaa64c0101e3"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Bochaganj" },
                    { new Guid("fe5e7c81-2d22-c761-173f-587e4cbe3c6b"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Melandah" },
                    { new Guid("fec0c504-a272-36f9-25dd-c2235d96e519"), new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"), false, "Taltali" },
                    { new Guid("fefcf56b-17b7-c6c6-5140-e445cefacc74"), new Guid("ac9ddfec-4168-50ad-1e4f-85213d6165cc"), false, "Kathalia" },
                    { new Guid("ff85bc3d-1cc4-f46a-3b42-e45d5d3ec80b"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Pangsa" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_country_id",
                schema: "public",
                table: "users",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_district_id",
                schema: "public",
                table: "users",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_region_id",
                schema: "public",
                table: "users",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_sub_district_id",
                schema: "public",
                table: "users",
                column: "sub_district_id");

            migrationBuilder.CreateIndex(
                name: "ix_regions_name",
                schema: "public",
                table: "regions",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_districts_name",
                schema: "public",
                table: "districts",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_countries_name",
                schema: "public",
                table: "countries",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_sub_districts_district_id",
                schema: "public",
                table: "sub_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_sub_districts_name",
                schema: "public",
                table: "sub_districts",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                schema: "public",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                schema: "public",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_localities_sub_districts_area_id",
                schema: "public",
                table: "localities",
                column: "area_id",
                principalSchema: "public",
                principalTable: "sub_districts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_localities_sub_districts_area_id",
                schema: "public",
                table: "localities");

            migrationBuilder.DropTable(
                name: "sub_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "ix_users_country_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_district_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_region_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_sub_district_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_regions_name",
                schema: "public",
                table: "regions");

            migrationBuilder.DropIndex(
                name: "ix_districts_name",
                schema: "public",
                table: "districts");

            migrationBuilder.DropIndex(
                name: "ix_countries_name",
                schema: "public",
                table: "countries");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("1223cfbf-e907-71ac-57eb-8bdeb57ac359"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("12faf1fa-4f13-06d4-fa85-a4f80925c668"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("162945d3-aac6-0e74-7458-5eee83fce942"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("3ac169eb-db1c-358f-b4f8-13b0675029d1"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("4b4f2c7d-72d5-80e0-80f5-b19f15187f04"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("4b7c60ca-7ab1-7c4f-e39b-59e9bb933b50"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("4d553c0f-da6d-794f-9eea-9775572680b0"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6397c618-7c6f-8f15-5424-7c4f68703e6b"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6f6426a6-9d21-2b5e-833e-6e05a82e9ec9"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("927a68c0-a14f-99f3-bd23-3c5e9071b900"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("9beccc58-5294-0f32-8bd6-2e00c774fa9a"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("a48d7732-d26e-af41-a467-9058c1e80e35"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("aa41bc76-0294-1770-50e1-5094947998e5"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("ac9ddfec-4168-50ad-1e4f-85213d6165cc"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("ea08cc00-f912-0a72-5a35-f5a12842cc80"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("eba4458e-932a-4aa2-efe6-53de9831f75a"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("ec5fedf6-1f79-abc4-a176-8d25abdb3efb"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("f1697231-83a5-99a2-da6a-9b2c6e98d2cf"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("fc538336-0267-537b-b467-6e83bef08d62"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("239931d1-44fb-fda4-ba64-6cb3cdc6172d"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("3376f1ac-f65e-403f-67ca-b303f202901f"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("b926ac14-8560-b45b-84d6-31851ba90b22"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("f849f565-8935-4e3e-fc3f-239ba432e4d3"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "countries",
                keyColumn: "id",
                keyValue: new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"));

            migrationBuilder.DropColumn(
                name: "address",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "country_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "district_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "region_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "sub_district_id",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role_code",
                schema: "public",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "is_new",
                schema: "public",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "is_new",
                schema: "public",
                table: "districts");

            migrationBuilder.DropColumn(
                name: "is_new",
                schema: "public",
                table: "countries");

            migrationBuilder.AlterColumn<bool>(
                name: "is_email_verified",
                schema: "public",
                table: "users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "public",
                table: "regions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "public",
                table: "regions",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "region_type",
                schema: "public",
                table: "regions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "public",
                table: "regions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "country_id",
                schema: "public",
                table: "districts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00001010-0000-0000-0000-010001000001"));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "public",
                table: "districts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "public",
                table: "districts",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "public",
                table: "districts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "public",
                table: "countries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "areas",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "description", "role_name" },
                values: new object[] { "Common User", "Client" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "is_email_verified", "is_mfa_enabled", "password_hash", "phone", "status", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "user1@gmail.com", "System Admin", false, false, "0CB47CF84CA0824A48EB7CDAD0B13AC83D6742E85A21B8A0FF58A235C2050DE9-ED1FD94795D453D2320B0A5444D4B31E", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "user2@gmail.com", "Normal User", false, false, "CDFCF4E8D89841B7A49EC50581EC9F5CA3AB0A93A9F23B78C69839B18BE43752-C4F0917170B9972DDE5015CBCFE31786", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "user3@gmail.com", "Demo User", false, false, "D3A38C51393060353567AF0865FC91B4E435AB433D177AF056F79BA1AEEADA0B-852250D8F97163710CF73F51EF6EE70D", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_districts_country_id",
                schema: "public",
                table: "districts",
                column: "country_id");

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

            migrationBuilder.AddForeignKey(
                name: "fk_districts_countries_country_id",
                schema: "public",
                table: "districts",
                column: "country_id",
                principalSchema: "public",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_localities_areas_area_id",
                schema: "public",
                table: "localities",
                column: "area_id",
                principalSchema: "public",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
