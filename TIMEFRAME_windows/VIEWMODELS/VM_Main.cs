﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TIMEFRAME_windows.MODELS;
using TIMEFRAME_windows.SERVICES;
using TIMEFRAME_windows.SERVICES.Interfaces;

namespace TIMEFRAME_windows.VIEWMODELS
{
    public class VM_Main : Base.GEN_ObservableObject
    {
        //  -----------------------------
        //  Private variable declarations
        //  -----------------------------
        // General
        // -------
        private ObservableCollection<Customer> _allCustomers;
        private List<Customer> _allCustomers_fromDB;

        private ObservableCollection<Project> _allProjects;
        private List<Project> _allProjects_fromDB;

        private ObservableCollection<TaskEntry> _allTaskEntries;
        private List<TaskEntry> _allTaskEntries_fromDB;

        private ObservableCollection<TimeEntry> _allTimeEntries;
        private List<TimeEntry> _allTimeEntries_fromDB;

        private string _UInotification;
        private Visibility _LoadingScreen_Visibility;


        // Record block
        // ------------
        // Selection drop downs
        private ObservableCollection<Project> _availProjects;
        private ObservableCollection<TaskEntry> _availTaskEntries;

        private int _selCustomerindex;
        private Customer _selCustomer;
        private int _selProjectindex;
        private Project _selProject;
        private int _selTaskEntryindex;
        private TaskEntry _selTaskEntry;

        // CONFIG block
        // --------------
        private ObservableCollection<Customer> _db_shownCustomers;
        private ObservableCollection<Project> _db_shownProjects;
        private ObservableCollection<TaskEntry> _db_shownTaskEntries;
        private ObservableCollection<TimeEntry> _db_shownTimeEntries;

        // CUSTOMER CONFIG
        private int _config_customer_selindex;
        private Customer _config_customer_selCustomer;
        // Customer: add/edit
        private Visibility _customer_addedit_Visibility;
        private string _customer_addedit_Title;
        private string _customer_addedit_Name;
        private string _customer_addedit_Surname;
        private string _customer_addedit_Email;
        // Customer: edit
        private bool _customer_edit_IsEnabled;
        private Visibility _customer_edit_Visibility;
        private string _customer_edit_Title;
        private string _customer_edit_Name;
        private string _customer_edit_Surname;
        private string _customer_edit_Email;
        // Customer: delete
        private bool _customer_delete_IsEnabled;

        // PROJECT CONFIG
        private int _config_project_selindex;
        private Project _config_project_selProject;
        // Project: add/edit
        private Visibility _project_addedit_Visibility;
        private int _project_addedit_selCustindex;
        private Customer _project_addedit_selCust;
        private string _project_addedit_Name;
        private string _project_addedit_Description;
        // Project: edit
        private bool _project_edit_IsEnabled;
        private Visibility _project_edit_Visibility;
        private int _project_edit_selCustindex;
        private Customer _project_edit_selCust;
        private string _project_edit_Name;
        private string _project_edit_Description;
        // Project: delete
        private bool _project_delete_IsEnabled;


        // Commands
        private VIEWMODELS.Base.GEN_RelayCommand _AddCustomer;
        private VIEWMODELS.Base.GEN_RelayCommand _EditCustomer;
        private VIEWMODELS.Base.GEN_RelayCommand _DeleteCustomer;
        private VIEWMODELS.Base.GEN_RelayCommand _AddProject;
        private VIEWMODELS.Base.GEN_RelayCommand _EditProject;
        private VIEWMODELS.Base.GEN_RelayCommand _DeleteProject;

        private VIEWMODELS.Base.GEN_RelayCommand _CloseLoadingScreen;

        // Services
        private IBackendService myBackendService;


        //  -----------------------------
        //  Actual Class variables
        //  -----------------------------


