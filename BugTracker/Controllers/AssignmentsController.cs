using BugTracker.Helpers;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();
        private ProjectHelper projectHelper = new ProjectHelper();

        #region RoleAssignments
        // GET: Assignments
        //[Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult ManageRoles()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "FullName");
            ViewBag.RoleName = new SelectList(db.Roles.Where(r => r.Name != "Admin"), "Name", "Name"); // give me all the role options that are not "Admin"
            return View(db.Users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string roleName)
        {
            if (userIds == null)
            {
                return RedirectToAction("RolesIndex"); // redirect here because no one is picked
            }
            else
            {
                foreach (var userId in userIds)
                {
                    //step 1: remove them from their roles
                    var userRoles = roleHelper.ListUserRoles(userId).ToList();
                    foreach (var role in userRoles)
                    {
                        roleHelper.RemoveUserFromRole(userId, role);
                    }
                    //step 2: after all people removed from previous roles, if a role is then chosen, add each person to that role
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        roleHelper.AddUserToRole(userId, roleName);
                    }

                }
            }
            return RedirectToAction("ManageRoles");
        }

        public ActionResult ManageUserRole()
        {
            return View();
        }
        #endregion

        #region Project Assignments
        public ActionResult ManageProjectUsers()
        {
            // I want 2 List Boxes in my View. Therefore, I want to load up 2 MultiSelectLists
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "FullName");

            // I load up my projects list box
            ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name");


            return View(db.Users.ToList());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(List<string> userIds, List<int> projectIds)
        {
            // If either user or project is left "unselected", redirect ... 
            if (userIds == null || projectIds == null)
            {
                // let user know that they cannot ignore any selection
                return RedirectToAction("ManageProjectUsers");

            }

            // other than that, iterate over each selected user and add them to the selected projects
            foreach (var userId in userIds)
            {
                //Assign this person to each project
                foreach (var projectId in projectIds)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }

            return RedirectToAction("ManageProjectUsers");
        }


        public ActionResult Manage_1UserProjects(string id)
        {
            var myProjectIds = projectHelper.ListUserProjects(id).Select(p => p.Id).ToList();
            ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name", myProjectIds);
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage_1UserProjects(string userId, List<int> projectIds)
        {
            // step 1: remove this user from all projects
            foreach (var project in projectHelper.ListUserProjects(userId))
            {
                projectHelper.RemoveUserFromProject(userId, project.Id);

            }

            // step 2: add this user back to the selected projects
            if (projectIds != null)
            {
                foreach (var projectId in projectIds)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }

            return RedirectToAction("Manage_1UserProjects");
        }

        #endregion


    }
}