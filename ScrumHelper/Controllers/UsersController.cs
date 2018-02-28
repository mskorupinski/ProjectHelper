using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumHelper.Models;

namespace ScrumHelper.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

    }
}