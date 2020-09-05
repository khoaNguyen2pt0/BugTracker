using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Helpers;

namespace BugTracker.Controllers
{
    [RequireHttps]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        // GET: User
        public ActionResult Index()
        {

            return View(db.Users.ToList());
        }

        public ActionResult Manage_1UserRole(string id)
        {
            // if this user already occupies a role, i need to display that role in the dropdown
            var userRole = roleHelper.ListUserRoles(id).FirstOrDefault();
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name", userRole);
            return View(db.Users.Find(id));
        }


        // POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage_1UserRole(string id, string roleName)
        {
            foreach (var role in roleHelper.ListUserRoles(id))
            {
                roleHelper.RemoveUserFromRole(id, role);
            }
            if (!string.IsNullOrEmpty(roleName))
            {
                roleHelper.AddUserToRole(id, roleName);
            }
            return RedirectToAction("Manage_1UserRole", new { id });
        }

    }
}
