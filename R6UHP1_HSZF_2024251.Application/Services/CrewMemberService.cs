using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class CrewMemberService : BaseService
    {
        // Delegált az eseményhez
        public delegate void OperationEventHandler(string message);

        // Esemény definiálása
        public event OperationEventHandler? OnOperationCompleted;

        // Create metódus: Új legénységi tag létrehozása
        public void CreateCrewMember(CrewMember newCrewMember)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.CrewMembers.Add(newCrewMember);
                    context.SaveChanges();
                    OnOperationCompleted?.Invoke("CrewMember created successfully.");
                }
                catch (Exception ex)
                {
                    OnOperationCompleted?.Invoke($"Failed to create CrewMember: {ex.Message}");
                }
            }
        }

        // Delete metódus: Legénységi tag törlése
        public bool DeleteCrewMember(int crewMemberId)
        {
            using (var context = new StarTrekDbContext())
            {
                var crewMember = context.CrewMembers.Find(crewMemberId);
                if (crewMember != null)
                {
                    try
                    {
                        context.CrewMembers.Remove(crewMember);
                        context.SaveChanges();
                        OnOperationCompleted?.Invoke("CrewMember deleted successfully.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to delete CrewMember: {ex.Message}");
                        return false;
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("CrewMember not found.");
                    return false;
                }
            }
        }
        public CrewMember? GetCrewMemberById(int id)
        {
            using (var context = new StarTrekDbContext())
            {
                // Lekérdezi a legénységi tagot az ID alapján
                return context.CrewMembers.FirstOrDefault(cm => cm.Id == id);
            }
        }

        public bool UpdateCrewMember(CrewMember crewMember)
        {
            using (var context = new StarTrekDbContext())
            {
                var existingCrewMember = context.CrewMembers.FirstOrDefault(cm => cm.Id == crewMember.Id);

                if (existingCrewMember == null)
                {
                    // Ha a legénységi tag nem található
                    return false;
                }

                // Frissítjük a legénységi tag mezőit
                existingCrewMember.Name = crewMember.Name;
                existingCrewMember.Rank = crewMember.Rank;
                existingCrewMember.MissionCount = crewMember.MissionCount;

                // Adatbázis mentése
                context.SaveChanges();
                return true;
            }
        }
        public List<CrewMember> GetAllCrewMembers()
        {
            using (var context = new StarTrekDbContext())
            {
                return context.CrewMembers.ToList();
            }
        }
        public List<CrewMember> GetCrewMembersByName(string name)
        {
            using (var context = new StarTrekDbContext())
            {
                return context.CrewMembers
                    .Where(cm => cm.Name.Contains(name))
                    .ToList();
            }
        }
        public List<CrewMember> GetPagedCrewMembers(int pageNumber, int pageSize)
        {
            using (var context = new StarTrekDbContext())
            {
                var query = context.CrewMembers.OrderBy(c => c.Name);
                return GetPagedResults(query, pageNumber, pageSize);
            }
        }

        public List<CrewMember> GetCrewMembersByRank(CrewMember.CrewMemberRank rank)
        {
            using (var context = new StarTrekDbContext())
            {
                return context.CrewMembers
                    .Where(cm => cm.Rank == rank)
                    .ToList();
            }
        }
    }
}
