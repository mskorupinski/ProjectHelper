using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers.API
{
    public class SprintsController : ApiController
    {
        private ApplicationDbContext _context;

        public SprintsController()
        {
            _context = new ApplicationDbContext();

        }
        //GET /api/sprints/id
        public IEnumerable<Sprint> GetProjectSprint(int id)
        {
            var sprintsInDb = _context.Sprints.ToList();
            
            if (sprintsInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            IList<Sprint> sprints = new List<Sprint>();

            foreach (var sprint in sprintsInDb)
            {
                
                if (sprint.ProjectId == id)
                {
                    sprints.Add(sprint);
                }
                
            }

            return sprints.AsEnumerable();
        }


    }
}
