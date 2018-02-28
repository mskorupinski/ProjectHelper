using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers
{
    public class UserWithProjectController : Controller
    {
        // GET: UserWithProject
        private ApplicationDbContext _context;

        public UserWithProjectController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(int projectID)
        {
            UserWithProject userWithProject = new UserWithProject();
            userWithProject.CurrentProjectID = projectID;
            userWithProject.ProjectUsers = _context.ProjectUsers.Where(u => u.ProjectId == projectID);  
            userWithProject.Users = _context.Users.ToList();
            
            return View(userWithProject);
        }

        public ActionResult AddUserToProject(int userID, int projectID)
        {
            if (!_context.ProjectUsers.Any(p => p.UserId == userID && p.ProjectId == projectID))
            {

                ProjectUser projectUser = new ProjectUser();
                projectUser.UserId = userID;
                projectUser.ProjectId = projectID;
                var user = _context.Users.SingleOrDefault(u => u.Id == userID);
                projectUser.User = user;
                _context.ProjectUsers.Add(projectUser);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "UserWithProject", new { projectID });
        }

        public ActionResult RemoveUserFromProject(int userID, int projectID)
        {
            var user = _context.ProjectUsers.SingleOrDefault(u => u.UserId == userID && u.ProjectId == projectID);
            var sprints = _context.Sprints.Where(s => s.ProjectId == projectID);
            foreach (var s in sprints)
            {
                var tasks = _context.SprintTasks.Where(t => t.SprintId == s.Id);
                foreach (var t in tasks)
                {
                    var usertask = _context.TaskUsers.Where(u => u.SprintTaskID == t.Id && u.UserID == userID);
                    _context.TaskUsers.RemoveRange(usertask);
                }
            }
            _context.ProjectUsers.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "UserWithProject", new { projectID });
        }

        public ActionResult Back(int projectID)
        {
            return RedirectToAction("Index", "ViewModel", new { projectID });
        }
    }
}