        //  -----------
        //  CONSTRUCTOR
        //  -----------
        public VM_Main()
        {
            // Initialization
            UInotification = "";
            LoadingScreen_Visibility = Visibility.Visible;

            // Inject Services
            //InitializeServiceInjections(new BackendService());
            myBackendService = new BackendService();

            allCustomers = new ObservableCollection<Customer>();
            allCustomers_fromDB = new List<Customer>();
            allProjects = new ObservableCollection<Project>();
            allProjects_fromDB = new List<Project>();
            allTaskEntries = new ObservableCollection<TaskEntry>();
            allTaskEntries_fromDB = new List<TaskEntry>();
            allTimeEntries = new ObservableCollection<TimeEntry>();
            allTimeEntries_fromDB = new List<TimeEntry>();

            availProjects = new ObservableCollection<Project>();
            availTaskEntries = new ObservableCollection<TaskEntry>();

            //selCustomer = new Customer();
            //selProject = new Project();
            //selTaskEntry = new TaskEntry();

            //selCustomerindex = -1;
            //selProjectindex = -1;
            //selTaskEntryindex = -1;

            db_shownCustomers = new ObservableCollection<Customer>();
            db_shownProjects = new ObservableCollection<Project>();
            db_shownTaskEntries = new ObservableCollection<TaskEntry>();
            db_shownTimeEntries = new ObservableCollection<TimeEntry>();
            customer_addedit_Visibility = Visibility.Visible;
            customer_edit_IsEnabled = false;
            customer_edit_Visibility = Visibility.Visible;

            project_addedit_selCustindex = -1;
            project_edit_selCustindex = -1;

            // Initializations
            InitializeData();

            // Load commands
            LoadCommands();

            // Initialize process
            Logger.SetPath(Environment.GetEnvironmentVariable("TEMP"));
            Logger.Initialize();
            
            // Search Background Worker event handler initialization
            
        }

        // ----------------------------
        // Public variable declarations
        // ----------------------------
        #region GENERAL
        public ObservableCollection<Customer> allCustomers
        {
            get { return _allCustomers; }
            set { if (value != _allCustomers) { _allCustomers = value; RaisePropertyChangedEvent("allCustomers"); } }
        }

        public List<Customer> allCustomers_fromDB
        {
            get { return _allCustomers_fromDB; }
            set { if (value != _allCustomers_fromDB) { _allCustomers_fromDB = value; RaisePropertyChangedEvent("allCustomers_fromDB"); 
                    ParseCustomerData(); 
                } }
        }

        public ObservableCollection<Project> allProjects
        {
            get { return _allProjects; }
            set { if (value != _allProjects) { _allProjects = value; RaisePropertyChangedEvent("allProjects"); } }
        }

        public List<Project> allProjects_fromDB
        {
            get { return _allProjects_fromDB; }
            set { if (value != _allProjects_fromDB) { _allProjects_fromDB = value; RaisePropertyChangedEvent("allProjects_fromDB"); 
                    ParseProjectData();
                } }
        }

        public ObservableCollection<TaskEntry> allTaskEntries
        {
            get { return _allTaskEntries; }
            set { if (value != _allTaskEntries) { _allTaskEntries = value; RaisePropertyChangedEvent("allTaskEntries"); } }
        }

        public List<TaskEntry> allTaskEntries_fromDB
        {
            get { return _allTaskEntries_fromDB; }
            set { if (value != _allTaskEntries_fromDB) { _allTaskEntries_fromDB = value; RaisePropertyChangedEvent("allTaskEntries_fromDB");
                    ParseTaskEntryData();
                } }
        }

        public ObservableCollection<TimeEntry> allTimeEntries
        {
            get { return _allTimeEntries; }
            set { if (value != _allTimeEntries) { _allTimeEntries = value; RaisePropertyChangedEvent("allTimeEntries"); } }
        }

        public List<TimeEntry> allTimeEntries_fromDB
        {
            get { return _allTimeEntries_fromDB; }
            set { if (value != _allTimeEntries_fromDB) { _allTimeEntries_fromDB = value; RaisePropertyChangedEvent("allTimeEntries_fromDB");
                    ParseTimeEntryData();
                } }
        }

        public string UInotification
        {
            get { return _UInotification; }
            set { if (value != _UInotification) { _UInotification = value; RaisePropertyChangedEvent("UInotification"); } }
        }

        public Visibility LoadingScreen_Visibility
        {
            get { return _LoadingScreen_Visibility; }
            set { if (value != _LoadingScreen_Visibility) { _LoadingScreen_Visibility = value; RaisePropertyChangedEvent("LoadingScreen_Visibility"); } }
        }
        #endregion



