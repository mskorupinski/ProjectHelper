using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ScrumHelper.Models
{
    public class Project
    {
        public int ID {get; set;}

        [Display(Name = "Nazwa projektu")]
        public string Name { get; set; }

        [Display(Name = "Opis projektu")]
        public string Notes { get; set; }

        public DateTime? DataAdded { get; set; }

        public virtual ICollection<User> UsersInProjects { get; set; }
    }

    
}