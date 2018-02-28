using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumHelper.Models
{
    public enum Status
    {
        ToDo, InProggress, Tested, Done
    }
    public class SprintTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public DateTime? DataAdded { get; set; }
        public Status? Status { get; set; }
        public int SprintId { get; set; }
        public int UserId { get; set; }

        public virtual Sprint Sprint { get; set; }

        public virtual ICollection<User> UsersInTask { get; set; }

    }
}