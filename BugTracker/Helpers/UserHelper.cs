using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class UserHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public string GetFullName(string userId)
        {
            var user = db.Users.Find(userId);
            return user.FullName;
        } 

        public string DisplayName(string userId)
        {
            var user = db.Users.Find(userId);
            return user.DisplayName;
        } 

        public string GetUserRole()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var roleId = db.Users.Where(u => u.UserId == userId);
            return roleId.ToString();

        }


    }
}