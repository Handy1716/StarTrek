using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class PlanetService : BaseService
    {
        // Delegált az eseményhez
        public delegate void OperationEventHandler(string message);

        // Esemény definiálása
        public event OperationEventHandler? OnOperationCompleted;
        // Create metódus: Új bolygó hozzáadása
        public void CreatePlanet(Planet newPlanet)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.Planets.Add(newPlanet);
                    context.SaveChanges();
                    OnOperationCompleted?.Invoke("Planet created successfully.");
                }
                catch (Exception ex)
                {
                    OnOperationCompleted?.Invoke($"Failed to create Planet: {ex.Message}");
                }
            }
        }

        // Delete metódus: Bolygó törlése és hozzá kapcsolódó küldetések törlése
        public bool DeletePlanet(int planetId)
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

                        return false;
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to delete Planet: {ex.Message}");
                        return false;
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("Planet not found.");
                    return false;
                }
            }
        }
        public void UpdatePlanet(int planetId, Action<Planet> updateAction)
        {
            using (var context = new StarTrekDbContext())
            {
                var planet = context.Planets.Find(planetId);
                if (planet != null)
                {
                    try
                    {
                        updateAction(planet); // Frissítési logika kívülről érkezik
                        context.SaveChanges();
                        OnOperationCompleted?.Invoke("Planet updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to update Planet: {ex.Message}");
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("Planet not found.");
                }
            }
        }

        public List<Planet> GetPagedPlanets(int pageNumber, int pageSize)
        {
            using (var context = new StarTrekDbContext())
            {
                var query = context.Planets.OrderBy(p => p.Name);
                return GetPagedResults(query, pageNumber, pageSize);
            }
        }
    }
}

