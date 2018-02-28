using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace ScrumHelper.Models
{
    public class Sprint
    {
        public int Id { get; set; }
        [Display(Name= "Numer Sprintu")]
        public int SprintNumber { get; set; }
        [Display(Name = "Czas trwania (dni)")]
        public int Duration { get; set; }
        public DateTime? DateAdded { get; set; }
        public int ProjectId { get; set; }

        public DateTime? EndDate { get; set; }

        
    }
}