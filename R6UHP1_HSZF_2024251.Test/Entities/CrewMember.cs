using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    public class CrewMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public CrewMemberRank Rank { get; set; }

        [Required]
        public int SpaceShipId { get; set; }
        public int? MissionCount { get; set; }

        // Navigation Property
        [ForeignKey("SpaceShipId")]
        public virtual SpaceShip SpaceShip { get; set; }


        public enum CrewMemberRank
        {
            Captain,      // Kapitány
            Commander,    // Parancsnok
            Lieutenant,   // Hadnagy
            Ensign,       // Zászlós
            Cadet,        // Kadét
            FirstOfficer
        }
    }
}
