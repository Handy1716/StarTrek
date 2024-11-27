using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    public class Crew
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public int SpaceShipId { get; set; }
        public int MissionCount { get; set; }

        // Navigation Property
        public SpaceShip SpaceShip { get; set; }
    }
}
