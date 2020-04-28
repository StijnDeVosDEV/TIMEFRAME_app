using System;
using System.Collections.Generic;
using System.Text;

namespace TIMEFRAME_windows.MODELS
{
    public class Customer
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }

        // Relationships
        public ICollection<Project> Projects { get; set; }
        //public List<Project> Projects { get; set; }
    }
}
