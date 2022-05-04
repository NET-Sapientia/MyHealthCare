using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.DataAccesLayers.Models
{
    public class ClientAppointmentWithNamesEntity
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string MedicName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Notes { get; set; }
    }
}
