using System;
using System.Collections.Generic;
using System.Text;

namespace TIMEFRAME_windows.MODELS
{
    public class TimeEntry
    {
        // Properties
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }

        // Relationships
        public int TaskEntryId { get; set; }
        public TaskEntry TaskEntry { get; set; }
    }
}
