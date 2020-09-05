using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class ProjectsViewModel
    {
        public List<Project> AllProjects { get; set; }
        public List<Project> MyProjects { get; set; }
        public List<ApplicationUser> AllProjectManagers { get; set; }
        public int ProjectCount { get; set; } 


        public ProjectsViewModel()
        {
            AllProjects = new List<Project>();
            MyProjects = new List<Project>();
            AllProjectManagers = new List<ApplicationUser>();
        }
    }
}