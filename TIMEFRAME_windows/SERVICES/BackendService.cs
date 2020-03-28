using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TIMEFRAME_windows.MODELS;
using TIMEFRAME_windows.SERVICES.Interfaces;

namespace TIMEFRAME_windows.SERVICES
{
    public class BackendService : IBackendService
    {
        // FIELDS
        private HttpClient client = new HttpClient();


        // CONSTRUCTOR
        public BackendService()
        {
            // Initialize HTTP client
            InitializeHTTPclient();
        }

        // PROPERTIES


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
        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> allCustomers = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/customers");

                if (response.IsSuccessStatusCode)
                {
                    allCustomers = await response.Content.ReadAsAsync<List<Customer>>();
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                }

                return allCustomers;
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
        public async Task<List<Project>> GetProjects()
        {
            List<Project> allProjects = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/projects");

                if (response.IsSuccessStatusCode)
                {
                    allProjects = await response.Content.ReadAsAsync<List<Project>>();
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                }

                return allProjects;
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

        public async Task<List<TaskEntry>> GetTaskEntries()
        {
            List<TaskEntry> allTaskEntries = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/taskentries");

                if (response.IsSuccessStatusCode)
                {
                    allTaskEntries = await response.Content.ReadAsAsync<List<TaskEntry>>();
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                }

                return allTaskEntries;
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
        #endregion

        #region TIME ENTRIES
        public async Task AddTimeEntry(TimeEntry timeEntry)
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
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while adding new time entry (BackendService - AddTimeEntry) : " + Environment.NewLine +
                    e.ToString());
            }
        }

        public async Task<List<TimeEntry>> GetTimeEntries()
        {
            List<TimeEntry> allTimeEntries = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + @"api/timeentries");

                if (response.IsSuccessStatusCode)
                {
                    allTimeEntries = await response.Content.ReadAsAsync<List<TimeEntry>>();
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR:  " + response.StatusCode.ToString());
                }

                return allTimeEntries;
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
        #endregion
    }
}
