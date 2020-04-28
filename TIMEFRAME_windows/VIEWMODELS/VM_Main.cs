using System;
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
using MaterialDesignThemes.Wpf;
using TIMEFRAME_windows.MODELS.Report;

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


        // LOGIN elements
        // --------------
        private Visibility _LoginScreen_Visibility;
        private MODELS.User _myUser;
        private string _LoginScreen_message;


        // RECORD block
        // ------------
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

        private DateTime _record_StartTime;
        private DateTime _record_StopTime;
        private TimeSpan _record_Duration;


        // CONFIG block
        // --------------
        // --------------
        private ObservableCollection<Customer> _db_shownCustomers;
        private ObservableCollection<Project> _db_shownProjects;
        private ObservableCollection<TaskEntry> _db_shownTaskEntries;
        private ObservableCollection<TimeEntry> _db_shownTimeEntries;

        // CUSTOMER CONFIG
        // ---------------
        // Customer: selection
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
        // --------------
        // Project: selection
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

        // TASK ENTRY CONFIG
        // -----------------
        // Task Entry: selection
        private int _config_taskentry_selindex;
        private TaskEntry _config_taskentry_selTaskEntry;
        // Task Entry: add/edit
        private Visibility _taskentry_addedit_Visibility;
        private int _taskentry_addedit_selCustindex;
        private Customer _taskentry_addedit_selCust;
        private ObservableCollection<Project> _taskentry_addedit_availProjects;
        private int _taskentry_addedit_selProjindex;
        private Project _taskentry_addedit_selProj;
        private string _taskentry_addedit_Name;
        private string _taskentry_addedit_Description;
        private string _taskentry_addedit_Status;
        private bool _taskentry_addedit_Status_IsActive;
        private bool _taskentry_addedit_Status_IsInactive;
        // Task Entry: edit
        //private bool _taskentry_edit_IsEnabled;
        private Visibility _taskentry_edit_Visibility;
        private int _taskentry_edit_selCustindex;
        private Customer _taskentry_edit_selCust;
        private ObservableCollection<Project> _taskentry_edit_availProjects;
        private int _taskentry_edit_selProjindex;
        private Project _taskentry_edit_selProj;
        private string _taskentry_edit_Name;
        private string _taskentry_edit_Description;
        private string _taskentry_edit_Status;
        private bool _taskentry_edit_Status_IsActive;
        private bool _taskentry_edit_Status_IsInactive;
        // Task Entry: delete
        //private bool _taskentry_delete_IsEnabled;

        // TIME ENTRY CONFIG
        // -----------------
        // Time Entry: selection
        private int _config_timeentry_selindex;
        private TimeEntry _config_timeentry_selTimeEntry;
        // Time Entry: add/edit
        private Visibility _timeentry_addedit_Visibility;
        private int _timeentry_addedit_selCustindex;
        private Customer _timeentry_addedit_selCust;
        private ObservableCollection<Project> _timeentry_addedit_availProjects;
        private int _timeentry_addedit_selProjindex;
        private Project _timeentry_addedit_selProj;
        private ObservableCollection<TaskEntry> _timeentry_addedit_availTaskEntries;
        private int _timeentry_addedit_selTaskEntryindex;
        private TaskEntry _timeentry_addedit_selTaskEntry;
        private DateTime _timeentry_addedit_DateTimeStart;
        private DateTime _timeentry_addedit_TimeStart;
        private DateTime _timeentry_addedit_DateTimeStop;
        private DateTime _timeentry_addedit_TimeStop;
        private TimeSpan _timeentry_addedit_Duration;
        // Time Entry: edit
        private Visibility _timeentry_edit_Visibility;
        private int _timeentry_edit_selCustindex;
        private Customer _timeentry_edit_selCust;
        private ObservableCollection<Project> _timeentry_edit_availProjects;
        private int _timeentry_edit_selProjindex;
        private Project _timeentry_edit_selProj;
        private ObservableCollection<TaskEntry> _timeentry_edit_availTaskEntries;
        private int _timeentry_edit_selTaskEntryindex;
        private TaskEntry _timeentry_edit_selTaskEntry;
        private DateTime _timeentry_edit_DateTimeStart;
        private DateTime _timeentry_edit_TimeStart;
        private DateTime _timeentry_edit_DateTimeStop;
        private DateTime _timeentry_edit_TimeStop;
        private TimeSpan _timeentry_edit_Duration;
        // Time Entry: delete


        // REPORT BLOCK
        // ------------
        // ------------
        // TOTALS
        // ------
        // Totals - Selection
        private int _report_totals_selCustomerIndex;
        private Customer _report_totals_selCustomer;
        private ObservableCollection<Project> _report_totals_availProjects;
        private int _report_totals_selProjectIndex;
        private Project _report_totals_selProject;
        private ObservableCollection<TaskEntry> _report_totals_availTaskEntries;
        private int _report_totals_selTaskEntryIndex;
        private TaskEntry _report_totals_selTaskEntry;
        private DateTime _report_totals_filter_FromDate;
        private DateTime _report_totals_filter_ToDate;

        // Totals - Summary
        private ObservableCollection<CustomerReport> _report_totals_targCustomerColl;
        private ObservableCollection<ProjectReport> _report_totals_targProjectColl;
        private ObservableCollection<TaskEntryReport> _report_totals_targTaskEntryColl;
        private ObservableCollection<TimeEntry> _report_totals_targTimeEntryColl;
        private MODELS.TimeSpanHMS _report_totals_targTimeSpan;





        // Commands
        private VIEWMODELS.Base.GEN_RelayCommand _AddRecordedTimeEntry;
        private VIEWMODELS.Base.GEN_RelayCommand _RecordReset;

        private VIEWMODELS.Base.GEN_RelayCommand _AddCustomer;
        private VIEWMODELS.Base.GEN_RelayCommand _EditCustomer;
        private VIEWMODELS.Base.GEN_RelayCommand _DeleteCustomer;
        private VIEWMODELS.Base.GEN_RelayCommand _AddProject;
        private VIEWMODELS.Base.GEN_RelayCommand _EditProject;
        private VIEWMODELS.Base.GEN_RelayCommand _DeleteProject;
        private VIEWMODELS.Base.GEN_RelayCommand _AddTaskEntry;
        private VIEWMODELS.Base.GEN_RelayCommand _EditTaskEntry;
        private VIEWMODELS.Base.GEN_RelayCommand _DeleteTaskEntry;
        private VIEWMODELS.Base.GEN_RelayCommand _AddTimeEntry;
        private VIEWMODELS.Base.GEN_RelayCommand _EditTimeEntry;
        private VIEWMODELS.Base.GEN_RelayCommand _DeleteTimeEntry;

        private VIEWMODELS.Base.GEN_RelayCommand _ReportTotals_ClearFilters;

        private VIEWMODELS.Base.GEN_RelayCommand _CloseLoadingScreen;

        private VIEWMODELS.Base.GEN_RelayCommand _ApplyBaseTheme;

        private VIEWMODELS.Base.GEN_RelayCommand _LogIn;
        private VIEWMODELS.Base.GEN_RelayCommand _LogOut;

        // Services
        private IBackendService myBackendService;
        private IAuthenticationService myAuthenticator;

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
            LoadingScreen_Visibility = Visibility.Hidden;


            // Inject Services
            //InitializeServiceInjections(new BackendService());
            myBackendService = new BackendService();
            myAuthenticator = new AuthenticationService();


            // Initialize Properties
            LoginScreen_Visibility = Visibility.Visible;
            myUser = new MODELS.User();
            LoginScreen_message = "";

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

            selCustomerindex = -1;
            selProjectindex = -1;
            selTaskEntryindex = -1;
            Reset("RECORD_TIMEENTRY");

            db_shownCustomers = new ObservableCollection<Customer>();
            db_shownProjects = new ObservableCollection<Project>();
            db_shownTaskEntries = new ObservableCollection<TaskEntry>();
            db_shownTimeEntries = new ObservableCollection<TimeEntry>();

            config_customer_selindex = -1;
            customer_addedit_Visibility = Visibility.Hidden;
            customer_edit_IsEnabled = false;
            customer_edit_Visibility = Visibility.Hidden;

            config_project_selindex = -1;
            project_addedit_selCustindex = -1;
            project_edit_selCustindex = -1;
            project_addedit_Visibility = Visibility.Hidden;
            project_edit_Visibility = Visibility.Hidden;

            config_taskentry_selindex = -1;
            taskentry_addedit_selCustindex = -1;
            taskentry_addedit_selProjindex = -1;
            taskentry_addedit_availProjects = new ObservableCollection<Project>();
            taskentry_addedit_Status_IsActive = true;
            taskentry_edit_selCustindex = -1;
            taskentry_edit_selProjindex = -1;
            taskentry_edit_availProjects = new ObservableCollection<Project>();
            taskentry_addedit_Visibility = Visibility.Hidden;
            taskentry_edit_Visibility = Visibility.Hidden;

            config_timeentry_selindex = -1;
            timeentry_addedit_selCustindex = -1;
            timeentry_addedit_selProjindex = -1;
            timeentry_addedit_selTaskEntryindex = -1;
            timeentry_addedit_availProjects = new ObservableCollection<Project>();
            timeentry_addedit_availTaskEntries = new ObservableCollection<TaskEntry>();
            timeentry_addedit_DateTimeStart = DateTime.Now;
            timeentry_addedit_DateTimeStop = DateTime.Now;
            timeentry_addedit_TimeStart = DateTime.Now;
            timeentry_addedit_TimeStop = DateTime.Now;
            timeentry_addedit_Visibility = Visibility.Hidden;

            timeentry_edit_selCustindex = -1;
            timeentry_edit_selProjindex = -1;
            timeentry_edit_selTaskEntryindex = -1;
            timeentry_edit_availProjects = new ObservableCollection<Project>();
            timeentry_edit_availTaskEntries = new ObservableCollection<TaskEntry>();
            //timeentry_edit_DateTimeStart = DateTime.Now;
            //timeentry_edit_DateTimeStop = DateTime.Now;
            //timeentry_edit_TimeStart = DateTime.Now;
            //timeentry_edit_TimeStop = DateTime.Now;
            timeentry_edit_Visibility = Visibility.Hidden;

            report_totals_availProjects = new ObservableCollection<Project>();
            report_totals_availTaskEntries = new ObservableCollection<TaskEntry>();
            report_totals_selCustomerIndex = -1;
            report_totals_selProjectIndex = -1;
            report_totals_selTaskEntryIndex = -1;
            report_totals_filter_FromDate = DateTime.MinValue;
            report_totals_filter_ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 999);
            report_totals_targCustomerColl = new ObservableCollection<CustomerReport>();
            report_totals_targProjectColl = new ObservableCollection<ProjectReport>();
            report_totals_targTaskEntryColl = new ObservableCollection<TaskEntryReport>();
            report_totals_targTimeEntryColl = new ObservableCollection<TimeEntry>();

            


            // Initializations
            //LoadDatabaseData();   // --> DO THIS ONLY AFTER LOGGING IN SUCCESSFULLY!

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
        #region LOGIN
        public User myUser
        {
            get { return _myUser; }
            set { if (value != _myUser) { _myUser = value; RaisePropertyChangedEvent("myUser"); } }
        }

        public Visibility LoginScreen_Visibility
        {
            get { return _LoginScreen_Visibility; }
            set { if (value != _LoginScreen_Visibility) { _LoginScreen_Visibility = value; RaisePropertyChangedEvent("LoginScreen_Visibility"); } }
        }

        public string LoginScreen_message
        {
            get { return _LoginScreen_message; }
            set { if (value != _LoginScreen_message) { _LoginScreen_message = value; RaisePropertyChangedEvent("LoginScreen_message"); } }
        }
        #endregion

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

        public DateTime record_StartTime
        {
            get { return _record_StartTime; }
            set { if (value != _record_StartTime) { _record_StartTime = value; RaisePropertyChangedEvent("record_StartTime");
                    CalculateDuration("RECORD");
                } }
        }

        public DateTime record_StopTime
        {
            get { return _record_StopTime; }
            set { if (value != _record_StopTime) { _record_StopTime = value; RaisePropertyChangedEvent("record_StopTime");
                    CalculateDuration("RECORD");
                } }
        }

        public TimeSpan record_Duration
        {
            get { return _record_Duration; }
            set { if (value != _record_Duration) { _record_Duration = value; RaisePropertyChangedEvent("record_Duration"); } }
        }
        #endregion

        #region CONFIGURATION Component
        // Shown lists
        #region Shown lists
        public ObservableCollection<Customer> db_shownCustomers
        {
            get { return _db_shownCustomers; }
            set { if (value != _db_shownCustomers) { _db_shownCustomers = value; RaisePropertyChangedEvent("db_shownCustomers"); } }
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
        #endregion


        // Configuration:  CUSTOMER
        // ------------------------
        // Configuration:  Customer SELECTION
        #region Customer: SELECTION
        public int config_customer_selindex
        {
            get { return _config_customer_selindex; }
            set
            {
                if (value != _config_customer_selindex)
                {
                    _config_customer_selindex = value; RaisePropertyChangedEvent("config_customer_selindex");
                    if (config_customer_selindex > -1)
                    {
                        config_customer_selCustomer = db_shownCustomers[config_customer_selindex];
                        customer_edit_IsEnabled = true;
                        Update_EditSelectionData(dataCategory.Customer);
                        customer_delete_IsEnabled = true;
                    }
                    else { config_customer_selCustomer = null; customer_edit_IsEnabled = false; customer_delete_IsEnabled = false; }
                }
            }
        }

        public Customer config_customer_selCustomer
        {
            get { return _config_customer_selCustomer; }
            set { if (value != _config_customer_selCustomer) { _config_customer_selCustomer = value; RaisePropertyChangedEvent("config_customer_selCustomer"); } }
        }
        #endregion

        // Configuration:  Customer ADD
        #region Customer: ADD
        public Visibility customer_addedit_Visibility
        {
            get { return _customer_addedit_Visibility; }
            set
            {
                if (value != _customer_addedit_Visibility)
                {
                    _customer_addedit_Visibility = value; RaisePropertyChangedEvent("customer_addedit_Visibility");
                    if (customer_addedit_Visibility == Visibility.Visible) { Update_SecondaryViewVisibilities(dataCategory.Customer, true); }
                }
            }
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
        #endregion

        // Configuration:  Customer EDIT
        #region Customer: EDIT
        public bool customer_edit_IsEnabled
        {
            get { return _customer_edit_IsEnabled; }
            set { if (value != _customer_edit_IsEnabled) { _customer_edit_IsEnabled = value; RaisePropertyChangedEvent("customer_edit_IsEnabled"); } }
        }

        public Visibility customer_edit_Visibility
        {
            get { return _customer_edit_Visibility; }
            set
            {
                if (value != _customer_edit_Visibility)
                {
                    _customer_edit_Visibility = value; RaisePropertyChangedEvent("customer_edit_Visibility");
                    if (customer_edit_Visibility == Visibility.Visible) { Update_SecondaryViewVisibilities(dataCategory.Customer, false); }
                }
            }
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
        #endregion

        // Configuration:  Customer DELETE
        #region Customer: DELETE
        public bool customer_delete_IsEnabled
        {
            get { return _customer_delete_IsEnabled; }
            set { if (value != _customer_delete_IsEnabled) { _customer_delete_IsEnabled = value; RaisePropertyChangedEvent("customer_delete_IsEnabled"); } }
        }
        #endregion


        // Configuration:  PROJECT
        // -----------------------
        // Configuration:  Project SELECTION
        #region Project: SELECTION
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
        #endregion

        // Configuration:  Project ADD
        #region Project: ADD
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
            set
            {
                if (value != _project_addedit_selCustindex)
                {
                    _project_addedit_selCustindex = value; RaisePropertyChangedEvent("project_addedit_selCustindex");
                    if (project_addedit_selCustindex > -1)
                    {
                        project_addedit_selCust = allCustomers[project_addedit_selCustindex];
                    }
                    else { project_addedit_selCust = null; }
                }
            }
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
        #endregion

        // Configuration:  Project EDIT
        #region Project: EDIT
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
        #endregion

        // Configuration:  Project DELETE
        #region Project: DELETE
        public bool project_delete_IsEnabled
        {
            get { return _project_delete_IsEnabled; }
            set { if (value != _project_delete_IsEnabled) { _project_delete_IsEnabled = value; RaisePropertyChangedEvent("project_delete_IsEnabled"); } }
        }
        #endregion


        // Configuration:  TASK ENTRY
        // --------------------------
        // Configuration:  Task Entry SELECTION
        #region Task Entry: SELECTION
        public int config_taskentry_selindex
        {
            get { return _config_taskentry_selindex; }
            set
            {
                if (value != _config_taskentry_selindex)
                {
                    _config_taskentry_selindex = value;
                    RaisePropertyChangedEvent("config_taskentry_selindex");
                    if (config_taskentry_selindex > -1)
                    {
                        config_taskentry_selTaskEntry = db_shownTaskEntries[config_taskentry_selindex];
                        Update_EditSelectionData(dataCategory.TaskEntry);
                    }
                    else { config_taskentry_selTaskEntry = null; }
                }
            }
        }

        public TaskEntry config_taskentry_selTaskEntry
        {
            get { return _config_taskentry_selTaskEntry; }
            set { if (value != _config_taskentry_selTaskEntry) { _config_taskentry_selTaskEntry = value; RaisePropertyChangedEvent("config_taskentry_selTaskEntry"); } }
        }
        #endregion

        // Configuration:  Task Entry ADD
        #region Task Entry: ADD
        public Visibility taskentry_addedit_Visibility
        {
            get { return _taskentry_addedit_Visibility; }
            set { if (value != _taskentry_addedit_Visibility) { _taskentry_addedit_Visibility = value; RaisePropertyChangedEvent("taskentry_addedit_Visibility"); } }
        }


        public int taskentry_addedit_selCustindex
        {
            get { return _taskentry_addedit_selCustindex; }
            set
            {
                if (value != _taskentry_addedit_selCustindex)
                {
                    _taskentry_addedit_selCustindex = value;
                    RaisePropertyChangedEvent("taskentry_addedit_selCustindex");
                    if (taskentry_addedit_selCustindex > -1)
                    {
                        // Assign new selected Customer object
                        taskentry_addedit_selCust = allCustomers[taskentry_addedit_selCustindex];

                        // Re-populate available Projects based on newly selected Customer object
                        taskentry_addedit_selProjindex = -1;

                        taskentry_addedit_availProjects.Clear();
                        foreach (Project project in allProjects.Where(x => x.CustomerId == taskentry_addedit_selCust.Id))
                        {
                            taskentry_addedit_availProjects.Add(project);
                        }
                    }
                }
            }
        }

        public Customer taskentry_addedit_selCust
        {
            get { return _taskentry_addedit_selCust; }
            set { if (value != _taskentry_addedit_selCust) { _taskentry_addedit_selCust = value; RaisePropertyChangedEvent("taskentry_addedit_selCust"); } }
        }

        public ObservableCollection<Project> taskentry_addedit_availProjects
        {
            get { return _taskentry_addedit_availProjects; }
            set { if (value != _taskentry_addedit_availProjects) { _taskentry_addedit_availProjects = value; RaisePropertyChangedEvent("taskentry_addedit_availProjects"); } }
        }

        public int taskentry_addedit_selProjindex
        {
            get { return _taskentry_addedit_selProjindex; }
            set
            {
                if (value != _taskentry_addedit_selProjindex)
                {
                    _taskentry_addedit_selProjindex = value;
                    RaisePropertyChangedEvent("taskentry_addedit_selProjindex");
                    if (taskentry_addedit_selProjindex > -1)
                    {
                        taskentry_addedit_selProj = taskentry_addedit_availProjects[taskentry_addedit_selProjindex];
                    }
                    else { taskentry_addedit_selProj = null; }
                }
            }
        }

        public Project taskentry_addedit_selProj
        {
            get { return _taskentry_addedit_selProj; }
            set { if (value != _taskentry_addedit_selProj) { _taskentry_addedit_selProj = value; RaisePropertyChangedEvent("taskentry_addedit_selProj"); } }
        }

        public string taskentry_addedit_Name
        {
            get { return _taskentry_addedit_Name; }
            set { if (value != _taskentry_addedit_Name) { _taskentry_addedit_Name = value; RaisePropertyChangedEvent("taskentry_addedit_Name"); } }
        }

        public string taskentry_addedit_Description
        {
            get { return _taskentry_addedit_Description; }
            set { if (value != _taskentry_addedit_Description) { _taskentry_addedit_Description = value; RaisePropertyChangedEvent("taskentry_addedit_Description"); } }
        }

        public string taskentry_addedit_Status
        {
            get { return _taskentry_addedit_Status; }
            set { if (value != _taskentry_addedit_Status) { _taskentry_addedit_Status = value; RaisePropertyChangedEvent("taskentry_addedit_Status"); } }
        }

        public bool taskentry_addedit_Status_IsActive
        {
            get { return _taskentry_addedit_Status_IsActive; }
            set
            {
                if (value != _taskentry_addedit_Status_IsActive)
                {
                    _taskentry_addedit_Status_IsActive = value;
                    RaisePropertyChangedEvent("taskentry_addedit_Status_IsActive");
                    if (taskentry_addedit_Status_IsActive == true)
                    {
                        taskentry_addedit_Status_IsInactive = false;
                        taskentry_addedit_Status = "Active";
                    }
                }
            }
        }

        public bool taskentry_addedit_Status_IsInactive
        {
            get { return _taskentry_addedit_Status_IsInactive; }
            set
            {
                if (value != _taskentry_addedit_Status_IsInactive)
                {
                    _taskentry_addedit_Status_IsInactive = value;
                    RaisePropertyChangedEvent("taskentry_addedit_Status_IsInactive");
                    if (taskentry_addedit_Status_IsInactive == true)
                    {
                        taskentry_addedit_Status_IsActive = false;
                        taskentry_addedit_Status = "Inactive";
                    }
                }
            }
        }
        #endregion

        // Configuration:  Task Entry EDIT
        #region Task Entry: EDIT
        public Visibility taskentry_edit_Visibility
        {
            get { return _taskentry_edit_Visibility; }
            set { if (value != _taskentry_edit_Visibility) { _taskentry_edit_Visibility = value; RaisePropertyChangedEvent("taskentry_edit_Visibility"); } }
        }

        public int taskentry_edit_selCustindex
        {
            get { return _taskentry_edit_selCustindex; }
            set
            {
                if (value != _taskentry_edit_selCustindex)
                {
                    _taskentry_edit_selCustindex = value;
                    RaisePropertyChangedEvent("taskentry_edit_selCustindex");
                    if (taskentry_edit_selCustindex > -1)
                    {
                        // Assign new selected Customer object
                        taskentry_edit_selCust = allCustomers[taskentry_edit_selCustindex];

                        // Re-populate available Projects based on newly selected Customer object
                        taskentry_edit_selProjindex = -1;

                        taskentry_edit_availProjects.Clear();
                        foreach (Project project in allProjects.Where(x => x.CustomerId == taskentry_edit_selCust.Id))
                        {
                            taskentry_edit_availProjects.Add(project);
                        }
                    }
                    else
                    {
                        taskentry_edit_selCust = null;
                        if(taskentry_edit_availProjects != null){ taskentry_edit_availProjects.Clear(); }
                    }
                }
            }
        }

        public Customer taskentry_edit_selCust
        {
            get { return _taskentry_edit_selCust; }
            set { if (value != _taskentry_edit_selCust) { _taskentry_edit_selCust = value; RaisePropertyChangedEvent("taskentry_edit_selCust"); } }
        }

        public ObservableCollection<Project> taskentry_edit_availProjects
        {
            get { return _taskentry_edit_availProjects; }
            set { if (value != _taskentry_edit_availProjects) { _taskentry_edit_availProjects = value; RaisePropertyChangedEvent("taskentry_edit_availProjects"); } }
        }

        public int taskentry_edit_selProjindex
        {
            get { return _taskentry_edit_selProjindex; }
            set
            {
                if (value != _taskentry_edit_selProjindex)
                {
                    _taskentry_edit_selProjindex = value;
                    RaisePropertyChangedEvent("taskentry_edit_selProjindex");
                    if (taskentry_edit_selProjindex > -1)
                    {
                        taskentry_edit_selProj = taskentry_edit_availProjects[taskentry_edit_selProjindex];
                    }
                    else { taskentry_edit_selProj = null; }
                }
            }
        }

        public Project taskentry_edit_selProj
        {
            get { return _taskentry_edit_selProj; }
            set { if (value != _taskentry_edit_selProj) { _taskentry_edit_selProj = value; RaisePropertyChangedEvent("taskentry_edit_selProj"); } }
        }

        public string taskentry_edit_Name
        {
            get { return _taskentry_edit_Name; }
            set { if (value != _taskentry_edit_Name) { _taskentry_edit_Name = value; RaisePropertyChangedEvent("taskentry_edit_Name"); } }
        }

        public string taskentry_edit_Description
        {
            get { return _taskentry_edit_Description; }
            set { if (value != _taskentry_edit_Description) { _taskentry_edit_Description = value; RaisePropertyChangedEvent("taskentry_edit_Description"); } }
        }

        public string taskentry_edit_Status
        {
            get { return _taskentry_edit_Status; }
            set { if (value != _taskentry_edit_Status) { _taskentry_edit_Status = value; RaisePropertyChangedEvent("taskentry_edit_Status"); } }
        }

        public bool taskentry_edit_Status_IsActive
        {
            get { return _taskentry_edit_Status_IsActive; }
            set
            {
                if (value != _taskentry_edit_Status_IsActive)
                {
                    _taskentry_edit_Status_IsActive = value;
                    RaisePropertyChangedEvent("taskentry_edit_Status_IsActive");
                    if (taskentry_edit_Status_IsActive == true)
                    {
                        taskentry_edit_Status_IsInactive = false;
                        taskentry_edit_Status = "Active";
                    }
                }
            }
        }

        public bool taskentry_edit_Status_IsInactive
        {
            get { return _taskentry_edit_Status_IsInactive; }
            set
            {
                if (value != _taskentry_edit_Status_IsInactive)
                {
                    _taskentry_edit_Status_IsInactive = value;
                    RaisePropertyChangedEvent("taskentry_edit_Status_IsInactive");
                    if (taskentry_edit_Status_IsInactive == true)
                    {
                        taskentry_edit_Status_IsActive = false;
                        taskentry_edit_Status = "Inactive";
                    }
                }
            }
        }
        #endregion


        // Configuration:  TIME ENTRY
        // --------------------------
        // Time Entry: selection
        #region Time Entry: SELECTION
        public int config_timeentry_selindex
        {
            get { return _config_timeentry_selindex; }
            set
            {
                if (value != _config_timeentry_selindex)
                {
                    _config_timeentry_selindex = value;
                    RaisePropertyChangedEvent("config_timeentry_selindex");
                    //Logger.Write("config_timeentry_selindex changed: " + config_timeentry_selindex.ToString());

                    if (config_timeentry_selindex > -1)
                    {
                        config_timeentry_selTimeEntry = db_shownTimeEntries[config_timeentry_selindex];
                        Update_EditSelectionData(dataCategory.TimeEntry);

                        Logger.Write("config_timeentry_selTimeEntry assigned");
                    }
                    else { config_timeentry_selTimeEntry = null; }
                }
            }
        }

        public TimeEntry config_timeentry_selTimeEntry
        {
            get { return _config_timeentry_selTimeEntry; }
            set { if (value != _config_timeentry_selTimeEntry) { _config_timeentry_selTimeEntry = value; RaisePropertyChangedEvent("config_timeentry_selTimeEntry"); } }
        }
        #endregion

        // Time Entry: add/edit
        #region Time Entry: ADD
        public Visibility timeentry_addedit_Visibility
        {
            get { return _timeentry_addedit_Visibility; }
            set { if (value != _timeentry_addedit_Visibility) { _timeentry_addedit_Visibility = value; RaisePropertyChangedEvent("timeentry_addedit_Visibility"); } }
        }

        public int timeentry_addedit_selCustindex
        {
            get { return _timeentry_addedit_selCustindex; }
            set
            {
                if (value != _timeentry_addedit_selCustindex)
                {
                    _timeentry_addedit_selCustindex = value;
                    RaisePropertyChangedEvent("timeentry_addedit_selCustindex");
                    if (timeentry_addedit_selCustindex > -1)
                    {
                        // Assign selected Customer
                        timeentry_addedit_selCust = allCustomers[timeentry_addedit_selCustindex];

                        // Re-populate available Projects based on newly selected Customer object
                        timeentry_addedit_selProjindex = -1;

                        timeentry_addedit_availProjects.Clear();
                        foreach (Project project in allProjects.Where(x => x.CustomerId == timeentry_addedit_selCust.Id))
                        {
                            timeentry_addedit_availProjects.Add(project);
                        }
                    }
                    else { timeentry_addedit_selCust = null; }
                }
            }
        }

        public Customer timeentry_addedit_selCust
        {
            get { return _timeentry_addedit_selCust; }
            set { if (value != _timeentry_addedit_selCust) { _timeentry_addedit_selCust = value; RaisePropertyChangedEvent("timeentry_addedit_selCust"); } }
        }

        public ObservableCollection<Project> timeentry_addedit_availProjects
        {
            get { return _timeentry_addedit_availProjects; }
            set { if (value != _timeentry_addedit_availProjects) { _timeentry_addedit_availProjects = value; RaisePropertyChangedEvent("timeentry_addedit_availProjects"); } }
        }

        public int timeentry_addedit_selProjindex
        {
            get { return _timeentry_addedit_selProjindex; }
            set
            {
                if (value != _timeentry_addedit_selProjindex)
                {
                    _timeentry_addedit_selProjindex = value;
                    RaisePropertyChangedEvent("timeentry_addedit_selProjindex");
                    if (timeentry_addedit_selProjindex > -1)
                    {
                        // Assign selected Project
                        timeentry_addedit_selProj = timeentry_addedit_availProjects[timeentry_addedit_selProjindex];

                        // Re-populate available Task Entries based on newly selected Project object
                        timeentry_addedit_selTaskEntryindex = -1;

                        timeentry_addedit_availTaskEntries.Clear();
                        foreach (TaskEntry taskEntry in allTaskEntries.Where(x => x.ProjectId == timeentry_addedit_selProj.Id))
                        {
                            timeentry_addedit_availTaskEntries.Add(taskEntry);
                        }
                    }
                    else { timeentry_addedit_selProj = null; }
                }
            }
        }

        public Project timeentry_addedit_selProj
        {
            get { return _timeentry_addedit_selProj; }
            set { if (value != _timeentry_addedit_selProj) { _timeentry_addedit_selProj = value; RaisePropertyChangedEvent("timeentry_addedit_selProj"); } }
        }

        public ObservableCollection<TaskEntry> timeentry_addedit_availTaskEntries
        {
            get { return _timeentry_addedit_availTaskEntries; }
            set { if (value != _timeentry_addedit_availTaskEntries) { _timeentry_addedit_availTaskEntries = value; RaisePropertyChangedEvent("timeentry_addedit_availTaskEntries"); } }
        }

        public int timeentry_addedit_selTaskEntryindex
        {
            get { return _timeentry_addedit_selTaskEntryindex; }
            set
            {
                if (value != _timeentry_addedit_selTaskEntryindex)
                {
                    _timeentry_addedit_selTaskEntryindex = value;
                    RaisePropertyChangedEvent("timeentry_addedit_selTaskEntryindex");
                    if (timeentry_addedit_selTaskEntryindex > -1)
                    {
                        // Assign selected Project
                        timeentry_addedit_selTaskEntry = timeentry_addedit_availTaskEntries[timeentry_addedit_selTaskEntryindex];
                    }
                    else { timeentry_addedit_selTaskEntry = null; }
                }
            }
        }

        public TaskEntry timeentry_addedit_selTaskEntry
        {
            get { return _timeentry_addedit_selTaskEntry; }
            set { if (value != _timeentry_addedit_selTaskEntry) { _timeentry_addedit_selTaskEntry = value; RaisePropertyChangedEvent("timeentry_addedit_selTaskEntry"); } }
        }

        public DateTime timeentry_addedit_DateTimeStart
        {
            get { return _timeentry_addedit_DateTimeStart; }
            set
            {
                if (value != _timeentry_addedit_DateTimeStart)
                {
                    _timeentry_addedit_DateTimeStart = value;

                    timeentry_addedit_DateTimeStart = new DateTime(
                        timeentry_addedit_DateTimeStart.Year,
                        timeentry_addedit_DateTimeStart.Month,
                        timeentry_addedit_DateTimeStart.Day,
                        timeentry_addedit_TimeStart.Hour,
                        timeentry_addedit_TimeStart.Minute,
                        timeentry_addedit_TimeStart.Second);

                    RaisePropertyChangedEvent("timeentry_addedit_DateTimeStart");

                    //Logger.Write("timeentry_addedit_DateTimeStart changed: " + Environment.NewLine +
                    //                "Date = " + timeentry_addedit_DateTimeStart.Date.ToLongDateString() + Environment.NewLine +
                    //                "Time = " + timeentry_addedit_DateTimeStart.TimeOfDay.ToString());

                    CalculateDuration("TIMEENTRY_ADDEDIT");
                }
            }
        }

        public DateTime timeentry_addedit_TimeStart
        {
            get { return _timeentry_addedit_TimeStart; }
            set
            {
                if (value != _timeentry_addedit_TimeStart)
                {
                    _timeentry_addedit_TimeStart = value;
                    
                    timeentry_addedit_DateTimeStart = new DateTime(
                        timeentry_addedit_DateTimeStart.Year,
                        timeentry_addedit_DateTimeStart.Month,
                        timeentry_addedit_DateTimeStart.Day,
                        timeentry_addedit_TimeStart.Hour,
                        timeentry_addedit_TimeStart.Minute,
                        timeentry_addedit_TimeStart.Second);

                    RaisePropertyChangedEvent("timeentry_addedit_TimeStart");
                }
            }
        }

        public DateTime timeentry_addedit_DateTimeStop
        {
            get { return _timeentry_addedit_DateTimeStop; }
            set
            {
                if (value != _timeentry_addedit_DateTimeStop)
                {
                    _timeentry_addedit_DateTimeStop = value;

                    timeentry_addedit_DateTimeStop = new DateTime(
                            timeentry_addedit_DateTimeStop.Year,
                            timeentry_addedit_DateTimeStop.Month,
                            timeentry_addedit_DateTimeStop.Day,
                            timeentry_addedit_TimeStop.Hour,
                            timeentry_addedit_TimeStop.Minute,
                            timeentry_addedit_TimeStop.Second);

                    RaisePropertyChangedEvent("timeentry_addedit_DateTimeStop");

                    //Logger.Write("timeentry_addedit_DateTimeStop changed: " + Environment.NewLine +
                    //                "Date = " + timeentry_addedit_DateTimeStop.Date.ToLongDateString() + Environment.NewLine +
                    //                "Time = " + timeentry_addedit_DateTimeStop.TimeOfDay.ToString());

                    CalculateDuration("TIMEENTRY_ADDEDIT");
                }
            }
        }


        public DateTime timeentry_addedit_TimeStop
        {
            get { return _timeentry_addedit_TimeStop; }
            set { if (value != _timeentry_addedit_TimeStop) { _timeentry_addedit_TimeStop = value;
                    
                    timeentry_addedit_DateTimeStop = new DateTime(
                            timeentry_addedit_DateTimeStop.Year,
                            timeentry_addedit_DateTimeStop.Month,
                            timeentry_addedit_DateTimeStop.Day,
                            timeentry_addedit_TimeStop.Hour,
                            timeentry_addedit_TimeStop.Minute,
                            timeentry_addedit_TimeStop.Second);

                    RaisePropertyChangedEvent("timeentry_addedit_TimeStop");
                } }
        }

        public TimeSpan timeentry_addedit_Duration
        {
            get { return _timeentry_addedit_Duration; }
            set { if (value != _timeentry_addedit_Duration) { _timeentry_addedit_Duration = value; RaisePropertyChangedEvent("timeentry_addedit_Duration"); } }
        }
        #endregion

        // Time Entry: edit
        #region Time Entry: EDIT
        public Visibility timeentry_edit_Visibility
        {
            get { return _timeentry_edit_Visibility; }
            set { if (value != _timeentry_edit_Visibility) { _timeentry_edit_Visibility = value; RaisePropertyChangedEvent("timeentry_edit_Visibility"); } }
        }

        public int timeentry_edit_selCustindex
        {
            get { return _timeentry_edit_selCustindex; }
            set
            {
                if (value != _timeentry_edit_selCustindex)
                {
                    _timeentry_edit_selCustindex = value;
                    RaisePropertyChangedEvent("timeentry_edit_selCustindex");
                    if (timeentry_edit_selCustindex > -1)
                    {
                        // Assign selected Customer
                        timeentry_edit_selCust = allCustomers[timeentry_edit_selCustindex];

                        // Re-populate available Projects based on newly selected Customer object
                        timeentry_edit_selProjindex = -1;

                        timeentry_edit_availProjects.Clear();
                        foreach (Project project in allProjects.Where(x => x.CustomerId == timeentry_edit_selCust.Id))
                        {
                            timeentry_edit_availProjects.Add(project);
                        }
                    }
                    else { timeentry_edit_selCust = null; if (timeentry_edit_availProjects != null) { timeentry_edit_availProjects.Clear(); } }
                }
            }
        }

        public Customer timeentry_edit_selCust
        {
            get { return _timeentry_edit_selCust; }
            set { if (value != _timeentry_edit_selCust) { _timeentry_edit_selCust = value; RaisePropertyChangedEvent("timeentry_edit_selCust"); } }
        }

        public ObservableCollection<Project> timeentry_edit_availProjects
        {
            get { return _timeentry_edit_availProjects; }
            set { if (value != _timeentry_edit_availProjects) { _timeentry_edit_availProjects = value; RaisePropertyChangedEvent("timeentry_edit_availProjects"); } }
        }

        public int timeentry_edit_selProjindex
        {
            get { return _timeentry_edit_selProjindex; }
            set
            {
                if (value != _timeentry_edit_selProjindex)
                {
                    _timeentry_edit_selProjindex = value;
                    RaisePropertyChangedEvent("timeentry_edit_selProjindex");
                    if (timeentry_edit_selProjindex > -1)
                    {
                        // Assign selected Project
                        timeentry_edit_selProj = timeentry_edit_availProjects[timeentry_edit_selProjindex];

                        // Re-populate available Task Entries based on newly selected Project object
                        timeentry_edit_selTaskEntryindex = -1;

                        timeentry_edit_availTaskEntries.Clear();
                        foreach (TaskEntry taskEntry in allTaskEntries.Where(x => x.ProjectId == timeentry_edit_selProj.Id))
                        {
                            timeentry_edit_availTaskEntries.Add(taskEntry);
                        }
                    }
                    else { timeentry_edit_selProj = null; if (timeentry_edit_availTaskEntries != null) { timeentry_edit_availTaskEntries.Clear(); } }
                }
            }
        }

        public Project timeentry_edit_selProj
        {
            get { return _timeentry_edit_selProj; }
            set { if (value != _timeentry_edit_selProj) { _timeentry_edit_selProj = value; RaisePropertyChangedEvent("timeentry_edit_selProj"); } }
        }

        public ObservableCollection<TaskEntry> timeentry_edit_availTaskEntries
        {
            get { return _timeentry_edit_availTaskEntries; }
            set { if (value != _timeentry_edit_availTaskEntries) { _timeentry_edit_availTaskEntries = value; RaisePropertyChangedEvent("timeentry_edit_availTaskEntries"); } }
        }

        public int timeentry_edit_selTaskEntryindex
        {
            get { return _timeentry_edit_selTaskEntryindex; }
            set
            {
                if (value != _timeentry_edit_selTaskEntryindex)
                {
                    _timeentry_edit_selTaskEntryindex = value;
                    RaisePropertyChangedEvent("timeentry_edit_selTaskEntryindex");
                    if (timeentry_edit_selTaskEntryindex > -1)
                    {
                        // Assign selected Project
                        timeentry_edit_selTaskEntry = timeentry_edit_availTaskEntries[timeentry_edit_selTaskEntryindex];
                    }
                    else { timeentry_edit_selTaskEntry = null; }
                }
            }
        }

        public TaskEntry timeentry_edit_selTaskEntry
        {
            get { return _timeentry_edit_selTaskEntry; }
            set { if (value != _timeentry_edit_selTaskEntry) { _timeentry_edit_selTaskEntry = value; RaisePropertyChangedEvent("timeentry_edit_selTaskEntry"); } }
        }

        public DateTime timeentry_edit_DateTimeStart
        {
            get { return _timeentry_edit_DateTimeStart; }
            set
            {
                if (value != _timeentry_edit_DateTimeStart)
                {
                    _timeentry_edit_DateTimeStart = value;

                    timeentry_edit_DateTimeStart = new DateTime(
                        timeentry_edit_DateTimeStart.Year,
                        timeentry_edit_DateTimeStart.Month,
                        timeentry_edit_DateTimeStart.Day,
                        timeentry_edit_TimeStart.Hour,
                        timeentry_edit_TimeStart.Minute,
                        timeentry_edit_TimeStart.Second);

                    RaisePropertyChangedEvent("timeentry_edit_DateTimeStart");

                    //Logger.Write("timeentry_edit_DateTimeStart changed: " + Environment.NewLine +
                    //                "Date = " + timeentry_edit_DateTimeStart.Date.ToLongDateString() + Environment.NewLine +
                    //                "Time = " + timeentry_edit_DateTimeStart.TimeOfDay.ToString());

                    CalculateDuration("TIMEENTRY_EDIT");
                }
            }
        }

        public DateTime timeentry_edit_TimeStart
        {
            get { return _timeentry_edit_TimeStart; }
            set
            {
                if (value != _timeentry_edit_TimeStart)
                {
                    _timeentry_edit_TimeStart = value;

                    timeentry_edit_DateTimeStart = new DateTime(
                        timeentry_edit_DateTimeStart.Year,
                        timeentry_edit_DateTimeStart.Month,
                        timeentry_edit_DateTimeStart.Day,
                        timeentry_edit_TimeStart.Hour,
                        timeentry_edit_TimeStart.Minute,
                        timeentry_edit_TimeStart.Second);

                    RaisePropertyChangedEvent("timeentry_edit_TimeStart");

                    Logger.Write("TIMEENTRY_EDIT_TIMESTART changed: " + Environment.NewLine +
                        timeentry_edit_TimeStart.ToLongTimeString());
                }
            }
        }

        public DateTime timeentry_edit_DateTimeStop
        {
            get { return _timeentry_edit_DateTimeStop; }
            set
            {
                if (value != _timeentry_edit_DateTimeStop)
                {
                    _timeentry_edit_DateTimeStop = value;

                    timeentry_edit_DateTimeStop = new DateTime(
                            timeentry_edit_DateTimeStop.Year,
                            timeentry_edit_DateTimeStop.Month,
                            timeentry_edit_DateTimeStop.Day,
                            timeentry_edit_TimeStop.Hour,
                            timeentry_edit_TimeStop.Minute,
                            timeentry_edit_TimeStop.Second);

                    RaisePropertyChangedEvent("timeentry_edit_DateTimeStop");

                    //Logger.Write("timeentry_edit_DateTimeStop changed: " + Environment.NewLine +
                    //                "Date = " + timeentry_edit_DateTimeStop.Date.ToLongDateString() + Environment.NewLine +
                    //                "Time = " + timeentry_edit_DateTimeStop.TimeOfDay.ToString());

                    CalculateDuration("TIMEENTRY_EDIT");
                }
            }
        }

        public DateTime timeentry_edit_TimeStop
        {
            get { return _timeentry_edit_TimeStop; }
            set
            {
                if (value != _timeentry_edit_TimeStop)
                {
                    _timeentry_edit_TimeStop = value;

                    timeentry_edit_DateTimeStop = new DateTime(
                            timeentry_edit_DateTimeStop.Year,
                            timeentry_edit_DateTimeStop.Month,
                            timeentry_edit_DateTimeStop.Day,
                            timeentry_edit_TimeStop.Hour,
                            timeentry_edit_TimeStop.Minute,
                            timeentry_edit_TimeStop.Second);

                    RaisePropertyChangedEvent("timeentry_edit_TimeStop");

                    Logger.Write("TIMEENTRY_EDIT_TIMESTOP changed: " + Environment.NewLine +
                        timeentry_edit_TimeStop.ToLongTimeString());
                }
            }
        }

        public TimeSpan timeentry_edit_Duration
        {
            get { return _timeentry_edit_Duration; }
            set { if (value != _timeentry_edit_Duration) { _timeentry_edit_Duration = value; 
                    RaisePropertyChangedEvent("timeentry_edit_Duration"); } }
        }
        #endregion

        // Time Entry: delete
        #endregion

        #region REPORTS Component
        #region TOTALS report
        // TOTALS report
        // -------------
        // Totals report:  Selection
        public int report_totals_selCustomerIndex
        {
            get { return _report_totals_selCustomerIndex; }
            set { if (value != _report_totals_selCustomerIndex) { _report_totals_selCustomerIndex = value; RaisePropertyChangedEvent("report_totals_selCustomerIndex");
                    if (report_totals_selCustomerIndex > -1)
                    {
                        report_totals_selCustomer = allCustomers[report_totals_selCustomerIndex];

                        // Re-populate available Projects based on newly selected Customer object
                        report_totals_selProjectIndex = -1;

                        report_totals_availProjects.Clear();
                        foreach (Project project in allProjects.Where(x => x.CustomerId == report_totals_selCustomer.Id))
                        {
                            report_totals_availProjects.Add(project);
                        }
                    }
                    else { report_totals_selCustomer = null; if (report_totals_availProjects!=null) { report_totals_availProjects.Clear(); } }

                    // Re-calculate target data collections for Totals report
                    GetTargetCollections_ReportTotals();
                } }
        }

        public Customer report_totals_selCustomer
        {
            get { return _report_totals_selCustomer; }
            set { if (value != _report_totals_selCustomer) { _report_totals_selCustomer = value; RaisePropertyChangedEvent("report_totals_selCustomer"); } }
        }

        public ObservableCollection<Project> report_totals_availProjects
        {
            get { return _report_totals_availProjects; }
            set { if (value != _report_totals_availProjects) { _report_totals_availProjects = value; RaisePropertyChangedEvent("report_totals_availProjects"); } }
        }

        public int report_totals_selProjectIndex
        {
            get { return _report_totals_selProjectIndex; }
            set { if (value != _report_totals_selProjectIndex) { _report_totals_selProjectIndex = value; RaisePropertyChangedEvent("report_totals_selProjectIndex");
                    if (report_totals_selProjectIndex > -1)
                    {
                        report_totals_selProject = report_totals_availProjects[report_totals_selProjectIndex];

                        // Re-populate available Task Entries based on newly selected Project object
                        report_totals_selTaskEntryIndex = -1;

                        report_totals_availTaskEntries.Clear();
                        foreach (TaskEntry task in allTaskEntries.Where(x => x.ProjectId == report_totals_selProject.Id))
                        {
                            report_totals_availTaskEntries.Add(task);
                        }
                    }
                    else { report_totals_selProject = null; if (report_totals_availTaskEntries != null) { report_totals_availTaskEntries.Clear(); } }

                    // Re-calculate target data collections for Totals report
                    GetTargetCollections_ReportTotals();
                } }
        }

        public Project report_totals_selProject
        {
            get { return _report_totals_selProject; }
            set { if (value != _report_totals_selProject) { _report_totals_selProject = value; RaisePropertyChangedEvent("report_totals_selProject"); } }
        }

        public ObservableCollection<TaskEntry> report_totals_availTaskEntries
        {
            get { return _report_totals_availTaskEntries; }
            set { if (value != _report_totals_availTaskEntries) { _report_totals_availTaskEntries = value; RaisePropertyChangedEvent("report_totals_availTaskEntries"); } }
        }

        public int report_totals_selTaskEntryIndex
        {
            get { return _report_totals_selTaskEntryIndex; }
            set { if (value != _report_totals_selTaskEntryIndex) { _report_totals_selTaskEntryIndex = value; RaisePropertyChangedEvent("report_totals_selTaskEntryIndex");
                    if (report_totals_selTaskEntryIndex > -1)
                    {
                        report_totals_selTaskEntry = report_totals_availTaskEntries[report_totals_selTaskEntryIndex];
                    }
                    else { report_totals_selTaskEntry = null; }

                    // Re-calculate target data collections for Totals report
                    GetTargetCollections_ReportTotals();
                } }
        }

        public TaskEntry report_totals_selTaskEntry
        {
            get { return _report_totals_selTaskEntry; }
            set { if (value != _report_totals_selTaskEntry) { _report_totals_selTaskEntry = value; RaisePropertyChangedEvent("report_totals_selTaskEntry"); } }
        }

        public DateTime report_totals_filter_FromDate
        {
            get { return _report_totals_filter_FromDate; }
            set { if (value != _report_totals_filter_FromDate) { _report_totals_filter_FromDate = value; RaisePropertyChangedEvent("report_totals_filter_FromDate");
                    // Re-calculate target data collections for Totals report
                    GetTargetCollections_ReportTotals();
                } }
        }

        public DateTime report_totals_filter_ToDate
        {
            get { return _report_totals_filter_ToDate; }
            set { if (value != _report_totals_filter_ToDate) { _report_totals_filter_ToDate = value; RaisePropertyChangedEvent("report_totals_filter_ToDate");
                    // Re-calculate target data collections for Totals report
                    GetTargetCollections_ReportTotals();
                } }
        }


        // Totals report:  Summary
        public ObservableCollection<CustomerReport> report_totals_targCustomerColl
        {
            get { return _report_totals_targCustomerColl; }
            set { if (value != _report_totals_targCustomerColl) { _report_totals_targCustomerColl = value; RaisePropertyChangedEvent("report_totals_targCustomerColl"); } }
        }

        public ObservableCollection<ProjectReport> report_totals_targProjectColl
        {
            get { return _report_totals_targProjectColl; }
            set { if (value != _report_totals_targProjectColl) { _report_totals_targProjectColl = value; RaisePropertyChangedEvent("report_totals_targProjectColl"); } }
        }

        public ObservableCollection<TaskEntryReport> report_totals_targTaskEntryColl
        {
            get { return _report_totals_targTaskEntryColl; }
            set { if (value != _report_totals_targTaskEntryColl) { _report_totals_targTaskEntryColl = value; RaisePropertyChangedEvent("report_totals_targTaskEntryColl"); } }
        }

        public ObservableCollection<TimeEntry> report_totals_targTimeEntryColl
        {
            get { return _report_totals_targTimeEntryColl; }
            set { if (value != _report_totals_targTimeEntryColl) { _report_totals_targTimeEntryColl = value; RaisePropertyChangedEvent("report_totals_targTimeEntryColl"); } }
        }

        public MODELS.TimeSpanHMS report_totals_targTimeSpan
        {
            get { return _report_totals_targTimeSpan; }
            set { if (value != _report_totals_targTimeSpan) { _report_totals_targTimeSpan = value; RaisePropertyChangedEvent("report_totals_targTimeSpan"); } }
        }
        #endregion

        #endregion

        // ---------------------------
        // Public command declarations
        // ---------------------------
        #region COMMANDS
        public ICommand AddRecordedTimeEntry { get { return _AddRecordedTimeEntry; } }
        public ICommand RecordReset { get { return _RecordReset; } }

        public ICommand AddCustomer { get { return _AddCustomer; } }
        public ICommand EditCustomer { get { return _EditCustomer; } }
        public ICommand DeleteCustomer { get { return _DeleteCustomer; } }
        public ICommand AddProject { get { return _AddProject; } }
        public ICommand EditProject { get { return _EditProject; } }
        public ICommand DeleteProject { get { return _DeleteProject; } }
        public ICommand AddTaskEntry { get { return _AddTaskEntry; } }
        public ICommand EditTaskEntry { get { return _EditTaskEntry; } }
        public ICommand DeleteTaskEntry { get { return _DeleteTaskEntry; } }
        public ICommand AddTimeEntry { get { return _AddTimeEntry; } }
        public ICommand EditTimeEntry { get { return _EditTimeEntry; } }
        public ICommand DeleteTimeEntry { get { return _DeleteTimeEntry; } }


        public ICommand ReportTotals_ClearFilters { get { return _ReportTotals_ClearFilters; } }


        public ICommand CloseLoadingScreen { get { return _CloseLoadingScreen; } }


        public ICommand ApplyBaseTheme { get { return _ApplyBaseTheme; } }

        public ICommand LogIn { get { return _LogIn; } }
        public ICommand LogOut { get { return _LogOut; } }
        #endregion


        //  -----------------
        //  Private functions
        //  -----------------
        private async Task LoadDatabaseData()
        {
            LoadingScreen_Visibility = Visibility.Visible;

            // Get all data from database
            allCustomers_fromDB = await myBackendService.GetCustomers(myUser);
            allProjects_fromDB = await myBackendService.GetProjects(myUser);
            allTaskEntries_fromDB = await myBackendService.GetTaskEntries(myUser);
            allTimeEntries_fromDB = await myBackendService.GetTimeEntries(myUser);

            FullyPopulateObservableCollections();

            GetTargetCollections_ReportTotals();
            ToggleLoadingScreen_Visibility();
        }

        // Parse Database Data
        #region Parse DB data
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

            if (allProjects_fromDB != null && allProjects_fromDB.Count > 0)
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

            allProjects = PopulateProjectsRelatedCustomerObjects(allProjects);
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

            allTaskEntries = PopulateTaskEntriesRelatedProjectObjects(allTaskEntries);
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

            allTimeEntries = PopulateTimeEntriesRelatedTaskEntryObjects(allTimeEntries);
            db_shownTimeEntries = allTimeEntries;


            //ToggleLoadingScreen_Visibility();
        }
        #endregion

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

                        if(selCustomerindex > -1)
                        {
                            foreach (Project project in allProjects)
                            {
                                if (project.CustomerId == selCustomer.Id)
                                {
                                    availProjects.Add(project);
                                }
                            }
                        }

                        selProjectindex = -1;

                        availTaskEntries.Clear();
                        selTaskEntryindex = -1;
                        break;

                    case dataCategory.Project:
                        availTaskEntries.Clear();

                        if (selProject != null)
                        {
                            foreach (TaskEntry taskentry in allTaskEntries)
                            {
                                if (taskentry.ProjectId == selProject.Id)
                                {
                                    availTaskEntries.Add(taskentry);
                                }
                            }
                        }

                        selTaskEntryindex = -1;
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

        private void UpdateRecordComponent()
        {
            selCustomerindex = -1;
        }

        private void UpdateConfigurationComponent()
        {
            db_shownCustomers = allCustomers;
            db_shownProjects = PopulateProjectsRelatedCustomerObjects(allProjects);
            db_shownTaskEntries = PopulateTaskEntriesRelatedProjectObjects(allTaskEntries);
            db_shownTimeEntries = PopulateTimeEntriesRelatedTaskEntryObjects(allTimeEntries);

            GetTargetCollections_ReportTotals();
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
                        //project_edit_selCustindex = allCustomers.IndexOf(allCustomers.Single(x => x.Id == config_project_selProject.CustomerId));
                        if (config_project_selProject.CustomerId > -1)
                        {
                            project_edit_selCustindex = allCustomers.IndexOf(allCustomers.Single(x => x.Id == config_project_selProject.CustomerId));
                        }
                        else
                        {
                            project_edit_selCustindex = -1;
                        }
                        break;

                    case dataCategory.TaskEntry:
                        taskentry_edit_Name = config_taskentry_selTaskEntry.Name;
                        taskentry_edit_Description = config_taskentry_selTaskEntry.Description;
                        taskentry_edit_selCustindex = allCustomers.IndexOf(allCustomers.Single(y => y.Id == (allProjects.Single(x => x.Id == config_taskentry_selTaskEntry.ProjectId)).CustomerId));
                        taskentry_edit_selProjindex = taskentry_edit_availProjects.IndexOf(config_taskentry_selTaskEntry.Project);
                        if (config_taskentry_selTaskEntry.Status == "Active")
                        {
                            taskentry_edit_Status_IsActive = true;
                        }
                        else
                        {
                            taskentry_edit_Status_IsInactive = true;
                        }
                        break;

                    case dataCategory.TimeEntry:
                        TaskEntry Task_linkedtoselTimeEntry = allTaskEntries.Single(x => x.Id == config_timeentry_selTimeEntry.TaskEntryId);
                        Project Project_linkedtoselTimeEntry = allProjects.Single(x => x.Id == Task_linkedtoselTimeEntry.ProjectId);
                        Customer Customer_linkedtoselTimeEntry = allCustomers.Single(x => x.Id == Project_linkedtoselTimeEntry.CustomerId);
                        timeentry_edit_selCustindex = allCustomers.IndexOf(Customer_linkedtoselTimeEntry);
                        timeentry_edit_selProjindex = timeentry_edit_availProjects.IndexOf(Project_linkedtoselTimeEntry);
                        timeentry_edit_selTaskEntryindex = timeentry_edit_availTaskEntries.IndexOf(Task_linkedtoselTimeEntry);

                        timeentry_edit_DateTimeStart = config_timeentry_selTimeEntry.Start;
                        timeentry_edit_TimeStart = config_timeentry_selTimeEntry.Start;
                        timeentry_edit_DateTimeStop = config_timeentry_selTimeEntry.Stop;
                        timeentry_edit_TimeStop = config_timeentry_selTimeEntry.Stop;
                        timeentry_edit_Duration = config_timeentry_selTimeEntry.Duration;
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

        private ObservableCollection<MODELS.Project> PopulateProjectsRelatedCustomerObjects(ObservableCollection<MODELS.Project> inpProjects)
        {
            ObservableCollection<MODELS.Project> popProjects = new ObservableCollection<MODELS.Project>();

            try
            {
                foreach (MODELS.Project project in inpProjects)
                {
                    project.Customer = allCustomers.Single(x => x.Id == project.CustomerId);
                    popProjects.Add(project);
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR while trying to PopulateProjectsRelatedCustomerObjects: " + Environment.NewLine +
                    e.ToString());
            }

            return popProjects;
        }

        private ObservableCollection<MODELS.TaskEntry> PopulateTaskEntriesRelatedProjectObjects(ObservableCollection<MODELS.TaskEntry> inpTaskEntries)
        {
            ObservableCollection<MODELS.TaskEntry> popTaskEntries = new ObservableCollection<MODELS.TaskEntry>();

            try
            {
                foreach (MODELS.TaskEntry taskEntry in inpTaskEntries)
                {
                    taskEntry.Project = allProjects.Single(x => x.Id == taskEntry.ProjectId);
                    popTaskEntries.Add(taskEntry);
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR while trying to PopulateTaskEntriesRelatedProjectObjects: " + Environment.NewLine +
                    e.ToString());
            }

            return popTaskEntries;
        }

        private ObservableCollection<TimeEntry> PopulateTimeEntriesRelatedTaskEntryObjects(ObservableCollection<TimeEntry> inpTimeEntries)
        {
            ObservableCollection<TimeEntry> popTimeEntries = new ObservableCollection<TimeEntry>();

            try
            {
                foreach (TimeEntry timeEntry in inpTimeEntries)
                {
                    timeEntry.TaskEntry = allTaskEntries.Single(x => x.Id == timeEntry.TaskEntryId);
                    popTimeEntries.Add(timeEntry);
                }
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR while trying to PopulateTimeEntriesRelatedTaskEntryObjects: " + Environment.NewLine +
                    e.ToString());
            }

            return popTimeEntries;
        }

        private void FullyPopulateObservableCollections()
        {
            // TaskEntries
            ObservableCollection<TaskEntry> populatedTaskEntries = new ObservableCollection<TaskEntry>();

            foreach (TaskEntry taskEntry in allTaskEntries)
            {
                taskEntry.TimeEntries = allTimeEntries.Where(x => x.TaskEntryId == taskEntry.Id).ToList();
                populatedTaskEntries.Add(taskEntry);
            }

            allTaskEntries = populatedTaskEntries;


            // Projects
            ObservableCollection<Project> populatedProjects = new ObservableCollection<Project>();

            foreach (Project project in allProjects)
            {
                project.TaskEntries = allTaskEntries.Where(x => x.ProjectId == project.Id).ToList();
                populatedProjects.Add(project);
            }

            allProjects = populatedProjects;


            // Customers
            ObservableCollection<Customer> populatedCustomers= new ObservableCollection<Customer>();

            foreach (Customer customer in allCustomers)
            {
                customer.Projects = allProjects.Where(x => x.CustomerId == customer.Id).ToList();
                populatedCustomers.Add(customer);
            }

            allCustomers = populatedCustomers;
        }
        
        private void CalculateDuration(string requestor)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);

            switch (requestor.ToUpper())
            {
                case "TIMEENTRY_ADDEDIT":

                    if (timeentry_addedit_DateTimeStop.Ticks > timeentry_addedit_DateTimeStart.Ticks)
                    {
                        duration = timeentry_addedit_DateTimeStop.Subtract(timeentry_addedit_DateTimeStart);
                    }
                    timeentry_addedit_Duration = new TimeSpan(duration.Days, duration.Hours, duration.Minutes, duration.Seconds);
                    break;

                case "TIMEENTRY_EDIT":
                    if (timeentry_edit_DateTimeStop.Ticks > timeentry_edit_DateTimeStart.Ticks)
                    {
                        duration = timeentry_edit_DateTimeStop.Subtract(timeentry_edit_DateTimeStart);
                    }
                    timeentry_edit_Duration = new TimeSpan(duration.Days, duration.Hours, duration.Minutes, duration.Seconds);
                    break;

                case "RECORD":
                    if (record_StopTime.Ticks > record_StartTime.Ticks)
                    {
                        duration = record_StopTime.Subtract(record_StartTime);
                    }
                    record_Duration = new TimeSpan(duration.Days, duration.Hours, duration.Minutes, duration.Seconds);
                    break;

                default:
                    break;
            }
        }

        private void Reset(string requestor)
        {
            switch (requestor)
            {
                case "RECORD_TIMEENTRY":
                    record_StartTime = DateTime.MinValue;
                    record_StopTime = DateTime.MinValue;
                    break;

                default:
                    break;
            }
        }

        private void GetTargetCollections_ReportTotals()
        {
            try
            {
                Logger.Write(Environment.NewLine +
                    "--- GET TARGET DATA COLLECTIONS FOR REPORT:  TOTALS ---");

                // Get target Time Entries
                // -----------------------
                // Clear current selection
                if (report_totals_targTimeEntryColl != null) { report_totals_targTimeEntryColl.Clear(); }
                List<TimeEntry> targTimeEntries = new List<TimeEntry>();

                // Assign filters
                if (report_totals_selCustomerIndex > -1)
                {
                    if (report_totals_selProjectIndex > -1)
                    {
                        if (report_totals_selTaskEntryIndex > -1)
                        {
                            targTimeEntries = allTimeEntries
                                .Where(x => (x.Date.Date >= report_totals_filter_FromDate.Date && x.Date.Date <= report_totals_filter_ToDate.Date))
                                .Where(y => y.TaskEntryId == report_totals_selTaskEntry.Id).ToList();
                        }
                        else
                        {
                            targTimeEntries = allTimeEntries
                            .Where(x => (x.Date.Date >= report_totals_filter_FromDate.Date && x.Date.Date <= report_totals_filter_ToDate.Date))
                            .Where(z => z.TaskEntry.ProjectId == report_totals_selProject.Id).ToList();
                        }
                    }
                    else
                    {
                        targTimeEntries = allTimeEntries
                            .Where(x => (x.Date.Date >= report_totals_filter_FromDate.Date && x.Date.Date <= report_totals_filter_ToDate.Date))
                            .Where(a => a.TaskEntry.Project.CustomerId == report_totals_selCustomer.Id).ToList();
                    }
                }
                else
                {
                    targTimeEntries = allTimeEntries
                        .Where(x => (x.Date.Date >= report_totals_filter_FromDate.Date && x.Date.Date <= report_totals_filter_ToDate.Date)).ToList();
                }

                //targTimeEntries = allTimeEntries
                //    .Where(x => (x.Date.Ticks >= report_totals_filter_FromDate.Ticks && x.Date.Ticks <= report_totals_filter_ToDate.Ticks))
                //    .Where(y => y.TaskEntryId == report_totals_selTaskEntry.Id)
                //    .Where(z => z.TaskEntry.ProjectId == report_totals_selProject.Id)
                //    .Where(a => a.TaskEntry.Project.CustomerId == report_totals_selCustomer.Id).ToList();

                Logger.Write("TIME ENTRIES:");
                foreach (TimeEntry timeEntry in targTimeEntries)
                {
                    report_totals_targTimeEntryColl.Add(timeEntry);
                    Logger.Write("   " + timeEntry.Id);
                }

                // Get target Task Entries
                // -----------------------
                if (report_totals_targTaskEntryColl != null) { report_totals_targTaskEntryColl.Clear(); }
                Logger.Write("TASK ENTRIES:");
                foreach (TaskEntry taskEntry in report_totals_targTimeEntryColl.Select(x => x.TaskEntry).GroupBy(y => y.Id).Select(g => g.First()).ToList())
                {
                    report_totals_targTaskEntryColl.Add(new TaskEntryReport(taskEntry));
                    Logger.Write("   " + taskEntry.Name);
                }

                // Get target Projects
                // -------------------
                if (report_totals_targProjectColl != null) { report_totals_targProjectColl.Clear(); }
                Logger.Write("PROJECTS:");
                foreach (Project project in report_totals_targTaskEntryColl.Select(x => x.TaskEntry.Project).GroupBy(y => y.Id).Select(g => g.First()).ToList())
                {
                    report_totals_targProjectColl.Add(new ProjectReport(project));
                    Logger.Write("   " + project.Name);
                }

                // Get target Customers
                // --------------------
                if (report_totals_targCustomerColl != null) { report_totals_targCustomerColl.Clear(); }
                Logger.Write("CUSTOMERS:");
                foreach (Customer customer in report_totals_targProjectColl.Select(x => x.Project.Customer).GroupBy(y => y.Id).Select(g => g.First()).ToList())
                {
                    report_totals_targCustomerColl.Add(new CustomerReport(customer));
                    Logger.Write("   " + customer.Name);
                }
                

                // Calculate new total TimeSpan
                // ----------------------------
                // ----------------------------
                report_totals_targTimeSpan = HelperService.CalculateTimespanHMS(report_totals_targTimeEntryColl.ToList());
                //CalculateTimespan_ReportTotals();


            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to get target data collections for Totals report: " + Environment.NewLine +
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
            _AddRecordedTimeEntry = new Base.GEN_RelayCommand(param => this.Perform_AddRecordedTimeEntry());
            _RecordReset = new Base.GEN_RelayCommand(param => this.Perform_RecordReset());

            _AddCustomer = new Base.GEN_RelayCommand(param => this.Perform_AddCustomer());
            _EditCustomer = new Base.GEN_RelayCommand(param => this.Perform_EditCustomer());
            _DeleteCustomer = new Base.GEN_RelayCommand(param => this.Perform_DeleteCustomer());

            _AddProject = new Base.GEN_RelayCommand(param => this.Perform_AddProject());
            _EditProject = new Base.GEN_RelayCommand(param => this.Perform_EditProject());
            _DeleteProject = new Base.GEN_RelayCommand(param => this.Perform_DeleteProject());

            _AddTaskEntry = new Base.GEN_RelayCommand(param => this.Perform_AddTaskEntry());
            _EditTaskEntry = new Base.GEN_RelayCommand(param => this.Perform_EditTaskEntry());
            _DeleteTaskEntry = new Base.GEN_RelayCommand(param => this.Perform_DeleteTaskEntry());

            _AddTimeEntry = new Base.GEN_RelayCommand(param => this.Perform_AddTimeEntry());
            _EditTimeEntry = new Base.GEN_RelayCommand(param => this.Perform_EditTimeEntry());
            _DeleteTimeEntry = new Base.GEN_RelayCommand(param => this.Perform_DeleteTimeEntry());

            _ReportTotals_ClearFilters = new Base.GEN_RelayCommand(param => this.Perform_ReportTotals_ClearFilters());

            _CloseLoadingScreen = new Base.GEN_RelayCommand(param => this.Perform_CloseLoadingScreen());

            _ApplyBaseTheme = new Base.GEN_RelayCommand(param => this.Perform_ApplyBaseTheme((bool)param));

            _LogIn = new Base.GEN_RelayCommand(param => this.Perform_LogIn());
            _LogOut = new Base.GEN_RelayCommand(param => this.Perform_LogOut());
        }

        private void Perform_ApplyBaseTheme(bool isDark)
        {
            ModifyTheme(theme => theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light));
        }

        private void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }


        private async void Perform_LogIn()
        {
            bool logoutSuccess = await myAuthenticator.Login();

            if (logoutSuccess)
            {
                LoginScreen_message = "";
                LoginScreen_Visibility = Visibility.Hidden;
                myUser = myAuthenticator.User;
                LoadDatabaseData();
            }
            else
            {
                LoginScreen_message = "Login process failed.";
            }
        }

        private async void Perform_LogOut()
        {
            bool logoutSuccess = await myAuthenticator.Logout();

            if (logoutSuccess)
            {
                LoginScreen_Visibility = Visibility.Visible;
                myUser = new User();
            }
            else
            {
                LoginScreen_Visibility = Visibility.Hidden;
            }
        }


        private async void Perform_AddRecordedTimeEntry()
        {
            try
            {
                // Create new Time Entry
                TimeEntry newTimeEntry = new TimeEntry()
                {
                    CreationDate = DateTime.Now,
                    Duration = record_Duration,
                    Start = record_StartTime,
                    Stop = record_StopTime,
                    TaskEntryId = selTaskEntry.Id,
                    Date = record_StartTime
                };

                // Update in database
                await myBackendService.AddTimeEntry(newTimeEntry);

                // Update in current app session
                newTimeEntry.Id = allTimeEntries.Count > 0 ? allTimeEntries.Select(x => x.Id).Max() + 1 : 1;
                allTimeEntries.Add(newTimeEntry);

                // Update UI
                Reset("RECORD_TIMEENTRY");
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Time Entry: " + Environment.NewLine +
                    e.ToString());
            }
        }
        private void Perform_RecordReset()
        {
            Reset("RECORD_TIMEENTRY");
        }

        #region CUSTOMER CRUD
        private async void Perform_AddCustomer()
        {
            try
            {
                // Create new Customer
                Customer newCustomer = new Customer()
                {
                    UserID = myUser.UserID,
                    Name = customer_addedit_Name,
                    Surname = customer_addedit_Surname,
                    Email = customer_addedit_Email,
                    CreationDate = DateTime.Now,
                    Status = "Active"
                };

                // Update in database
                await myBackendService.AddCustomer(newCustomer);

                // Update in current app session
                //newCustomer.Id = allCustomers.Count > 0 ? allCustomers.Select(x => x.Id).Max() + 1 : 1;
                newCustomer.Id = myBackendService.Customer_maxIndex + 1;
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
                    UserID = myUser.UserID,
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
                Logger.Write("!ERROR occurred while trying to start editing existing customer: " + Environment.NewLine +
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
                Logger.Write("!ERROR occurred while trying to start deleting existing customer: " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region PROJECT CRUD
        private async void Perform_AddProject()
        {
            try
            {
                // Create new Project
                Project newProject = new Project()
                {
                    UserID = myUser.UserID,
                    CustomerId = project_addedit_selCust.Id,
                    //Customer = project_addedit_selCust,
                    Name = project_addedit_Name,
                    Description = project_addedit_Description,
                    CreationDate = DateTime.Now,
                    Status = "Active"
                };

                //Logger.Write("PERFORM_ADDPROJECT: " + Environment.NewLine +
                //    "CustomerId    = " + newProject.CustomerId.ToString() + Environment.NewLine +
                //    //"Customer      = " + newProject.Customer.Name + Environment.NewLine +
                //    "Name          = " + newProject.Name + Environment.NewLine +
                //    "Description   = " + newProject.Description + Environment.NewLine +
                //    "CreationDate  = " + newProject.CreationDate.ToString() + Environment.NewLine +
                //    "Status        = " + newProject.Status);

                // Update in database
                await myBackendService.AddProject(newProject);

                // Update in current app session
                newProject.Id = myBackendService.Project_maxIndex + 1;
                //newProject.Id = allProjects.Count > 0 ? allProjects.Select(x => x.Id).Max() + 1 : 1;
                allProjects.Add(newProject);

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

        private async void Perform_EditProject()
        {
            try
            {
                // Get modified Project
                Project modProject = config_project_selProject;

                modProject.Name = project_edit_Name;
                modProject.Description = project_edit_Description;
                modProject.CustomerId = project_edit_selCust.Id;

                // Update in database
                await myBackendService.EditProject(modProject);

                // Update in current app session
                allProjects[allProjects.IndexOf(allProjects.Single(x => x.Id == config_project_selProject.Id))] = modProject;


                // Update UI
                project_edit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();

                Logger.Write("Perform_EditProject -  Project edited");
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start editing existing project: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_DeleteProject()
        {
            try
            {
                // Delete in database
                await myBackendService.DeleteProject(config_project_selProject.Id);

                // Delete in current session
                allProjects.Remove(config_project_selProject);

                // Update UI
                config_project_selindex = -1;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start delete existing project: " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region TASK ENTRY CRUD
        private async void Perform_AddTaskEntry()
        {
            try
            {
                // Create new Task Entry
                TaskEntry newTaskEntry = new TaskEntry()
                {
                    UserID = myUser.UserID,
                    ProjectId = taskentry_addedit_selProj.Id,
                    Name = taskentry_addedit_Name,
                    Description = taskentry_addedit_Description,
                    CreationDate = DateTime.Now,
                    Status = taskentry_addedit_Status
                };

                // Update in database
                await myBackendService.AddTaskEntry(newTaskEntry);

                // Update in current app session
                newTaskEntry.Id = myBackendService.TaskEntry_maxIndex + 1; ;
                //newTaskEntry.Id = allTaskEntries.Count > 0 ? allTaskEntries.Select(x => x.Id).Max() + 1 : 1;
                allTaskEntries.Add(newTaskEntry);

                // Update UI
                taskentry_addedit_selCustindex = -1;
                taskentry_addedit_Name = "";
                taskentry_addedit_Description = "";
                taskentry_addedit_Status_IsActive = true;
                taskentry_addedit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Task Entry: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_EditTaskEntry()
        {
            try
            {
                // Get modified Task Entry
                TaskEntry modTaskEntry = config_taskentry_selTaskEntry;

                modTaskEntry.Name = taskentry_edit_Name;
                modTaskEntry.Description = taskentry_edit_Description;
                modTaskEntry.ProjectId = taskentry_edit_selProj.Id;
                modTaskEntry.Project = null;
                modTaskEntry.Status = taskentry_edit_Status;

                // Update in database
                await myBackendService.EditTaskEntry(modTaskEntry);

                // Update in current app session
                allTaskEntries[allTaskEntries.IndexOf(allTaskEntries.Single(x => x.Id == modTaskEntry.Id))] = modTaskEntry;

                // Update UI
                taskentry_edit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();

                Logger.Write("Perform_EditTaskEntry -  Task Entry edited");
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start editing existing task entry: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_DeleteTaskEntry()
        {
            try
            {
                // Delete in database
                await myBackendService.DeleteTaskEntry(config_taskentry_selTaskEntry.Id);

                // Delete in current session
                allTaskEntries.Remove(config_taskentry_selTaskEntry);

                // Update UI
                config_taskentry_selindex = -1;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start delete existing task entry: " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region TIME ENTRY CRUD
        private async void Perform_AddTimeEntry()
        {
            try
            {
                // Create new Time Entry
                TimeEntry newTimeEntry = new TimeEntry()
                {
                    UserID = myUser.UserID,
                    CreationDate = DateTime.Now,
                    Duration = timeentry_addedit_Duration,
                    Start = timeentry_addedit_DateTimeStart,
                    Stop = timeentry_addedit_DateTimeStop,
                    TaskEntryId = timeentry_addedit_selTaskEntry.Id,
                    Date = timeentry_addedit_DateTimeStart
                };

                // Update in database
                await myBackendService.AddTimeEntry(newTimeEntry);

                // Update in current app session
                newTimeEntry.Id = myBackendService.TimeEntry_maxIndex + 1;
                //newTimeEntry.Id = allTimeEntries.Count > 0 ? allTimeEntries.Select(x => x.Id).Max() + 1 : 1;
                allTimeEntries.Add(newTimeEntry);

                // Update UI
                timeentry_addedit_selCustindex = -1;
                timeentry_addedit_selProjindex = -1;
                timeentry_addedit_selTaskEntryindex = -1;
                timeentry_addedit_DateTimeStart = DateTime.Now;
                timeentry_addedit_DateTimeStop = DateTime.Now;
                timeentry_addedit_TimeStart = DateTime.Now;
                timeentry_addedit_TimeStop = DateTime.Now;
                timeentry_addedit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start adding new Time Entry: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_EditTimeEntry()
        {
            try
            {
                // Get modified Time Entry
                TimeEntry modTimeEntry = config_timeentry_selTimeEntry;

                modTimeEntry.TaskEntryId = timeentry_edit_selTaskEntry.Id;
                modTimeEntry.Start = timeentry_edit_DateTimeStart;
                modTimeEntry.Stop = timeentry_edit_DateTimeStop;
                modTimeEntry.Date = timeentry_edit_DateTimeStart;
                modTimeEntry.Duration = timeentry_edit_Duration;

                // Update in database
                await myBackendService.EditTimeEntry(modTimeEntry);

                // Update in current app session
                allTimeEntries[allTimeEntries.IndexOf(allTimeEntries.Single(x => x.Id == modTimeEntry.Id))] = modTimeEntry;

                // Update UI
                timeentry_edit_Visibility = Visibility.Hidden;
                UpdateConfigurationComponent();

                Logger.Write("Perform_EditTaskEntry -  Time Entry edited");
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start editing existing time entry: " + Environment.NewLine +
                    e.ToString());
            }
        }

        private async void Perform_DeleteTimeEntry()
        {
            try
            {
                // Delete in database
                await myBackendService.DeleteTimeEntry(config_timeentry_selTimeEntry.Id);

                // Delete in current session
                allTimeEntries.Remove(config_timeentry_selTimeEntry);

                // Update UI
                config_timeentry_selindex = -1;
                UpdateConfigurationComponent();
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to start delete existing time entry: " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        #region REPORTS - Totals
        private void Perform_ReportTotals_ClearFilters()
        {
            try
            {
                report_totals_selCustomerIndex = -1;
                report_totals_selProjectIndex = -1;
                report_totals_selTaskEntryIndex = -1;
                report_totals_filter_FromDate = DateTime.MinValue;
                report_totals_filter_ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 999);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to clear Filters of Totals report: " + Environment.NewLine +
                    e.ToString());
            }
        }
        #endregion

        private void Perform_CloseLoadingScreen()
        {
            ToggleLoadingScreen_Visibility();
        }
    }
}
