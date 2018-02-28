using System;
using System.Collections.Generic;
using System.Data;
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
    public class SprintController : Controller
    {
        private ApplicationDbContext _context;

        public SprintController()
        {
            _context = new ApplicationDbContext();
        }


        public async Task<ViewResult> SprintList(int projectID)
        {

            var req = WebRequest.Create(@"http://localhost:52049/api/sprints/" + projectID.ToString());
            var r = await req.GetResponseAsync().ConfigureAwait(false);

            var responseReader = new StreamReader(r.GetResponseStream());
            var responseData = await responseReader.ReadToEndAsync();

            var d = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sprint>>(responseData);
            if (d == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return View(d);

        }

        public ActionResult NewSprint(int ProjectID)
        {
            Sprint sprint = new Sprint();
            sprint.ProjectId = ProjectID;
            return View(sprint);
        }

        public ActionResult SaveSprint(Sprint sprint, int ProjectID)
        {
            if (sprint.Id == 0)
            {

                var sprintInDb = _context.Sprints.Single(s => s.Id == sprint.Id);
            }
            else
            {
                sprint.DateAdded = DateTime.Now;
                sprint.ProjectId = ProjectID;
                sprint.SprintNumber = sprint.SprintNumber;
                sprint.Duration = sprint.Duration;
                sprint.EndDate = DateTime.Now.AddDays(sprint.Duration);
                _context.Sprints.Add(sprint);

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "ViewModel", new {ProjectID});
        }

        public ActionResult Remove(int SprintID, int ProjectID)
        {
            Sprint sprint = _context.Sprints.Single(s => s.Id == SprintID);
            
            _context.Sprints.Remove(sprint);
            _context.SaveChanges();
            return RedirectToAction("Index", "ViewModel", new { ProjectID });
        }

    }
}