using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesCRUD_MVC.Models
{
    public class LoginPO
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }
    }
}