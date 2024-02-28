using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shows.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShowData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var ZdravkoColicId = new Guid("98929bd4-f099-41eb-a994-f1918b724b5a");
            var RafCamoraId = new Guid("b512d7cf-b331-4b54-8dae-d1228d128e8d");
            var BeogradskoDramskoPozoristeId = new Guid("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06");
            var CirkusDeLaVegaId = new Guid("fd630a57-2352-4731-b25c-db9cc7601b16");

            migrationBuilder.InsertData(
                table: "Performers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { ZdravkoColicId, "Zdravko Colic" },
                    { RafCamoraId, "Raf Camora" },
                    { BeogradskoDramskoPozoristeId, "Beogradsko dramsko pozoriste" },
                    { CirkusDeLaVegaId, "Cirkus De La Vega" }
                });

            migrationBuilder.InsertData(
                table: "PerformerInfo",
                columns: new[] { "Id", "Name", "Value", "PerformerId" },
                values: new object[,]
                {
                    //Zdravko Colic
                    { new Guid("ef2577c3-4fca-4dbe-989c-12063141feda"), "Date of birth", "30.05.1951.", ZdravkoColicId},
                    { new Guid("c4f27825-ec7c-408b-acb2-f7dc43e5baac"), "Marital status", "Married", ZdravkoColicId},
                    { new Guid("a5f29db8-3423-4dce-a2b0-beda784e93fa"), "Nationality", "Serbian", ZdravkoColicId},
                    { new Guid("2fd0cb68-5a9b-485f-9bbc-1fc1ba2c1779"), "Type of performer", "Singer", ZdravkoColicId},

                    //Raf Camora
                    { new Guid("5e151324-6d92-454c-939b-863c2a6f29b5"), "Date of birth", "04.06.1984.", RafCamoraId},
                    { new Guid("790e6839-45cc-4f3f-a3e2-c6543a5e1c3f"), "Full name", "Raphael Ragucci", RafCamoraId},
                    { new Guid("c2bfcc29-d356-4da9-8413-03b348e256e2"), "Nationality", "Austrian", RafCamoraId},
                    { new Guid("a8173f98-bdff-4552-a29c-83763f8588d9"), "Type of performer", "Singer", RafCamoraId},

                    //Beogradsko dramsko pozoriste
                    { new Guid("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), "Description", "The Belgrade Drama Theatre is a theatre located in Belgrade, the capital of Serbia", BeogradskoDramskoPozoristeId},

                    { new Guid("01b72336-4da0-4d90-8dc2-4444f1822aed"), "Description", "Circus from Serbia that was formed by Sliva family", CirkusDeLaVegaId},
                    { new Guid("41fc2693-3bb6-44c5-9b03-b10649a766f3"), "Year of establishment", "1705", CirkusDeLaVegaId},
                });

            var concertId = new Guid("e8518239-ff9c-4f6c-aeba-443e94c6d670");
            var theaterPerformaceId = new Guid("7493e17e-2298-4052-80d8-c411a1b946cd");
            var circusId = new Guid("72343e9d-9575-41ab-a5d7-d2a617807485");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Description", "Status" },
                values: new object[,]
                {
                    { concertId, "Concert", "Live music performance in front of an audience", "IsValid" },
                    { theaterPerformaceId, "Theater performance", "Collaborative form of performing art that uses live performers, usually actors or actresses", "IsValid" },
                    { circusId, "Circus", "Diverse entertainment type of shows that may include clowns, acrobats, trained animals", "IsValid" },
                });

            var ZdravkoColicConcert1Id = new Guid("93829cd6-c1f1-43d4-9ca0-31d9237bae99");
            var ZdravkoColicConcert2Id = new Guid("0f4c6f53-1cca-4d62-bcc8-a4797d2e0334");
            var ZdravkoColicConcert3Id = new Guid("86ccbb64-4245-4ff8-ae13-58371d26ffa7");
            var ZdravkoColicConcert4Id = new Guid("e1f48bf0-ff7c-4485-87cb-c1401fadd87f");
            var ZdravkoColicConcert5Id = new Guid("c3463d20-e728-4641-a149-a0df732e068c");

            var RafCamoraConcert1Id = new Guid("44977503-4f90-46d3-b306-9704c036d086");
            var RafCamoraConcert2Id = new Guid("06b524eb-0fae-4dd0-b69a-75c3a778e53a");
            var RafCamoraConcert3Id = new Guid("e5dd71a0-8ef4-4b18-af7f-56d5644a3b45");
            var RafCamoraConcert4Id = new Guid("b3d99114-e03d-4f78-95fd-f7e86f7fa49c");

            var beogradskoDramskoPozoriste1Id = new Guid("7f64a219-a0b0-4500-ba3a-4b32f6d8b577");
            var beogradskoDramskoPozoriste2Id = new Guid("3aea54d4-3e23-4198-908b-7dd564d3a1bd");
            var beogradskoDramskoPozoriste3Id = new Guid("b6426a03-3347-4ff2-9980-7c404aa7ffb8");

            var cirkusDeLaVega1Id = new Guid("219302f1-18f7-449f-84e2-6504cb96f805");
            var cirkusDeLaVega2Id = new Guid("ff0bac9e-e6b4-4b07-8d10-6d0afdad70bd");

            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            //var jsonPath = Path.Combine(basePath, "base64images.json");

            //var jsonContent = File.ReadAllText(jsonPath);
            //var imageData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "Name", "Description", "Picture", "Location", "Status", "NumberOfPlaces", "TicketPrice_Currency", "TicketPrice_Amount", "PerformerId", "CategoryId", "StartingDateTime" },
                values: new object[,]
                {
                    //Zdravko Colic concerts
                    { ZdravkoColicConcert1Id, "Zdravko Colic concert Belgrade", "This is first concert of Zdravko Colic in Belgrade, Serbia in Belgrade Arena.", Base64Pictures.ReadFromFile("Migrations/Pictures/ZdravkoColicConcert1.txt"), "Belgrade Arena", "IsSoldOut", "1000", "rsd", "2500", ZdravkoColicId, concertId, "2023-02-18 20:00:00.0000" },
                    { ZdravkoColicConcert2Id, "Zdravko Colic concert Belgrade", "This is second concert of Zdravko Colic in Belgrade, Serbia in Belgrade Arena.", Base64Pictures.ReadFromFile("Migrations/Pictures/ZdravkoColicConcert2.txt"), "Belgrade Arena", "IsSoldOut", "1000", "rsd", "2500", ZdravkoColicId, concertId, "2023-09-18 20:00:00.0000" },
                    { ZdravkoColicConcert3Id, "Zdravko Colic concert Belgrade", "This is third concert of Zdravko Colic in Belgrade, Serbia in Belgrade Arena.", Base64Pictures.ReadFromFile("Migrations/Pictures/ZdravkoColicConcert3.txt"),"Belgrade Arena", "HasTickets", "1000", "rsd", "2500", ZdravkoColicId, concertId, "2024-09-18 20:00:00.0000" },
                    { ZdravkoColicConcert4Id, "Zdravko Colic concert Zagreb", "This is first concert of Zdravko Colic in Zagreb, Croatia in Zagreb Arena.", Base64Pictures.ReadFromFile("Migrations/Pictures/ZdravkoColicConcert4.txt"),"Zagreb Arena", "HasTickets", "1000", "eur", "30", ZdravkoColicId, concertId, "2024-08-18 20:00:00.0000" },
                    { ZdravkoColicConcert5Id, "Zdravko Colic concert Jahorina", "This is first concert of Zdravko Colic in Jahorina, BiH in one of the bes hotels: Jahorina hotel.", Base64Pictures.ReadFromFile("Migrations/Pictures/ZdravkoColicConcert5.txt"), "Jahorina hotel", "HasTickets", "150", "eur", "60", ZdravkoColicId, concertId, "2023-12-12 20:00:00.0000" },

                    //Raf Camora concerts
                    { RafCamoraConcert1Id, "Raf Camora concert Belgrade", "This is first concert of Raf Camora in Belgrade, Serbia in Belgrade Arena.", Base64Pictures.ReadFromFile("Migrations/Pictures/RafCamoraConcert1.txt"), "Belgrade Arena", "IsSoldOut", "1000", "rsd", "2500", RafCamoraId, concertId, "2023-05-18 20:00:00.0000" },
                    { RafCamoraConcert2Id, "Raf Camora concert Vienna", "This is first concert of Raf Camora in Vienna, Austia in the biggest arena in Europe-Vienna Arena.", Base64Pictures.ReadFromFile("Migrations/Pictures/RafCamoraConcert2.txt"), "Vienna Arena", "IsSoldOut", "2000", "eur", "80", RafCamoraId, concertId, "2020-09-18 20:00:00.0000" },
                    { RafCamoraConcert3Id, "Raf Camora concert Paris", "This is first concert of Raf Camora in Paris, France in Paris Arena. This is the biggest concert in Paris in october 2024.", Base64Pictures.ReadFromFile("Migrations/Pictures/RafCamoraConcert3.txt"),"Paris Arena", "HasTickets", "2400", "eur", "100", RafCamoraId, concertId, "2024-10-16 20:00:00.0000" },
                    { RafCamoraConcert4Id, "Raf Camora concert Belgrade", "This is second concert of Raf Camora in Belgrade, Serbia. It will be on Splav Tag.", Base64Pictures.ReadFromFile("Migrations/Pictures/RafCamoraConcert4.txt"),"Splav Tag", "HasTickets", "2400", "rsd", "3500", RafCamoraId, concertId, "2024-07-16 20:00:00.0000" },

                    //Beogradsko dramsko pozoriste 
                    { beogradskoDramskoPozoriste1Id, "Gospodja ministarka", "Gospodja ministarka is a theater play based on Branislav Nušić's comedy from 1929.", Base64Pictures.ReadFromFile("Migrations/Pictures/BeogradskoDramsko1.txt"), "Beogradsko dramsko pozoriste", "HasTickets", "400", "rsd", "2000", BeogradskoDramskoPozoristeId, theaterPerformaceId, "2024-10-16 20:00:00.0000" },
                    { beogradskoDramskoPozoriste2Id, "Pokondirena tikva", "Pokondirena tikva is a comedy by Jovan Sterija Popović from 1838. In this work, Jovan Sterija Popović made fun of the main character, Fema, who, crazy about nobleness, tries to escape from her craftsman's world and make her way into higher social circles.",
                        Base64Pictures.ReadFromFile("Migrations/Pictures/BeogradskoDramsko2.txt"), "Beogradsko dramsko pozoriste", "HasTickets", "400", "rsd", "2000", BeogradskoDramskoPozoristeId, theaterPerformaceId, "2024-10-15 20:00:00.0000" },
                    { beogradskoDramskoPozoriste3Id, "Neki to vole vruce", "This is one musical which is performed in Nis Serbia for couple of years. It is written by Aleksandar Marinkovic.", Base64Pictures.ReadFromFile("Migrations/Pictures/BeogradskoDramsko3.txt"), "Beogradsko dramsko pozoriste", "HasTickets", "400", "rsd", "2000", BeogradskoDramskoPozoristeId, theaterPerformaceId, "2024-10-14 20:00:00.0000" },

                    //Cirkus De La Vega
                    { cirkusDeLaVega1Id, "Circus Performance Cair", "This is the first circus performance in Nis, Serbia which is performed by one of the best circus groups on Balkan.", Base64Pictures.ReadFromFile("Migrations/Pictures/CirkusDeLaVega1.txt"), "Park Cair Nis", "HasTickets", "50", "rsd", "350", CirkusDeLaVegaId, circusId, "2024-05-05 18:00:00.0000" },
                    { cirkusDeLaVega2Id, "Circus Performance Pirot", "This is the first circus performance in Pirot, Serbia which is performed by one of the best circus groups on Balkan.", Base64Pictures.ReadFromFile("Migrations/Pictures/CirkusDeLaVega2.txt"), "Pirotska tvrdjava", "HasTickets", "50", "rsd", "350", CirkusDeLaVegaId, circusId, "2024-06-05 18:00:00.0000" },
                });

            //ShowMessage
            migrationBuilder.InsertData(
                table: "ShowMessage",
                columns: new[] { "Id", "Name", "Value", "ShowId" },
                values: new object[,]
                {
                    //Zdravko Colic concerts
                    { new Guid("e5ddf93a-6bfd-4b34-ac8b-573856989297"), "Guest performer", "This concert has secret guest performer", ZdravkoColicConcert1Id },
                    { new Guid("57d40410-569f-4e2d-bfe7-fe7d1a1c5d0c"), "Fan pit", "First 10 reservations get fan pit", ZdravkoColicConcert2Id },
                    { new Guid("7fab9ccc-9b49-4e22-a500-dde1aeb098d8"), "Free tickets", "By listening Play Radio, you can get 3 free tickets", ZdravkoColicConcert3Id },
                    { new Guid("73083ee2-036d-466d-bec9-ec2721cc5441"), "Guest performer", "This concert has secret guest performer", ZdravkoColicConcert4Id },

                    //Raf Camora concerts
                    { new Guid("9debf467-0eeb-4641-a27a-ad0a611c8de5"), "Guest performer", "This concert has Senidah as guest performer", RafCamoraConcert1Id },
                    { new Guid("3df6c48e-cfb0-4f16-a7de-6539ca980505"), "Free gifts", "Raf Camora will give free gifts at this concert", RafCamoraConcert2Id },
                    { new Guid("1de357d2-2dc7-49fe-9601-299e0fbf7b4e"), "Clothing discount", "By attending this concert, you will get 10% of for buying Nike clothing in all Nike stores in Paris", RafCamoraConcert3Id },
                    { new Guid("c14ae7c8-9092-49f7-a6c3-a1b9cd9f1eed"), "Guest performer", "This concert has Senidah as guest performer", RafCamoraConcert4Id },
                    { new Guid("d5843389-bfcf-40ee-8869-dd9a7eeebdcf"), "Free gifts", "Raf Camora will give free gifts at this concert", RafCamoraConcert4Id },

                    //Beogradsko dramsko pozoriste
                    { new Guid("2876bd8b-a9d7-40ee-9191-0fcce4d1522d"), "Connected seats", "If you want multiple connected seats, please go to Beogradsko dramsko pozoriste as soon as possible", beogradskoDramskoPozoriste1Id },
                    { new Guid("9aada4f9-5b96-4e89-8993-99ae9f67495b"), "Connected seats", "If you want multiple connected seats, please go to Beogradsko dramsko pozoriste as soon as possible", beogradskoDramskoPozoriste2Id },

                    //Cirkus De La Vega
                    { new Guid("7ed32673-cde0-48ba-84df-f252b31f93b0"), "Bad weather location", "If it is raining, circus performance will be in Hala Cair.", cirkusDeLaVega1Id },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
