using System;
using System.Collections.Generic;
using System.Text;
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
            Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Hidden;
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
            switch (Grid_ConfigCustomers_AddEdit.Visibility)
            {
                case Visibility.Visible:
                    Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Hidden;
                    break;
                case Visibility.Hidden:
                    Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Visible;
                    break;
                case Visibility.Collapsed:
                    break;
                default:
                    break;
            }
        }

        private void Img_ConfigCustomers_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigCustomers_AddEdit.Visibility = Visibility.Hidden;
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
            if (Grid_ConfigCustomers_Edit.Visibility == Visibility.Visible)
            {
                Grid_ConfigCustomers_Edit.Visibility = Visibility.Hidden;
            }
            else
            {
                Grid_ConfigCustomers_Edit.Visibility = Visibility.Visible;
            }
        }

        private void Img_ConfigCustomers_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigCustomers_Edit.Visibility = Visibility.Hidden;
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


        // ----------------------
        // CONFIGURATION PROJECTS
        // ----------------------
        #region CONFIGURATION PROJECTS
        private void Img_Projects_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (Grid_ConfigProjects_AddEdit.Visibility)
            {
                case Visibility.Visible:
                    Grid_ConfigProjects_AddEdit.Visibility = Visibility.Hidden;
                    break;
                case Visibility.Hidden:
                    Grid_ConfigProjects_AddEdit.Visibility = Visibility.Visible;
                    break;
                case Visibility.Collapsed:
                    break;
                default:
                    break;
            }
        }

        private void Img_ConfigProjects_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigProjects_AddEdit.Visibility = Visibility.Hidden;
        }

        private void Img_Projects_Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Grid_ConfigProjects_Edit.Visibility == Visibility.Visible)
            {
                Grid_ConfigProjects_Edit.Visibility = Visibility.Hidden;
            }
            else
            {
                Grid_ConfigProjects_Edit.Visibility = Visibility.Visible;
            }
        }
        private void Img_ConfigProjects_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigProjects_Edit.Visibility = Visibility.Hidden;
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
            if (TB_TaskEntry_AddEdit_Name.Text != "" && TB_TaskEntry_AddEdit_Description.Text != "" && Combo_TaksEntry_AddEdit_AvailProjects.SelectedIndex > -1)
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
            if (TB_TaskEntry_Edit_Name.Text != "" && TB_TaskEntry_Edit_Description.Text != "" && Combo_TaskEntry_Edit_AvailProjects.SelectedIndex > -1)
            {
                Img_ConfigTaskEntries_Edit_Edit.IsEnabled = true;
            }
            else
            {
                Img_ConfigTaskEntries_Edit_Edit.IsEnabled = false;
            }
        }

        private void Img_ConfigTaskEntries_AddEdit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigTaskEntries_AddEdit.Visibility = Visibility.Hidden;
        }

        private void Img_ConfigTaskEntries_Edit_Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigTaskEntries_Edit.Visibility = Visibility.Hidden;
        }

        private void Img_Tasks_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigTaskEntries_AddEdit.Visibility = (Grid_ConfigTaskEntries_AddEdit.Visibility == Visibility.Visible)
                ? Visibility.Hidden
                : Visibility.Visible;

            Grid_ConfigTaskEntries_Edit.Visibility = Visibility.Hidden;
        }

        private void Img_Tasks_Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_ConfigTaskEntries_Edit.Visibility = (Grid_ConfigTaskEntries_Edit.Visibility == Visibility.Visible)
                ? Visibility.Hidden
                : Visibility.Visible;

            Grid_ConfigTaskEntries_AddEdit.Visibility = Visibility.Hidden;
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
            if (DataGrid_Tasks.SelectedIndex > -1)
            {
                Img_Tasks_Edit.IsEnabled = true;
                Img_Tasks_Delete.IsEnabled = true;
            }
            else
            {
                Img_Tasks_Edit.IsEnabled = false;
                Img_Tasks_Delete.IsEnabled = false;
            }
        }
    }
}
