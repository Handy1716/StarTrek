using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public class CrewMemberService
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
        public void DeleteCrewMember(int crewMemberId)
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
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to delete CrewMember: {ex.Message}");
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("CrewMember not found.");
                }
            }
        }
        public void UpdateCrewMember(int crewMemberId, Action<CrewMember> updateAction)
        {
            using (var context = new StarTrekDbContext())
            {
                var crewMember = context.CrewMembers.Find(crewMemberId);
                if (crewMember != null)
                {
                    try
                    {
                        updateAction(crewMember); // Frissítési logika kívülről érkezik
                        context.SaveChanges();
                        OnOperationCompleted?.Invoke("CrewMember updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        OnOperationCompleted?.Invoke($"Failed to update CrewMember: {ex.Message}");
                    }
                }
                else
                {
                    OnOperationCompleted?.Invoke("CrewMember not found.");
                }
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
