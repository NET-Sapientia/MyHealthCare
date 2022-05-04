using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class AddClientModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name maximum length is 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Not an email address")]
        [MaxLength(100, ErrorMessage = "Email maximum length is 100")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "Address maximum length is 100")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password length must be between 8 and 100 characters")]
        public string Password { get; set; }

    }
}
