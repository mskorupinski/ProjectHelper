using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumHelper.Models
{
    public class UserWithProject
    {
        public IEnumerable<ProjectUser> ProjectUsers { get; set; }
        public IEnumerable<User> Users { get; set; }

        public int CurrentProjectID { get; set; }
    }
}