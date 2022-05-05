using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class AddFeedbackModel
    {
        public int AppointmentId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public float Billing { get; set; }
    }
}
