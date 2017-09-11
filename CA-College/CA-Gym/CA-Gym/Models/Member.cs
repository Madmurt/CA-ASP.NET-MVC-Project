using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public int TypeID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int IsAdmin { get; set; }

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