using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    //This decoration is used to transfer the validation from the UserMetadata
    //class to our User class.
    [MetadataType(typeof(UserMetadata))]
    public partial class User //This class is necessary because validations applied to the auto 
    {                        // generated User.cs will be lost if the associated table is modified.

        public string ConfirmPassword { get; set; } //This field will be displayed on registration page
                                                    //but will not save to the database.
    }
    //We create a UserMetadata class so we can apply validation to our properties
    public class UserMetadata
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Addresss Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum of six characters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}