        #region RECORD Component
        public ObservableCollection<Project> availProjects
        {
            get { return _availProjects; }
            set { if(value != _availProjects) { _availProjects = value; RaisePropertyChangedEvent("availProjects"); } }
        }

        public ObservableCollection<TaskEntry> availTaskEntries
        {
            get { return _availTaskEntries; }
            set { if(value!= _availTaskEntries) { _availTaskEntries = value; RaisePropertyChangedEvent("availTaskEntries"); } }
        }

        public int selCustomerindex
        {
            get { return _selCustomerindex; }
            set { if(value!= _selCustomerindex) { _selCustomerindex = value; RaisePropertyChangedEvent("selCustomerindex"); 
                    if(selCustomerindex > -1) { selCustomer = allCustomers[selCustomerindex];}
                    else { selCustomer = null;}
                } }
        }
        public Customer selCustomer
        {
            get { return _selCustomer; }
            set { if(value != _selCustomer) { _selCustomer = value; RaisePropertyChangedEvent("selCustomer"); UpdateRecordData(dataCategory.Customer); } }
        }

        public int selProjectindex
        {
            get { return _selProjectindex; }
            set { if (value != _selProjectindex) { _selProjectindex = value; RaisePropertyChangedEvent("selProjectindex"); 
                    if(selProjectindex > -1)
                    {
                        selProject = availProjects[selProjectindex];
                    }
                    else
                    {
                        selProject = null;
                    } } }
        }
        public Project selProject
        {
            get { return _selProject; }
            set { if (value != _selProject) { _selProject = value; RaisePropertyChangedEvent("selProject"); UpdateRecordData(dataCategory.Project); } }
        }

        public int selTaskEntryindex
        {
            get { return _selTaskEntryindex; }
            set { if (value != _selTaskEntryindex) { _selTaskEntryindex = value; RaisePropertyChangedEvent("selTaskEntryindex");
                    if (selTaskEntryindex > -1)
                    {
                        selTaskEntry = availTaskEntries[selTaskEntryindex];
                    }
                    else { selTaskEntry = null; }
                     } }
        }
        public TaskEntry selTaskEntry
        {
            get { return _selTaskEntry; }
            set { if (value != _selTaskEntry) { _selTaskEntry = value; RaisePropertyChangedEvent("selTaskEntry"); } }
        }
        #endregion

        #region CONFIGURATION Component
        // Shown lists
        public ObservableCollection<Customer> db_shownCustomers
        {
            get { return _db_shownCustomers ; }
            set { if(value != _db_shownCustomers) { _db_shownCustomers = value; RaisePropertyChangedEvent("db_shownCustomers"); } }
        }

        public ObservableCollection<Project> db_shownProjects
        {
            get { return _db_shownProjects; }
            set { if (value != _db_shownProjects) { _db_shownProjects = value; RaisePropertyChangedEvent("db_shownProjects"); } }
        }

        public ObservableCollection<TaskEntry> db_shownTaskEntries
        {
            get { return _db_shownTaskEntries; }
            set { if (value != _db_shownTaskEntries) { _db_shownTaskEntries = value; RaisePropertyChangedEvent("db_shownTaskEntries"); } }
        }

        public ObservableCollection<TimeEntry> db_shownTimeEntries
        {
            get { return _db_shownTimeEntries; }
            set { if (value != _db_shownTimeEntries) { _db_shownTimeEntries = value; RaisePropertyChangedEvent("db_shownTimeEntries"); } }
        }



        // Configuration:  CUSTOMER
        // ------------------------
        // Configuration:  Customer SELECTION
        public int config_customer_selindex
        {
            get { return _config_customer_selindex; }
            set { if (value != _config_customer_selindex) { _config_customer_selindex = value; RaisePropertyChangedEvent("config_customer_selindex");
                    if (config_customer_selindex > -1) 
                    { 
                        config_customer_selCustomer = db_shownCustomers[config_customer_selindex];
                        customer_edit_IsEnabled = true;
                        Update_EditSelectionData(dataCategory.Customer);
                        customer_delete_IsEnabled = true;
                    }
                    else { config_customer_selCustomer = null; customer_edit_IsEnabled = false; customer_delete_IsEnabled = false; }
                } }
        }

