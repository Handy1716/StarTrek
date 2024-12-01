using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class MissionService : BaseService
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
        public bool DeleteMission(int missionId)
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
                        return true;
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to delete Mission: {ex.Message}");
                        return false;
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("Mission not found.");
                    return false;
                }
            }
        }
        public Mission? GetMissionById(int id)
        {
            using (var context = new StarTrekDbContext())
            {
                // Lekérdezi a küldetést az ID alapján
                return context.Missions.FirstOrDefault(m => m.Id == id);
            }
        }

        public bool UpdateMission(Mission mission)
        {
            using (var context = new StarTrekDbContext())
            {
                var existingMission = context.Missions.FirstOrDefault(m => m.Id == mission.Id);

                if (existingMission == null)
                {
                    // Ha a küldetés nem található
                    return false;
                }

                // Frissítjük a küldetés mezőit
                existingMission.TargetPlanetId = mission.TargetPlanetId;
                existingMission.StartDate = mission.StartDate;
                existingMission.EndDate = mission.EndDate;
                existingMission.Status = mission.Status;

                // Adatbázis mentése
                context.SaveChanges();
                return true;
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
        public List<Mission> GetPagedMissions(int pageNumber, int pageSize)
        {
            using (var context = new StarTrekDbContext())
            {
                var query = context.Missions.OrderBy(m => m.StartDate);
                return GetPagedResults(query, pageNumber, pageSize);
            }
        }
    }

}
