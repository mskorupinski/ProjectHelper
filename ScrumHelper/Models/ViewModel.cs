using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScrumHelper.Controllers.API;

namespace ScrumHelper.Models
{
    public class ViewModel
    {
        public IEnumerable<SprintTask> SprintTasks { get; set; }
        public IEnumerable<Sprint> Sprints { get; set; }

        public int CurrentProjectID { get; set; }
    }
}