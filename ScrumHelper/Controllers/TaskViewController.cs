using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScrumHelper.Models;
using System.Net.Mail;
using System.Net;
using Rotativa;
namespace ScrumHelper.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TaskViewController : Controller
    {
        private ApplicationDbContext _context;

        public TaskViewController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: TaskView


        public ActionResult TaskPage(int taskID, int sprintID, int projectID)
        {
            TaskView task = new TaskView();
            task.CurrentSprintID = sprintID;
            task.CurrnetTaskID = taskID;
            task.SprintTask = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            task.ProjectUsers = _context.ProjectUsers.Where(u => u.ProjectId == projectID).ToList();
            task.TaskUsers = _context.TaskUsers.Where(u => u.SprintTaskID == taskID);
            return View(task);
        }


        public ActionResult ChangeStatusTaskUp(int taskID)
        {
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            task.Status += 1;

            _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new {sprint.ProjectId});

        }

        public ActionResult ChangeStatusTaskDown(int taskID)
        {
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            task.Status -= 1;

            _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new {sprint.ProjectId});

        }

        public ActionResult RemoveTask(int taskID)
        {
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);

            _context.SprintTasks.Remove(task);
            _context.SaveChanges();
            var sprint = _context.Sprints.SingleOrDefault(m => m.Id == task.SprintId);
            return RedirectToAction("Index", "ViewModel", new {sprint.ProjectId});
        }

        public ActionResult NewTask(int sprintID)
        {
            SprintTask task = new SprintTask();
            task.SprintId = sprintID;
            return View(task);
        }

        public ActionResult UserInPro(int projectID, int taskID, int sprintID)
        {
            TaskView task = new TaskView();
            task.CurrentSprintID = sprintID;
            task.CurrnetTaskID = taskID;
            task.SprintTask = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            task.ProjectUsers = _context.ProjectUsers.Where(u => u.ProjectId == projectID).ToList();
            task.TaskUsers = _context.TaskUsers.Where(u => u.SprintTaskID == taskID);
            return View(task);
        }

        public ActionResult AddUserToTask(int userID, int taskID, int projectID, int sprintID)
        {
            if (!_context.TaskUsers.Any(p => p.UserID == userID && p.SprintTaskID == taskID))
            {
                TaskUser taskUser = new TaskUser();
                taskUser.UserID = userID;
                taskUser.SprintTaskID = taskID;
                _context.TaskUsers.Add(taskUser);
                _context.SaveChanges();
            }

            return RedirectToAction("UserInPro", "TaskView", new {projectID, taskID, sprintID});
        }

        public ActionResult RemoveUserFromTask(int userID, int TaskID, int sprintID, int projectID)
        {
            var user = _context.TaskUsers.SingleOrDefault(u => u.UserID == userID && u.SprintTaskID == TaskID);
            _context.TaskUsers.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("UserInPro", "TaskView", new {projectID, TaskID, sprintID});
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

        public ActionResult Back(int projectID)
        {
            return RedirectToAction("Index", "ViewModel", new {projectID});
        }


        public ActionResult Reminder(int userID, int taskID, int sprintID, int projectID)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userID);
            var task = _context.SprintTasks.SingleOrDefault(t => t.Id == taskID);
            var project = _context.Projects.SingleOrDefault(p => p.ID == projectID);
            var sprint = _context.Sprints.SingleOrDefault(s => s.Id == sprintID);

            var fromAddress = new MailAddress("m.skorupinski11@gmail.com", "From Name");
            var toAddress = new MailAddress(user.Mail, "To"+user.Name+ " "+ user.Lastname);
            const string fromPassword = ""; //hasło 
            string subject = task.Name;
            string body = "Witaj, " + user.Name + "\n\n" +
                          "Pamiętaj o wykonaniu zadania: " + task.Name + " z projektu " + project.Name + ". \n"
                          + "Termin to: " + sprint.EndDate.Value.Date.ToString()
                          + "\n" 
                ;
                          

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return RedirectToAction("TaskPage", "TaskView", new {taskID, sprintID, projectID});
        }
    }
}