using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Rotativa;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers
{
    [System.Web.Mvc.Authorize]
    public class RaportController : Controller
    {
        // GET: Raport
        private ApplicationDbContext _context;

        public RaportController()
        {
            _context = new ApplicationDbContext();
        }
        public async Task<ActionResult> Raport(int projectID)
        {
            ViewModel viewmodel = new ViewModel();
            var userID = User.Identity.GetUserId();
            string ID;
            var user = _context.Users.SingleOrDefault(u => u.ApplicationUserId == userID);


            var req2 = WebRequest.Create(@"http://localhost:52049/api/sprints/" + projectID.ToString());
            var r2 = await req2.GetResponseAsync().ConfigureAwait(false);

            var responseReader2 = new StreamReader(r2.GetResponseStream());
            var responseData2 = await responseReader2.ReadToEndAsync();

            var d2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sprint>>(responseData2);
            if (d2 == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            viewmodel.Sprints = d2.ToList();

            List<SprintTask> tempList = new List<SprintTask>();
            foreach (var sprint in d2)
            {
                var req = WebRequest.Create(@"http://localhost:52049/api/tasks/" + sprint.Id.ToString());
                var r = await req.GetResponseAsync().ConfigureAwait(false);

                var responseReader = new StreamReader(r.GetResponseStream());
                var responseData = await responseReader.ReadToEndAsync();

                var d = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SprintTask>>(responseData);
                if (d == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                foreach (var taskTemp in d)
                {
                    tempList.Add(taskTemp);
                }


            }
            viewmodel.SprintTasks = tempList.AsEnumerable();
            viewmodel.CurrentProjectID = projectID;

            Raport raport = new Raport();
            List<TaskView> taskList = new List<TaskView>();
            foreach (var tasktemp in viewmodel.SprintTasks)
            {
                TaskView task = new TaskView();
                task.CurrentSprintID = tasktemp.SprintId;
                task.CurrnetTaskID = tasktemp.Id;
                task.SprintTask = _context.SprintTasks.SingleOrDefault(t => t.Id == tasktemp.Id);
                task.ProjectUsers = _context.ProjectUsers.Where(u => u.ProjectId == projectID).ToList();
                task.TaskUsers = _context.TaskUsers.Where(u => u.SprintTaskID == tasktemp.Id);
                taskList.Add(task);
            }
           
            

            
            raport.ViewModel = viewmodel;
            raport.TaskViews = taskList;
            return new Rotativa.ViewAsPdf(raport);
        }
    }
}