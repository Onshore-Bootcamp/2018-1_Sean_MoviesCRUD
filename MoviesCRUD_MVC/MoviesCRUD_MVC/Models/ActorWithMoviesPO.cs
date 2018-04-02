using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesCRUD_MVC.Models
{
    public class ActorWithMoviesPO
    {
        public ActorPO Actor { get; set; } = new ActorPO();
        public List<MoviePO> Movies { get; set; } = new List<MoviePO>();
    }
}