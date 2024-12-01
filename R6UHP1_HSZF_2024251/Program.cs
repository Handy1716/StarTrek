using R6UHP1_HSZF_2024251.Application.Services;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Services;
using System.Xml.Linq;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.CrewMember;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.Mission;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.Planet;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.SpaceShip;
using Console = System.Console;

namespace R6UHP1_HSZF_2024251.Console
{
    using Console = System.Console;
    internal class Program

    {
        static void Main(string[] args)
        {
            ReadIn();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Star Trek Data Management System ===");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Read");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Exit");
                Console.Write("Please select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleCreate();
                        break;
                    case "2":
                        HandleRead();
                        break;
                    case "3":
                        HandleUpdate();
                        break;
                    case "4":
                        HandleDelete();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option! Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void HandleCreate()
        {
            Console.Clear();
            Console.WriteLine("=== Create ===");
            Console.WriteLine("1. SpaceShip");
            Console.WriteLine("2. Planet");
            Console.WriteLine("3. CrewMember");
            Console.WriteLine("4. Mission");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Please select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateSpaceShip();
                    break;
                case "2":
                    CreatePlanet();
                    break;
                case "3":
                    CreateCrewMember();
                    break;
                case "4":
                    CreateMission();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option! Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        static void HandleRead()
        {
            Console.Clear();
            Console.WriteLine("=== Read ===");
            Console.WriteLine("1. List SpaceShips");
            Console.WriteLine("2. List Planets");
            Console.WriteLine("3. List CrewMembers");
            Console.WriteLine("4. List Missions");
            Console.WriteLine("5. Name or Status");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Please select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListPagedSpaceShips();
                    break;
                case "2":
                    ListPagedPlanets();
                    break;
                case "3":
                    ListPagedCrewMembers();
                    break;
                case "4":
                    ListPagedMissions();
                    break;
                case "5":
                    NameAndStatus();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option! Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        static void HandleUpdate()
        {
            Console.Clear();
            Console.WriteLine("=== Update ===");
            Console.WriteLine("1. Update SpaceShip");
            Console.WriteLine("2. Update Planet");
            Console.WriteLine("3. Update CrewMember");
            Console.WriteLine("4. Update Mission");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Please select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UpdateSpaceShip();
                    break;
                case "2":
                    UpdatePlanet();
                    break;
                case "3":
                    UpdateCrewMember();
                    break;
                case "4":
                    UpdateMission();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option! Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        static void HandleDelete()
        {
            Console.Clear();
            Console.WriteLine("=== Delete ===");
            Console.WriteLine("1. Delete SpaceShip");
            Console.WriteLine("2. Delete Planet");
            Console.WriteLine("3. Delete CrewMember");
            Console.WriteLine("4. Delete Mission");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Please select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DeleteSpaceShip();
                    break;
                case "2":
                    DeletePlanet();
                    break;
                case "3":
                    DeleteCrewMember();
                    break;
                case "4":
                    DeleteMission();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option! Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        // CRUD methods implementation
        static void CreateSpaceShip()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Create SpaceShip ===");

            // Űrhajó neve
            System.Console.Write("Enter the name of the spaceship: ");
            var name = System.Console.ReadLine();

            // Típus kiválasztása menüből
            System.Console.WriteLine("Select the type of the spaceship:");
            foreach (var type in Enum.GetValues(typeof(SpaceShip.SpaceShipType)))
            {
                System.Console.WriteLine($"{(int)type} - {type}");
            }
            SpaceShip.SpaceShipType selectedType;
            while (true)
            {
                System.Console.Write("Enter the number corresponding to the type: ");
                if (int.TryParse(System.Console.ReadLine(), out int typeSelection) &&
                    Enum.IsDefined(typeof(SpaceShip.SpaceShipType), typeSelection))
                {
                    selectedType = (SpaceShip.SpaceShipType)typeSelection;
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid selection. Please try again.");
                }
            }

            // Legénység létszáma
            System.Console.Write("Enter the crew count: ");
            var crewCount = int.TryParse(System.Console.ReadLine(), out int crewCountValue) ? crewCountValue : 0;

            // Státusz kiválasztása menüből
            System.Console.WriteLine("Select the status of the spaceship:");
            foreach (var status in Enum.GetValues(typeof(SpaceShip.SpaceShipStatus)))
            {
                System.Console.WriteLine($"{(int)status} - {status}");
            }
            SpaceShip.SpaceShipStatus selectedStatus;
            while (true)
            {
                System.Console.Write("Enter the number corresponding to the status: ");
                if (int.TryParse(System.Console.ReadLine(), out int statusSelection) &&
                    Enum.IsDefined(typeof(SpaceShip.SpaceShipStatus), statusSelection))
                {
                    selectedStatus = (SpaceShip.SpaceShipStatus)statusSelection;
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid selection. Please try again.");
                }
            }

            // Bolygó ID (opcionális)
            System.Console.Write("Enter the PlanetId (or leave blank if none): ");
            var planetIdInput = System.Console.ReadLine();
            int? planetId = string.IsNullOrWhiteSpace(planetIdInput) ? null : int.Parse(planetIdInput);

            // Űrhajó létrehozása
            var newSpaceShip = new SpaceShip
            {
                Name = name,
                Type = selectedType,
                CrewCount = crewCountValue,
                Status = selectedStatus,
                PlanetId = planetId
            };

            // Mentés

            var spaceShipService = new SpaceShipService();
            spaceShipService.CreateSpaceShip(newSpaceShip);
            System.Console.WriteLine("SpaceShip created successfully!");

            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }
        static void CreatePlanet()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Create Planet ===");

            // Bolygó neve
            System.Console.Write("Enter the name of the planet: ");
            var name = System.Console.ReadLine();

            // Bolygó típusa menüből
            System.Console.WriteLine("Select the type of the planet:");
            foreach (var type in Enum.GetValues(typeof(Planet.PlanetType)))
            {
                System.Console.WriteLine($"{(int)type} - {type}");
            }
            Planet.PlanetType selectedType;
            while (true)
            {
                System.Console.Write("Enter the number corresponding to the type: ");
                if (int.TryParse(System.Console.ReadLine(), out int typeSelection) &&
                    Enum.IsDefined(typeof(Planet.PlanetType), typeSelection))
                {
                    selectedType = (Planet.PlanetType)typeSelection;
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid selection. Please try again.");
                }
            }

            // ExplorationShipId (opcionális)
            System.Console.Write("Enter the ExplorationShipId (or leave blank if none): ");
            var explorationShipIdInput = System.Console.ReadLine();
            int? explorationShipId = string.IsNullOrWhiteSpace(explorationShipIdInput) ? null : int.Parse(explorationShipIdInput);

            // Bolygó létrehozása
            var newPlanet = new Planet
            {
                Name = name,
                Type = selectedType,
                ExplorationShipId = explorationShipId
            };

            // Mentés
            var planetService = new PlanetService();
            planetService.CreatePlanet(newPlanet);
            System.Console.WriteLine("Planet created successfully!");

            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }

        static void CreateCrewMember()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Create CrewMember ===");

            // Legénység neve
            System.Console.Write("Enter the name of the crew member: ");
            var name = System.Console.ReadLine();

            // Rank kiválasztása menüből
            System.Console.WriteLine("Select the rank of the crew member:");
            foreach (var rank in Enum.GetValues(typeof(CrewMember.CrewMemberRank)))
            {
                System.Console.WriteLine($"{(int)rank} - {rank}");
            }
            CrewMember.CrewMemberRank selectedRank;
            while (true)
            {
                System.Console.Write("Enter the number corresponding to the rank: ");
                if (int.TryParse(System.Console.ReadLine(), out int rankSelection) &&
                    Enum.IsDefined(typeof(CrewMember.CrewMemberRank), rankSelection))
                {
                    selectedRank = (CrewMember.CrewMemberRank)rankSelection;
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid selection. Please try again.");
                }
            }

            // SpaceshipId megadása
            System.Console.Write("Enter the SpaceshipId the crew member belongs to: ");
            var spaceshipId = int.Parse(System.Console.ReadLine() ?? "0");

            // Legénységi tag létrehozása
            var newCrewMember = new CrewMember
            {
                Name = name,
                Rank = selectedRank,
                SpaceShipId = spaceshipId
            };

            // Mentés
            var crewMemberService = new CrewMemberService();
            crewMemberService.CreateCrewMember(newCrewMember);
            System.Console.WriteLine("Crew member created successfully!");

            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }

        static void CreateMission()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Create Mission ===");

            // Cél bolygó megadása
            System.Console.Write("Enter the Target PlanetId: ");
            var targetPlanetId = int.Parse(System.Console.ReadLine() ?? "0");

            // Kezdő dátum megadása
            System.Console.Write("Enter the Start Date (yyyy-MM-dd): ");
            var startDate = DateTime.Parse(System.Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            // Befejező dátum (opcionális)
            System.Console.Write("Enter the End Date (yyyy-MM-dd, or leave blank if ongoing): ");
            var endDateInput = System.Console.ReadLine();
            DateTime? endDate = string.IsNullOrWhiteSpace(endDateInput) ? null : DateTime.Parse(endDateInput);

            // Státusz kiválasztása menüből
            System.Console.WriteLine("Select the status of the mission:");
            foreach (var status in Enum.GetValues(typeof(Mission.MissionStatus)))
            {
                System.Console.WriteLine($"{(int)status} - {status}");
            }
            Mission.MissionStatus selectedStatus;
            while (true)
            {
                System.Console.Write("Enter the number corresponding to the status: ");
                if (int.TryParse(System.Console.ReadLine(), out int statusSelection) &&
                    Enum.IsDefined(typeof(Mission.MissionStatus), statusSelection))
                {
                    selectedStatus = (Mission.MissionStatus)statusSelection;
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid selection. Please try again.");
                }
            }

            // Küldetés létrehozása
            var newMission = new Mission
            {
                TargetPlanetId = targetPlanetId,
                StartDate = startDate,
                EndDate = endDate,
                Status = selectedStatus
            };

            // Mentés
            var missionService = new MissionService();
            missionService.CreateMission(newMission);
            System.Console.WriteLine("Mission created successfully!");

            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }

        static void ListPagedSpaceShips()
        {
            int pageNumber = 1;
            int pageSize = 2;

            var spaceShipService = new SpaceShipService();

            while (true)
            {
                Console.Clear();
                var spaceShips = spaceShipService.GetPagedSpaceShips(pageNumber, pageSize);

                if (!spaceShips.Any())
                {
                    Console.WriteLine("No data available.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    return; // Visszalépés a menübe
                }

                Console.WriteLine($"=== SpaceShips (Page {pageNumber}) ===");
                foreach (var ship in spaceShips)
                {
                    Console.WriteLine($"ID: {ship.Id}, Name: {ship.Name}, Type: {ship.Type}, CrewCount: {ship.CrewCount}, Status: {ship.Status}, PlanetId: {ship.PlanetId}");
                }

                Console.WriteLine("\nOptions:");
                Console.WriteLine("[n] Next Page");
                Console.WriteLine("[p] Previous Page");
                Console.WriteLine("[b] Back to Read Menu");

                var input = Console.ReadKey().KeyChar;

                if (input == 'n')
                {
                    pageNumber++;
                }
                else if (input == 'p' && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (input == 'b')
                {
                    return; // Visszalépés a Read menübe
                }
                else
                {
                    Console.WriteLine("\nInvalid option! Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        static void ListPagedPlanets()
        {
            int pageNumber = 1;
            const int pageSize = 2;

            var planetService = new PlanetService();

            while (true)
            {
                Console.Clear();
                var planets = planetService.GetPagedPlanets(pageNumber, pageSize);

                if (!planets.Any())
                {
                    Console.WriteLine("No data available.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    return; // Visszalépés a menübe
                }

                Console.WriteLine($"=== Planets (Page {pageNumber}) ===");
                foreach (var planet in planets)
                {
                    Console.WriteLine($"ID: {planet.Id}, Name: {planet.Name}, Type: {planet.Type}, ExplorationShipId: {planet.ExplorationShipId}");
                }

                Console.WriteLine("\nOptions:");
                Console.WriteLine("[n] Next Page");
                Console.WriteLine("[p] Previous Page");
                Console.WriteLine("[b] Back to Read Menu");

                var input = Console.ReadKey().KeyChar;

                if (input == 'n')
                {
                    pageNumber++;
                }
                else if (input == 'p' && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (input == 'b')
                {
                    return; // Visszalépés a Read menübe
                }
                else
                {
                    Console.WriteLine("\nInvalid option! Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        static void ListPagedCrewMembers()
        {
            int pageNumber = 1;
            const int pageSize = 2;

            var crewMemberService = new CrewMemberService();

            while (true)
            {
                Console.Clear();
                var crewMembers = crewMemberService.GetPagedCrewMembers(pageNumber, pageSize);

                if (!crewMembers.Any())
                {
                    Console.WriteLine("No data available.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    return; // Visszalépés a menübe
                }

                Console.WriteLine($"=== CrewMembers (Page {pageNumber}) ===");
                foreach (var member in crewMembers)
                {
                    Console.WriteLine($"ID: {member.Id}, Name: {member.Name}, Rank: {member.Rank}, SpaceShipId: {member.SpaceShipId}, MissionCount: {member.MissionCount}");
                }

                Console.WriteLine("\nOptions:");
                Console.WriteLine("[n] Next Page");
                Console.WriteLine("[p] Previous Page");
                Console.WriteLine("[b] Back to Read Menu");

                var input = Console.ReadKey().KeyChar;

                if (input == 'n')
                {
                    pageNumber++;
                }
                else if (input == 'p' && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (input == 'b')
                {
                    return; // Visszalépés a Read menübe
                }
                else
                {
                    Console.WriteLine("\nInvalid option! Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void ListPagedMissions()
        {
            int pageNumber = 1;
            const int pageSize = 2;

            var missionService = new MissionService();

            while (true)
            {
                Console.Clear();
                var missions = missionService.GetPagedMissions(pageNumber, pageSize);

                if (!missions.Any())
                {
                    Console.WriteLine("No data available.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    return; // Visszalépés a menübe
                }

                Console.WriteLine($"=== Missions (Page {pageNumber}) ===");
                foreach (var mission in missions)
                {
                    Console.WriteLine($"ID: {mission.Id}, TargetPlanetId: {mission.TargetPlanetId}, StartDate: {mission.StartDate}, EndDate: {mission.EndDate}, Status: {mission.Status}");
                }

                Console.WriteLine("\nOptions:");
                Console.WriteLine("[n] Next Page");
                Console.WriteLine("[p] Previous Page");
                Console.WriteLine("[b] Back to Read Menu");

                var input = Console.ReadKey().KeyChar;

                if (input == 'n')
                {
                    pageNumber++;
                }
                else if (input == 'p' && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (input == 'b')
                {
                    return; // Visszalépés a Read menübe
                }
                else
                {
                    Console.WriteLine("\nInvalid option! Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void NameAndStatus() { }

        static void UpdateSpaceShip() { /* Implement SpaceShip update */ }
        static void UpdatePlanet() { /* Implement Planet update */ }
        static void UpdateCrewMember() { /* Implement CrewMember update */ }
        static void UpdateMission() { /* Implement Mission update */ }

        static void DeleteSpaceShip()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Delete SpaceShip ===");

            System.Console.Write("Enter the ID of the spaceship to delete: ");
            var spaceShipId = int.Parse(System.Console.ReadLine() ?? "0");

            var spaceShipService = new SpaceShipService();

            var success = spaceShipService.DeleteSpaceShip(spaceShipId);

            if (success)
            {
                System.Console.WriteLine("SpaceShip deleted successfully!");
            }
            else
            {
                System.Console.WriteLine("SpaceShip not found or could not be deleted.");
            }


            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }

        static void DeletePlanet()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Delete Planet ===");

            System.Console.Write("Enter the ID of the planet to delete: ");
            var planetId = int.Parse(System.Console.ReadLine() ?? "0");

            var planetService = new PlanetService();

            var success = planetService.DeletePlanet(planetId);

            if (success)
            {
                System.Console.WriteLine("Planet deleted successfully!");
            }
            else
            {
                System.Console.WriteLine("Planet not found or could not be deleted.");
            }


            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }

        static void DeleteCrewMember()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Delete CrewMember ===");

            System.Console.Write("Enter the ID of the crew member to delete: ");
            var crewMemberId = int.Parse(System.Console.ReadLine() ?? "0");

            var crewMemberService = new CrewMemberService();

            var success = crewMemberService.DeleteCrewMember(crewMemberId);

            if (success)
            {
                System.Console.WriteLine("CrewMember deleted successfully!");
            }
            else
            {
                System.Console.WriteLine("CrewMember not found or could not be deleted.");
            }


            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }

        static void DeleteMission()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Delete Mission ===");

            System.Console.Write("Enter the ID of the mission to delete: ");
            var missionId = int.Parse(System.Console.ReadLine() ?? "0");

            var missionService = new MissionService();
            var success = missionService.DeleteMission(missionId);

            if (success)
            {
                System.Console.WriteLine("Mission deleted successfully!");
            }
            else
            {
                System.Console.WriteLine("Mission not found or could not be deleted.");
            }

            System.Console.WriteLine("Press any key to return to the menu...");
            System.Console.ReadKey();
        }
        public static void ReadIn()
        {
            var importService = new DataImportService();
            importService.ReadIn();
        }

        //ReadIn();
        //var spaceShipService = new SpaceShipService();
        //using (var context = new StarTrekDbContext())
        //{
        //    var ship = context.SpaceShips.FirstOrDefault();
        //    System.Console.WriteLine($"Ship Name: {ship.Name}");

        //    // Lazy loading: a Planet csak akkor töltődik be, amikor először hozzáférünk
        //    if (ship.PlanetId != null)
        //    {
        //        System.Console.WriteLine($"Assigned Planet: {ship.Planet.Name}");
        //    }
        //    else
        //    {
        //        System.Console.WriteLine("This ship is not assigned to any planet.");
        //    }
        //}


        //////-------- 4 feladat riportok
        //string reportPath = "SpaceShipReport.xml";

        //// Riport generálása
        //spaceShipService.GenerateSpaceShipReport(reportPath);
        //var activeShips = spaceShipService.GetSpaceShipsByStatus(SpaceShip.SpaceShipStatus.Active);
        //var missionService = new MissionService();
        //// Riport fájl elérési útja
        //string missionReportPath = "MissionReport.txt";
        //// Riport generálása
        //missionService.GenerateMissionReport(missionReportPath);
        ////-------------

        //SpaceShipKlingon
        //var spaceShips = new List<SpaceShip>
        //    {
        //    new SpaceShip { Id = 1, Name = "USSEnterprise", Type = SpaceShip.SpaceShipType.Explorer, CrewCount = 100, Status = SpaceShip.SpaceShipStatus.Active, PlanetId = 1 },
        //    new SpaceShip { Id = 2, Name = "USSVoyager", Type = SpaceShip.SpaceShipType.Medical, CrewCount = 150, Status = SpaceShip.SpaceShipStatus.Inactive, PlanetId = null }
        //    };

        //// Riport generálása
        //string filePath = "SpaceShipKlingonReport.xml";
        //spaceShipService.GenerateKlingonXml(filePath, spaceShips);

        //Paged();
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

        //public static void Paged()
        //{
        //    var spaceShipService = new SpaceShipService();
        //    var crewMemberService = new CrewMemberService();
        //    var missionService = new MissionService();
        //    var currentPage = 1;
        //    var totalPages = 3;

        //    while (true)
        //    {

        //        System.Console.Clear();
        //        System.Console.WriteLine($"Page {currentPage}/{totalPages}");

        //        switch (currentPage)
        //        {
        //            case 1:
        //                System.Console.WriteLine("SpaceShips:");
        //                var spaceShips = spaceShipService.GetAllSpaceShips(); // Összes űrhajó lekérdezése
        //                foreach (var ship in spaceShips)
        //                {
        //                    System.Console.WriteLine($"- {ship.Name} (Status: {ship.Status})");
        //                }
        //                break;

        //            case 2:
        //                System.Console.WriteLine("CrewMembers:");
        //                var crewMembers = crewMemberService.GetAllCrewMembers(); // Összes legénységi tag lekérdezése
        //                foreach (var member in crewMembers)
        //                {
        //                    System.Console.WriteLine($"- {member.Name} (Rank: {member.Rank})");
        //                }
        //                break;

        //            case 3:
        //                System.Console.WriteLine("Missions:");
        //                var missions = missionService.GetAllMissions(); // Összes küldetés lekérdezése
        //                foreach (var mission in missions)
        //                {
        //                    System.Console.WriteLine($"- Mission to Planet {mission.TargetPlanetId} (Status: {mission.Status})");
        //                }
        //                break;
        //        }

        //        System.Console.WriteLine("\nPress 'n' for next page, 'p' for previous page, or any other key to exit.");
        //        var input = System.Console.ReadLine();

        //        if (input == "n" && currentPage < totalPages)
        //        {
        //            currentPage++;
        //        }
        //        else if (input == "p" && currentPage > 1)
        //        {
        //            currentPage--;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //}

    }
}


