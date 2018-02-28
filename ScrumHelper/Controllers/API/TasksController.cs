using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers.API
{
    public class TasksController : ApiController
    {
        private ApplicationDbContext _context;

        public TasksController()
        {
            _context = new ApplicationDbContext();

        }
        // GET api/<controller>
        public IEnumerable<SprintTask> GetSprintTask(int id)
        {
            var SprintTaskInDb = _context.SprintTasks.ToList();
       
            
            if (SprintTaskInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            IList<SprintTask> tasksInSprint = new List<SprintTask>();

            foreach (var sprintTask in SprintTaskInDb)
            {

                if (sprintTask.SprintId == id)
                {
                    tasksInSprint.Add(sprintTask);
                }
                
            }

            return tasksInSprint.AsEnumerable();
        }

    }
}
