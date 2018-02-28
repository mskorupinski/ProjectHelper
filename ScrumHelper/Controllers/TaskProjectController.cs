using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TaskProjectController : Controller
    {
        private ApplicationDbContext _context;

            // GET: TaskProject


        public TaskProjectController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult TaskPage(int taskID)
        {
            SprintTask taskTemp = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            return View(taskTemp);
        }

        public ActionResult ChangeStatusTaskUp(int taskID)
        {
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            task.Status += 1;

            _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new { sprint.ProjectId });

        }
        public ActionResult ChangeStatusTaskDown(int taskID)
        {
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            task.Status -= 1;

            _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new { sprint.ProjectId });

        }

        public ActionResult RemoveTask(int taskID)
        {
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);

            _context.SprintTasks.Remove(task);
            _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new { sprint.ProjectId });
        }
        public ActionResult NewTask(int sprintID) 
        {
            SprintTask task = new SprintTask();
            task.SprintId = sprintID;
            return View(task);
        }

        public ActionResult SaveTask(SprintTask task)
        {
            var userID = User.Identity.GetUserId();
            string ID;
            var user = _context.Users.SingleOrDefault(u => u.ApplicationUserId == userID);
            if (task.Id == 0)
            {

                task.DataAdded = DateTime.Now;
                task.Status = Status.ToDo;
                task.Name = task.Name;
                task.Note = task.Note;
                task.UserId = user.Id; 
                _context.SprintTasks.Add(task);
            }
            else
            {

                var taskInDb = _context.SprintTasks.Single(s => s.Id == task.Id);
            }

        
        _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new {sprint.ProjectId});
        }

        
    }
}