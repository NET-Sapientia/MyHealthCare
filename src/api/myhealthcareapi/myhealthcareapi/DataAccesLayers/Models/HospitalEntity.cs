using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers.Models
{
    public class HospitalEntity
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
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public virtual ICollection<DepartmentEntity> Department { get; set; }
        
    }
}
