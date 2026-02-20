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
    }
}
