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


    }
}
