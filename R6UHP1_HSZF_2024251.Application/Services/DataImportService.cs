using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Services
{
    public class XmlDataReader
    {
        public StarTrekData LoadDataFromXml(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);

            var spaceShips = xdoc.Descendants("Spaceship")
                .Select(x => new SpaceShip
                {
                    Id = (int)x.Element("Id"),
                    Name = (string)x.Element("Name"),
                    Type = (string)x.Element("Type"),
                    CrewCount = (int)x.Element("CrewCount"),
                    Status = (string)x.Element("Status"),
                    PlanetId = (int)x.Element("PlanetId")
                }).ToList();

            var crewMembers = xdoc.Descendants("CrewMember")
                .Select(x => new Crew
                {
                    Id = (int)x.Element("Id"),
                    Name = (string)x.Element("Name"),
                    Rank = (string)x.Element("Rank"),
                    SpaceShipId = (int)x.Element("SpaceShipId")
                }).ToList();

            var planets = xdoc.Descendants("Planet")
                .Select(x => new Planet
                {
                    Id = (int)x.Element("Id"),
                    Name = (string)x.Element("Name"),
                    Type = (string)x.Element("Type"),
                    SpaceShip = (int)x.Element("ExplorationShipId")
                }).ToList();

            var missions = xdoc.Descendants("Mission")
                .Select(x => new Mission
                {
                    Id = (int)x.Element("Id"),
                    TargetPlanetId = (int)x.Element("TargetPlanetId"),
                    StartDate = DateTime.Parse((string)x.Element("StartDate")),
                    EndDate = DateTime.Parse((string)x.Element("EndDate")),
                    Status = (string)x.Element("Status")
                }).ToList();

            return new StarTrekData
            {
                SpaceShips = spaceShips,
                CrewMembers = crewMembers,
                Planets = planets,
                Missions = missions
            };
        }
    }

    public class StarTrekData
    {
        public List<SpaceShip> SpaceShips { get; set; }
        public List<Crew> CrewMembers { get; set; }
        public List<Planet> Planets { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
