using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdressSeed : Migration
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

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "public",
                table: "countries",
                newName: "is_new");

            migrationBuilder.AddColumn<bool>(
                name: "is_new",
                schema: "public",
                table: "regions",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_new",
                schema: "public",
                table: "districts",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sub_districts",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_new = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
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

            migrationBuilder.InsertData(
                schema: "public",
                table: "countries",
                columns: new[] { "id", "capital", "is_new", "name", "phone_code" },
                values: new object[] { new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), "", false, "Bangladesh", "" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "regions",
                columns: new[] { "id", "country_id", "is_new", "name" },
                values: new object[,]
                {
                    { new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Chittagong" },
                    { new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Rajshahi" },
                    { new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Rangpur" },
                    { new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Sylhet" },
                    { new Guid("3376f1ac-f65e-403f-67ca-b303f202901f"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Dhaka" },
                    { new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Mymensingh" },
                    { new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Barishal" },
                    { new Guid("b926ac14-8560-b45b-84d6-31851ba90b22"), new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"), false, "Khulna" }
                });

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
                    { new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"), false, "Chapai Nawabganj", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), false, "Khulna", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), false, "Bagerhat", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), false, "Jashore", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), false, "Narail", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), false, "Gazipur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), false, "Habiganj", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), false, "Pabna", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), false, "Manikganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), false, "Netrokona", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), false, "Brahmanbaria", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), false, "Bandarban", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), false, "Chuadanga", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), false, "Sylhet", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("545faa51-e539-521f-4801-5237fc519c2c"), false, "Barguna", new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5") },
                    { new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), false, "Magura", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), false, "Noakhali", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), false, "Barishal", new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5") },
                    { new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"), false, "Lakshmipur", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), false, "Chandpur", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), false, "Kurigram", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), false, "Pirojpur", new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5") },
                    { new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), false, "Rajshahi", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), false, "Satkhira", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), false, "Shariatpur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), false, "Narsingdi", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), false, "Patuakhali", new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5") },
                    { new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), false, "Nilphamari", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), false, "Kishoreganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), false, "Joypurhat", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), false, "Meherpur", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), false, "Cumilla", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), false, "Jhenaidah", new Guid("b926ac14-8560-b45b-84d6-31851ba90b22") },
                    { new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), false, "Mymensingh", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), false, "Jamalpur", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), false, "Khagrachari", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), false, "Dhaka", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), false, "Sunamganj", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), false, "Chattogram", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), false, "Feni", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), false, "Gaibandha", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), false, "Moulvibazar", new Guid("1ecc38ae-d5b4-f983-9628-e13df2e50bf4") },
                    { new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), false, "Madaripur", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), false, "Sherpur", new Guid("4f488d44-0774-a9e0-3b36-2f1f9756a0b2") },
                    { new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), false, "Tangail", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), false, "Natore", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), false, "Dinajpur", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), false, "Sirajganj", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), false, "Bhola", new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5") },
                    { new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), false, "Rangpur", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("dd888605-c609-34bc-1c86-2ddd13870d8f"), false, "Jhalokati", new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5") },
                    { new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), false, "Panchagarh", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), false, "Munshiganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), false, "Naogaon", new Guid("0aedc658-2da3-7140-7b98-cc26ea4bbec7") },
                    { new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), false, "Cox's Bazar", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") },
                    { new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), false, "Rajbari", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), false, "Lalmonirhat", new Guid("1402e1f8-dfc5-de8b-f1a2-cb1295e457b3") },
                    { new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), false, "Gopalganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), false, "Narayanganj", new Guid("3376f1ac-f65e-403f-67ca-b303f202901f") },
                    { new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), false, "Rangamati", new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc") }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "sub_districts",
                columns: new[] { "id", "district_id", "name" },
                values: new object[,]
                {
                    { new Guid("000a5548-dd43-7183-57f3-0921bee7f624"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), "Baliakandi" },
                    { new Guid("0041deb6-09fe-0839-6e3f-605e313ce35b"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), "Thakurgaon Sadar" },
                    { new Guid("0164eedf-2983-baf7-d080-a59569838206"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Nagarpur" },
                    { new Guid("01730eb0-590c-6fb8-218f-c99bc7a71484"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), "Shariatpur Sadar" },
                    { new Guid("02ec1340-ab6d-a35b-b08a-b499fe018c38"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Juraichhari" },
                    { new Guid("0329ab85-5358-ac58-2535-524128fa102c"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Koyra" },
                    { new Guid("03afed44-0510-9c7d-5a57-cc0d32873bd6"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Sadullapur" },
                    { new Guid("03cf2558-40ed-f57b-0b60-8bbcac37aece"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Baraigram" },
                    { new Guid("03e7f9ed-3bc6-bd47-f1f8-821e3686836f"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Fatikchhari" },
                    { new Guid("04b3af02-a81e-896c-866a-c03a55bc858f"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), "Hatibandha" },
                    { new Guid("0560190b-c4a9-9555-2326-1255a8a96926"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Rangunia" },
                    { new Guid("059e24e1-3ea7-e000-5b65-dd60a5fe6ccf"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Begumganj" },
                    { new Guid("05aa8303-92a0-dd6c-bbe6-e0827626bf38"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), "Damurhuda" },
                    { new Guid("0635be1c-fdbb-ad14-75d2-10d7f78eb674"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Debhata" },
                    { new Guid("070a4664-bb89-2ae8-615d-ac4158a3c9a3"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Tarail" },
                    { new Guid("08268a90-1cd5-2293-0d35-5a66df9ae405"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Dighalia" },
                    { new Guid("08d64174-998c-1cde-2e5c-c5aaec01c450"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Muktagachha" },
                    { new Guid("092335d5-c82e-a1a9-108a-49de6e07f6bc"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Paikgachha" },
                    { new Guid("0a25a35c-511c-a55c-654e-e11328e6bdf2"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Dowarabazar" },
                    { new Guid("0bc80bcf-658d-fdbb-6bd5-9ac224bb35eb"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Hathazari" },
                    { new Guid("0d132a82-4f9a-2236-9e5d-9fe5125d10f9"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Birganj" },
                    { new Guid("0d13763e-aa8b-7118-8de1-23fb026a0232"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Shivalaya" },
                    { new Guid("0d1c09cf-9401-9d69-6e97-2b74af9a7655"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Sreemangal" },
                    { new Guid("0d252b70-1c37-4c73-d14c-08c660b422ea"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Kaharole" },
                    { new Guid("0ea0d040-300a-c87c-7c7f-2aa566325a0c"), new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), "Mujibnagar" },
                    { new Guid("1035b5dc-6677-2caf-30f1-ceecd15cccf7"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Madhupur" },
                    { new Guid("1050cad3-50df-56d4-ff9c-c49fee575c0d"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Teknaf" },
                    { new Guid("110218b4-933f-491a-82ff-be5eb49b496a"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Bogura Sadar" },
                    { new Guid("11516a0a-c115-e33f-878a-73b2ba38a883"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Dumuria" },
                    { new Guid("118a6e51-9af6-5fd4-6c46-f71630d3681a"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), "Kaliganj" },
                    { new Guid("118ef8ad-4fcb-272e-da8f-df2f89c8a4b4"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Banaripara" },
                    { new Guid("11f1eb4e-facf-9db1-ce14-ed41870c1665"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Chakaria" },
                    { new Guid("1205bc03-b525-4a8d-64db-3484f6d9e137"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Rajarhat" },
                    { new Guid("12061905-6e84-8c51-2c5c-64efd14767fe"), new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), "Daganbhuiyan" },
                    { new Guid("121a07d7-04bd-23a6-fe1a-b44b6921fb91"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), "Zajira" },
                    { new Guid("12344c4d-534c-0595-0533-29d093ac26d2"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Mohanganj" },
                    { new Guid("1274688e-baa4-ca72-6e2f-50c622427431"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Harirampur" },
                    { new Guid("1364ce6c-0f2f-4e6a-fb35-15b07a29bc47"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), "Mirpur" },
                    { new Guid("136dcf17-f783-7ccd-314a-5b75f6005ebe"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Nasirnagar" },
                    { new Guid("13827ed1-3ea6-e2b6-a553-660919035950"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), "Rajoir" },
                    { new Guid("13e6c95a-e1b5-7a07-9c60-93bafc8f699a"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), "Rupganj" },
                    { new Guid("14390d65-506f-2eff-b768-fdb2fa3c47f5"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Noakhali Sadar" },
                    { new Guid("144ea8c8-a90b-9612-4145-1b732beec355"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Itna" },
                    { new Guid("15136a50-3af4-490c-b9f6-669b3ba8217f"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Trishal" },
                    { new Guid("155b9e52-0b45-000f-bb05-e68f60ed24fb"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Naldanga" },
                    { new Guid("1650833e-3f03-b998-68d8-b0590690e068"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Chatmohar" },
                    { new Guid("17188831-65c8-8a22-b6dd-029a467e9a54"), new Guid("545faa51-e539-521f-4801-5237fc519c2c"), "Bamna" },
                    { new Guid("179beb38-09b2-452c-adce-3587d9d3e81b"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Mohanpur" },
                    { new Guid("18e1e762-d138-ee27-468c-bf537b058fd6"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Tara Khanda" },
                    { new Guid("1a89657a-155f-a2e4-dc85-abbf50bb5ac3"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Manikgonj Sadar" },
                    { new Guid("1b32351e-6a8e-4bc6-57ea-6b7b8f444d26"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Chitalmari" },
                    { new Guid("1b4f9dc4-9b04-1813-4dd3-07ef6c61fa92"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Badarganj" },
                    { new Guid("1c1a845f-2ef2-5e39-5b2a-01abf5c9b8de"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), "Sreebardi" },
                    { new Guid("1c2983e2-7c33-56fa-ee73-b692d883a39a"), new Guid("dd888605-c609-34bc-1c86-2ddd13870d8f"), "Jhalokati Sadar" },
                    { new Guid("1ca982f1-ce1e-4447-0f92-a00264273dda"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Galachipa" },
                    { new Guid("1d731552-f133-b02f-981d-6f3021a39db0"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Sandwip" },
                    { new Guid("1dda9b89-12a4-cdb9-8ef0-324f478223f7"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Mirsharai" },
                    { new Guid("1dec672c-6fa7-d5ca-600d-8ef0f81aa3ca"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), "Gosairhat" },
                    { new Guid("1e20c93d-6b10-3935-cd34-1a57909a4965"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Mathbaria" },
                    { new Guid("1e8e9d62-2018-2fac-e316-6fffb8322299"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), "Dhamrai" },
                    { new Guid("1f252529-e41b-4ee6-b0ac-989601cec689"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Kalapara" },
                    { new Guid("213ddbe7-3aa6-077e-5082-a400c1629136"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Kishoreganj Sadar" },
                    { new Guid("21f9e1cb-466e-fa3b-8b10-9cd189aee252"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Sarankhola" },
                    { new Guid("221dd88a-c033-1dc5-8e04-f830b64dead6"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Godagari" },
                    { new Guid("223d1038-6d9e-6aa8-2bbc-2b116327cfaf"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Singair" },
                    { new Guid("2277004e-6475-fa6f-2166-f8169c7d833f"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Jamalpur Sadar" },
                    { new Guid("22e7c60e-28c3-2890-895e-ccf2910851c2"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), "Shailkupa" },
                    { new Guid("2452584a-59e8-7306-80eb-1746a714aa58"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Satkhira Sadar" },
                    { new Guid("256cd122-093d-894b-07a5-55df1a9105a2"), new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"), "Shibganj" },
                    { new Guid("262c644b-0114-a786-c12a-4ba0c89f00af"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), "Pangsha" },
                    { new Guid("265debad-3b53-fd76-af53-c5cf972a4779"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Chaugachha" },
                    { new Guid("27d68e4c-4fce-7eb0-c2d0-e1b82542723c"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Companiganj" },
                    { new Guid("27d69f7f-f23a-012f-acde-739d55b2d7f5"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Daulatpur" },
                    { new Guid("28218d4c-121b-4e4b-c329-1ea8ff836d84"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), "Panchbibi" },
                    { new Guid("28297a1c-9ec6-c49f-debb-b01f772f9300"), new Guid("545faa51-e539-521f-4801-5237fc519c2c"), "Taltali" },
                    { new Guid("29251309-9b7e-5a28-8b02-a3059511a927"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Madhabpur" },
                    { new Guid("297ad6e0-10d7-8cf2-bf46-e471d296d70e"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Paba" },
                    { new Guid("2a7884dd-7d0b-560b-2787-d390a4a553d6"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Lalpur" },
                    { new Guid("2b5b4bd6-8b9b-69a1-442a-1305e1733b67"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), "Naria" },
                    { new Guid("2c0b7487-c0e4-abd5-ca67-b26d8a4094ac"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), "Panchagarh Sadar" },
                    { new Guid("2c622a70-1d54-8c7b-a0cb-08f886c31b92"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), "Kumarkhali" },
                    { new Guid("2e279f52-086a-f6d3-416d-6692afdfc51d"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Kalaroa" },
                    { new Guid("2e446e37-2ce2-9630-c47c-9ef8ecdc7e15"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Kamarkhanda" },
                    { new Guid("2ed65972-7cc5-54d6-18a4-63b8282132e7"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Chhatak" },
                    { new Guid("2ef1d722-4d99-35a7-22da-623e640c102d"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), "Jaldhaka" },
                    { new Guid("2f252e97-c11f-bd8c-f7a0-0291c0f8040e"), new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), "Kalia" },
                    { new Guid("303b8a89-df16-15c1-33eb-d870f604dcea"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), "Nilphamari Sadar" },
                    { new Guid("305e55a4-5f18-0b99-ecac-06076d9c9edc"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Atrai" },
                    { new Guid("32c171a4-07e8-7058-cb6c-57dd47401630"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Boalmari" },
                    { new Guid("32e771ed-3b2f-e69f-a511-4f9c2baa788d"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), "Monohardi" },
                    { new Guid("3323c23a-9cb8-9a1d-7964-dd8d5b414087"), new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"), "Lakshmipur Sadar" },
                    { new Guid("33f4d0ac-e937-e9b9-2ef2-67e9844ecbe8"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Gangachhara" },
                    { new Guid("34312048-0dd4-d52f-b3e5-e8875882c3d8"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Satkania" },
                    { new Guid("343f351a-c055-2007-fc50-8120325847f0"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Burhanuddin" },
                    { new Guid("344bd5b4-80a2-d406-3089-2e116239c713"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), "Tongibari" },
                    { new Guid("3475ea4e-4d45-89bd-fd75-281495dabe44"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Kaunia" },
                    { new Guid("34d03f97-df8c-9dda-3fd5-36cfc5da8295"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Burichang" },
                    { new Guid("34de041b-894a-37bb-3683-3d6114782d39"), new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), "Sonagazi" },
                    { new Guid("35c0b4a0-ae64-b3c3-bdf4-fe400e66c568"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Rajnagar" },
                    { new Guid("36029ac1-af0f-0554-c68f-3ee9a19978e9"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Dhanbari" },
                    { new Guid("36e284c8-19e1-ad7e-4a88-76af4ea34208"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), "Kalukhali" },
                    { new Guid("37278ba2-d9c4-2e3f-2885-d69892583e31"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), "Daulatpur" },
                    { new Guid("3743aaab-6c40-8180-ddb4-880ddccd1f4d"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Laksam" },
                    { new Guid("379603ef-0e7b-26c4-c65a-af3368e5ea87"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Lama" },
                    { new Guid("383bfe04-5d5f-4b22-3324-24f7dfb6afba"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Chandina" },
                    { new Guid("384b34fa-9f14-00ea-7a9b-89d88390f54b"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Dhamoirhat" },
                    { new Guid("384fa8e5-ac6a-c21b-26b6-70fa3460ae14"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Pakundia" },
                    { new Guid("38850f84-c2f5-36a0-d81b-d6351bc0397b"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Ullahpara" },
                    { new Guid("393d1a60-467e-731a-c8a2-18c13d608e02"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), "Kishoreganj" },
                    { new Guid("39e6b9cb-e3f8-4bd2-acee-8d8a5c3c7712"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), "Palash" },
                    { new Guid("3a4c90d6-a2a8-b721-33c8-c8fe6ee470eb"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Puthia" },
                    { new Guid("3ab078a1-a6ee-16f0-6511-7a369f6e016c"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Chauhali" },
                    { new Guid("3b347057-7f6b-23b3-f0c1-3a634d436f8d"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Kawkhali" },
                    { new Guid("3b974655-f7f7-09c6-06d7-56a4155437b6"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Baniyachong" },
                    { new Guid("3c1afb99-0312-b99b-f9da-49e81eab1e48"), new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), "Gangni" },
                    { new Guid("3c2c6df6-41b9-6ac6-6adb-e75b6260566a"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Babuganj" },
                    { new Guid("3c5dd60a-2c17-adf5-032d-500aea4c28e4"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Chandpur Sadar" },
                    { new Guid("3dd2e835-6528-161e-9be2-0a9b870b8c6e"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Naikhongchhari" },
                    { new Guid("3f8438ee-6743-b25d-b967-9673490fc906"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), "Gazipur Sadar" },
                    { new Guid("3f926ba6-df22-411f-5ac5-0d9a9918fd84"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Delduar" },
                    { new Guid("3f9a4558-0e08-bb5b-e83a-2e1b3ea3127c"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Thanchi" },
                    { new Guid("3fa1e2f0-44cf-23c9-4571-ff016b3c6612"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), "Atwari" },
                    { new Guid("3fc1c056-dae6-685f-d074-8aa55c6078bc"), new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), "Lohagara" },
                    { new Guid("3fc7d060-ad80-2c55-3e16-8a585788f12e"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), "Sreenagar" },
                    { new Guid("404a0c42-2d26-c6c3-6b9a-872934203380"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Parbatipur" },
                    { new Guid("40be78d2-47b4-5f06-9769-6fe3a65eb84b"), new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"), "Gomastapur" },
                    { new Guid("40e3cabc-2de4-15df-992f-664bd7af6ffa"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Sayestaganj" },
                    { new Guid("41a50992-d7cf-57e2-fc4f-c3c4b1b1a0c3"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Chirirbandar" },
                    { new Guid("41b042c3-6e56-e59b-c62f-fc4bc2880e16"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), "Narsingdi Sadar" },
                    { new Guid("4274483b-a3d7-c49c-90f6-fd7d8cfac129"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Raiganj" },
                    { new Guid("43c1359b-a452-d95a-a825-4193efbec975"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Nabiganj" },
                    { new Guid("43d7408c-722d-5f3c-a419-40a4f0673bc0"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), "Nakla" },
                    { new Guid("44a22ced-fe64-5fb1-d80b-6c133708f508"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Khansama" },
                    { new Guid("46a3ceea-cd33-c0e5-146f-78255c7d6fc5"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Kawkhali" },
                    { new Guid("47a6f81b-5226-eb83-8ba2-5b5c03389ade"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Sullah" },
                    { new Guid("47b48862-2138-c1e9-406d-3356dc063645"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Muradnagar" },
                    { new Guid("4835affe-883d-c79c-abdd-c9a159c12d5d"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), "Savar" },
                    { new Guid("491fca4b-ae19-3cd8-20c3-be481e72477d"), new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"), "Chapai Nawabganj Sadar" },
                    { new Guid("494020da-4d9f-eca2-24b4-0f80d1323b8f"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Lakhai" },
                    { new Guid("4953b997-2173-d934-ceb6-1f3a6fbc72b9"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), "Sonargaon" },
                    { new Guid("495b4f5a-9933-059a-233c-96503c68ba32"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Durgapur" },
                    { new Guid("496ae137-2977-7a81-58f8-19faf0a3d33b"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Bhairab" },
                    { new Guid("49dcb88e-f7dc-2d77-e0bf-285443d7668b"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Bera" },
                    { new Guid("4a6c32e8-a87a-5715-b620-4cdba19444a4"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Gowainghat" },
                    { new Guid("4a864ccd-f901-6cfd-72e2-da7c53d3648e"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Dewanganj" },
                    { new Guid("4ae3098b-2c87-d57b-f6b5-b586c1c3be59"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Kachua" },
                    { new Guid("4af87a66-5a13-b0c2-b498-33b178ea33fb"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), "Akkelpur" },
                    { new Guid("4cff3a59-8128-5ce4-018f-95d1162455b2"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Tala" },
                    { new Guid("4d8e2d55-634c-6828-e07a-7a1eae9006f9"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Panchhari" },
                    { new Guid("4f2a65a4-a72f-0692-06f1-0f4c8fbbd6e3"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Gurudaspur" },
                    { new Guid("4f724266-b32c-2be0-39ee-0db426ac557a"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Mithapukur" },
                    { new Guid("4fb93e2c-e1f5-d8c0-2796-7c9dd08cebbc"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Shyamnagar" },
                    { new Guid("4fdec0c6-e8df-3e71-6e7c-84a6b76e5e11"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), "Kotchandpur" },
                    { new Guid("50286e46-37be-ecfe-e056-8a9e65208d59"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), "Alamdanga" },
                    { new Guid("505f19d6-9b09-66c6-e31e-3f47e887f78b"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Senbagh" },
                    { new Guid("511349b4-8b89-729f-e75d-088891e40182"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Debidwar" },
                    { new Guid("51f365fa-917d-e970-f26a-4f2965095a0c"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Bagatipara" },
                    { new Guid("5230af0c-d208-d452-238f-7a028605b2aa"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Pirganj" },
                    { new Guid("523b7ec8-815a-f80e-be43-c68d96d014d7"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Charghat" },
                    { new Guid("52abf885-c7fd-daec-436d-4c50bc737b82"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Madarganj" },
                    { new Guid("536ea8c7-5874-5bd2-69d0-d4402e20496f"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Bhurungamari" },
                    { new Guid("54401e20-68b5-da27-c6ff-875dfc555a59"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Atgharia" },
                    { new Guid("5599b926-2794-42a5-dbfd-95abcf1f1436"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Companigonj" },
                    { new Guid("563de8da-a571-f7e7-0392-24385aadf2f3"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Derai" },
                    { new Guid("570ebbdc-1da7-f080-a8a6-ba6dffde9a73"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Gafargaon" },
                    { new Guid("5839a385-ddfe-7580-b4e9-0f0b7b651846"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Pabna Sadar" },
                    { new Guid("5851f2f3-8500-fd3a-8ae5-5ff89d0b6385"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), "Shalikha" },
                    { new Guid("5a9912c9-7d5e-fae1-7c85-d75d95f4dfa6"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Karimganj" },
                    { new Guid("5aa834d3-841b-6302-9419-f4a08672320f"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), "Sherpur Sadar" },
                    { new Guid("5afb28b6-663b-e3ab-71d5-8b62b28c7157"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Durgapur" },
                    { new Guid("5b7cacb8-2448-b47d-e908-2a93d549447b"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Haluaghat" },
                    { new Guid("5b8f0d1d-eeb1-bd20-a3a3-95f2f5a7bf89"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Phulbari" },
                    { new Guid("5c64e73e-4ab1-b701-453e-28d207a3358c"), new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"), "Ramganj" },
                    { new Guid("5d864207-f73a-f97c-94d4-dce573d17f2d"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Sundarganj" },
                    { new Guid("5e6683c4-f66c-80c4-e3f8-8816c55436ec"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Tangail Sadar" },
                    { new Guid("5e90b580-ff51-791f-eb7a-53b1520eff74"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), "Bandar" },
                    { new Guid("5f122b45-a4cd-5540-52a3-b81e560352f9"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Pekua" },
                    { new Guid("5f608ced-a20b-de03-585d-7fc5af4bc351"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Nandail" },
                    { new Guid("605960e0-5c26-4891-0ee0-62b61a337638"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Nangalkot" },
                    { new Guid("60b4d12d-e9ad-ac11-019c-3e4ff8c6d830"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Sapahar" },
                    { new Guid("61609177-0b09-0abd-5d42-7ff3bcbe25f4"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Sakhipur" },
                    { new Guid("61af893b-5e68-8134-4845-cb3f9988d01e"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Pirgachha" },
                    { new Guid("62a6ac17-4c5b-5fc6-5a42-c5acdf623458"), new Guid("545faa51-e539-521f-4801-5237fc519c2c"), "Patharghata" },
                    { new Guid("63b9d670-d20c-a467-c23e-439d8d525f89"), new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"), "Ramgati" },
                    { new Guid("64a08849-1cc6-dfc6-dc36-c259ba0601b5"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), "Kaliganj" },
                    { new Guid("64b86657-f7de-19d8-d7f5-3f61b456e4ad"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Bhola Sadar" },
                    { new Guid("654cf3f6-727b-cf46-9306-e1e0d6abf570"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Sadarpur" },
                    { new Guid("65ad6515-f0ab-beb3-71f7-a41531872a6c"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), "Kaliganj" },
                    { new Guid("65efd6c6-01db-a814-3e18-73df2d38e0a3"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Belkuchi" },
                    { new Guid("6845285a-b929-5df3-e437-a99dd3c6ff36"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), "Kalkini" },
                    { new Guid("6849a559-6a39-ee8e-1df7-13dac42d5103"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Sunamganj Sadar" },
                    { new Guid("686aed5d-61f0-868c-e03f-75e5957e5cbe"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), "Nawabganj" },
                    { new Guid("68b4d267-b3ad-0d3c-321e-c3ea2f036d95"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Dakshin Surma" },
                    { new Guid("68bc83c8-7561-9ed9-f186-16c69cd5951f"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Gopalpur" },
                    { new Guid("69251b6e-923e-7995-1617-f813f1f5ae72"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Birampur" },
                    { new Guid("6925b81e-8de7-c492-0673-776c38ebf2f2"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), "Madaripur Sadar" },
                    { new Guid("69e6e534-7be8-7471-ea92-700a8b4d003f"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Dhobaura" },
                    { new Guid("6a459910-1dde-03f8-47f6-5f71c265a44b"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Gauripur" },
                    { new Guid("6a69f133-48b4-7057-d93b-3b6534958eb0"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), "Magura Sadar" },
                    { new Guid("6cb34d95-454d-8939-fad9-fc93a08de40b"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Katiadi" },
                    { new Guid("6ce751a0-47e2-6096-c846-a365fa4b4e7e"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Chandanaish" },
                    { new Guid("6e3c764a-43f9-01fa-94f8-97e7e9e40113"), new Guid("dd888605-c609-34bc-1c86-2ddd13870d8f"), "Kathalia" },
                    { new Guid("6e5a31e4-7e46-34c1-9e9e-dd4497b1ad7c"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Naogaon Sadar" },
                    { new Guid("6ec4a51b-962f-7046-ca29-768241464b06"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Ramu" },
                    { new Guid("6fe37cd2-8ca8-52f9-150b-81aa9023d762"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Khaliajuri" },
                    { new Guid("7053dbac-0eb0-3986-22cd-de7dffe7b8f1"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Mehendiganj" },
                    { new Guid("7160f74f-5516-f9dc-5d12-16b4b8456063"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), "Debiganj" },
                    { new Guid("71e83078-b4ee-75f9-e20b-79aec181895c"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), "Gazaria" },
                    { new Guid("721b5203-407f-cc2c-9c29-062cb9db768e"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Titas" },
                    { new Guid("724b2933-2c88-0575-aa26-e5d9fe3042b9"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Kahaloo" },
                    { new Guid("72a7b939-d3e4-9bff-eee6-9c63f086e392"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Madan" },
                    { new Guid("72d3f138-973c-f202-882d-45cb8e9ec156"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Haziganj" },
                    { new Guid("72eed8d5-6b22-58e7-130c-2eb95cbaef72"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Dacope" },
                    { new Guid("73bcb1ca-6c8b-ccad-87b2-1634d92c35ef"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Cumilla Sadar Dakshin" },
                    { new Guid("74e11064-786a-fb1f-c28a-6d88946133dd"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Ghatail" },
                    { new Guid("75339a37-be7b-cef2-2336-e2f32af89639"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Netrokona Sadar" },
                    { new Guid("756c5011-6f9a-9019-05c0-b756c5b57e60"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), "Lalmonirhat Sadar" },
                    { new Guid("761d4475-b90f-43e7-70b8-086762896663"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), "Bheramara" },
                    { new Guid("76ac7dd6-26ce-da9a-ab74-67cc82487c69"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Chilmari" },
                    { new Guid("77f4bec1-0f57-fa56-de93-a2fb21f2b125"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Phulpur" },
                    { new Guid("78300055-f9fe-6c8c-0968-ba77f019729b"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Melandaha" },
                    { new Guid("78c96147-df87-8926-e357-5382d65f6184"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Hossainpur" },
                    { new Guid("78cd54d7-9664-d18b-48f9-10243202d410"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Anwara" },
                    { new Guid("7993d9db-9e19-f307-9828-aa58bb06cb92"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Shibganj" },
                    { new Guid("7af2de48-f15a-fbdb-a27d-4a925824c3b1"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), "Jhenaidah Sadar" },
                    { new Guid("7b07cd3b-a637-0690-5428-615e90e2bfe5"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Rajasthali" },
                    { new Guid("7b0f44fb-28e9-dca7-a7d9-6c59dce5442e"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Biral" },
                    { new Guid("7b99a45f-e69e-dea1-f3e9-b286bdfe4dd5"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Phulbari" },
                    { new Guid("7b9cc486-650d-f82b-b23d-960bfdc34e09"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Mongla" },
                    { new Guid("7bb23d03-c694-7dc9-de51-1fd92a7c4687"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Morrelganj" },
                    { new Guid("7d28fc01-e3c1-1c53-28d1-6e8805d62a9d"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), "Kapasia" },
                    { new Guid("7d3b769c-f707-c5f2-42ef-42ef463a1e15"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Indurkani" },
                    { new Guid("7ed6067e-41b7-1e1d-b319-0b711ff19237"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Manpura" },
                    { new Guid("809db6f7-5f3e-3947-c0e0-9746bc01dd34"), new Guid("545faa51-e539-521f-4801-5237fc519c2c"), "Barguna Sadar" },
                    { new Guid("80b38405-25ab-70c6-99c1-f4b8e51bf95a"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Cumilla Adarsha Sadar" },
                    { new Guid("80da5f6d-b9f4-ec41-b321-5d86a027b449"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), "Tetulia" },
                    { new Guid("81ab3583-9134-ec03-33d7-bb176cff2bf6"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Meghna" },
                    { new Guid("81d50da4-30a7-b518-5435-3555b35d88fd"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Fulbaria" },
                    { new Guid("81edd54b-85e7-1153-0f31-e2962793ebd7"), new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"), "Nachole" },
                    { new Guid("825baf08-bbb8-475b-5a0a-b2e18bbca786"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), "Munshiganj Sadar" },
                    { new Guid("828817a2-4293-ad2d-c4f6-ceaa464c3d66"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Osmani Nagar" },
                    { new Guid("82c4d163-78dd-d032-3a7c-649da8a26ecb"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Bishwamvarpur" },
                    { new Guid("841d0e5b-c69b-ac04-4796-c747f95cd62f"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Chauddagram" },
                    { new Guid("844db2a8-437b-11f8-df16-a84e9b8789f8"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Terokhada" },
                    { new Guid("849d413f-629e-c6da-f343-4ade426a6b89"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Gaibandha Sadar" },
                    { new Guid("84f33576-5b7c-1feb-7a5b-f1b69a26a09a"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), "Kushtia Sadar" },
                    { new Guid("85463bd5-3c48-c57a-77fc-4d07568866e1"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Sirajganj Sadar" },
                    { new Guid("856da358-784b-dcc0-0db1-86da37fb0b23"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Nagarkanda" },
                    { new Guid("85c31dc5-b607-8390-ec50-2d7c2231e940"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Sonaimuri" },
                    { new Guid("86bf941a-9ed2-24a2-af8d-64dc3c200972"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), "Nalitabari" },
                    { new Guid("874dc821-f638-bbfc-7024-b2a65e7c0ba1"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Assasuni" },
                    { new Guid("8768d540-b213-a83d-3887-f836b133bfb1"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), "Pirganj" },
                    { new Guid("87b4ce03-7038-798d-0083-364cf447e4b8"), new Guid("b78c0d34-ea00-c92b-567b-932701cae79c"), "Shibchar" },
                    { new Guid("88d0ae5f-8ebc-7aab-e6bc-8759c68e99d1"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Akhaura" },
                    { new Guid("8950a212-9284-2c12-dae5-f7fa367502e2"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Raozan" },
                    { new Guid("8a5aa27f-32af-0069-c0ec-fbb522514d9d"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Habiganj Sadar" },
                    { new Guid("8b5f1641-6c38-3acb-75a6-9ad2ffc1b89d"), new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), "Parshuram" },
                    { new Guid("8e4b45eb-a2f0-dbbb-10e7-4da1c8f77549"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Nesarabad (Swarupkati)" },
                    { new Guid("8e9421e0-ad46-46e4-40c1-5ea36d522f71"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Nabinagar" },
                    { new Guid("8fee7b3b-7051-711c-d57c-13adf81c8afa"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Hatiya" },
                    { new Guid("900c4601-c129-57cc-4dc1-95d50c915dc0"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), "Rajbari Sadar" },
                    { new Guid("90e8a9ca-28f1-0518-e839-d97a7e76b55d"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Ukhia" },
                    { new Guid("913366b4-2ba2-dc67-3233-4947b5a69fa9"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Bagaichhari" },
                    { new Guid("9163e163-54f1-116a-9fe1-6bf91a3b9aaf"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Tahirpur" },
                    { new Guid("916cb24f-880b-55ad-ea15-031e848f45bb"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Keshabpur" },
                    { new Guid("91b40663-9e66-5707-a7fa-ae75f88c8503"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Rangpur Sadar" },
                    { new Guid("92b00402-65c3-7b0f-ccf6-e19d79ad3b2f"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Porsha" },
                    { new Guid("92b6c1c5-96ea-8585-feb7-96e68f525bbc"), new Guid("dd888605-c609-34bc-1c86-2ddd13870d8f"), "Rajapur" },
                    { new Guid("93040219-c90b-2e09-450c-1d44f9b99bad"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), "Maheshpur" },
                    { new Guid("933f58d6-d188-e671-730f-0c9c13fde7ab"), new Guid("545faa51-e539-521f-4801-5237fc519c2c"), "Amtali" },
                    { new Guid("935c53ea-2d1a-109a-8f43-793a36f74863"), new Guid("e00de0d2-bf91-a269-3353-d73d745cef16"), "Boda" },
                    { new Guid("9446468f-ce69-2c4c-193d-9397805a9182"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Juri" },
                    { new Guid("94507861-c3c1-7608-660e-e415e69b7f28"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Jagannathpur" },
                    { new Guid("9488185d-3046-8094-7760-548a8ff98817"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Kurigram Sadar" },
                    { new Guid("94cdc571-32e7-cefe-6cdc-6a4a3199b0fb"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Phultala" },
                    { new Guid("94f28d7a-b2c1-b265-d305-0c53a9cc6a65"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Zakiganj" },
                    { new Guid("954dc63c-e65b-bd53-6000-22b62f89148b"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Islampur" },
                    { new Guid("95a8cf7f-7ec5-2eb9-5ce2-724b1de8c530"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), "Aditmari" },
                    { new Guid("95d33f03-98e8-ed8a-46dd-d2d70c8c09b4"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Chatkhil" },
                    { new Guid("95e8fac3-a1b9-ae75-852f-c422940742af"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Mahalchhari" },
                    { new Guid("96687724-b497-752a-cdd2-80daf0121a32"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Kanaighat" },
                    { new Guid("96d11de2-9e62-c5b5-2147-331ed944d095"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Barisal Sadar" },
                    { new Guid("96f0dd1e-0f36-ca5b-4536-e1526cf76080"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Manikchhari" },
                    { new Guid("9759d391-bd58-b5d2-9e4e-cb372061c007"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Kulaura" },
                    { new Guid("9778473c-292f-76c8-403f-bdc3cfb2a1ed"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Kamalganj" },
                    { new Guid("978ef25f-9e13-f080-f304-f7cb8eadec9c"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Bandarban Sadar" },
                    { new Guid("97d2d941-fe27-3b56-4218-f219eef31caa"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Agailjhara" },
                    { new Guid("97d666e3-e248-7785-0428-9da4a1df27fe"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Sonatola" },
                    { new Guid("990e8ccd-2c8b-0e7e-b644-2036adf7af12"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Bhangura" },
                    { new Guid("9961520c-2dff-879a-9ee7-c09218d9a285"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Balaganj" },
                    { new Guid("9992079c-e9f6-8034-b6d3-43104c8daa77"), new Guid("2ab1a8f7-aa97-25ee-29d6-8415901cee49"), "Narail Sadar" },
                    { new Guid("9a0a698b-6193-6261-9c95-a84066eedfc8"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), "Chuadanga Sadar" },
                    { new Guid("9ac73591-f6e3-5868-98ad-bafefcf21316"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Raninagar" },
                    { new Guid("9ad6e546-55b6-9cd8-613f-228bf3f08735"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Boalkhali" },
                    { new Guid("9b178315-53b9-95a8-9845-14b00d394bcd"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Ramgarh" },
                    { new Guid("9b830c9d-8c19-88af-f147-5d5c61158e1b"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Mollahat" },
                    { new Guid("9bee51ee-a67a-450a-a98a-e9f00367221f"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Ishwardi" },
                    { new Guid("9c134fb9-45f0-7a6c-0df2-874aa1108d43"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Dumki" },
                    { new Guid("9c39cb1b-16be-1b28-536f-b395a3c9006e"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Bhuapur" },
                    { new Guid("9c677a01-d670-364e-ae3e-704139f76901"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Adamdighi" },
                    { new Guid("9d46be49-6061-afd0-e58a-adec18063e2f"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Ali Kadam" },
                    { new Guid("9d55936c-64d0-3de4-46de-9fb5a6665480"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Fenchuganj" },
                    { new Guid("9d600d31-0d2d-defb-ce7b-5954be09f1c8"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Kachua" },
                    { new Guid("9eec4858-a83b-1daf-b04b-f742fca5878b"), new Guid("07912d8b-f3d2-78c3-0f69-2314a65fee21"), "Khoksa" },
                    { new Guid("9f188d0f-53fd-f0c4-1b30-f51a0c54deca"), new Guid("f55c9f44-e97d-9197-21f8-fb3efb76b4e3"), "Goalandaghat" },
                    { new Guid("9f1dedd1-34f2-2a8b-0165-16f5bbd524c7"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Sariakandi" },
                    { new Guid("a007c6b3-f680-9ce9-08e7-7f273abce6ea"), new Guid("45d33529-fef9-2dd3-f2a8-bf4090d5377c"), "Jibannagar" },
                    { new Guid("a0148de2-970c-f845-19f7-64f12b72eaca"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Bagherpara" },
                    { new Guid("a059f7b3-3070-9964-5932-1312e1f90d03"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), "Kotalipara" },
                    { new Guid("a11df1ec-7c0d-d5f3-0791-4244c6d93b7f"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Patnitala" },
                    { new Guid("a16e578b-7d8a-73c2-4f9d-e915f86ecc26"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), "Narayanganj Sadar" },
                    { new Guid("a1c9ec0d-68ff-4c0f-f71d-5420e78c4553"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Kasba" },
                    { new Guid("a1ca3029-00a7-8627-1df9-d26b0624e749"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), "Belabo" },
                    { new Guid("a20c90e0-1486-2982-7d6b-4faf134e6347"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Matlab Dakshin" },
                    { new Guid("a221cdd3-7a26-d5ce-e85d-fcbcffbd743c"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), "Kalai" },
                    { new Guid("a264bfe4-476f-442d-70a8-0b9513614c62"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Jamalganj" },
                    { new Guid("a2b91187-e9fb-d9c7-4f0b-e3e479b05549"), new Guid("f6ebfeee-1227-3b4c-ac79-7d2bb383af27"), "Patgram" },
                    { new Guid("a3176e53-4064-d225-f0fc-dfc1de17ea06"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Batiaghata" },
                    { new Guid("a3927475-2916-6eb9-c8c6-8d1ffeba485e"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Sarishabari" },
                    { new Guid("a443a1f4-5fe7-b02a-ade4-037740efa645"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Gobindaganj" },
                    { new Guid("a448fe20-5a06-399d-f7bd-4aece108c720"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Sherpur" },
                    { new Guid("a48268c5-5f06-8e32-1ce6-d6732690dfe4"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Ghior" },
                    { new Guid("a49a5b4e-f197-8286-c96b-7104837fd767"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Sharsha" },
                    { new Guid("a4b06889-ea98-afbf-6014-32c94a389ad5"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Banshkhali" },
                    { new Guid("a5b849db-a819-7d12-3d3e-1b49ec79b1a1"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), "Tungipara" },
                    { new Guid("a60b8890-77b3-4839-9255-2bd7f64b5ace"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Natore Sadar" },
                    { new Guid("a6df2b7d-b5c4-7596-918e-f4b12ea3ae3c"), new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), "Chhagalnaiya" },
                    { new Guid("a6ee814b-134b-0bff-29fa-cf4f65e37c22"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), "Ranisankail" },
                    { new Guid("a74a1c32-f6e0-b618-6d56-49bf2c4bd813"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Niamatpur" },
                    { new Guid("a7ed4347-bda3-ef29-94c6-aa04927d643c"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), "Sreepur" },
                    { new Guid("a80e5621-66d9-5af5-1de5-f17c3df5c53b"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Basail" },
                    { new Guid("a8ec9478-0914-fb80-04aa-b84f43fe8dc0"), new Guid("b97eac6f-652d-1b3e-628d-fd5caa765722"), "Jhenaigati" },
                    { new Guid("abc6511b-199a-c63e-465d-6c9b7dd96cd5"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Moulvibazar Sadar" },
                    { new Guid("abd1a1ab-b281-ed17-aafc-67903e7bf878"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Dighinala" },
                    { new Guid("ac894bce-024d-38de-2eed-9862eabc00af"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Matlab Uttar" },
                    { new Guid("add3617a-3114-a9a1-80df-7d8eec20bfc0"), new Guid("6e36f859-9516-4847-1c0b-186f2c7572b0"), "Kaliganj" },
                    { new Guid("ade69fba-bb25-f1c5-5ca2-3f841e795e5b"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Hizla" },
                    { new Guid("adec1410-d861-cff6-c955-6bef3b504070"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Barhatta" },
                    { new Guid("ae0a702d-1e86-4fd5-fdb6-df50fb92052b"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Daulatkhan" },
                    { new Guid("af37ffa9-960d-b513-3751-e6d6577d6240"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Maheshkhali" },
                    { new Guid("af521dc4-455b-8103-bb83-24c3f3c4c0fb"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Subarnachar" },
                    { new Guid("af845a80-e7d4-b5a8-4e4d-f7961b6be029"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Bagha" },
                    { new Guid("afb7a893-475d-a319-f6d9-a5e9ab8570df"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Santhia" },
                    { new Guid("afcc39c5-71c0-e59f-9ca7-05b801a82dd1"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Kaptai" },
                    { new Guid("b094296a-a196-e86f-bf63-4c79ec5e7627"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), "Raipura" },
                    { new Guid("b28070d5-026c-723f-9b08-7b053e877589"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Bakerganj" },
                    { new Guid("b3002add-4e49-98f6-e905-545cec7fde66"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), "Keraniganj" },
                    { new Guid("b3223dda-99df-8bf9-e331-a7cd6f671f92"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Sylhet Sadar" },
                    { new Guid("b3726244-f965-b3d0-8f53-e05a0f3a19fb"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), "Muksudpur" },
                    { new Guid("b3fc068b-ea28-15ff-ac75-255a3909e329"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Manirampur" },
                    { new Guid("b433e01f-fcae-1ae7-7ed6-d212710ba434"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Sarail" },
                    { new Guid("b49e5678-358c-560f-a4b8-1ad7c937f2ea"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Charbhadrasan" },
                    { new Guid("b4b3a982-4e27-1013-647c-6bff688e2983"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Jhikargachha" },
                    { new Guid("b4c36f4d-05bd-99e7-d7a7-48b6f9c496eb"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Abhaynagar" },
                    { new Guid("b52be133-d6fb-2ca8-0bf2-439668e3fe46"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Jaintiapur" },
                    { new Guid("b66a341b-99cb-8091-726f-69bac2d267c8"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Nikli" },
                    { new Guid("b6cfade7-533d-0bf6-61a0-9ef1dfef9f65"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Lohagara" },
                    { new Guid("b6ff3efc-a3da-dfe5-3084-90b515d595b5"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Bagerhat Sadar" },
                    { new Guid("b7bd23d0-d319-eee6-0b24-222c2e3796c6"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Bauphal" },
                    { new Guid("b7c18501-e2aa-1a08-be28-83c0bde45e86"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Tanore" },
                    { new Guid("b7d18a12-817f-bd8b-6660-c3dcc55278b9"), new Guid("545faa51-e539-521f-4801-5237fc519c2c"), "Betagi" },
                    { new Guid("b998456b-fff5-b777-c393-5e5a9ac86464"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Saltha" },
                    { new Guid("b9d7cdf2-d245-630a-18fb-95178d8a206d"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), "Joypurhat Sadar" },
                    { new Guid("b9f6b2f4-2458-64c8-a1d4-9f040f53493b"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Dhunat" },
                    { new Guid("ba76505c-905d-9fa1-856a-3269b037a71b"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Manda" },
                    { new Guid("bb827458-2417-0822-f475-dfbbbb5fa7ed"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Madhukhali" },
                    { new Guid("bbca4e81-ecfb-388d-634a-5c68470b4874"), new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"), "Bholahat" },
                    { new Guid("bc725aa8-bb84-f7bb-7db1-774f30a64088"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Ishwarganj" },
                    { new Guid("bc725c3d-693f-07e6-b8c2-d125fd1dc2d3"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), "Lohajang" },
                    { new Guid("bc7f576c-c4d0-c2ef-efa4-fb1cf93d9990"), new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"), "Raipur" },
                    { new Guid("bd74745f-5d94-e9c8-ec4a-4f9e5b431aeb"), new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"), "Araihazar" },
                    { new Guid("bd9a57f6-5fc0-dbe0-5d05-c89b3d2aafe5"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), "Baliadangi" },
                    { new Guid("bea2c11f-17ab-8d45-491e-fd1933f8e472"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Naniyachar" },
                    { new Guid("bebb9337-50ee-08d7-a9ea-30b4579a7d31"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Char Fasson" },
                    { new Guid("bfa59874-4ba4-5d31-0416-438747982e6d"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Bhanga" },
                    { new Guid("c035110b-0779-2fdf-e67c-1b564b14a5b7"), new Guid("8aea12c0-ee83-7c9b-23de-2082de358c9b"), "Harinakunda" },
                    { new Guid("c059a819-fd5e-8e70-d792-9b9a33855aba"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Raomari" },
                    { new Guid("c14b02ca-898a-0d09-a5b8-fcab0549e607"), new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"), "Baksiganj" },
                    { new Guid("c165789e-716f-0487-dc14-11651144c404"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Beanibazar" },
                    { new Guid("c1c5756a-2f42-7473-c92d-e1fa7cc91222"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Gabtali" },
                    { new Guid("c1eb1c03-2255-9b7a-0887-2fc077a807a7"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Faridpur Sadar" },
                    { new Guid("c336e4f0-94a9-424e-6fd0-13d2a2bfab7b"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Belaichhari" },
                    { new Guid("c33e07af-1106-cc82-1181-ad46217afd37"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Purbadhala" },
                    { new Guid("c46783c0-cdca-7d63-89da-7e56979e0eec"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Monohargonj" },
                    { new Guid("c496d83e-5934-b059-c6cd-822ba3abd2e6"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Badalgachhi" },
                    { new Guid("c49b5a07-9380-8717-8418-54988ed812e0"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Khagrachhari Sadar" },
                    { new Guid("c4bb31c6-7dbb-8f72-f9f8-3474ffbabe57"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Palashbari" },
                    { new Guid("c4edc3c6-4320-5d97-8c8e-093c6a935ed2"), new Guid("810e35c2-a4ed-2132-c6d9-9ba5bd05d3ce"), "Meherpur Sadar" },
                    { new Guid("c5edb94a-9668-1a8a-f37b-ec7d9231d81b"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Nawabganj" },
                    { new Guid("c644cc88-7f96-6b88-7561-56a8dd72f12c"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), "Bhedarganj" },
                    { new Guid("c7105d82-443a-c0c9-1779-0abb3f01b6be"), new Guid("6be765bc-e9d8-a385-67a4-02d2fecea93e"), "Bagmara" },
                    { new Guid("c7332c24-9642-3f6c-459a-8171accb399a"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Matiranga" },
                    { new Guid("c739f18d-953e-8699-182b-f696d09db03e"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Sitakunda" },
                    { new Guid("c77cdddb-0e20-3a0c-236f-915ab05cb89e"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), "Dimla" },
                    { new Guid("c84d0dd9-5ce3-9a08-7838-e9e66118ce3a"), new Guid("e7fa3693-185a-5cb3-48fa-2aa5012acd45"), "Mohadevpur" },
                    { new Guid("c9177470-60f1-002e-0946-5536c40bccde"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Mirzaganj" },
                    { new Guid("c98b3263-b392-bf11-49cf-8fe04f8f66c0"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Rangabali" },
                    { new Guid("c9c4cc89-72f1-9862-cf6d-a66a2cbb0645"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Phulchhari" },
                    { new Guid("cc10f43f-b27c-d984-2dc6-b7b858b10895"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Lalmai" },
                    { new Guid("ce1a7b03-f68d-07ae-b57a-cebb81bac738"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Shahjadpur" },
                    { new Guid("ce8cd570-43bd-30df-5c23-c0f0c024ecba"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Bishwanath" },
                    { new Guid("cecb81f4-373f-a0e8-4b1d-e8d9e780dc05"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Dashmina" },
                    { new Guid("cece2116-31c9-8edb-7c45-6f24f7a34d95"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Kalmakanda" },
                    { new Guid("cf7bcc70-3c14-ba17-c78b-c7f4c50dc976"), new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"), "Lakshmichhari" },
                    { new Guid("cf7ca879-3718-1f4b-d60d-b895ee4e3077"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Hakimpur" },
                    { new Guid("cf9702d4-e3d6-3b14-14cb-8c01a8b30e05"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), "Saidpur" },
                    { new Guid("d0e76a73-afa5-a888-db27-1c741144db1d"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Cox's Bazar Sadar" },
                    { new Guid("d285f312-0460-9bbe-b307-af4a2342e804"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Rowangchhari" },
                    { new Guid("d33370d1-99ef-c097-37f6-d5d121de1bcd"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Langadu" },
                    { new Guid("d35a4e43-ab1a-1c7d-232b-a3997b1924ee"), new Guid("731e1f8a-95a5-66aa-9c38-06b2295aec79"), "Domar" },
                    { new Guid("d3d85da0-2c4e-81e7-c75a-16de86f2a5ce"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Fakirhat" },
                    { new Guid("d3e228c8-1ab1-b753-a837-ac16dcb88b9c"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Karnaphuli" },
                    { new Guid("d408422a-f86e-8a7f-f643-2124074800e5"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Austagram" },
                    { new Guid("d438b671-31a5-a99f-38f1-ad3b784a68c9"), new Guid("7d577f87-303d-5a6f-1dd1-42f38c4f4e21"), "Khetlal" },
                    { new Guid("d4801fe0-a413-cbc5-46b5-c9dfb95e4902"), new Guid("4b39b23b-12a2-9a35-2922-a864e142cfe2"), "Golapganj" },
                    { new Guid("d540fe5d-8b74-d130-eaa3-da4593159175"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Daudkandi" },
                    { new Guid("d54fa408-4f2e-28e7-c142-1e3384cae727"), new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"), "Dohar" },
                    { new Guid("d5d6e3b8-a590-47ae-934a-edfa497df9c9"), new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"), "Patuakhali Sadar" },
                    { new Guid("d62f2638-9a4c-20e9-3732-2825ef2b9472"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Rangamati Sadar" },
                    { new Guid("d6b01d8c-a466-6db3-909a-58bebf4c445f"), new Guid("238bec1c-42c3-41e7-9dbc-34aa427d19eb"), "Jashore Sadar" },
                    { new Guid("d6ef626d-f583-44ce-9142-35eae200cce3"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Bijoynagar" },
                    { new Guid("d702307f-1df4-5c12-1a6b-82ac2e5d7e6b"), new Guid("b6d240a6-2e9c-6ac8-7d9b-befaa3c5867c"), "Sughatta" },
                    { new Guid("d77136bb-ebcc-dbbd-3573-66cef8aa9bf0"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Kendua" },
                    { new Guid("d7fceda6-0076-c0f2-7815-986aeccf16c6"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Dhupchanchia" },
                    { new Guid("d8254169-acee-8303-8b0a-be7ac995eec0"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Bhandaria" },
                    { new Guid("d8d5c74d-f547-84d9-a1cb-e5eb970adf81"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Tarash" },
                    { new Guid("d8e0bbdb-0bd4-7f52-1f30-7fcf3ce38fb9"), new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"), "Barkal" },
                    { new Guid("d94024da-2272-f3d7-20f7-0e8010c4b378"), new Guid("e52f1475-0eda-385a-bc3e-0275217579ac"), "Sirajdikhan" },
                    { new Guid("dabf22a1-43d8-2a88-4949-1edabb86a40f"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Kalihati" },
                    { new Guid("db4a0888-fb44-9e1f-db2d-938c23f91cb5"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), "Sreepur" },
                    { new Guid("dbafa463-f875-e0bb-877e-0b14b6881d40"), new Guid("b723d037-c6ad-ca55-9d27-f960bbf7d25f"), "Barlekha" },
                    { new Guid("dbf7d56b-aff0-75fc-d4dd-3da4b67f5ed4"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Nageshwari" },
                    { new Guid("dd618d14-a19f-64b2-a824-e6ce5970eb91"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Ulipur" },
                    { new Guid("de3c9a94-ea69-55b6-2328-1140b7dab5b2"), new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"), "Ruma" },
                    { new Guid("de6aa7ef-1603-2db7-af2b-eef66a2505fb"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Kuliarchar" },
                    { new Guid("de84db3e-b608-7098-4e7e-a2a59553746c"), new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"), "Mohammadpur" },
                    { new Guid("df0f6046-3895-1a35-1d42-95948c395b6d"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Sujanagar" },
                    { new Guid("df8fb7e8-48dc-41ba-88cf-6df518492a71"), new Guid("6f1fe228-c46c-592f-92f5-c63f351f39bc"), "Shibpur" },
                    { new Guid("dfb07b40-0af4-6d14-9133-e8b879e6df0b"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Lalmohan" },
                    { new Guid("e0f65b96-d2ef-ec07-a9ca-9622412872c0"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Bancharampur" },
                    { new Guid("e1538f4c-b30b-5726-4790-7b5ffc963e03"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), "Gopalganj Sadar" },
                    { new Guid("e2fff838-d2f3-292c-ed6b-422dd554c533"), new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), "Fulgazi" },
                    { new Guid("e411459f-f4d0-fed8-1945-e80e1ece10dd"), new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"), "Char Rajibpur" },
                    { new Guid("e66f4370-c4ba-b3cd-c688-a363b8a6d53d"), new Guid("38896f5f-1bf1-adb6-451d-dc3a7bf301ab"), "Faridpur" },
                    { new Guid("e6d9dd81-6cd0-59a6-859f-f6f86c120fc8"), new Guid("b9f43e62-acea-f038-d0ba-34fb2d5f7e50"), "Mirzapur" },
                    { new Guid("e6e894f8-5a61-f55d-aa91-18112652bcc2"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Ghoraghat" },
                    { new Guid("e72ebda9-49a1-3540-d260-9c10769846c5"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Ashuganj" },
                    { new Guid("e831c8c2-af31-2981-4c46-dd998b4c6982"), new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"), "Feni Sadar" },
                    { new Guid("e8535b6e-d135-8baf-ade3-3bd77d1bd0b8"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Bajitpur" },
                    { new Guid("e85edc42-5323-1c31-6561-58ee282fa8e4"), new Guid("c8ea5991-6389-e469-7d15-8b7a5644a005"), "Singra" },
                    { new Guid("e889f295-3384-b6c3-9a24-10793a7f19cd"), new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"), "Kutubdia" },
                    { new Guid("e89f4d70-a7a3-244d-2605-b7e7dddc2025"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Gaurnadi" },
                    { new Guid("e99c7c8f-b8c5-e992-3383-2cea0afb641e"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Dinajpur Sadar" },
                    { new Guid("ea700e18-14a0-03b9-790f-978490beda70"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Nandigram" },
                    { new Guid("eb95e5aa-9cb0-c81e-f59d-8ea8af9ee93d"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Faridganj" },
                    { new Guid("ec0cfea4-fd91-23ed-c0fc-e9be23f53f3c"), new Guid("394565c7-1ff4-6d44-88c5-6740a6b83e3a"), "Saturia" },
                    { new Guid("ed5278fc-7073-25b3-b840-ac8b59361679"), new Guid("6e9dd53c-7d4b-f539-4026-4d0dc40da777"), "Damudya" },
                    { new Guid("ed6392c3-68be-9dd3-e42d-4ab65b1e7099"), new Guid("f8cce496-c817-1eaf-34bd-0940c19fce52"), "Kashiani" },
                    { new Guid("ed6501bf-1f3a-119d-3574-a1a47025ae58"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Mymensingh Sadar" },
                    { new Guid("edac5235-8fab-5ba8-0280-9a9d47f68e94"), new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"), "Patiya" },
                    { new Guid("edb89670-e597-21f5-0956-44ba67c1552b"), new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"), "Taraganj" },
                    { new Guid("ee6039cb-8be5-a75e-ba69-b29c11fa7e86"), new Guid("dd888605-c609-34bc-1c86-2ddd13870d8f"), "Nalchhiti" },
                    { new Guid("eec9ff7e-a2f1-8799-b82d-467914da30bf"), new Guid("21f8ceb9-d3cb-8144-215b-4a2c4d266d6f"), "Rampal" },
                    { new Guid("ef2f782d-53a6-0702-e9a9-04838bbbf002"), new Guid("77fbd028-9d7a-65e4-f444-d6b35de2ca81"), "Mithamain" },
                    { new Guid("f02d9078-c3cd-8c73-a749-0b83b9b7caad"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Ajmiriganj" },
                    { new Guid("f071f30a-31ac-bff8-cdf7-cc2009448d43"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Brahmanpara" },
                    { new Guid("f351286b-0805-a0a2-1bb3-64bfa54ee4f1"), new Guid("3a8df94b-1bbc-633a-96b1-15b44ccb95bf"), "Atpara" },
                    { new Guid("f4954c5a-7d09-c83c-3a75-22a98e66e4a5"), new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"), "Kabirhat" },
                    { new Guid("f4c4b0ad-9174-00f5-ffb7-c027d85594f9"), new Guid("162945d3-aac6-0e74-7458-5eee83fce942"), "Rupsha" },
                    { new Guid("f4d16298-90ca-50db-fc6c-d5cce3bdede9"), new Guid("0a25b049-a107-03a6-b771-58a94d1ff5c4"), "Haripur" },
                    { new Guid("f686ed1e-bfb5-3b23-ad92-39190806452d"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Bahubal" },
                    { new Guid("f6d044cf-f992-376a-ef22-3e988723b61c"), new Guid("3166c5b2-d1e6-c75c-7382-db86d07270a3"), "Chunarughat" },
                    { new Guid("f7239828-56ee-3f5a-1891-93e046477873"), new Guid("0a05ee01-fb60-1878-5ae9-c279c0727b6f"), "Shajahanpur" },
                    { new Guid("f7650516-e35d-1909-c54e-6a6601bb31fb"), new Guid("051d19aa-01de-53dc-87b9-91a2d7c5fda0"), "Alfadanga" },
                    { new Guid("f7a04657-b77d-6c24-ac83-212957a2be2b"), new Guid("cbc430ea-f24b-8556-31c4-bb59754f003a"), "Kazipur" },
                    { new Guid("f7a11a44-560b-59a9-5983-a93e31b24d8a"), new Guid("8c679bcf-b41c-7740-7ecf-7ed946a187ab"), "Bhaluka" },
                    { new Guid("f8758abc-9879-bf54-3a2f-a6e0f65b9c98"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Homna" },
                    { new Guid("fab724bf-0ffa-a920-f878-4d4312bf0d19"), new Guid("2b0c07c6-2559-a098-1286-27b3ab33f3b5"), "Kaliakair" },
                    { new Guid("fbcb2e57-2c03-5eea-fc76-3d7438ed1c7b"), new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"), "Tazumuddin" },
                    { new Guid("fc08bfb3-4f66-bed0-7d94-2197d466cffe"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Dakshin Sunamganj" },
                    { new Guid("fc3aa99a-f782-5a70-8717-3d0d46e02bfd"), new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"), "Dharamapasha" },
                    { new Guid("fd1adccb-e5fa-45ac-986f-579fe08d2836"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Haimchar" },
                    { new Guid("fde4f919-9fb8-cc3e-a6e1-eaa64c0101e3"), new Guid("cb795c14-c9ff-faf8-2884-4ddf4a799fa9"), "Bochaganj" },
                    { new Guid("fe045475-fe7c-f33f-8144-33a1d24e067e"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Nazirpur" },
                    { new Guid("fe088e55-1129-4de8-5742-6e488c6f4f9a"), new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"), "Shahrasti" },
                    { new Guid("fee48b1a-2ee9-2a4e-6e18-3004900e0eb3"), new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"), "Kamalnagar" },
                    { new Guid("ff308730-c165-f447-4f6f-8eb4ebf2ad31"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Wazirpur" },
                    { new Guid("ff56f787-6049-6ede-4033-de3e0f46d39d"), new Guid("58131dd9-a1be-4460-f455-d375708be7f5"), "Muladi" },
                    { new Guid("ff773926-4525-9540-2f06-ffea3d132abd"), new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"), "Pirojpur Sadar" },
                    { new Guid("ffa36f63-cf97-c2e3-62f8-faecfaceb1c6"), new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"), "Brahmanbaria Sadar" },
                    { new Guid("fff09475-97c9-d5ab-f95c-19473cd1624e"), new Guid("822efe31-5931-a3ec-b243-17de4e04c560"), "Barura" }
                });

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
                keyValue: new Guid("0d125f33-1bf7-27e3-828d-b468c44fd2b3"));

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
                keyValue: new Guid("41700242-8007-a7bb-2c9a-4e80df143e48"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("4365d783-aa5f-19bb-e78b-1ce78402440f"));

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
                keyValue: new Guid("545faa51-e539-521f-4801-5237fc519c2c"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("54d2d149-dd72-1597-d98f-fff64d7db299"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("578243fd-ddf6-2766-25c8-befb5d5bafe0"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("58131dd9-a1be-4460-f455-d375708be7f5"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("590b6be0-bb00-3fa2-2bc1-03bec6b5beec"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("5ec19d16-f475-6d79-78a7-54bc609dce4a"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("62b4904d-b8bb-f043-612d-e328b835ba10"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("6869b3c4-e123-4d2d-b366-f19e485d7002"));

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
                keyValue: new Guid("72df421e-f97f-05b8-64df-83fe1d8df45c"));

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
                keyValue: new Guid("822efe31-5931-a3ec-b243-17de4e04c560"));

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
                keyValue: new Guid("94849018-8928-d25d-137f-8d5e698ff3d3"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("a496cd53-c652-13e8-1476-9643b0bd72ba"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("a90251ed-98b4-2959-58d2-7192dcb96d62"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("af91cb40-e4b8-d277-5fd9-5648720d213d"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b260cb77-d4cc-dae5-5a3f-6980965e1a48"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("b378b1d3-bee5-1814-6031-6263d7e04670"));

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
                keyValue: new Guid("cec2411d-b2c5-dcb5-66b9-a44e4c7f4979"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("d116efa0-608a-a8c6-3b46-32525d2cce34"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("dd888605-c609-34bc-1c86-2ddd13870d8f"));

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
                keyValue: new Guid("f08d313c-7b13-4019-a6b7-c1870659d2ae"));

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
                keyValue: new Guid("fd8f25bf-77a6-6665-9cf0-4c45c1ac657c"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "districts",
                keyColumn: "id",
                keyValue: new Guid("fec95ca4-426d-9452-205c-533f37ca5db9"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("02b6235a-64ba-a2be-54d4-49a6f52cfbbc"));

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
                keyValue: new Guid("a9f852d2-cfb5-7c22-ec1e-3feee93570c5"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("b926ac14-8560-b45b-84d6-31851ba90b22"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "countries",
                keyColumn: "id",
                keyValue: new Guid("5c0c81b8-d88d-36dc-d47b-e8866a03a755"));

            migrationBuilder.DropColumn(
                name: "is_new",
                schema: "public",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "is_new",
                schema: "public",
                table: "districts");

            migrationBuilder.RenameColumn(
                name: "is_new",
                schema: "public",
                table: "countries",
                newName: "is_active");

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
                nullable: false);

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
