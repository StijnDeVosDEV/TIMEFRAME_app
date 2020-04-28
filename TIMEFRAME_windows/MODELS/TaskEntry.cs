using System;
using System.Collections.Generic;
using System.Text;

namespace TIMEFRAME_windows.MODELS
{
    public class TaskEntry
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }

        // Relationships
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
