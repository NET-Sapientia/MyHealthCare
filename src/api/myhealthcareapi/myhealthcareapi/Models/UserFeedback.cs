using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class UserFeedback
    {
        public int Id { get; set; }
        public ClientAppointmentWithNames Appointment{ get; set; }
        public string Message { get; set; }
        public float Billing { get; set; }
    }
}
