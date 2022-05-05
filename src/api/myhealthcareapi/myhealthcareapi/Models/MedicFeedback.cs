using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class MedicFeedback
    {
        public int Id { get; set; }
        public MedicAppointmentWithNames Appointment { get; set; }
        public string Message { get; set; }
        public float Billing { get; set; }
    }
}
