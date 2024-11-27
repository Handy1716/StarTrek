using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;

namespace R6UHP1_HSZF_2024251
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StarTrekDbContext())
            {
                var spaceship = new SpaceShip
                {
                    Name = "USS Enterprise",
                    Type = "Explorer",
                    CrewCount = 100,
                    Status = "Active"
                };

                context.SpaceShips.Add(spaceship);
                context.SaveChanges();

                Console.WriteLine("Űrhajó mentve az adatbázisba.");
            }
        }
    }
}
