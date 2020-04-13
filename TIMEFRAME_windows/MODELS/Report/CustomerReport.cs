using System;
using System.Collections.Generic;
using System.Text;
using TIMEFRAME_windows.SERVICES;

namespace TIMEFRAME_windows.MODELS.Report
{
    public class CustomerReport
    {
        // CONSTRUCTOR
        public CustomerReport()
        {
            // ...
        }

        public CustomerReport(Customer customer)
        {
            Customer = customer;

            int taskCount = 0;
            Projects = new List<ProjectReport>();
            foreach (Project project in customer.Projects)
            {
                Projects.Add(new ProjectReport(project));
                taskCount += project.TaskEntries.Count;
            }

            AmProjects = customer.Projects.Count;
            AmTaskEntries = taskCount;
            TimeTotal = HelperService.CalculateTimespanHMS(customer);
        }

        // PROPERTIES
        public Customer Customer { get; set; }
        public List<ProjectReport> Projects { get; set; }
        public int AmProjects { get; set; }
        public int AmTaskEntries { get; set; }
        public TimeSpanHMS TimeTotal { get; set; }
    }
}
