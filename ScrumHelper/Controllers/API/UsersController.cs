using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers.API
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();

        }
        // GET api/<controller>
        public IEnumerable<Project> GetUserProjects(int id)
        {
            var projectInDb = _context.Projects.ToList();
            var projectUsersInDb = _context.ProjectUsers.ToList();
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (projectInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            IList<Project> project = new List<Project>();
            foreach (var pro in projectInDb)
            {
                foreach (var proUs in projectUsersInDb)
                {
                    if (pro.ID == proUs.ProjectId && proUs.UserId == user.Id)
                    {
                        project.Add(pro);
                    }
                }
            }
            
            return project.AsEnumerable();
        }

       
    }
}