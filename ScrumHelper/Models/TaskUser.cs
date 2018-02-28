using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumHelper.Models
{
    public class TaskUser
    {
        public int Id { get; set; }
        public int SprintTaskID { get; set; }
        public int UserID { get; set; }
        public virtual SprintTask SprintTask { get; set; }
        public virtual User User { get; set; }
    }
}