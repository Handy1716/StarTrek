using R6UHP1_HSZF_2024251.Application.Services;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Services;
using System.Xml.Linq;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.CrewMember;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.Mission;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.Planet;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.SpaceShip;

namespace R6UHP1_HSZF_2024251.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReadIn();
            var spaceShipService = new SpaceShipService();
            string reportPath = "SpaceShipReport.xml";

            // Riport generálása
            spaceShipService.GenerateSpaceShipReport(reportPath);
            var activeShips = spaceShipService.GetSpaceShipsByStatus(SpaceShip.SpaceShipStatus.Active);


            var missionService = new MissionService();

            // Riport fájl elérési útja
            string missionReportPath = "MissionReport.txt";

            // Riport generálása
            missionService.GenerateMissionReport(missionReportPath);

            //SpaceShipKlingon
            var spaceShips = new List<SpaceShip>
                {
                new SpaceShip { Id = 1, Name = "USS Enterprise", Type = SpaceShip.SpaceShipType.Explorer, CrewCount = 100, Status = SpaceShip.SpaceShipStatus.Active, PlanetId = 1 },
                new SpaceShip { Id = 2, Name = "USS Voyager", Type = SpaceShip.SpaceShipType.Medical, CrewCount = 150, Status = SpaceShip.SpaceShipStatus.Inactive, PlanetId = null }
                };

            // Riport generálása
            string filePath = "SpaceShipKlingonReport.xml";
            spaceShipService.GenerateKlingonXml(filePath, spaceShips);

            Paged();
            //foreach (var ship in activeShips)
            //{
            //    System.Console.WriteLine($"Active Ship: {ship.Id} {ship.Name}");
            //}
     

            //spaceShipService.OnOperationCompleted += message =>
            //{
            //    System.Console.WriteLine($"Event Message: {message}");
            //};
            //var newSpaceShip = new SpaceShip
            //{
            //    Name = "USS Enterprise",
            //    Type = SpaceShip.SpaceShipType.Fighter,
            //    CrewCount = 1001,
            //    Status = SpaceShip.SpaceShipStatus.Active,
            //    PlanetId = null
            //};
            //spaceShipService.UpdateSpaceShip(1, spaceShip =>
            //{
            //    spaceShip.Name = "USS Enterprise-D"; // Új név
            //    spaceShip.Status = SpaceShip.SpaceShipStatus.Inactive; // Új státusz
            //});
            //spaceShipService.CreateSpaceShip(newSpaceShip);
            //spaceShipService.DeleteSpaceShip(1);



            //var crewMemberService = new CrewMemberService();
            //var planetService = new PlanetService();
            //var missionService = new MissionService();




            //var newCrewMember = new CrewMember
            //{
            //    Name = "James T. Kirk",
            //    Rank = CrewMember.CrewMemberRank.Captain,
            //    SpaceShipId = 1, // A megfelelő SpaceShip azonosítója
            //    MissionCount = 5
            //};
            //crewMemberService.CreateCrewMember(newCrewMember);
            //crewMemberService.DeleteCrewMember(crewMemberId: 1);


            //var newMission = new Mission
            //{
            //    TargetPlanetId = 1, // A célbolygó azonosítója
            //    StartDate = DateTime.Now,
            //    EndDate = null, // Opcionális: nincs befejezve
            //    Status = Mission.MissionStatus.InProgress
            //};
            //missionService.CreateMission(newMission);
            //missionService.DeleteMission(missionId: 1);


            //var newPlanet = new Planet
            //{
            //    Name = "Vulcan",
            //    Type = Planet.PlanetType.GasGiant,
            //    ExplorationShipId = 1 // Opcionális: az űrhajó azonosítója, amely felfedezte
            //};
            //planetService.DeletePlanet(planetId: 1);
            //planetService.CreatePlanet(newPlanet);
        }
        public static void ReadIn()
        {
            var importService = new DataImportService();
            importService.ReadIn();
        }
        public static void Paged()
        {
            var spaceShipService = new SpaceShipService();
            var crewMemberService = new CrewMemberService();
            var missionService = new MissionService();
            var currentPage = 1;
            var totalPages = 3;

            while (true)
            {
                
                System.Console.Clear();
                System.Console.WriteLine($"Page {currentPage}/{totalPages}");

                switch (currentPage)
                {
                    case 1:
                        System.Console.WriteLine("SpaceShips:");
                        var spaceShips = spaceShipService.GetAllSpaceShips(); // Összes űrhajó lekérdezése
                        foreach (var ship in spaceShips)
                        {
                            System.Console.WriteLine($"- {ship.Name} (Status: {ship.Status})");
                        }
                        break;

                    case 2:
                        System.Console.WriteLine("CrewMembers:");
                        var crewMembers = crewMemberService.GetAllCrewMembers(); // Összes legénységi tag lekérdezése
                        foreach (var member in crewMembers)
                        {
                            System.Console.WriteLine($"- {member.Name} (Rank: {member.Rank})");
                        }
                        break;

                    case 3:
                        System.Console.WriteLine("Missions:");
                        var missions = missionService.GetAllMissions(); // Összes küldetés lekérdezése
                        foreach (var mission in missions)
                        {
                            System.Console.WriteLine($"- Mission to Planet {mission.TargetPlanetId} (Status: {mission.Status})");
                        }
                        break;
                }

                System.Console.WriteLine("\nPress 'n' for next page, 'p' for previous page, or any other key to exit.");
                var input = System.Console.ReadLine();

                if (input == "n" && currentPage < totalPages)
                {
                    currentPage++;
                }
                else if (input == "p" && currentPage > 1)
                {
                    currentPage--;
                }
                else
                {
                    break;
                }
            }
        }

    }

}
