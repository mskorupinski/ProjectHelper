using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ScrumHelper.Models
{
    public class User
    {
        public int Id { get; set; }
        public virtual ApplicationUser UserID{ get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Imię ")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nazwisko ")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Adres e-mail ")]
        public string Mail { get; set; }
        public virtual ICollection<ProjectUser> UsersInProjectUserses { get; set; }




    }
}