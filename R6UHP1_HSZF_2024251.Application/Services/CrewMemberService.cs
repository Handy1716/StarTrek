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
        // Create metódus: Új legénységi tag hozzáadása
        public void CreateCrewMember(CrewMember newCrewMember)
        {
            using (var context = new StarTrekDbContext())
            {
                try
                {
                    context.CrewMembers.Add(newCrewMember);
                    context.SaveChanges();
                    Console.WriteLine("CrewMember created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create CrewMember: {ex.Message}");
                }
            }
        }

        // Delete metódus: Egy legénységi tag törlése
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
                        Console.WriteLine("CrewMember deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to delete CrewMember: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("CrewMember not found.");
                }
            }
        }

    }
}
