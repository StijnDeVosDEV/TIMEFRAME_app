﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Xps.Serialization;
using TIMEFRAME_windows.MODELS;
using TIMEFRAME_windows.SERVICES.Interfaces;

namespace TIMEFRAME_windows.SERVICES
{
    public class BackendService : IBackendService
    {
        // FIELDS
        private HttpClient client = new HttpClient();
        private int _Customer_maxIndex;
        private int _Project_maxIndex;
        private int _TaskEntry_maxIndex;
        private int _TimeEntry_maxIndex;


        // CONSTRUCTOR
        public BackendService()
        {
            // Initialize HTTP client
            InitializeHTTPclient();

            // Initialize Properties
            Customer_maxIndex = 0;
            Project_maxIndex = 0;
            TaskEntry_maxIndex = 0;
            TimeEntry_maxIndex = 0;
        }

        // PROPERTIES
        public int Customer_maxIndex
        {
            get { return _Customer_maxIndex; }
            set { if (value != _Customer_maxIndex) { _Customer_maxIndex = value; } }
        }
        public int Project_maxIndex
        {
            get { return _Project_maxIndex; }
            set { if (value != _Project_maxIndex) { _Project_maxIndex = value; } }
        }
        public int TaskEntry_maxIndex
        {
            get { return _TaskEntry_maxIndex; }
            set { if (value != _TaskEntry_maxIndex) { _TaskEntry_maxIndex = value; } }
        }
        public int TimeEntry_maxIndex
        {
            get { return _TimeEntry_maxIndex; }
            set { if (value != _TimeEntry_maxIndex) { _TimeEntry_maxIndex = value; } }
        }


        // METHODS
        public void InitializeHTTPclient()
        {
            //client.BaseAddress = new Uri("http://localhost:44303/");                        // DEVELOPMENT URI
            client.BaseAddress = new Uri("https://timeframeapi-test.azurewebsites.net/");    // PRODUCTION URI
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region CUSTOMERS
        // POST Customer
        public async Task AddCustomer(Customer customer)
        {
            try
            {
                Logger.Write("--- ADDING NEW CUSTOMER ---");
                
                HttpResponseMessage response = await client.PostAsJsonAsync("api/customers", customer);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while adding new customer : " + Environment.NewLine +
                    e.ToString());
            }
        }

