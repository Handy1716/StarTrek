using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.CrewMember;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.Mission;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.Planet;
using static R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities.SpaceShip;

namespace R6UHP1_HSZF_2024251
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StarTrekDbContext())
            {
                var crewMember = new CrewMember
                {
                    Name = "Spock",
                    Rank = CrewMemberRank.Commander,
                    SpaceShipId = 50,
                    MissionCount = 1
                };

                context.CrewMembers.Add(crewMember);
                context.SaveChanges();

                Console.WriteLine("CrewMember tábla újra létrehozva és adat mentve.");
            }
        }
    }
}
