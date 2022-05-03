using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers.Models
{
    public class MedicDepartmentEntity
    {
        public DepartmentEntity Department { get; set; }
        public int DepartmentId { get; set; }
        public MedicEntity Medic { get; set; }
        public int MedicId { get; set; }
    }
}
