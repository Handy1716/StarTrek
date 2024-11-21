using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    public class SpaceShip
    {
        public int Id { get; set; }
        int alma;
        public string Name { get; set; }
        public string Type { get; set; }
        public int CrewCount { get; set; }
        public string Status { get; set; }

        // Navigation Property
        public int? PlanetId { get; set; }
        public Planet Planet { get; set; }
        public ICollection<Crew> CrewMembers { get; set; }
        public ICollection<Mission> Missions { get; set; }
    }

}
