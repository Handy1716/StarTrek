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
        public string Name { get; set; }
        public string Type { get; set; }
        public int CrewCount { get; set; }
        public string Status { get; set; }
        public int? PlanetId { get; set; }

        // Navigation Property
        public ICollection<Crew> CrewMembers { get; set; }
    }

}
