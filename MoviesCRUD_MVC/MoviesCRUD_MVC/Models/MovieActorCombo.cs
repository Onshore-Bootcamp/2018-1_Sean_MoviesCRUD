using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesCRUD_MVC.Models
{
    public class MovieActorCombo
    {
        public MovieActorCombo()
        {
            ActorDropDown = new List<SelectListItem>();
        }

        [DisplayName("Actors")]
        public int ActorId { get; set; }
    
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public List<SelectListItem> ActorDropDown { get; set; }
    }
}