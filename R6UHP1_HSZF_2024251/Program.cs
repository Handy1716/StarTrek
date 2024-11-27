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
                // Hozz létre egy űrhajót, ha még nincs
                var spaceship = context.SpaceShips.FirstOrDefault();
                if (spaceship == null)
                {
                    spaceship = new SpaceShip
                    {
                        Name = "USS Enterprise",
                        Type = SpaceShipType.Battleship,
                        CrewCount = 0,
                        Status = SpaceShipStatus.Active
                    };

                    context.SpaceShips.Add(spaceship);
                    context.SaveChanges();
                }

                // Hozz létre egy legénységi tagot az űrhajóhoz
                var crewMember = new CrewMember
                {
                    Name = "Spock",
                    Rank = CrewMemberRank.Commander,
                    SpaceShipId = spaceship.Id, // Hozzárendelés az űrhajóhoz
                    MissionCount = 5
                };

                context.CrewMembers.Add(crewMember);
                context.SaveChanges();

                Console.WriteLine("CrewMember mentve az adatbázisba.");
            }
        }
    }
}
