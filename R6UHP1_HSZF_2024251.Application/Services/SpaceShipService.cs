using R6UHP1_HSZF_2024251.Model.Atributes;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Model.Entities;
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


        public SpaceShip? GetSpaceShipById(int id)
        {
            using (var context = new StarTrekDbContext())
            {
                // Lekérdezi az űrhajót az ID alapján
                return context.SpaceShips.FirstOrDefault(s => s.Id == id);
            }
        }

        public bool UpdateSpaceShip(SpaceShip spaceShip)
        {
            using (var context = new StarTrekDbContext())
            {
                var existingSpaceShip = context.SpaceShips.FirstOrDefault(s => s.Id == spaceShip.Id);

                if (existingSpaceShip == null)
                {
                    // Ha az űrhajó nem található
                    return false;
                }

                // Frissítjük a létező SpaceShip mezőit
                existingSpaceShip.Name = spaceShip.Name;
                existingSpaceShip.Type = spaceShip.Type;
                existingSpaceShip.CrewCount = spaceShip.CrewCount;
                existingSpaceShip.Status = spaceShip.Status;

                // Adatbázis mentése
                context.SaveChanges();
                return true;
            }
        }

        public bool DeleteSpaceShip(int spaceShipId)
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
                    return true;
                }
                else
                {
                    return false;
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
            var rootElement = new XElement("qeylIS");

            foreach (var spaceShip in spaceShips)
            {
                var shipElement = new XElement("ghIlghameS");

                foreach (var prop in typeof(SpaceShip).GetProperties())
                {
                    var propAttribute = (KlingonTranslationAttribute)Attribute.GetCustomAttribute(prop, typeof(KlingonTranslationAttribute));
                    if (propAttribute != null)
                    {
                        // Szűrés az XML címkéknél
                        var sanitizedName = SanitizeXmlName(propAttribute.Translation);
                        var value = prop.GetValue(spaceShip)?.ToString() ?? "null";
                        shipElement.Add(new XElement(sanitizedName, value));
                    }
                }

                rootElement.Add(shipElement);
            }

            rootElement.Save(filePath);
            Console.WriteLine($"Report generated successfully at: {filePath}");
        }
        public static string SanitizeXmlName(string name)
        {
            // Csere: távolítsa el az érvénytelen karaktereket
            return new string(name.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());
        }
        //public List<SpaceShip> GetAllSpaceShips()
        //{
        //    using (var context = new StarTrekDbContext())
        //    {
        //        return context.SpaceShips.ToList();
        //    }
        //}

        //public List<SpaceShip> GetSpaceShipsByName(string name)
        //{
        //    using (var context = new StarTrekDbContext())
        //    {
        //        return context.SpaceShips
        //            .Where(s => s.Name.Contains(name))
        //            .ToList();
        //    }
        //}
        //public List<SpaceShip> GetSpaceShipsByStatus(SpaceShip.SpaceShipStatus status)
        //{
        //    using (var context = new StarTrekDbContext())
        //    {
        //        return context.SpaceShips
        //            .Where(s => s.Status == status)
        //            .ToList();
        //    }
        //}
        //public List<SpaceShip> GetPagedSpaceShipsByStatus(SpaceShip.SpaceShipStatus status, int pageNumber, int pageSize)
        //{
        //    using (var context = new StarTrekDbContext())
        //    {
        //        var query = context.SpaceShips
        //            .Where(s => s.Status == status)
        //            .OrderBy(s => s.Name);

        //        return GetPagedResults(query, pageNumber, pageSize);
        //    }
        //}


        public List<SpaceShip> GetPagedSpaceShips(int pageNumber, int pageSize)
        {
            using (var context = new StarTrekDbContext())
            {
                var query = context.SpaceShips.OrderBy(s => s.Name);
                return GetPagedResults(query, pageNumber, pageSize);
            }
        }
        public List<SpaceShip> GetPagedSpaceShipsByNameOrStatus(int pageNumber, int pageSize, string nameOrStatus)
        {
            using (var context = new StarTrekDbContext())
            {
                // Szűrés névre vagy státuszra
                var query = context.SpaceShips
                    .Where(s => s.Name.Contains(nameOrStatus) ||
                                s.Status.ToString() == nameOrStatus) // Nem használunk StringComparison-t
                    .OrderBy(s => s.Name);

                return GetPagedResults(query, pageNumber, pageSize);
            }
        }


    }
}
