using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesCRUD_MVC.Models
{
    public class ActorPO
    {
        [Required]
        public int ActorID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Bio")]
        public string Bio { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Trivia")]
        public string Trivia { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Quotes")]
        public string Quotes { get; set; }
    }
}