        public Customer config_customer_selCustomer
        {
            get { return _config_customer_selCustomer; }
            set { if (value != _config_customer_selCustomer) { _config_customer_selCustomer = value; RaisePropertyChangedEvent("config_customer_selCustomer"); } }
        }

        // Configuration:  Customer ADD
        public Visibility customer_addedit_Visibility
        {
            get { return _customer_addedit_Visibility; }
            set { if (value != _customer_addedit_Visibility) { _customer_addedit_Visibility = value; RaisePropertyChangedEvent("customer_addedit_Visibility");
                    if (customer_addedit_Visibility == Visibility.Visible) { Update_SecondaryViewVisibilities(dataCategory.Customer, true); }
                } }
        }
        
        public string customer_addedit_Title
        {
            get { return _customer_addedit_Title; }
            set { if (value != _customer_addedit_Title) { _customer_addedit_Title = value; RaisePropertyChangedEvent("customer_addedit_Title"); } }
        }
        
        public string customer_addedit_Name
        {
            get { return _customer_addedit_Name; }
            set { if (value != _customer_addedit_Name) { _customer_addedit_Name = value; RaisePropertyChangedEvent("customer_addedit_Name"); } }
        }

        public string customer_addedit_Surname
        {
            get { return _customer_addedit_Surname; }
            set { if (value != _customer_addedit_Surname) { _customer_addedit_Surname = value; RaisePropertyChangedEvent("customer_addedit_Surname"); } }
        }

        public string customer_addedit_Email
        {
            get { return _customer_addedit_Email; }
            set { if (value != _customer_addedit_Email) { _customer_addedit_Email = value; RaisePropertyChangedEvent("customer_addedit_Email"); } }
        }


        // Configuration:  Customer EDIT
        public bool customer_edit_IsEnabled
        {
            get { return _customer_edit_IsEnabled; }
            set { if (value != _customer_edit_IsEnabled) { _customer_edit_IsEnabled = value; RaisePropertyChangedEvent("customer_edit_IsEnabled"); } }
        }

        public Visibility customer_edit_Visibility
        {
            get { return _customer_edit_Visibility; }
            set { if (value != _customer_edit_Visibility) { _customer_edit_Visibility = value; RaisePropertyChangedEvent("customer_edit_Visibility");
                    if (customer_edit_Visibility == Visibility.Visible) { Update_SecondaryViewVisibilities(dataCategory.Customer, false); }
                } }
        }

        public string customer_edit_Title
        {
            get { return _customer_edit_Title; }
            set { if (value != _customer_edit_Title) { _customer_edit_Title = value; RaisePropertyChangedEvent("customer_edit_Title"); } }
        }

        public string customer_edit_Name
        {
            get { return _customer_edit_Name; }
            set { if (value != _customer_edit_Name) { _customer_edit_Name = value; RaisePropertyChangedEvent("customer_edit_Name"); } }
        }

        public string customer_edit_Surname
        {
            get { return _customer_edit_Surname; }
            set { if (value != _customer_edit_Surname) { _customer_edit_Surname = value; RaisePropertyChangedEvent("customer_edit_Surname"); } }
        }

        public string customer_edit_Email
        {
            get { return _customer_edit_Email; }
            set { if (value != _customer_edit_Email) { _customer_edit_Email = value; RaisePropertyChangedEvent("customer_edit_Email"); } }
        }

        // Configuration:  Customer DELETE
        public bool customer_delete_IsEnabled
        {
            get { return _customer_delete_IsEnabled; }
            set { if (value != _customer_delete_IsEnabled) { _customer_delete_IsEnabled = value; RaisePropertyChangedEvent("customer_delete_IsEnabled"); } }
        }



        // Configuration:  PROJECT
        // -----------------------
        // Configuration:  Project SELECTION
        public int config_project_selindex
        {
            get { return _config_project_selindex; }
            set
            {
                if (value != _config_project_selindex)
                {
                    _config_project_selindex = value; RaisePropertyChangedEvent("config_project_selindex");
                    if (config_project_selindex > -1)
                    {
                        config_project_selProject = db_shownProjects[config_project_selindex];
                        project_edit_IsEnabled = true;
                        Update_EditSelectionData(dataCategory.Project);
                        project_delete_IsEnabled = true;
                    }
                    else { config_project_selProject = null; project_edit_IsEnabled = false; project_delete_IsEnabled = false; }
                }
            }
        }

