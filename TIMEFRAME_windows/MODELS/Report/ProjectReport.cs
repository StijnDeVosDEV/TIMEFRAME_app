using System;
using System.Collections.Generic;
using System.Text;
using TIMEFRAME_windows.SERVICES;

namespace TIMEFRAME_windows.MODELS.Report
{
    public class ProjectReport
    {
        // CONSTRUCTOR
        public ProjectReport() { }

        public ProjectReport(Project project)
        {
            Project = project;

            TaskEntries = new List<TaskEntryReport>();
            if (project.TaskEntries != null)
            {
                foreach (TaskEntry taskEntry in project.TaskEntries)
                {
                    TaskEntries.Add(new TaskEntryReport(taskEntry));
                }
            }

            AmTaskEntries = project.TaskEntries.Count;
            TimeTotal = HelperService.CalculateTimespanHMS(project);
        }

        // PROPERTIES
        public Project Project { get; set; }
        public List<TaskEntryReport> TaskEntries { get; set; }
        public int AmTaskEntries { get; set; }
        public TimeSpanHMS TimeTotal { get; set; }
    }
}
