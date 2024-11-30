using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class PlanetService
    {
        // Create metódus: Új bolygó hozzáadása
        public void CreatePlanet(Planet newPlanet)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.Planets.Add(newPlanet);
                    context.SaveChanges();
                    Console.WriteLine("Planet created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create Planet: {ex.Message}");
                }
            }
        }

        // Delete metódus: Bolygó törlése és hozzá kapcsolódó küldetések törlése
        public void DeletePlanet(int planetId)
        {
            using (var context = new StarTrekDbContext())
            {
                var planet = context.Planets.Find(planetId);
                if (planet != null)
                {
                    try
                    {
                        // Töröljük a bolygóhoz kapcsolódó küldetéseket
                        var missions = context.Missions.Where(m => m.TargetPlanetId == planetId).ToList();
                        context.Missions.RemoveRange(missions);

                        // Töröljük magát a bolygót
                        context.Planets.Remove(planet);
                        context.SaveChanges();

                        Console.WriteLine("Planet and related Missions deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to delete Planet: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Planet not found.");
                }
            }
        }
    }
}

