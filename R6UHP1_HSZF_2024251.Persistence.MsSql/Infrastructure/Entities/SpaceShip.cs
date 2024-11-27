using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    public class SpaceShip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        [Required]
        public string Type { get; set; }

        [Required]
        public int CrewCount { get; set; }

        [StringLength(100)]
        [Required]
        public string Status { get; set; }
        [Required]
        public int? PlanetId { get; set; }

        // Navigation Property
        public ICollection<Crew> CrewMembers { get; set; }
    }

}
