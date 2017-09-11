using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class UserLogin
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is Required")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}