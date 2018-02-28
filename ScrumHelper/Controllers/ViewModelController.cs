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
using Rotativa;

namespace ScrumHelper.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ViewModelController : Controller
    {


        private ApplicationDbContext _context;

        public ViewModelController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ViewModel
        public async Task<ActionResult> Index(int projectID)
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
                var req = WebRequest.Create(@"http://localhost:52049/api/tasks/"+sprint.Id.ToString());
                var r = await req.GetResponseAsync().ConfigureAwait(false);

                var responseReader = new StreamReader(r.GetResponseStream());
                var responseData = await responseReader.ReadToEndAsync();

                var d = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SprintTask>>(responseData);
                if (d == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                foreach (var task in d)
                {
                    tempList.Add(task);
                }
                

            }
            viewmodel.SprintTasks = tempList.AsEnumerable();
            viewmodel.CurrentProjectID = projectID;

            return View(viewmodel);
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
                foreach (var task in d)
                {
                    tempList.Add(task);
                }


            }
            viewmodel.SprintTasks = tempList.AsEnumerable();
            viewmodel.CurrentProjectID = projectID;
            

            return new Rotativa.ViewAsPdf(viewmodel);
        }
    }
}