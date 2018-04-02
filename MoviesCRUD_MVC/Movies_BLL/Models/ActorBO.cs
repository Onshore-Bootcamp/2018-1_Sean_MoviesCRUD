using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_BLL.Models
{
    public class ActorBO
    {
        public int ActorID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Bio { get; set; }

        public string Trivia { get; set; }

        public string Quotes { get; set; }
    }
}
