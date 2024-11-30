using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class SpaceShipService
    {
        public void CreateSpaceShip(SpaceShip newSpaceShip)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.SpaceShips.Add(newSpaceShip);
                    context.SaveChanges();
                    Console.WriteLine("SpaceShip created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create SpaceShip: {ex.Message}");
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


    }
}
