using System;
using System.Collections.Generic;
using System.Text;
using TIMEFRAME_windows.SERVICES;

namespace TIMEFRAME_windows.MODELS.Report
{
    public class TaskEntryReport
    {
        // CONSTRUCTOR
        public TaskEntryReport() { }

        public TaskEntryReport(TaskEntry taskEntry)
        {
            TaskEntry = taskEntry;
            TimeTotal = HelperService.CalculateTimespanHMS(taskEntry);
        }

        // PROPERTIES
        public TaskEntry TaskEntry { get; set; }
        public TimeSpanHMS TimeTotal { get; set; }
    }
}
