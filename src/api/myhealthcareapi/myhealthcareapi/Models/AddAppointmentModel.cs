using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class AddAppointmentModel
    {
        public int ClientId { get; set; }

        public int DepartmentId { get; set; }
        public int MedicId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Notes { get; set; }
    }
}
