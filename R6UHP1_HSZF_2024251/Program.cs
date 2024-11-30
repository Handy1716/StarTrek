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
            //var crewMemberService = new CrewMemberService();
            //var planetService = new PlanetService();
            //var missionService = new MissionService();


            spaceShipService.OnOperationCompleted += message =>
            {
                System.Console.WriteLine($"Event Message: {message}");
            };
            var newSpaceShip = new SpaceShip
            {
                Name = "USS Enterprise",
                Type = SpaceShip.SpaceShipType.Fighter,
                CrewCount = 1001,
                Status = SpaceShip.SpaceShipStatus.Active,
                PlanetId = null
            };
            spaceShipService.CreateSpaceShip(newSpaceShip);

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

    }

}
