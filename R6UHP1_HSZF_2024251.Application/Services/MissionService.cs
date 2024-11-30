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
        // Delegált az eseményhez
        public delegate void OperationEventHandler(string message);

        // Esemény definiálása
        public event OperationEventHandler? OnOperationCompleted;

        // Create metódus: Új küldetés létrehozása
        public void CreateMission(Mission newMission)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.Missions.Add(newMission);
                    context.SaveChanges();
                    OnOperationCompleted?.Invoke("Mission created successfully.");
                }
                catch (Exception ex)
                {
                    OnOperationCompleted?.Invoke($"Failed to create Mission: {ex.Message}");
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
                        OnOperationCompleted?.Invoke("Mission deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to delete Mission: {ex.Message}");
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("Mission not found.");
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
                        updateAction(spaceShip); // Frissítési logika kívülről érkezik
                        context.SaveChanges();
                        OnOperationCompleted?.Invoke("SpaceShip updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to update SpaceShip: {ex.Message}");
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("SpaceShip not found.");
                }
            }
        }
        public void GenerateMissionReport(string filePath)
        {
            using (var context = new StarTrekDbContext())
            {
                // Az aktuális év
                var oneYearAgo = DateTime.Now.AddYears(-1);

                // Lekérjük az elmúlt évben végrehajtott küldetéseket
                var completedMissions = context.Missions
                    .Where(m => m.Status == Mission.MissionStatus.Completed && m.EndDate.HasValue && m.EndDate.Value > oneYearAgo)
                    .OrderBy(m => m.EndDate)
                    .ToList();

                // Riport generálása szöveges formában
                var reportLines = new List<string>
        {
            "Mission Report - Completed Missions in the Last Year",
            "---------------------------------------------------"
        };

                foreach (var mission in completedMissions)
                {
                    reportLines.Add($"Mission ID: {mission.Id}");
                    reportLines.Add($"Target Planet ID: {mission.TargetPlanetId}");
                    reportLines.Add($"Start Date: {mission.StartDate}");
                    reportLines.Add($"End Date: {mission.EndDate}");
                    reportLines.Add($"Status: {mission.Status}");
                    reportLines.Add("---------------------------------------------------");
                }

                // TXT fájl mentése
                File.WriteAllLines(filePath, reportLines);

                Console.WriteLine($"Report generated successfully at: {filePath}");
            }
        }


        public List<Mission> GetAllMissions()
        {
            using (var context = new StarTrekDbContext())
            {
                return context.Missions.ToList();
            }
        }
        public List<Mission> GetMissionsByStatus(Mission.MissionStatus status)
        {
            using (var context = new StarTrekDbContext())
            {
                return context.Missions
                    .Where(m => m.Status == status)
                    .ToList();
            }
        }
    }

}
