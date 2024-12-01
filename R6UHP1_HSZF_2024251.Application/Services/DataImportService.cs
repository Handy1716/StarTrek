using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using R6UHP1_HSZF_2024251.Model.Entities;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using static R6UHP1_HSZF_2024251.Model.Entities.Planet;
namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Services
{
    public class DataImportService
    {
        public void ReadIn()
        {
            XDocument xdoc = XDocument.Load("StarTrekData2.xml");
            var spaceships = xdoc.Descendants("Spaceship").Select(x =>
            {
                var newSpaceShip = new SpaceShip
                {
                    Name = (string)x.Element("Name"),
                    Type = (SpaceShip.SpaceShipType)Enum.Parse(typeof(SpaceShip.SpaceShipType), (string)x.Element("Type")),
                    CrewCount = (int)x.Element("CrewCount"),
                    Status = (SpaceShip.SpaceShipStatus)Enum.Parse(typeof(SpaceShip.SpaceShipStatus), (string)x.Element("Status")),
                    PlanetId = x.Element("PlanetId") != null ? (int?)x.Element("PlanetId") : null,
                };
                return newSpaceShip;
            }).ToList();

            var planets = xdoc.Descendants("Planet").Select(x =>
            {
                var newPlanet = new Planet
                {
                    Name = (string)x.Element("Name"),
                    Type = (Planet.PlanetType)Enum.Parse(typeof(Planet.PlanetType), (string)x.Element("Type")),
                    ExplorationShipId = x.Element("ExplorationShipId") != null ? (int?)x.Element("ExplorationShipId") : null
                    
                };
                

                return newPlanet;
            }).ToList();

            var crewmembers = xdoc.Descendants("CrewMember").Select(x =>
            {
                var newCrewMember = new CrewMember
                {
                    Name = (string)x.Element("Name"),
                    Rank = (CrewMember.CrewMemberRank)Enum.Parse(typeof(CrewMember.CrewMemberRank), (string)x.Element("Rank")),
                    SpaceShipId = (int)x.Element("SpaceshipId"),
                    MissionCount = x.Element("MissionCount") != null ? (int?)x.Element("MissionCount") : null,
                };
                return newCrewMember;
            }).ToList();

            var missions = xdoc.Descendants("Mission").Select(x =>
            {
                var newMission = new Mission
                {
                    TargetPlanetId = (int)x.Element("TargetPlanetId"),
                    StartDate = DateTime.Parse((string)x.Element("StartDate")),
                    EndDate = x.Element("EndDate") != null ? (DateTime?)DateTime.Parse((string)x.Element("EndDate")) : null,
                    Status = (Mission.MissionStatus)Enum.Parse(typeof(Mission.MissionStatus), (string)x.Element("Status")),
                };
                return newMission;
            }).ToList();


            var context = new StarTrekDbContext(true);
            planets.ForEach(planet =>
            {
                context.Planets.Add(planet);
            });
            context.SaveChanges();
            spaceships.ForEach(ship =>
            {
                context.SpaceShips.Add(ship);
            });
            context.SaveChanges();
            crewmembers.ForEach(crewmember =>
            {
                context.CrewMembers.Add(crewmember);
            });
            context.SaveChanges();
            missions.ForEach(mission =>
            {
                context.Missions.Add(mission);
            });
            context.SaveChanges();
        }
    }
}


