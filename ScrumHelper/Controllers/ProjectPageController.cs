using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ScrumHelper.Controllers.API;
using ScrumHelper.Models;
using Microsoft.AspNet.Identity;

namespace ScrumHelper.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ProjectPageController: Controller
    {
        private ApplicationDbContext _context;

        public ProjectPageController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult NewProject()
        {
            return View();
        }


        public async Task<ViewResult> ProjectList()
        {
            var userID = User.Identity.GetUserId();

            var user = _context.Users.SingleOrDefault(u => u.ApplicationUserId == userID);
            var req = WebRequest.Create(@"http://localhost:52049/api/users/" + user.Id.ToString());
            var r = await req.GetResponseAsync().ConfigureAwait(false);

            var responseReader = new StreamReader(r.GetResponseStream());
            var responseData = await responseReader.ReadToEndAsync();

            var d = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Project>>(responseData);
            if (d == null) 
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return View(d);

        }
        
        public ViewResult ProjectView(int projectID)
        {
            var project = _context.Projects.SingleOrDefault(p => p.ID == projectID);
            
            
            return View(project);

        }

        
        public ActionResult SaveProject(Project project)
        {

            var userID = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.ApplicationUserId == userID);
            if (project.ID == 0)
            {
                project.DataAdded = DateTime.Now;               
                _context.Projects.Add(project);
                var userProject = new ProjectUser();
                userProject.ProjectId = project.ID;
                userProject.UserId = user.Id;
                _context.ProjectUsers.Add(userProject);
            }
            else
            {
                var projectInDb = _context.Projects.Single(c => c.ID == project.ID);
                projectInDb.Name = project.Name;
                projectInDb.Notes = project.Notes;
                projectInDb.DataAdded = DateTime.Now;

            }

            _context.SaveChanges();

            return RedirectToAction("ProjectList", "ProjectPage");
        }

        public ActionResult RemoveProject(int projectID)
        {
            var project = _context.Projects.SingleOrDefault(p => p.ID == projectID);
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList", "ProjectPage");
        }





    }
}