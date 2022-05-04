using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers.Models
{
    public class AppointmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(ClientId))]
        public ClientEntity Client { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public DepartmentEntity Department { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(MedicId))]
        public MedicEntity Medic { get; set; }
        public int MedicId { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public virtual ICollection<FeedBackEntity> FeedBacks { get; set; }

    }
}
