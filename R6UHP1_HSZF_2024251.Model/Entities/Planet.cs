using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R6UHP1_HSZF_2024251.Model.Entities;

namespace R6UHP1_HSZF_2024251.Model.Entities
{
    public class Planet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public PlanetType Type { get; set; }

        public int? ExplorationShipId { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }

        public enum PlanetType
        {
            Terrestrial,
            GasGiant,
            IceGiant,
            Dwarf,
            Exotic,
            Ice
        }
    }
}
