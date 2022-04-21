using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers.Models
{
    public class DepartmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Contact { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey(nameof(HospitalId))]
        public HospitalEntity Hospital { get; set; }
        public int HospitalId { get; set; }

        public virtual ICollection<AppointmentEntity> Appointments { get; set; }
        public virtual ICollection<MedicDepartmentEntity> MedicDepartments { get; set; }
    }
}