        public Project config_project_selProject
        {
            get { return _config_project_selProject; }
            set { if (value != _config_project_selProject) { _config_project_selProject = value; RaisePropertyChangedEvent("config_project_selProject"); } }
        }

        // Configuration:  Project ADD
        public Visibility project_addedit_Visibility
        {
            get { return _project_addedit_Visibility; }
            set
            {
                if (value != _project_addedit_Visibility)
                {
                    _project_addedit_Visibility = value; RaisePropertyChangedEvent("project_addedit_Visibility");
                    if (project_addedit_Visibility == Visibility.Visible) { Update_SecondaryViewVisibilities(dataCategory.Project, true); }
                }
            }
        }

        public int project_addedit_selCustindex
        {
            get { return _project_addedit_selCustindex; }
            set { if (value != _project_addedit_selCustindex) { _project_addedit_selCustindex = value; RaisePropertyChangedEvent("project_addedit_selCustindex");
                    if (project_addedit_selCustindex > -1)
                    {
                        project_addedit_selCust = allCustomers[project_addedit_selCustindex];
                    }
                    else { project_addedit_selCust = null; }
                } }
        }

        public Customer project_addedit_selCust
        {
            get { return _project_addedit_selCust; }
            set { if (value != _project_addedit_selCust) { _project_addedit_selCust = value; RaisePropertyChangedEvent("project_addedit_selCust"); } }
        }

        public string project_addedit_Name
        {
            get { return _project_addedit_Name; }
            set { if (value != _project_addedit_Name) { _project_addedit_Name = value; RaisePropertyChangedEvent("project_addedit_Name"); } }
        }

        public string project_addedit_Description
        {
            get { return _project_addedit_Description; }
            set { if (value != _project_addedit_Description) { _project_addedit_Description = value; RaisePropertyChangedEvent("project_addedit_Description"); } }
        }




        // Configuration:  Project EDIT
        public bool project_edit_IsEnabled
        {
            get { return _project_edit_IsEnabled; }
            set { if (value != _project_edit_IsEnabled) { _project_edit_IsEnabled = value; RaisePropertyChangedEvent("project_edit_IsEnabled"); } }
        }

        public Visibility project_edit_Visibility
        {
            get { return _project_edit_Visibility; }
            set
            {
                if (value != _project_edit_Visibility)
                {
                    _project_edit_Visibility = value; RaisePropertyChangedEvent("project_edit_Visibility");
                    if (project_edit_Visibility == Visibility.Visible) { Update_SecondaryViewVisibilities(dataCategory.Project, false); }
                }
            }
        }
        public int project_edit_selCustindex
        {
            get { return _project_edit_selCustindex; }
            set
            {
                if (value != _project_edit_selCustindex)
                {
                    _project_edit_selCustindex = value; RaisePropertyChangedEvent("project_edit_selCustindex");
                    if (project_edit_selCustindex > -1)
                    {
                        project_edit_selCust = allCustomers[project_edit_selCustindex];
                    }
                    else { project_edit_selCust = null; }
                }
            }
        }

        public Customer project_edit_selCust
        {
            get { return _project_edit_selCust; }
            set { if (value != _project_edit_selCust) { _project_edit_selCust = value; RaisePropertyChangedEvent("project_edit_selCust"); } }
        }

        public string project_edit_Name
        {
            get { return _project_edit_Name; }
            set { if (value != _project_edit_Name) { _project_edit_Name = value; RaisePropertyChangedEvent("project_edit_Name"); } }
        }

        public string project_edit_Description
        {
            get { return _project_edit_Description; }
            set { if (value != _project_edit_Description) { _project_edit_Description = value; RaisePropertyChangedEvent("project_edit_Description"); } }
        }

        // Configuration:  Project DELETE
        public bool project_delete_IsEnabled
        {
            get { return _project_delete_IsEnabled; }
            set { if (value != _project_delete_IsEnabled) { _project_delete_IsEnabled = value; RaisePropertyChangedEvent("project_delete_IsEnabled"); } }
        }
        #endregion


