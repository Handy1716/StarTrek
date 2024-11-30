using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class MissionService
    {
        // Create metódus: Új küldetés hozzáadása
        public void CreateMission(Mission newMission)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.Missions.Add(newMission);
                    context.SaveChanges();
                    Console.WriteLine("Mission created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create Mission: {ex.Message}");
                }
            }
        }

        // Delete metódus: Küldetés törlése
        public void DeleteMission(int missionId)
        {
            using (var context = new StarTrekDbContext())
            {
                var mission = context.Missions.Find(missionId);
                if (mission != null)
                {
                    try
                    {
                        context.Missions.Remove(mission);
                        context.SaveChanges();
                        Console.WriteLine("Mission deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to delete Mission: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Mission not found.");
                }
            }
        }
    }
}
