using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumHelper.Models
{
    public class TaskView
    {
        public List<ProjectUser> ProjectUsers { get; set; }
        public IQueryable<TaskUser> TaskUsers { get; set; }
        public SprintTask SprintTask { get; set; }
        public int CurrentSprintID { get; set; }

        public Sprint Sprint { get; set; }
        public int CurrnetTaskID { get; set; }
    }
}