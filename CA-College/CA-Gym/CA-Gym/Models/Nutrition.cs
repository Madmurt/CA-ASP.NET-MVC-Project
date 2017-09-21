using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA_Gym.Models
{
    public class Nutrition
    {

        public string Name { get; set; }

        [Required]
        public string Advice { get; set; }

        public Nutrition()
        {

        }

        public Nutrition(string name, string advice)
        {
            Name = name;
            Advice = advice;
        }
    }
}