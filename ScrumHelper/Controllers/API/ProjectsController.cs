using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScrumHelper.Models;
using System.Data.Entity;

namespace ScrumHelper.Controllers.API
{
    public class ProjectsController : ApiController
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
            
        }

        //GET /api/projects
        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.Include(p => p.UsersInProjects).ToList();
        }

        //GET /api/projects/id
        public Project GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.ID == id);

            if (project == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return project;
        }

        //POST /api/projects
        public Project CreateProject(Project project)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }


        //PUT /api/projects/1
        [HttpPut]
        public void UpdateProject(int id, Project project)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var projectInDb = _context.Projects.SingleOrDefault(p => p.ID == id);

            if(projectInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            projectInDb.Name = project.Name;
            projectInDb.Notes = project.Notes;


            _context.SaveChanges();
        }

        //DELETE /api/projects/1
        [HttpDelete]
        public void DeleteProject(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var projectInDb = _context.Projects.SingleOrDefault(p => p.ID == id);

            _context.Projects.Remove(projectInDb);
            _context.SaveChanges();
        }


    }
}
