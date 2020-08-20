namespace BugTracker.Migrations
{
    using BugTracker.Helpers;
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        private ProjectHelper projectHelper = new ProjectHelper();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            #region Role Creation
            var roleManager = new RoleManager<IdentityRole>(
               new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole() { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole() { Name = "Submitter" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole() { Name = "Project Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole() { Name = "Developer" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            #endregion
            #region User Creation And Assingment
            if (!context.Users.Any(u => u.Email == "bnnt92708@gmail.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "bnnt92708@gmail.com",
                    UserName = "bnnt92708@gmail.com",
                    FirstName = "Khoa",
                    LastName = "Nguyen",
                    DisplayName = "Noob",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Learntocode$2020");

                var userId = userManager.FindByEmail("bnnt92708@gmail.com").Id;
                userManager.AddToRole(userId, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "jasongoodman@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "jasongoodman@mailinator.com",
                    UserName = "jasongoodman@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Professor",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("jasongoodman@mailinator.com").Id;
                userManager.AddToRole(userId, "Project Manager");
            }

            if (!context.Users.Any(u => u.Email == "yunobigboi@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "yunobigboi@mailinator.com",
                    UserName = "yunobigboi@mailinator.com",
                    FirstName = "Yuno",
                    LastName = "Nguyen",
                    DisplayName = "Yuno",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("yunobigboi@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "rickystar@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "rickystar@mailinator.com",
                    UserName = "rickystar@mailinator.com",
                    FirstName = "Ricky",
                    LastName = "Tran",
                    DisplayName = "Ricky",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("rickystar@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "tage@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "tage@mailinator.com",
                    UserName = "tage@mailinator.com",
                    FirstName = "tage",
                    LastName = "Tran",
                    DisplayName = "tage",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("tage@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "karik@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "karik@mailinator.com",
                    UserName = "karik@mailinator.com",
                    FirstName = "karik",
                    LastName = "Vu",
                    DisplayName = "Karik",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("karik@mailinator.com").Id;
                userManager.AddToRole(userId, "Submitter");
            }

            if (!context.Users.Any(u => u.Email == "wowy@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "wowy@mailinator.com",
                    UserName = "wowy@mailinator.com",
                    FirstName = "wowy",
                    LastName = "nguyen",
                    DisplayName = "wowy",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("wowy@mailinator.com").Id;
                userManager.AddToRole(userId, "Submitter");
            }

            if (!context.Users.Any(u => u.Email == "binz@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "binz@mailinator.com",
                    UserName = "binz@mailinator.com",
                    FirstName = "binz",
                    LastName = "vu",
                    DisplayName = "binz",
                    AvatarPath = "/Avatar/default_user.png"
                }, "Abc&123");

                var userId = userManager.FindByEmail("binz@mailinator.com").Id;
                userManager.AddToRole(userId, "Submitter");
                #endregion
                context.SaveChanges();

                #region TicketType Seed
                context.TicketTypes.AddOrUpdate
                    (
                        tt => tt.Name,
                        new TicketType() { Name = "Software" },
                        new TicketType() { Name = "Hardware" },
                        new TicketType() { Name = "UI" },
                        new TicketType() { Name = "Defect" },
                        new TicketType() { Name = "Other" }
                    );
                #endregion

                #region TicketPriority Seed
                context.TicketPriorities.AddOrUpdate
                    (
                        tp => tp.Name,
                        new TicketPriority() { Name = "Low" },
                        new TicketPriority() { Name = "Medium" },
                        new TicketPriority() { Name = "High" },
                        new TicketPriority() { Name = "On Hold" }
                    );
                #endregion

                #region TicketStatus Seed
                context.TicketStatuses.AddOrUpdate
                    (
                        ts => ts.Name,
                        new TicketStatus() { Name = "Open" },
                        new TicketStatus() { Name = "Assigned" },
                        new TicketStatus() { Name = "Resolved" },
                        new TicketStatus() { Name = "Reopened" },
                        new TicketStatus() { Name = "Archived" }
                    );
                #endregion

                #region Project Seed
                context.Projects.AddOrUpdate
                    (
                        p => p.Name,
                        new Project() { Name = "Seed 1", Created = DateTime.Now.AddDays(-60), IsArchived = true },
                        new Project() { Name = "Seed 2", Created = DateTime.Now.AddDays(-45) },
                        new Project() { Name = "Seed 3", Created = DateTime.Now.AddDays(-30), IsArchived = true },
                        new Project() { Name = "Seed 4", Created = DateTime.Now.AddDays(-15) },
                        new Project() { Name = "Seed 5", Created = DateTime.Now.AddDays(-7), IsArchived = true }
                    );
                #endregion
                context.SaveChanges();

                #region Ticket Seed
                //we will do it another day
                #endregion


                #region Assign seeded Users to to Seeded Projects
                var userList = context.Users.ToList();
                var projectList = context.Projects.ToList();
                foreach (var project in projectList)
                {
                    foreach (var user in userList)
                    {
                        projectHelper.AddUserToProject(user.Id, project.Id);
                    }
                }
                #endregion
                context.SaveChanges();
            }
        }
    }
}
