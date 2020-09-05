using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class ChartData
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }

        public ChartData()
        {
            Labels = new List<string>();
            Data = new List<int>();
        }
    }
}