using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TIMEFRAME_windows.MODELS;

namespace TIMEFRAME_windows.SERVICES
{
    public static class HelperService
    {
        // METHODS
        public static List<T> ConvertToList<T>(ObservableCollection<T> myObservableColl)
        {
            List<T> myList = new List<T>();

            foreach (T item in myObservableColl)
            {
                myList.Add(item);
            }

            return myList;
        }

        /// <summary>
        /// Calculates total time recorded in HMS (Hours, Minutes, Seconds) format, for given input object(s)
        /// </summary>
        /// <param name="timeEntries"></param>
        /// <returns></returns>
        public static TimeSpanHMS CalculateTimespanHMS(List<TimeEntry> timeEntries)
        {
            // Initialize
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            TimeSpanHMS timeSpanHMS = new TimeSpanHMS();

            try
            {
                // Calculate sum of all target Time Entries
                foreach (TimeEntry timeEntry in timeEntries)
                {
                    timeSpan = timeSpan + timeEntry.Duration;
                }

                // Assign resulting TimeSpan data to correct object
                timeSpanHMS = new TimeSpanHMS(timeSpan);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to CalculateTimespanHMS: " + Environment.NewLine +
                    e.ToString());
            }

            return timeSpanHMS;
        }

        public static TimeSpanHMS CalculateTimespanHMS(TaskEntry taskEntry)
        {
            // Initialize
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            TimeSpanHMS timeSpanHMS = new TimeSpanHMS();

            try
            {
                // Calculate sum of all target Time Entries
                foreach (TimeEntry timeEntry in taskEntry.TimeEntries)
                {
                    timeSpan = timeSpan + timeEntry.Duration;
                }

                // Assign resulting TimeSpan data to correct object
                timeSpanHMS = new TimeSpanHMS(timeSpan);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to CalculateTimespanHMS: " + Environment.NewLine +
                    e.ToString());
            }

            return timeSpanHMS;
        }

        public static TimeSpanHMS CalculateTimespanHMS(Project project)
        {
            // Initialize
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            TimeSpanHMS timeSpanHMS = new TimeSpanHMS();

            try
            {
                foreach (TaskEntry taskEntry in project.TaskEntries)
                {
                    // Calculate sum of all target Time Entries
                    foreach (TimeEntry timeEntry in taskEntry.TimeEntries)
                    {
                        timeSpan = timeSpan + timeEntry.Duration;
                    }
                }

                // Assign resulting TimeSpan data to correct object
                timeSpanHMS = new TimeSpanHMS(timeSpan);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to CalculateTimespanHMS: " + Environment.NewLine +
                    e.ToString());
            }

            return timeSpanHMS;
        }

        public static TimeSpanHMS CalculateTimespanHMS(Customer customer)
        {
            // Initialize
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            TimeSpanHMS timeSpanHMS = new TimeSpanHMS();

            try
            {
                foreach (Project project in customer.Projects)
                {
                    foreach (TaskEntry taskEntry in project.TaskEntries)
                    {
                        // Calculate sum of all target Time Entries
                        foreach (TimeEntry timeEntry in taskEntry.TimeEntries)
                        {
                            timeSpan = timeSpan + timeEntry.Duration;
                        }
                    }
                }

                // Assign resulting TimeSpan data to correct object
                timeSpanHMS = new TimeSpanHMS(timeSpan);
            }
            catch (Exception e)
            {
                Logger.Write("!ERROR occurred while trying to CalculateTimespanHMS: " + Environment.NewLine +
                    e.ToString());
            }

            return timeSpanHMS;
        }
    }
}
