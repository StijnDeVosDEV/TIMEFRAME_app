using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TIMEFRAME_windows.VIEWS
{
    /// <summary>
    /// Interaction logic for V_Main.xaml
    /// </summary>
    public partial class V_Main : Window
    {
        // Local variables
        private static BrushConverter bc = new BrushConverter();
        private Brush highlightColor_SecondMenu = (Brush)bc.ConvertFrom("#FF3FC1C9");
        private Brush highlightColor_Transparent = new SolidColorBrush(Colors.Transparent);

        private static double OrigHeight = 0.0;

        private static bool IsActive_Configuration = true;
        private static bool IsActive_Reports = false;
        private static bool IsActive_Settings = false;

        private static ConfigBlocks Config_ActiveState; 

        enum ConfigBlocks
        {
            Customers,
            Projects,
            Tasks,
            TimeEntries
        }

        // CONSTRUCTOR
        public V_Main()
        {
            InitializeComponent();


            // Initialize Content area
            Grid_Configuration.Visibility = Visibility.Visible;
            Grid_Reports.Visibility = Visibility.Hidden;
            Grid_Settings.Visibility = Visibility.Hidden;

            IsActive_Configuration = true;
            IsActive_Reports = false;
            IsActive_Settings = false;

            Img_Configuration.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Database_White.png"));

            Grid_ConfigCustomers.Visibility = Visibility.Visible;
            Grid_ConfigProjects.Visibility = Visibility.Hidden;
            Grid_ConfigTasks.Visibility = Visibility.Hidden;
            Grid_ConfigTimeEntries.Visibility = Visibility.Hidden;

            Config_ActiveState = ConfigBlocks.Customers;
            StackPanel_Config_Customer.Background = highlightColor_SecondMenu;


            // Initialize Configuration area


            // Initialize Settings area
            ScrollViewer_LogFile.Visibility = Visibility.Hidden;
            StackPanel_Style.Visibility = Visibility.Hidden;
        }

        private void StackPanel_Config_Customer_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_Config_Customer.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_Config_Customer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Config_ActiveState != ConfigBlocks.Customers)
            {
                StackPanel_Config_Customer.Background = highlightColor_Transparent;
            }
        }

        private void StackPanel_Config_Project_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_Config_Project.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_Config_Project_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Config_ActiveState != ConfigBlocks.Projects)
            {
                StackPanel_Config_Project.Background = highlightColor_Transparent;
            }
        }

        private void StackPanel_Config_Task_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_Config_Task.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_Config_Task_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Config_ActiveState != ConfigBlocks.Tasks)
            {
                StackPanel_Config_Task.Background = highlightColor_Transparent;
            }
        }

        private void StackPanel_Config_TimeEntry_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_Config_TimeEntry.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_Config_TimeEntry_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Config_ActiveState != ConfigBlocks.TimeEntries)
            {
                StackPanel_Config_TimeEntry.Background = highlightColor_Transparent;
            }
        }

        private void Img_Expand2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Grid_Content.Visibility == Visibility.Visible)
            {
                Grid_Content.Visibility = Visibility.Collapsed;
                Img_Expand2.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/SortArrowDOWN_Black.png"));

                OrigHeight = V_Main1.Height;
                V_Main1.Height = Grid_Core.Height + 35;
            }
            else
            {
                Grid_Content.Visibility = Visibility.Visible;
                Img_Expand2.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/SortArrowUP_Black.png"));

                V_Main1.Height = OrigHeight;
            }

            //if (Grid_Content.Height > 0)
            //{
            //    OrigHeight = Grid_Content.Height;
            //    Grid_Content.Height = 0;
                
            //    //Img_Expand2.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/SortArrowDOWN_Black.png"));
            //}
            //else
            //{
            //    Grid_Content.Height = OrigHeight;
            //    //Img_Expand2.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/SortArrowUP_Black.png"));
            //}
        }

        private void StackPanel_LogFileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_LogFileButton.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_LogFileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel_LogFileButton.Background = highlightColor_Transparent;
        }

        private void Img_Configuration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_Configuration.Visibility = Visibility.Visible;
            Grid_Reports.Visibility = Visibility.Hidden;
            Grid_Settings.Visibility = Visibility.Hidden;

            IsActive_Configuration = true;
            IsActive_Reports = false;
            IsActive_Settings = false;

            Img_Reports.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Statistics_Black.png"));
            Img_Settings.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Settings_black.PNG"));
        }

        private void Img_Reports_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_Configuration.Visibility = Visibility.Hidden;
            Grid_Reports.Visibility = Visibility.Visible;
            Grid_Settings.Visibility = Visibility.Hidden;

            IsActive_Configuration = false;
            IsActive_Reports = true;
            IsActive_Settings = false;

            Img_Configuration.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Database_Black.png"));
            Img_Settings.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Settings_black.PNG"));
        }

        private void Img_Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_Configuration.Visibility = Visibility.Hidden;
            Grid_Reports.Visibility = Visibility.Hidden;
            Grid_Settings.Visibility = Visibility.Visible;

            IsActive_Configuration = false;
            IsActive_Reports = false;
            IsActive_Settings = true;

            Img_Configuration.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Database_Black.png"));
            Img_Reports.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Statistics_Black.png"));
        }

        private void Img_Configuration_MouseEnter(object sender, MouseEventArgs e)
        {
            Img_Configuration.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Database_White.png"));
        }

        private void Img_Configuration_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsActive_Configuration)
            {
                Img_Configuration.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Database_Black.png"));
            }
        }

        private void Img_Reports_MouseEnter(object sender, MouseEventArgs e)
        {
            Img_Reports.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Statistics_White.png"));
        }

        private void Img_Reports_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsActive_Reports)
            {
                Img_Reports.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Statistics_Black.png"));
            }
        }

        private void Img_Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            Img_Settings.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Settings_adj.png"));
        }

        private void Img_Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsActive_Settings)
            {
                Img_Settings.Source = new BitmapImage(new Uri("pack://application:,,,/TIMEFRAME_windows;component/IMAGES/Settings_black.PNG"));
            }
        }

        private void StackPanel_LogFileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SERVICES.Logger.Write("Getting log content...");
            TextBlock_LogFile.Text = SERVICES.Logger.GetLogContent();

            toggleVisibility_Settings(SettingsPanels.Log);
        }

        private void StackPanel_OpenAirButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_OpenAirButton.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_OpenAirButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel_OpenAirButton.Background = highlightColor_Transparent;
        }

        private void StackPanel_TotalsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel_TotalsButton.Background = highlightColor_SecondMenu;
        }

        private void StackPanel_TotalsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel_TotalsButton.Background = highlightColor_Transparent;
        }

        private void StackPanel_Config_Customer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Config_ActiveState = ConfigBlocks.Customers;

            Grid_ConfigCustomers.Visibility = Visibility.Visible;
            Grid_ConfigProjects.Visibility = Visibility.Hidden;
            Grid_ConfigTasks.Visibility = Visibility.Hidden;
            Grid_ConfigTimeEntries.Visibility = Visibility.Hidden;

            StackPanel_Config_Project.Background = highlightColor_Transparent;
            StackPanel_Config_Task.Background = highlightColor_Transparent;
            StackPanel_Config_TimeEntry.Background = highlightColor_Transparent;
        }

        private void StackPanel_Config_Project_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Config_ActiveState = ConfigBlocks.Projects;

            Grid_ConfigCustomers.Visibility = Visibility.Hidden;
            Grid_ConfigProjects.Visibility = Visibility.Visible;
            Grid_ConfigTasks.Visibility = Visibility.Hidden;
            Grid_ConfigTimeEntries.Visibility = Visibility.Hidden;

            StackPanel_Config_Customer.Background = highlightColor_Transparent;
            StackPanel_Config_Task.Background = highlightColor_Transparent;
            StackPanel_Config_TimeEntry.Background = highlightColor_Transparent;
        }

        private void StackPanel_Config_Task_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Config_ActiveState = ConfigBlocks.Tasks;

            Grid_ConfigCustomers.Visibility = Visibility.Hidden;
            Grid_ConfigProjects.Visibility = Visibility.Hidden;
            Grid_ConfigTasks.Visibility = Visibility.Visible;
            Grid_ConfigTimeEntries.Visibility = Visibility.Hidden;

            StackPanel_Config_Customer.Background = highlightColor_Transparent;
            StackPanel_Config_Project.Background = highlightColor_Transparent;
            StackPanel_Config_TimeEntry.Background = highlightColor_Transparent;
        }

        private void StackPanel_Config_TimeEntry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Config_ActiveState = ConfigBlocks.TimeEntries;

            Grid_ConfigCustomers.Visibility = Visibility.Hidden;
            Grid_ConfigProjects.Visibility = Visibility.Hidden;
            Grid_ConfigTasks.Visibility = Visibility.Hidden;
            Grid_ConfigTimeEntries.Visibility = Visibility.Visible;

            StackPanel_Config_Customer.Background = highlightColor_Transparent;
            StackPanel_Config_Project.Background = highlightColor_Transparent;
            StackPanel_Config_Task.Background = highlightColor_Transparent;
        }



        // -----------------------
        // CONFIGURATION CUSTOMERS
        // -----------------------
        #region CONFIGURATION CUSTOMERS
        private void Img_Customers_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Customer_Add.IsOpen = !Dialog_Customer_Add.IsOpen;

            //switch (Grid_ConfigCustomers_AddEdit.Visibility)
            //{
            //    case Visibility.Visible:
            //        Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Hidden;
            //        break;
            //    case Visibility.Hidden:
            //        Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Visible;
            //        break;
            //    case Visibility.Collapsed:
            //        break;
            //    default:
            //        break;
            //}
        }

        private void Img_ConfigCustomers_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Customer_Add.IsOpen = false;

            //Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Hidden;
        }

        private void TB_Customer_AddEdit_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_CustomerConfig_AddButton();
        }
        private void TB_Customer_AddEdit_Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_CustomerConfig_AddButton();
        }

        private void TB_Customer_AddEdit_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_CustomerConfig_AddButton();
        }

        private void Img_Customers_Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Customer_Edit.IsOpen = !Dialog_Customer_Edit.IsOpen;

            //if (Grid_ConfigCustomers_Edit.Visibility == Visibility.Visible)
            //{
            //    Grid_ConfigCustomers_Edit.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Grid_ConfigCustomers_Edit.Visibility = Visibility.Visible;
            //}
        }

        private void Img_ConfigCustomers_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Customer_Edit.IsOpen = false;

            //Grid_ConfigCustomers_Edit.Visibility = Visibility.Hidden;
        }

        private void TB_Customer_Edit_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_CustomerConfig_EditButton();
        }

        private void TB_Customer_Edit_Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_CustomerConfig_EditButton();
        }

        private void TB_Customer_Edit_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_CustomerConfig_EditButton();
        }
        #endregion

        // ----------------------
        // CONFIGURATION PROJECTS
        // ----------------------
        #region CONFIGURATION PROJECTS
        private void Img_Projects_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Project_Add.IsOpen = !Dialog_Project_Add.IsOpen;

            //switch (Grid_ConfigProjects_AddEdit.Visibility)
            //{
            //    case Visibility.Visible:
            //        Grid_ConfigProjects_AddEdit.Visibility = Visibility.Hidden;
            //        break;
            //    case Visibility.Hidden:
            //        Grid_ConfigProjects_AddEdit.Visibility = Visibility.Visible;
            //        break;
            //    case Visibility.Collapsed:
            //        break;
            //    default:
            //        break;
            //}
        }

        private void Img_ConfigProjects_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Project_Add.IsOpen = false;

            //Grid_ConfigProjects_AddEdit.Visibility = Visibility.Hidden;
        }

        private void Img_Projects_Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Project_Edit.IsOpen = !Dialog_Project_Edit.IsOpen;

            //if (Grid_ConfigProjects_Edit.Visibility == Visibility.Visible)
            //{
            //    Grid_ConfigProjects_Edit.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Grid_ConfigProjects_Edit.Visibility = Visibility.Visible;
            //}
        }
        private void Img_ConfigProjects_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Project_Edit.IsOpen = false;

            //Grid_ConfigProjects_Edit.Visibility = Visibility.Hidden;
        }

        private void Combo_Project_AddEdit_AvailCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_ProjectConfig_AddButton();
        }
        private void TB_Project_AddEdit_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_ProjectConfig_AddButton();
        }
        private void TB_Project_AddEdit_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_ProjectConfig_AddButton();
        }

        private void Combo_Project_Edit_AvailCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_ProjectConfig_EditButton();
        }

        private void TB_Project_Edit_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_ProjectConfig_EditButton();
        }

        private void TB_Project_Edit_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_ProjectConfig_EditButton();
        }
        #endregion

        // ------------
        // TASK ENTRIES
        // ------------
        #region CONFIGURATION TASK ENTRIES
        private void Img_ConfigTaskEntries_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Task_Add.IsOpen = false;

            //Grid_ConfigTaskEntries_AddEdit.Visibility = Visibility.Hidden;
        }

        private void Img_ConfigTaskEntries_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Task_Edit.IsOpen = false;

            //Grid_ConfigTaskEntries_Edit.Visibility = Visibility.Hidden;
        }

        private void Img_Tasks_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Task_Add.IsOpen = !Dialog_Task_Add.IsOpen;

            Dialog_Task_Edit.IsOpen = false;

            //Grid_ConfigTaskEntries_AddEdit.Visibility = (Grid_ConfigTaskEntries_AddEdit.Visibility == Visibility.Visible)
            //    ? Visibility.Hidden
            //    : Visibility.Visible;

            //Grid_ConfigTaskEntries_Edit.Visibility = Visibility.Hidden;
        }

        private void Img_Tasks_Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_Task_Edit.IsOpen = !Dialog_Task_Edit.IsOpen;

            Dialog_Task_Add.IsOpen = false;

            //Grid_ConfigTaskEntries_Edit.Visibility = (Grid_ConfigTaskEntries_Edit.Visibility == Visibility.Visible)
            //    ? Visibility.Hidden
            //    : Visibility.Visible;

            //Grid_ConfigTaskEntries_AddEdit.Visibility = Visibility.Hidden;
        }

        private void Combo_TaksEntry_AddEdit_AvailProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Update_TaskEntryConfig_AddButton();
        }

        private void TB_TaskEntry_AddEdit_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TaskEntryConfig_AddButton();
        }

        private void TB_TaskEntry_AddEdit_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TaskEntryConfig_AddButton();
        }

        private void DataGrid_Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Img_Tasks_Edit.IsEnabled = (DataGrid_Tasks.SelectedIndex > -1) ? true : false;
            Img_Tasks_Delete.IsEnabled = (DataGrid_Tasks.SelectedIndex > -1) ? true : false;
        }

        private void Combo_TaksEntry_Edit_AvailCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TaskEntryConfig_EditButton();
        }

        private void Combo_TaskEntry_Edit_AvailProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TaskEntryConfig_EditButton();
        }

        private void TB_TaskEntry_Edit_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TaskEntryConfig_EditButton();
        }

        private void TB_TaskEntry_Edit_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TaskEntryConfig_EditButton();
        }

        private void Img_ConfigTimeEntries_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_TimeEntry_Add.IsOpen = false;

            //Grid_ConfigTimeEntries_AddEdit.Visibility = Visibility.Hidden;
        }

        private void Img_ConfigTimeEntries_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_TimeEntry_Edit.IsOpen = false;

            //Grid_ConfigTimeEntries_Edit.Visibility = Visibility.Hidden;
        }
        #endregion

        // ------------
        // TIME ENTRIES
        // ------------
        #region CONFIGURATION TIME ENTRIES
        private void TB_TimeEntry_AddEdit_StartTime_HH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_AddEdit_StartTime_MM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_AddEdit_StartTime_SS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_AddEdit_StopTime_HH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_AddEdit_StopTime_MM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_AddEdit_StopTime_SS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_Edit_StartTime_HH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_Edit_StartTime_MM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_Edit_StartTime_SS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_Edit_StopTime_HH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_Edit_StopTime_MM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void TB_TimeEntry_Edit_StopTime_SS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericValue(e.Text);
        }

        private void Img_TimeEntries_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_TimeEntry_Add.IsOpen = !Dialog_TimeEntry_Add.IsOpen;

            Dialog_TimeEntry_Edit.IsOpen = false;

            //Grid_ConfigTimeEntries_AddEdit.Visibility = (Grid_ConfigTimeEntries_AddEdit.Visibility == Visibility.Visible)
            //    ? Visibility.Hidden
            //    : Visibility.Visible;
        }

        private void Img_TimeEntries_Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialog_TimeEntry_Edit.IsOpen = !Dialog_TimeEntry_Edit.IsOpen;

            Dialog_TimeEntry_Add.IsOpen = false;

            //Grid_ConfigTimeEntries_Edit.Visibility = (Grid_ConfigTimeEntries_Edit.Visibility == Visibility.Visible)
            //    ? Visibility.Hidden
            //    : Visibility.Visible;
        }

        private void DataGrid_TimeEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Img_TimeEntries_Edit.IsEnabled = (DataGrid_TimeEntries.SelectedIndex > -1) ? true : false;
            Img_TimeEntries_Delete.IsEnabled = (DataGrid_TimeEntries.SelectedIndex > -1) ? true : false;
        }

        private void Combo_TimeEntry_AddEdit_AvailCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TimeEntryConfig_AddButton();
        }

        private void Combo_TimeEntry_AddEdit_AvailProjects_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Update_TimeEntryConfig_AddButton();
        }

        private void Combo_TimeEntry_AddEdit_AvailTaskEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TimeEntryConfig_AddButton();
        }

        private void TB_TimeEntry_AddEdit_Duration_HH_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TimeEntryConfig_AddButton();
        }

        private void TB_TimeEntry_AddEdit_Duration_MM_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TimeEntryConfig_AddButton();
        }

        private void TB_TimeEntry_AddEdit_Duration_SS_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TimeEntryConfig_AddButton();
        }

        private void Combo_TimeEntry_Edit_AvailCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TimeEntryConfig_EditButton();
        }

        private void Combo_TimeEntry_Edit_AvailProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TimeEntryConfig_EditButton();
        }

        private void Combo_TimeEntry_Edit_AvailTaskEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_TimeEntryConfig_EditButton();
        }

        private void TB_TimeEntry_Edit_Duration_HH_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TimeEntryConfig_EditButton();
        }

        private void TB_TimeEntry_Edit_Duration_MM_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TimeEntryConfig_EditButton();
        }

        private void TB_TimeEntry_Edit_Duration_SS_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_TimeEntryConfig_EditButton();
        }
        #endregion

        // -------------------------
        // CUSTOM METHODS
        // -------------------------
        private void Update_CustomerConfig_AddButton()
        {
            if (TB_Customer_AddEdit_Name.Text != "" && TB_Customer_AddEdit_Surname.Text != "" && TB_Customer_AddEdit_Email.Text != "")
            {
                Img_ConfigCustomers_AddEdit_AddorEdit.IsEnabled = true;
            }
            else
            {
                Img_ConfigCustomers_AddEdit_AddorEdit.IsEnabled = false;
            }
        }

        private void Update_CustomerConfig_EditButton()
        {
            if (TB_Customer_Edit_Name.Text != "" && TB_Customer_Edit_Surname.Text != "" && TB_Customer_Edit_Email.Text != "")
            {
                Img_ConfigCustomers_Edit_Edit.IsEnabled = true;
            }
            else
            {
                Img_ConfigCustomers_Edit_Edit.IsEnabled = false;
            }
        }

        private void Update_ProjectConfig_AddButton()
        {
            if (TB_Project_AddEdit_Name.Text != "" && TB_Project_AddEdit_Description.Text != "" && Combo_Project_AddEdit_AvailCustomers.SelectedIndex > -1)
            {
                Img_ConfigProjects_AddEdit_AddorEdit.IsEnabled = true;
            }
            else
            {
                Img_ConfigProjects_AddEdit_AddorEdit.IsEnabled = false;
            }
        }

        private void Update_ProjectConfig_EditButton()
        {
            if (TB_Project_Edit_Name.Text != "" && TB_Project_Edit_Description.Text != "" && Combo_Project_Edit_AvailCustomers.SelectedIndex > -1)
            {
                Img_ConfigProjects_Edit_Edit.IsEnabled = true;
            }
            else
            {
                Img_ConfigProjects_Edit_Edit.IsEnabled = false;
            }
        }

        private void Update_TaskEntryConfig_AddButton()
        {
            if (TB_TaskEntry_AddEdit_Name.Text != "" && TB_TaskEntry_AddEdit_Description.Text != "" && Combo_TaskEntry_AddEdit_AvailProjects.SelectedIndex > -1)
            {
                Img_ConfigTaskEntries_AddEdit_AddorEdit.IsEnabled = true;
            }
            else
            {
                Img_ConfigTaskEntries_AddEdit_AddorEdit.IsEnabled = false;
            }
        }

        private void Update_TaskEntryConfig_EditButton()
        {
            if (TB_TaskEntry_Edit_Name.Text != "" && 
                TB_TaskEntry_Edit_Description.Text != "" && 
                Combo_TaskEntry_Edit_AvailProjects.SelectedIndex > -1 && 
                Combo_TaskEntry_Edit_AvailCustomers.SelectedIndex > -1)
            {
                Img_ConfigTaskEntries_Edit_Edit.IsEnabled = true;
            }
            else
            {
                Img_ConfigTaskEntries_Edit_Edit.IsEnabled = false;
            }
        }

        private void Update_TimeEntryConfig_AddButton()
        {
            //try
            //{
            //    if (
            //        Combo_TimeEntry_AddEdit_AvailCustomers.SelectedIndex > -1 &&
            //        Combo_TimeEntry_AddEdit_AvailProjects.SelectedIndex > -1 &&
            //        Combo_TimeEntry_AddEdit_AvailTaskEntries.SelectedIndex > -1)
            //    {
            //        Img_ConfigTimeEntries_AddEdit_AddorEdit.IsEnabled = true;
            //    }
            //    else
            //    {
            //        Img_ConfigTimeEntries_AddEdit_AddorEdit.IsEnabled = false;
            //    }
            //}
            //catch (Exception)
            //{
            //    Img_ConfigTimeEntries_AddEdit_AddorEdit.IsEnabled = false;
            //}
        }

        private void Update_TimeEntryConfig_EditButton()
        {
            //try
            //{
            //    if ((Convert.ToInt32(TB_TimeEntry_Edit_Duration_DD.Text) + Convert.ToInt32(TB_TimeEntry_Edit_Duration_HH.Text) + Convert.ToInt32(TB_TimeEntry_Edit_Duration_MM.Text) + Convert.ToInt32(TB_TimeEntry_Edit_Duration_SS.Text)) != 0 &&
            //        Combo_TimeEntry_Edit_AvailCustomers.SelectedIndex > -1 &&
            //        Combo_TimeEntry_Edit_AvailProjects.SelectedIndex > -1 &&
            //        Combo_TimeEntry_Edit_AvailTaskEntries.SelectedIndex > -1)
            //    {
            //        Img_ConfigTimeEntries_Edit_Edit.IsEnabled = true;
            //    }
            //    else
            //    {
            //        Img_ConfigTimeEntries_Edit_Edit.IsEnabled = false;
            //    }
            //}
            //catch (Exception)
            //{
            //    Img_ConfigTimeEntries_Edit_Edit.IsEnabled = false;
            //}
        }


        private string TimePicker_ValidateHOUR(string hour)
        {
            try
            {
                int hour_value = Convert.ToInt32(hour);
                if (hour_value < 0 || hour_value > 23)
                {
                    return "00";
                }
                else
                {
                    return hour;
                }
            }
            catch (Exception e)
            {
                return "00";
            }
        }

        private string TimePicker_ValidateMINUTE(string minute)
        {
            try
            {
                int minute_value = Convert.ToInt32(minute);
                if (minute_value < 0 || minute_value > 59)
                {
                    return "00";
                }
                else
                {
                    return minute;
                }
            }
            catch (Exception e)
            {
                return "00";
            }
        }

        private string TimePicker_ValidateSECOND(string second)
        {
            try
            {
                int second_value = Convert.ToInt32(second);
                if (second_value < 0 || second_value > 59)
                {
                    return "00";
                }
                else
                {
                    return second;
                }
            }
            catch (Exception e)
            {
                return "00";
            }
        }

        private bool IsNumericValue(string inputText)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(inputText);
        }


        // --------------
        // SETTINGS PANEL
        // --------------
        private enum SettingsPanels
        {
            Log,
            Style
        }

        private void toggleVisibility_Settings(SettingsPanels target)
        {
            // Hide everything in Settings panel
            ScrollViewer_LogFile.Visibility = Visibility.Hidden;

            StackPanel_Style.Visibility = Visibility.Hidden;
            Border_StyleButton.BorderThickness = new Thickness(0);

            // Show only what is requested
            switch (target)
            {
                case SettingsPanels.Log:
                    ScrollViewer_LogFile.Visibility = Visibility.Visible;
                    break;
                case SettingsPanels.Style:
                    StackPanel_Style.Visibility = Visibility.Visible;
                    Border_StyleButton.BorderThickness = new Thickness(1);
                    break;
                default:
                    break;
            }
        }

        #region SETTINGS PANEL
        private void StackPanel_StyleButton_MouseEnter(object sender, MouseEventArgs e)
        {
            //Label_StyleButton.Foreground = MaterialDesignColors.SecondaryColor.
            //MaterialDesignThemes.Wpf.ColorZoneAssist.SetMode(Label_StyleButton, ColorZoneMode.Accent);
            //MaterialDesignThemes.Wpf.
        }

        private void StackPanel_StyleButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            toggleVisibility_Settings(SettingsPanels.Style);
        }
        #endregion


        // RECORD COMPONENT
        private void Img_Play_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimePicker_StartTime.SelectedTime = DateTime.Now;
        }

        private void Img_Stopo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimePicker_StopTime.SelectedTime = DateTime.Now;
        }

        private void SetEnablement_SaveButton()
        {
            if (Img_Save != null)
            {
                if (Combo_Customer.SelectedIndex > -1 &&
                Combo_Project.SelectedIndex > -1 &&
                Combo_Task.SelectedIndex > -1 &&
                ((TimeSpan)Label_Duration.Content).TotalSeconds > 0)
                {
                    Img_Save.IsEnabled = true;
                }
                else
                {
                    Img_Save.IsEnabled = false;
                }
            }
        }

        private void Combo_Customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetEnablement_SaveButton();
        }

        private void Combo_Project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetEnablement_SaveButton();
        }

        private void Combo_Task_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetEnablement_SaveButton();
        }

        private void TimePicker_StartTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            SetEnablement_SaveButton();
        }

        private void TimePicker_StopTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            SetEnablement_SaveButton();
        }

        private void TB_UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_Logout.Visibility = TB_UserName.Text != "" ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
