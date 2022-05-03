using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Not an email address")]
        [MaxLength(100)]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "The password length is not between 8 and 100 characters")]
        public string Password { get; set; }
    }
}
