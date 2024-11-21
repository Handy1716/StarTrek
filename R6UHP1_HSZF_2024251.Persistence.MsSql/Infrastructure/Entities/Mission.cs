using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    public class Mission
    {
        public int Id { get; set; }
        public int? TargetPlanetId { get; set; }
        public int? SpaceShipId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        // Navigation Properties
        public Planet TargetPlanet { get; set; }
        public SpaceShip SpaceShip { get; set; }
        public ICollection<Crew> CrewMembers { get; set; }
    }
}
