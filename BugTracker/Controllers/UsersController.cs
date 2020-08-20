using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Helpers;

namespace BugTracker.Controllers
{
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
            // spin through all the roles for this user and remove them (techniquely he/she can only have 1 role)
            foreach (var role in roleHelper.ListUserRoles(id))
            {
                roleHelper.RemoveUserFromRole(id, role);
            }

            // add selected user to that role
            if (!string.IsNullOrEmpty(roleName))
            {
                roleHelper.AddUserToRole(id, roleName);
            }

            // now that the user has been assigned a role, redirect them back to the page
            return RedirectToAction("Manage_1UserRole", new { id });
        }

    }
}
