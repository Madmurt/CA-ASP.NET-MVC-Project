using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public int TypeID { get; }  //Changed property to get only

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Password must be 5 to 10 characters long", MinimumLength = 5)]
        public string Password { get; set; }

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

        public string Address { get; set; }
        public int IsAdmin { get; set; }
        //WHAT IS THIS FELIX?
        public Role MemberRole { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public Member(int memberID, int typeID, string email, string password, string firstName, string lastName, string gender, int age, string phone, string address, int isAdmin)
        {
            MemberID = memberID;
            TypeID = typeID;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Phone = phone;
            Address = address;
            IsAdmin = isAdmin;
        }
    }
}