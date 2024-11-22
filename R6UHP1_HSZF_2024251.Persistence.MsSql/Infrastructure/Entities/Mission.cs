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
        public int? TargetPlanetId { get; set; } // Opcionális kapcsolat a Planet entitással
        public int? SpaceShipId { get; set; } // Opcionális kapcsolat a SpaceShip entitással
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        // Navigation Properties
        public Planet TargetPlanet { get; set; }
        public SpaceShip SpaceShip { get; set; }
        public ICollection<Crew> CrewMembers { get; set; }
    }
}
