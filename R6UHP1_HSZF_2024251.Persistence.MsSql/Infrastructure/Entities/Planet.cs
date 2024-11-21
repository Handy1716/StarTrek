using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        // Navigation Property
        public ICollection<SpaceShip> SpaceShips { get; set; }
        public ICollection<Mission> Missions { get; set; }
    }
}
