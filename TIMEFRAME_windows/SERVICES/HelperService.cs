using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }
}
