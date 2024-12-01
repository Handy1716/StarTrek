using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Atributes;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class SpaceShipService : BaseService
    {
        public delegate void OperationEventHandler(string message);
        public event OperationEventHandler? OnOperationCompleted;
        public void CreateSpaceShip(SpaceShip newSpaceShip)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.SpaceShips.Add(newSpaceShip);
                    context.SaveChanges();
                    OnOperationCompleted?.Invoke("SpaceShip created successfully.");
                }
                catch (Exception ex)
                {
                    OnOperationCompleted?.Invoke($"Failed to create SpaceShip: {ex.Message}");
                }
            }
        }


        public void UpdateSpaceShip(int spaceShipId, Action<SpaceShip> updateAction)
        {
            using (var context = new StarTrekDbContext())
            {
                var spaceShip = context.SpaceShips.Find(spaceShipId);
                if (spaceShip != null)
                {
                    try
                    {
                        updateAction(spaceShip); // A frissítési logika kívülről érkezik
                        context.SaveChanges();
                        Console.WriteLine("SpaceShip updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to update SpaceShip: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("SpaceShip not found.");
                }
            }
        }

        public void DeleteSpaceShip(int spaceShipId)
        {
            using (var context = new StarTrekDbContext())
            {
                // Nullázd a Planets táblában az ExplorationShipId-t
                var planets = context.Planets.Where(p => p.ExplorationShipId == spaceShipId).ToList();
                planets.ForEach(p => p.ExplorationShipId = null);

                // Mentsd el a változásokat
                context.SaveChanges();

                // Töröljük az összes kapcsolódó legénységi tagot
                var crewMembers = context.CrewMembers.Where(cm => cm.SpaceShipId == spaceShipId).ToList();
                context.CrewMembers.RemoveRange(crewMembers);

                // Töröljük magát az űrhajót
                var spaceShip = context.SpaceShips.Find(spaceShipId);
                if (spaceShip != null)
                {
                    context.SpaceShips.Remove(spaceShip);
                    context.SaveChanges();
                    Console.WriteLine("SpaceShip deleted successfully, and related CrewMembers and Planets updated.");
                }
                else
                {
                    Console.WriteLine("SpaceShip not found.");
                }
            }
        }
        public void GenerateSpaceShipReport(string filePath)
        {
            using (var context = new StarTrekDbContext())
            {
                // Lekérjük az összes űrhajót az adatbázisból
                var spaceShips = context.SpaceShips.ToList();

                // XML fájl generálása
                var xDocument = new XDocument(
                    new XElement("SpaceShips",
                        spaceShips.Select(ship =>
                            new XElement("SpaceShip",
                                new XElement("Id", ship.Id),
                                new XElement("Name", ship.Name),
                                new XElement("Type", ship.Type.ToString()),
                                new XElement("CrewCount", ship.CrewCount),
                                new XElement("Status", ship.Status.ToString()),
                                new XElement("PlanetId", ship.PlanetId.HasValue ? ship.PlanetId.ToString() : "null")
                            )
                        )
                    )
                );

                // Fájl mentése
                xDocument.Save(filePath);

                Console.WriteLine($"Report generated successfully at: {filePath}");
            }
        }
        public void GenerateKlingonXml(string filePath, List<SpaceShip> spaceShips)
        {
            // Gyökér elem az osztály attribútuma alapján
            var rootAttribute = (KlingonTranslationAttribute)Attribute.GetCustomAttribute(typeof(SpaceShip), typeof(KlingonTranslationAttribute));
            var rootElement = new XElement(rootAttribute.Translation);

            foreach (var spaceShip in spaceShips)
            {
                // SpaceShip elem
                var classAttribute = (KlingonTranslationAttribute)Attribute.GetCustomAttribute(typeof(SpaceShip), typeof(KlingonTranslationAttribute));
                var shipElement = new XElement("ghIlghameS");

                // Property-k bejárása
                foreach (var prop in typeof(SpaceShip).GetProperties())
                {
                    var propAttribute = (KlingonTranslationAttribute)Attribute.GetCustomAttribute(prop, typeof(KlingonTranslationAttribute));
                    if (propAttribute != null)
                    {
                        var value = prop.GetValue(spaceShip)?.ToString() ?? "null";
                        shipElement.Add(new XElement(propAttribute.Translation, value));
                    }
                }

                rootElement.Add(shipElement);
            }

            // XML fájl mentése
            var xDocument = new XDocument(rootElement);
            xDocument.Save(filePath);

            Console.WriteLine($"Klingon XML generated successfully at: {filePath}");
        }
        public List<SpaceShip> GetAllSpaceShips()
        {
            using (var context = new StarTrekDbContext())
            {
                return context.SpaceShips.ToList();
            }
        }

        public List<SpaceShip> GetSpaceShipsByName(string name)
        {
            using (var context = new StarTrekDbContext())
            {
                return context.SpaceShips
                    .Where(s => s.Name.Contains(name))
                    .ToList();
            }
        }
        public List<SpaceShip> GetSpaceShipsByStatus(SpaceShip.SpaceShipStatus status)
        {
            using (var context = new StarTrekDbContext())
            {
                return context.SpaceShips
                    .Where(s => s.Status == status)
                    .ToList();
            }
        }
        public List<SpaceShip> GetPagedSpaceShipsByStatus(SpaceShip.SpaceShipStatus status, int pageNumber, int pageSize)
        {
            using (var context = new StarTrekDbContext())
            {
                var query = context.SpaceShips
                    .Where(s => s.Status == status)
                    .OrderBy(s => s.Name);

                return GetPagedResults(query, pageNumber, pageSize);
            }
        }


        public List<SpaceShip> GetPagedSpaceShips(int pageNumber, int pageSize)
        {
            using (var context = new StarTrekDbContext())
            {
                var query = context.SpaceShips.OrderBy(s => s.Name);
                return GetPagedResults(query, pageNumber, pageSize);
            }
        }


    }
}
