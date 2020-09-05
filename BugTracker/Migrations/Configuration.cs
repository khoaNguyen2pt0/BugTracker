namespace BugTracker.Migrations
{
    using BugTracker.Helpers;
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        private ProjectHelper projectHelper = new ProjectHelper();
        private RoleHelper roleHelper = new RoleHelper();
        Random random = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(
               new RoleStore<IdentityRole>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            #region Role Creation
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
            #endregion

            #region User Creation And Assingment
            if (!context.Users.Any(u => u.Email == "demoadmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "demoadmin@mailinator.com",
                    UserName = "demoadmin@mailinator.com",
                    FirstName = "Anthony",
                    LastName = "Brown",
                    DisplayName = "Admin Anthony",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("demoadmin@mailinator.com").Id;
                userManager.AddToRole(userId, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "demopm@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "demopm@mailinator.com",
                    UserName = "demopm@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twitchell",
                    DisplayName = "Manager Jason",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("demopm@mailinator.com").Id;
                userManager.AddToRole(userId, "Project Manager");
            }

            if (!context.Users.Any(u => u.Email == "andrewrussel@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "andrewrussel@mailinator.com",
                    UserName = "andrewrussel@mailinator.com",
                    FirstName = "Andrew",
                    LastName = "Russel",
                    DisplayName = "Senior",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("andrewrussel@mailinator.com").Id;
                userManager.AddToRole(userId, "Project Manager");
            }

            if (!context.Users.Any(u => u.Email == "demodeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "demodeveloper@mailinator.com",
                    UserName = "demodeveloper@mailinator.com",
                    FirstName = "Dave",
                    LastName = "Edward",
                    DisplayName = "Developer Dave",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("demodeveloper@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "dennischoi@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "dennischoi@mailinator.com",
                    UserName = "dennischoi@mailinator.com",
                    FirstName = "Dennis",
                    LastName = "Choi",
                    DisplayName = "Dennis C",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("dennischoi@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "dianafranklyn@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "dianafranklyn@mailinator.com",
                    UserName = "dianafranklyn@mailinator.com",
                    FirstName = "Diana",
                    LastName = "Franklyn",
                    DisplayName = "Diana F",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("dianafranklyn@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "daitotsuchiya@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "daitotsuchiya@mailinator.com",
                    UserName = "daitotsuchiya@mailinator.com",
                    FirstName = "Daito",
                    LastName = "Tsuchiya",
                    DisplayName = "Daito T",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("daitotsuchiya@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "denvau@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "denvau@mailinator.com",
                    UserName = "denvau@mailinator.com",
                    FirstName = "Den",
                    LastName = "Vau",
                    DisplayName = "Den V",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("denvau@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "dennisdang@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "dennisdang@mailinator.com",
                    UserName = "dennisdang@mailinator.com",
                    FirstName = "Dennis",
                    LastName = "Dang",
                    DisplayName = "Dennis D",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("dennisdang@mailinator.com").Id;
                userManager.AddToRole(userId, "Developer");
            }


            if (!context.Users.Any(u => u.Email == "demosubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "demosubmitter@mailinator.com",
                    UserName = "demosubmitter@mailinator.com",
                    FirstName = "Sam",
                    LastName = "Ericson",
                    DisplayName = "Submitter Sam",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("demosubmitter@mailinator.com").Id;
                userManager.AddToRole(userId, "Submitter");
            }

            if (!context.Users.Any(u => u.Email == "sandragonzalez@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "sandragonzalez@mailinator.com",
                    UserName = "sandragonzalez@mailinator.com",
                    FirstName = "Sandra",
                    LastName = "Gonzalez",
                    DisplayName = "Sandra G",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("sandragonzalez@mailinator.com").Id;
                userManager.AddToRole(userId, "Submitter");
            }

            if (!context.Users.Any(u => u.Email == "sethgotti@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "sethgotti@mailinator.com",
                    UserName = "sethgotti@mailinator.com",
                    FirstName = "Seth",
                    LastName = "Gotti",
                    DisplayName = "Seth G",
                    AvatarPath = "/Avatar/default_user.png"
                }, demoPassword);

                var userId = userManager.FindByEmail("sethgotti@mailinator.com").Id;
                userManager.AddToRole(userId, "Submitter");
            }

            #endregion
            context.SaveChanges();

            #region TicketType Seed
            context.TicketTypes.AddOrUpdate
                (
                    tt => tt.Name,
                    new TicketType() { Name = "Software" },
                    new TicketType() { Name = "Hardware" },
                    new TicketType() { Name = "Database" },
                    new TicketType() { Name = "UI" },
                    new TicketType() { Name = "Defect" },
                    new TicketType() { Name = "Training" }

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
                    new Project() { Name = "Best Movers", Created = DateTime.Now.AddDays(-60), IsArchived = true },
                    new Project() { Name = "Royal Holdings", Created = DateTime.Now.AddDays(-45) },
                    new Project() { Name = "Forex Frenzy", Created = DateTime.Now.AddDays(-30), IsArchived = true },
                    new Project() { Name = "Repair X", Created = DateTime.Now.AddDays(-15) },
                    new Project() { Name = "Dakota Toyota", Created = DateTime.Now.AddDays(-7) },
                    new Project() { Name = "Japanese Eatery", Created = DateTime.Now.AddDays(-60), IsArchived = true },
                    new Project() { Name = "Dancing Toes", Created = DateTime.Now.AddDays(-14) },
                    new Project() { Name = "Offroading Stars", Created = DateTime.Now.AddDays(-1) },
                    new Project() { Name = "Cali Melody", Created = DateTime.Now.AddDays(-90), IsArchived = true }

                );
            #endregion
            context.SaveChanges();

            #region Ticket Seed
            List<Ticket> ticketList = new List<Ticket>();
            List<ApplicationUser> projectManagers = new List<ApplicationUser>();
            List<ApplicationUser> developers = new List<ApplicationUser>();
            List<ApplicationUser> submitters = new List<ApplicationUser>();
            projectManagers.AddRange(roleHelper.UsersInRole("Project Manager"));
            developers.AddRange(roleHelper.UsersInRole("Developer"));
            submitters.AddRange(roleHelper.UsersInRole("Submitter"));
            #endregion

            #region Assigning Users to Projects by Role
            foreach (var project in context.Projects)
            {
                foreach (var user in roleHelper.UsersInRole("Admin"))
                {
                    projectHelper.AddUserToProject(user.Id, project.Id);
                }

                projectHelper.AddUserToProject(projectManagers[random.Next(projectManagers.Count)].Id, project.Id);

                var firstDev = developers[random.Next(developers.Count)].Id;
                var secondDev = developers[random.Next(developers.Count)].Id;
                while (firstDev == secondDev)
                {
                    secondDev = developers[random.Next(developers.Count)].Id;
                }

                projectHelper.AddUserToProject(firstDev, project.Id);
                projectHelper.AddUserToProject(secondDev, project.Id);

                var firstSub = submitters[random.Next(submitters.Count)].Id;
                var secondSub = submitters[random.Next(submitters.Count)].Id;
                while (firstSub == secondSub)
                {
                    secondSub = submitters[random.Next(submitters.Count)].Id;
                }

                projectHelper.AddUserToProject(firstSub, project.Id);
                projectHelper.AddUserToProject(secondSub, project.Id);
                context.Tickets.AddRange(ticketList);
                context.SaveChanges();
            }

            foreach (var project in context.Projects.ToList())
            {
                projectManagers = projectHelper.ListUsersOnProjectInRole(project.Id, "Project Manager");
                developers = projectHelper.ListUsersOnProjectInRole(project.Id, "Developer");
                submitters = projectHelper.ListUsersOnProjectInRole(project.Id, "Submitter");

                foreach (var type in context.TicketTypes.ToList())
                {

                    foreach (var status in context.TicketStatuses.ToList())
                    {
                        foreach (var priority in context.TicketPriorities.ToList())
                        {
                            var randomizer = random.Next(12);
                            if (randomizer == 1) // only seed 1 out of 12 tickets to reduce the number of tickets seeded
                            {
                                var devId = developers[random.Next(developers.Count)].Id;
                                if (status.Name == "Open")
                                {
                                    devId = null;
                                }
                                var resolved = false;
                                var archived = false;
                                if (status.Name == "Resolved")
                                {
                                    resolved = true;
                                }
                                if (status.Name == "Archived" || project.IsArchived)
                                {
                                    archived = true;
                                }
                                var newTicket = new Ticket()
                                {
                                    ProjectId = project.Id,
                                    TicketPriorityId = priority.Id,
                                    TicketTypeId = type.Id,
                                    TicketStatusId = status.Id,
                                    SubmitterId = submitters[random.Next(submitters.Count)].Id,
                                    DeveloperId = devId,
                                    Created = DateTime.Now,
                                    Issue = $"This is a  seeded ticket of type {type.Name} with a status of {status.Name} and priority of {priority.Name}",
                                    IssueDescription = $"This is a description of a ticket of a type {type.Name} on {project.Name}",
                                    IsResolved = resolved,
                                    IsArchived = archived

                                };
                                ticketList.Add(newTicket);

                            }
                        }
                    }
                }

            }
            context.Tickets.AddRange(ticketList);
            context.SaveChanges();
            #endregion
        }
    }
}

