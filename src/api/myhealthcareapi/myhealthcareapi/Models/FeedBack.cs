using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public AppointmentEntity Appointment { get; set; }
        public string Message { get; set; }
        public float Billing { get; set; }
    }
}