        // ---------------------------
        // Public command declarations
        // ---------------------------
        #region COMMANDS
        public ICommand AddCustomer { get { return _AddCustomer; } }
        public ICommand EditCustomer { get { return _EditCustomer; } }
        public ICommand DeleteCustomer { get { return _DeleteCustomer; } }
        public ICommand AddProject { get { return _AddProject; } }
        public ICommand EditProject { get { return _EditProject; } }
        public ICommand DeleteProject { get { return _DeleteProject; } }
        public ICommand CloseLoadingScreen { get { return _CloseLoadingScreen; } }
        #endregion


        //  -----------------
        //  Private functions
        //  -----------------
        //public void InitializeServiceInjections(IBackendService backendService)
        //{
        //    myBackendService = backendService;
        //}

        private async Task InitializeData()
        {
            // Get all data from database
            allCustomers_fromDB = await myBackendService.GetCustomers();
            allProjects_fromDB = await myBackendService.GetProjects();
            allTaskEntries_fromDB = await myBackendService.GetTaskEntries();
            allTimeEntries_fromDB = await myBackendService.GetTimeEntries();
        }

        private void ParseCustomerData()
        {
            Logger.Write("PARSING CUSTOMER DATA:");
            Logger.Write("allcustomers_placeholder.Count = " + allCustomers_fromDB.Count.ToString());

            allCustomers.Clear();

            if (allCustomers_fromDB != null && allCustomers_fromDB.Count > 0)
            {
                foreach (Customer customer in allCustomers_fromDB)
                {
                    allCustomers.Add(customer);
                    Logger.Write("- Added:  " + customer.Name.ToUpper());
                }
            }
            else
            {
                Logger.Write("allCustomers_fromDB = null or does not contain any data!");
                ShowUINotification("No customers were found in storage!");
            }

            db_shownCustomers = allCustomers;
        }

        private void ParseProjectData()
        {
            Logger.Write("PARSING PROJECT DATA:");
            //Logger.Write("allprojects_placeholder.Count = " + allprojects_placeholder.Count.ToString());

            allProjects.Clear();

            if(allProjects_fromDB != null && allProjects_fromDB.Count > 0)
            {
                foreach (Project project in allProjects_fromDB)
                {
                    allProjects.Add(project);
                    Logger.Write("- Added:  " + project.Name.ToUpper());
                }
            }
            else
            {
                Logger.Write("allProjects_fromDB = null or does not contain any data!");
                ShowUINotification("No projects were found in storage!");
            }

            db_shownProjects = allProjects;
        }

        private void ParseTaskEntryData()
        {
            Logger.Write("PARSING TASK ENTRY DATA:");

            allTaskEntries.Clear();

            if (allTaskEntries_fromDB != null && allTaskEntries_fromDB.Count > 0)
            {
                foreach (TaskEntry taskEntry in allTaskEntries_fromDB)
                {
                    allTaskEntries.Add(taskEntry);
                    Logger.Write("- Added:  " + taskEntry.Name.ToUpper());
                }
            }
            else
            {
                Logger.Write("allTaskEntries_fromDB = null or does not contain any data!");
                ShowUINotification("No Task Entries were found in storage!");
            }

            db_shownTaskEntries = allTaskEntries;
        }

        private void ParseTimeEntryData()
        {
            Logger.Write("PARSING TIME ENTRY DATA:");

            allTimeEntries.Clear();

            if (allTimeEntries_fromDB != null && allTimeEntries_fromDB.Count > 0)
            {
                foreach (TimeEntry timeEntry in allTimeEntries_fromDB)
                {
                    allTimeEntries.Add(timeEntry);
                    Logger.Write("- Added time entry");
                }
            }
            else
            {
                Logger.Write("allTimeEntries_fromDB = null or does not contain any data!");
                ShowUINotification("No Time Entries were found in storage!");
            }

            db_shownTimeEntries = allTimeEntries;


            ToggleLoadingScreen_Visibility();
        }

        private enum dataCategory
        {
            Customer,
            Project,
            TaskEntry,
            TimeEntry
        }

