using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_DAL.Models
{
    public class MovieDO
    {
        public int MovieID { get; set; }

        public string Title { get; set; }

        public string Rating { get; set; }

        public int Runtime { get; set; }

        public string Director { get; set; }

        public string Synopsis { get; set; }

    }
}