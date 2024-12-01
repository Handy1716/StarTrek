using R6UHP1_HSZF_2024251.Model.Atributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities
{
    [KlingonTranslation("qeylIS")]
    public class SpaceShip
    {
        [KlingonTranslation("chu'")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [KlingonTranslation("pong")]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [KlingonTranslation("Segh")]
        [Required]
        public SpaceShipType Type { get; set; }

        [KlingonTranslation("pe'vIl")]
        [Range(0, int.MaxValue)]
        public int CrewCount { get; set; }

        [KlingonTranslation("Dotlh")]
        [Required]
        public SpaceShipStatus Status { get; set; }

        [KlingonTranslation("yuQHa'")]
        public int? PlanetId { get; set; }

        [ForeignKey("PlanetId")]
        public virtual Planet? Planet { get; set; }
        public virtual ICollection<CrewMember> CrewMembers { get; set; }

        public enum SpaceShipStatus
        {
            Active,
            Inactive,
        }

        public enum SpaceShipType
        {
            Explorer,
            Fighter,
            Cargo,
            Medical,
            Research,
            Battleship
        }
    }

}
