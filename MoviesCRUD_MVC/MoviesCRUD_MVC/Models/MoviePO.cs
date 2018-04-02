using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesCRUD_MVC.Models
{
    public class MoviePO
    {
        [Required]
        public int MovieID { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Rating")]
        public string Rating { get; set; }
        
        [Required]
        [StringLength(20)]
        [DisplayName("Runtime")]
        public int Runtime { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Director")]
        public string Director { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Synopsis")]
        public string Synopsis { get; set; }

        public IEnumerable<SelectListItem> Movies { get; set; }
    }
}