using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesCRUD_MVC.Models
{
    public class MovieWithActorsPO
    {
        public MoviePO Movie { get; set; } = new MoviePO();
        public List<ActorPO> Actors { get; set; } = new List<ActorPO>();

        public List<SelectListItem> ActorsList { get; set; }
        public int Actor { get; set; }
        
    }
}