        private void UpdateRecordData(dataCategory target)
        {
            try
            {
                switch (target)
                {
                    case dataCategory.Customer:
                        availProjects.Clear();

                        if (selCustomer.Projects != null && selCustomer.Projects.Count > 0)
                        {
                            foreach (Project project in selCustomer.Projects)
                            {
                                availProjects.Add(project);
                            }
                        }
                        else
                        {
                            Logger.Write("UPDATE RECORD DATA - CUSTOMER SELECTION - Get Project list of selected customer:  selCustomer.Projects is null or empty");
                        }

                        availTaskEntries.Clear();
                        selTaskEntry = null;
                        break;

                    case dataCategory.Project:
                        availTaskEntries.Clear();

                        if (selProject.TaskEntries != null && selProject.TaskEntries.Count > 0)
                        {
                            foreach (TaskEntry taskEntry in selProject.TaskEntries)
                            {
                                availTaskEntries.Add(taskEntry);
                            }
                        }
                        else
                        {
                            Logger.Write("UPDATE RECORD DATA - PROJECT SELECTION - Get Task Entry list of selected project:  selProject.TaskEntries is null or empty");
                        }
                        
                        selTaskEntry = null;
                        break;

                    case dataCategory.TaskEntry:
                        break;
                    case dataCategory.TimeEntry:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR while trying to update record block data: " + Environment.NewLine +
                    e.ToString());
            }
        }


        private void UpdateConfigurationComponent()
        {
            db_shownCustomers = allCustomers;
            db_shownProjects = allProjects;
            db_shownTaskEntries = allTaskEntries;
            db_shownTimeEntries = allTimeEntries;
        }

