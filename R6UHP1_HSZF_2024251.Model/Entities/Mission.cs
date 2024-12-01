using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Model.Entities
{
    public class Mission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TargetPlanetId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public MissionStatus Status { get; set; }

        // Navigation Properties
        [ForeignKey("TargetPlanetId")]
        public virtual Planet TargetPlanet { get; set; }




        public enum MissionStatus
        {
            InProgress,
            Completed,
            Failed
        }
    }
}
