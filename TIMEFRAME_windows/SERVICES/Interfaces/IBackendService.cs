using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TIMEFRAME_windows.MODELS;

namespace TIMEFRAME_windows.SERVICES.Interfaces
{
    public interface IBackendService
    {
        // PROPERTIES
        public int Customer_maxIndex { get; set; }
        public int Project_maxIndex { get; set; }
        public int TaskEntry_maxIndex { get; set; }
        public int TimeEntry_maxIndex { get; set; }

        // METHODS
        public void InitializeHTTPclient();

        #region CRUD - CUSTOMER
        public Task<bool> AddCustomer(Customer customer);

        public Task<List<Customer>> GetCustomers(MODELS.User myUser);

        public Task<bool> EditCustomer(Customer customer);

        public Task<bool> DeleteCustomer(int customerID);
        #endregion

        #region PROJECTS
        // POST Project
        public Task<bool> AddProject(Project project);

        // GET all Projects
        public Task<List<Project>> GetProjects(MODELS.User myUser);

        public Task<bool> EditProject(Project project);

        public Task<bool> DeleteProject(int projectID);
        #endregion

        #region TASK ENTRIES
        // POST Task Entry
        public Task<bool> AddTaskEntry(TaskEntry taskEntry);

        // GET all Task Entries
        public Task<List<TaskEntry>> GetTaskEntries(MODELS.User myUser);

        public Task<bool> EditTaskEntry(TaskEntry taskEntry);

        public Task<bool> DeleteTaskEntry(int taskEntryID);
        #endregion

        #region TIME ENTRIES
        // POST Time Entry
        public Task<bool> AddTimeEntry(TimeEntry timeEntry);

        // GET all Time Entries
        public Task<bool> TimeEntryExists(int index);

        public Task<List<TimeEntry>> GetTimeEntries(MODELS.User myUser);

        public Task<bool> EditTimeEntry(TimeEntry timeEntry);

        public Task<bool> DeleteTimeEntry(int timeEntryID);
        #endregion
    }
}
