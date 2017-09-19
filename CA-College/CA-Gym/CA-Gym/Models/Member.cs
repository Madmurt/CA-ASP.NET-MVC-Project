using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Member
    {
        public int MemTypeID { get; set; }  //Changed property to get only

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Password must be 5 to 10 characters long", MinimumLength = 5)]
        public string MemPass { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }

        public string MemAddress { get; set; }
        public bool IsAdmin { get; set; }
        //WHAT IS THIS FELIX?
        public Role MemberRole { get; set; }

        //[Required]
        [Display(Name = "Confirm Password")]
        [Compare("MemPass", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public Member()
        {

        }

        public Member(int memTypeID, string email, string memPass, string firstName, string lastName, string gender, int age, string phone, string memAddress, bool isAdmin)
        {
            MemTypeID = memTypeID;
            Email = email;
            MemPass = memPass;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Phone = phone;
            MemAddress = memAddress;
            IsAdmin = isAdmin;
        }
    }
}