        private void Update_SecondaryViewVisibilities(dataCategory SecondaryViewShown, bool shouldADDopen)
        {
            try
            {
                switch (SecondaryViewShown)
                {
                    case dataCategory.Customer:
                        if (shouldADDopen) { customer_edit_Visibility = Visibility.Hidden; } else { customer_addedit_Visibility = Visibility.Hidden; }
                        break;
                    case dataCategory.Project:
                        if (shouldADDopen) { project_edit_Visibility = Visibility.Hidden; } else { project_addedit_Visibility = Visibility.Hidden; }
                        break;
                    case dataCategory.TaskEntry:
                        break;
                    case dataCategory.TimeEntry:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR while trying to update record block data: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private void ToggleLoadingScreen_Visibility()
        {
            LoadingScreen_Visibility = LoadingScreen_Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void ShowUINotification(string message)
        {
            UInotification = message;
        }

        private void Update_EditSelectionData(dataCategory targetEditView)
        {
            try
            {
                switch (targetEditView)
                {
                    case dataCategory.Customer:
                        customer_edit_Name = config_customer_selCustomer.Name;
                        customer_edit_Surname = config_customer_selCustomer.Surname;
                        customer_edit_Email = config_customer_selCustomer.Email;
                        break;

                    case dataCategory.Project:
                        project_edit_Name = config_project_selProject.Name;
                        project_edit_Description = config_project_selProject.Description;
                        //project_edit_selCustindex = allCustomers.IndexOf(allCustomers.Single(x => x.Id == config_project_selProject.Id));
                        project_edit_selCustindex = config_project_selProject.Customer.Id;

                        break;
                    case dataCategory.TaskEntry:
                        break;
                    case dataCategory.TimeEntry:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR while trying to update record block data: " + Environment.NewLine +
                    e.ToString());
            }
        }

        //  ----------------------
        // COMMAND RELATED METHODS
        //  ----------------------
        /// <summary>
        /// Load all GUI commands
        /// </summary>
        private void LoadCommands()
        {
            _AddCustomer = new Base.GEN_RelayCommand(param => this.Perform_AddCustomer());
            _EditCustomer = new Base.GEN_RelayCommand(param => this.Perform_EditCustomer());
            _DeleteCustomer = new Base.GEN_RelayCommand(param => this.Perform_DeleteCustomer());
            _AddProject = new Base.GEN_RelayCommand(param => this.Perform_AddProject());
            _EditProject = new Base.GEN_RelayCommand(param => this.Perform_EditProject());
            _DeleteProject = new Base.GEN_RelayCommand(param => this.Perform_DeleteProject());

            _CloseLoadingScreen = new Base.GEN_RelayCommand(param => this.Perfrom_CloseLoadingScreen());
        }

        private void Perfrom_CloseLoadingScreen()
        {
            ToggleLoadingScreen_Visibility();
        }

        private async void Perform_AddCustomer()
        {
            try
            {
                // Create new Customer
                Customer newCustomer = new Customer()
                {
                    Name = customer_addedit_Name,
                    Surname = customer_addedit_Surname,
                    Email = customer_addedit_Email,
                    CreationDate = DateTime.Now,
                    Status = "Active"
                };

                // Update in database
                await myBackendService.AddCustomer(newCustomer);

                // Update in current app session
                newCustomer.Id = allCustomers.Select(x => x.Id).Max() + 1;
                allCustomers.Add(newCustomer);

                // Update UI
                customer_addedit_Name = "";
                customer_addedit_Surname = "";
                customer_addedit_Email = "";
                customer_addedit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Customer: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_EditCustomer()
        {
            try
            {
                // Get modified Customer
                Customer modCustomer = new Customer()
                {
                    Id = config_customer_selCustomer.Id,
                    Name = customer_edit_Name,
                    Surname = customer_edit_Surname,
                    Email = customer_edit_Email,
                    CreationDate = config_customer_selCustomer.CreationDate,
                    Status = config_customer_selCustomer.Status
                };

                // Update in database
                await myBackendService.EditCustomer(modCustomer);

                // Update in current app session
                allCustomers[allCustomers.IndexOf(allCustomers.Single(x => x.Id == modCustomer.Id))] = modCustomer;


                // Update UI
                customer_edit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Customer: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_DeleteCustomer()
        {
            try
            {
                // Update in database
                await myBackendService.DeleteCustomer(config_customer_selCustomer.Id);

                // Update in current app session
                
                allCustomers.Remove(allCustomers.Single(x => x.Id == config_customer_selCustomer.Id));
                selCustomerindex = -1;
                config_customer_selindex = -1;

                // Update UI
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Customer: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_AddProject()
        {
            try
            {
                // Create new Project
                Project newProject = new Project()
                {
                    CustomerId = project_addedit_selCust.Id,
                    //Customer = project_addedit_selCust,
                    Name = project_addedit_Name,
                    Description = project_addedit_Description,
                    CreationDate = DateTime.Now,
                    Status = "Active"
                };

                Logger.Write("PERFORM_ADDPROJECT: " + Environment.NewLine + 
                    "CustomerId    = " + newProject.CustomerId.ToString() + Environment.NewLine + 
                    //"Customer      = " + newProject.Customer.Name + Environment.NewLine + 
                    "Name          = " + newProject.Name + Environment.NewLine + 
                    "Description   = " + newProject.Description + Environment.NewLine + 
                    "CreationDate  = " + newProject.CreationDate.ToString() + Environment.NewLine + 
                    "Status        = " + newProject.Status);

                // Update in database
                await myBackendService.AddProject(newProject);

                // Update in current app session
                newProject.Id = allProjects.Count > 0 ? allProjects.Select(x => x.Id).Max() + 1 : 1;
                newProject.Customer = project_addedit_selCust;
                allProjects.Add(newProject);

                if (allCustomers.Single(x => x.Id == newProject.CustomerId) != null)
                {
                    if (allCustomers.Single(x => x.Id == newProject.CustomerId).Projects == null)
                    {
                        allCustomers.Single(x => x.Id == newProject.CustomerId).Projects = new Collection<Project>
                        {
                            newProject
                        };
                    }
                    else
                    {
                        allCustomers.Single(x => x.Id == newProject.CustomerId).Projects.Add(newProject);
                    }
                }
                else
                {
                    Logger.Write("allCustomers.Single(x => x.Id == newProject.CustomerId) returns NULL!");
                }
                

                // Update UI
                project_addedit_selCustindex = -1;
                project_addedit_Name = "";
                project_addedit_Description = "";
                project_addedit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Project: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private void Perform_EditProject()
        {
            throw new NotImplementedException();
        }

        private void Perform_DeleteProject()
        {
            throw new NotImplementedException();
        }
    }
}