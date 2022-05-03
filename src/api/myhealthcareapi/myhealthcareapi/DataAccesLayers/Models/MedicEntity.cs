using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers.Models
{
    public class MedicEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public DateTime? HireDate { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        public virtual ICollection<AppointmentEntity> Appointments { get; set; }
        public virtual ICollection<MedicDepartmentEntity> MedicDepartments { get; set; }
    }
}
