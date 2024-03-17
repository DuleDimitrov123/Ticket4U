using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var ZdravkoColicConcert1ExternalId = new Guid("93829cd6-c1f1-43d4-9ca0-31d9237bae99");
            var ZdravkoColicConcert1Id = new Guid("b29306df-c4aa-4573-87d5-f0186f192eef");

            var ZdravkoColicConcert2ExternalId = new Guid("0f4c6f53-1cca-4d62-bcc8-a4797d2e0334");
            var ZdravkoColicConcert2Id = new Guid("ecea3e94-e781-4ad0-8f5a-1933b6ad4df4");

            var ZdravkoColicConcert3ExternalId = new Guid("86ccbb64-4245-4ff8-ae13-58371d26ffa7");
            var ZdravkoColicConcert3Id = new Guid("6d2c4a3b-add0-4d25-95a9-8e5f954d0a10");

            var ZdravkoColicConcert4ExternalId = new Guid("e1f48bf0-ff7c-4485-87cb-c1401fadd87f");
            var ZdravkoColicConcert4Id = new Guid("b4996258-653c-4ccd-b2a0-08f1f569f998");

            var ZdravkoColicConcert5ExternalId = new Guid("c3463d20-e728-4641-a149-a0df732e068c");
            var ZdravkoColicConcert5Id = new Guid("7cf5099f-e474-4dc0-b970-23eb1aff21ab");

            var RafCamoraConcert1ExternalId = new Guid("44977503-4f90-46d3-b306-9704c036d086");
            var RafCamoraConcert1Id = new Guid("8fee6822-54b7-432f-9681-901266d42a65");

            var RafCamoraConcert2ExternalId = new Guid("06b524eb-0fae-4dd0-b69a-75c3a778e53a");
            var RafCamoraConcert2Id = new Guid("3603dc14-e7ac-42a6-a976-8f896eb03656");

            var RafCamoraConcert3ExternalId = new Guid("e5dd71a0-8ef4-4b18-af7f-56d5644a3b45");
            var RafCamoraConcert3Id = new Guid("e839cc12-b4bf-40e5-8a2a-f3edae545247");

            var RafCamoraConcert4ExternalId = new Guid("b3d99114-e03d-4f78-95fd-f7e86f7fa49c");
            var RafCamoraConcert4Id = new Guid("31a0d6b7-939e-4dae-a045-15983de06b37");

            var beogradskoDramskoPozoriste1ExternalId = new Guid("7f64a219-a0b0-4500-ba3a-4b32f6d8b577");
            var beogradskoDramskoPozoriste1Id = new Guid("95230bf1-d8a4-429b-b058-fe3e223285ab");

            var beogradskoDramskoPozoriste2ExternalId = new Guid("3aea54d4-3e23-4198-908b-7dd564d3a1bd");
            var beogradskoDramskoPozoriste2Id = new Guid("dd9afd4a-96f8-4769-bad2-d68ef6557eea");

            var beogradskoDramskoPozoriste3ExternalId = new Guid("b6426a03-3347-4ff2-9980-7c404aa7ffb8");
            var beogradskoDramskoPozoriste3Id = new Guid("8db9c1a8-b16f-4282-a3ff-c43e74fac6fb");

            var cirkusDeLaVega1ExternalId = new Guid("219302f1-18f7-449f-84e2-6504cb96f805");
            var cirkusDeLaVega1Id = new Guid("b13a3e09-f72d-4811-a963-6950758b4505");

            var cirkusDeLaVega2ExternalId = new Guid("ff0bac9e-e6b4-4b07-8d10-6d0afdad70bd");
            var cirkusDeLaVega2Id = new Guid("cff04491-ca8c-4b8a-977c-82d366fe8594");

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "Name", "StartingDateTime", "NumberOfPlaces", "IsSoldOut", "ExternalId" },
                values: new object[,]
                {
                    //Zdravko Colic concerts
                    { ZdravkoColicConcert1Id, "Zdravko Colic concert Belgrade", "2023-02-18 20:00:00.0000", 1000, true, ZdravkoColicConcert1ExternalId },
                    { ZdravkoColicConcert2Id, "Zdravko Colic concert Belgrade", "2023-09-18 20:00:00.0000", 1000, true, ZdravkoColicConcert2ExternalId },
                    { ZdravkoColicConcert3Id, "Zdravko Colic concert Belgrade", "2024-09-18 20:00:00.0000", 1000, true, ZdravkoColicConcert3ExternalId },
                    { ZdravkoColicConcert4Id, "Zdravko Colic concert Zagreb", "2024-08-18 20:00:00.0000", 1000, false, ZdravkoColicConcert4ExternalId },
                    { ZdravkoColicConcert5Id, "Zdravko Colic concert Jahorina", "2023-12-12 20:00:00.0000", 150, false, ZdravkoColicConcert5ExternalId },

                    //Raf Camora concerts
                    { RafCamoraConcert1Id, "Raf Camora concert Belgrade", "2023-05-18 20:00:00.0000", 1000, true, RafCamoraConcert1ExternalId },
                    { RafCamoraConcert2Id, "Raf Camora concert Vienna", "2020-09-18 20:00:00.0000", 2000, true, RafCamoraConcert2ExternalId },
                    { RafCamoraConcert3Id, "Raf Camora concert Paris", "2024-10-16 20:00:00.0000", 2400, false, RafCamoraConcert3ExternalId },
                    { RafCamoraConcert4Id, "Raf Camora concert Belgrade", "2024-07-16 20:00:00.0000", 2400, false, RafCamoraConcert4ExternalId },

                    //Beogradsko dramsko pozoriste
                    { beogradskoDramskoPozoriste1Id, "Gospodja ministarka", "2024-10-16 20:00:00.0000", 2000, false, beogradskoDramskoPozoriste1ExternalId },
                    { beogradskoDramskoPozoriste2Id, "Pokondirena tikva", "2024-10-15 20:00:00.0000", 2000, false, beogradskoDramskoPozoriste2ExternalId },
                    { beogradskoDramskoPozoriste3Id, "Neki to vole vruce", "2024-10-14 20:00:00.0000", 2000, false, beogradskoDramskoPozoriste3ExternalId },

                    //Cirkus De La Vega
                    { cirkusDeLaVega1Id, "Circus Performance Cair", "2024-05-05 18:00:00.0000", 350, false, cirkusDeLaVega1ExternalId },
                    { cirkusDeLaVega2Id, "Circus Performance Pirot", "2024-06-05 18:00:00.0000", 350, false, cirkusDeLaVega2ExternalId },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
