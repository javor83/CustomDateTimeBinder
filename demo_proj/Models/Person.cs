using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demo_proj.Models
{
   
    public class Person
    {
        [Required]
        public DateTime pAge { get; set; }

        [Required]
        public string PName { get; set; }


        [Required]
        [CanSmoke(nameof(Person.Smoke))]
        public int Age { get; set; }

        [Required]
        public bool Smoke { get; set; }

        public string[] Grades { get; set; } = new string[] { "A","B","C"};

        public string JoinGrades()
        {
            return string.Join(",", this.Grades);
        }

        public PointCards PointCards { get; set; } = new PointCards() { X = 10, Y = 20 };
    }
}