        // GET all Customers
        public async Task<List<Customer>> GetCustomers(MODELS.User myUser)
        {
            List<Customer> allCustomers = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/customers");

                if (response.IsSuccessStatusCode)
                {
                    allCustomers = await response.Content.ReadAsAsync<List<Customer>>();

                    Customer_maxIndex = allCustomers.Max(x => x.Id);
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                    return allCustomers;
                }

                return allCustomers.Where(x => x.UserID == myUser.UserID).ToList();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while getting all customers : " + Environment.NewLine +
                    e.ToString());
                System.Windows.MessageBox.Show("Leaving GetCustomers() : ERROR: " + Environment.NewLine +
                    e.ToString());
                return allCustomers;
            }
        }

        // PUT Customer
        public async Task EditCustomer(Customer customer)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/customers/" + customer.Id.ToString(), customer);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while modifying customer : " + Environment.NewLine +
                    e.ToString());
            }
        }

        // DELETE Customer
        public async Task DeleteCustomer(int customerID)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/customers/" + customerID.ToString());
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while deleting customer : " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region PROJECTS
        // POST Project
        public async Task AddProject(Project project)
        {
            try
            {
                Logger.Write("--- ADDING NEW PROJECT (BackendService - AddProject) ---");

                HttpResponseMessage response = await client.PostAsJsonAsync("api/projects", project);

                if (!response.IsSuccessStatusCode)
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString() + Environment.NewLine +
                        Environment.NewLine +
                        "Content        : " + response.Content + Environment.NewLine +
                        "RequestMessage : " + response.RequestMessage + Environment.NewLine + 
                        "ReasonPhrase   : " + response.ReasonPhrase, "Adding new Project in database");
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while adding new project (BackendService - AddProject) : " + Environment.NewLine +
                    e.ToString());
            }
        }
        
        // GET all Projects
        public async Task<List<Project>> GetProjects(MODELS.User myUser)
        {
            List<Project> allProjects = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/projects");

                if (response.IsSuccessStatusCode)
                {
                    allProjects = await response.Content.ReadAsAsync<List<Project>>();

                    Project_maxIndex = allProjects.Max(x => x.Id);
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                    return allProjects;
                }

                return allProjects.Where(x => x.UserID == myUser.UserID).ToList();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while getting all projects : " + Environment.NewLine +
                    e.ToString());
                System.Windows.MessageBox.Show("Leaving GetProjects() : ERROR: " + Environment.NewLine +
                    e.ToString());
                return allProjects;
            }
        }

        // PUT Project
        public async Task EditProject(Project project)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/projects/" + project.Id.ToString(), project);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while modifying project : " + Environment.NewLine +
                    e.ToString());
            }
        }

        // DELETE Project
        public async Task DeleteProject(int projectID)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/projects/" + projectID.ToString());
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while deleting project : " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region TASK ENTRIES
        public async Task AddTaskEntry(TaskEntry taskEntry)
        {
            try
            {
                Logger.Write("--- ADDING NEW TASK ENTRY (BackendService - AddTaskEntry) ---");

                HttpResponseMessage response = await client.PostAsJsonAsync("api/taskentries", taskEntry);

                if (!response.IsSuccessStatusCode)
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString() + Environment.NewLine +
                        Environment.NewLine +
                        "Content        : " + response.Content + Environment.NewLine +
                        "RequestMessage : " + response.RequestMessage + Environment.NewLine +
                        "ReasonPhrase   : " + response.ReasonPhrase, "Adding new Task Entry in database");
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while adding new task entry (BackendService - AddTaskEntry) : " + Environment.NewLine +
                    e.ToString());
            }
        }

        public async Task<List<TaskEntry>> GetTaskEntries(MODELS.User myUser)
        {
            List<TaskEntry> allTaskEntries = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/taskentries");

                if (response.IsSuccessStatusCode)
                {
                    allTaskEntries = await response.Content.ReadAsAsync<List<TaskEntry>>();

                    TaskEntry_maxIndex = allTaskEntries.Max(x => x.Id);
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                    return allTaskEntries;
                }

                return allTaskEntries.Where(x => x.UserID == myUser.UserID).ToList();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while getting all task entries : " + Environment.NewLine +
                    e.ToString());
                System.Windows.MessageBox.Show("Leaving GetTaskEntries() : ERROR: " + Environment.NewLine +
                    e.ToString());
                return allTaskEntries;
            }
        }

        public async Task EditTaskEntry(TaskEntry taskEntry)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/taskentries/" + taskEntry.Id.ToString(), taskEntry);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while modifying task entry : " + Environment.NewLine +
                    e.ToString());
            }
        }

        public async Task DeleteTaskEntry(int taskEntryID)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/taskentries/" + taskEntryID.ToString());
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while deleting task entry : " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region TIME ENTRIES
        public async Task<bool> AddTimeEntry(TimeEntry timeEntry)
        {
            try
            {
                Logger.Write("--- ADDING NEW TIME ENTRY (BackendService - AddTimeEntry) ---");

                HttpResponseMessage response = await client.PostAsJsonAsync("api/timeentries", timeEntry);

                if (!response.IsSuccessStatusCode)
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString() + Environment.NewLine +
                        Environment.NewLine +
                        "Content        : " + response.Content + Environment.NewLine +
                        "RequestMessage : " + response.RequestMessage + Environment.NewLine +
                        "ReasonPhrase   : " + response.ReasonPhrase, "Adding new Time Entry in database");

                    return false;
                }
                else
                {
                    TimeEntry_maxIndex += 1;

                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while adding new time entry (BackendService - AddTimeEntry) : " + Environment.NewLine +
                    e.ToString());

                return false;
            }
        }

        public async Task<bool> TimeEntryExists(int index)
        {
            bool TimeEntryExists = false;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/timeentries/" + index.ToString());

                if (response.IsSuccessStatusCode)
                {
                    TimeEntryExists = true;
                }

                return TimeEntryExists;
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while retrieving time entry (BackendService - TimeEntryExists) : " + Environment.NewLine +
                    e.ToString());

                return TimeEntryExists;
            }
        }

        public async Task<List<TimeEntry>> GetTimeEntries(MODELS.User myUser)
        {
            List<TimeEntry> allTimeEntries = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/timeentries");

                if (response.IsSuccessStatusCode)
                {
                    allTimeEntries = await response.Content.ReadAsAsync<List<TimeEntry>>();

                    TimeEntry_maxIndex = allTimeEntries.Max(x => x.Id);
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                    return allTimeEntries;
                }

                return allTimeEntries.Where(x => x.UserID == myUser.UserID).ToList();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while getting all time entries : " + Environment.NewLine +
                    e.ToString());
                System.Windows.MessageBox.Show("Leaving GetTimeEntries() : ERROR: " + Environment.NewLine +
                    e.ToString());
                return allTimeEntries;
            }
        }

        public async Task EditTimeEntry(TimeEntry timeEntry)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/timeentries/" + timeEntry.Id.ToString(), timeEntry);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while modifying time entry : " + Environment.NewLine +
                    e.ToString());
            }
        }

        public async Task DeleteTimeEntry(int timeEntryID)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/timeentries/" + timeEntryID.ToString());
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while deleting time entry : " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion
    }
}
