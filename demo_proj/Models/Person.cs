using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demo_proj.Models
{
   
    public class Person
    {
        //************************************************************************************
        [Required]
        public DateTime? pAge { get; set; }
        //************************************************************************************
        [Required]
        public string? PName { get; set; }

        //************************************************************************************
        [Required]
        [CanSmoke(nameof(Person.Smoke))]
        public int? Age { get; set; }
        //************************************************************************************
        [Required]
        public bool Smoke { get; set; }
        //************************************************************************************
        [Required]
        public string[]? Grades { get; set; }
        //************************************************************************************
        
        public string JoinGrades()
        {
            if(this.Grades==null)
            {
                return "empty text";
            }
            return string.Join(",", this.Grades);
        }
        
        //************************************************************************************
        [Required]
        public PointCards? PointCards { get; set; }
        //************************************************************************************
    }
}
