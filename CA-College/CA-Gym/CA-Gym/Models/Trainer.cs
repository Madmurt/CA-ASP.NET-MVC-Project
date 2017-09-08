using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Trainer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Speciality { get; set; }

        public Trainer()
        {

        }

        public Trainer(string n, int a, string gen, string spec)
        {
            Name = n;
            Age = a;
            Gender = gen;
            Speciality = spec;
        }

    }
}