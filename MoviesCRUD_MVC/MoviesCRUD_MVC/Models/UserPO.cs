using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesCRUD_MVC.Models
{
    public class UserPO
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        [StringLength(50, MinimumLength = 8)]
        public string Email { get; set; }

        public int RoleID { get; set; }
    }
}