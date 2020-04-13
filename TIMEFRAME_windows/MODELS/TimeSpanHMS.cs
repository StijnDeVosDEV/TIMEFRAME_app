using System;
using System.Collections.Generic;
using System.Text;

namespace TIMEFRAME_windows.MODELS
{
    public class TimeSpanHMS
    {
        // FIELDS
        private int _Hours;
        private int _Minutes;
        private int _Seconds;

        // CONSTRUCTOR
        public TimeSpanHMS()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public TimeSpanHMS(TimeSpan timeSpan)
        {
            Hours = timeSpan.Hours + (24 * timeSpan.Days);
            Minutes = timeSpan.Minutes;
            Seconds = timeSpan.Seconds;
        }

        // PROPERTIES
        public int Hours { get { return _Hours; } set { _Hours = value; } }
        public int Minutes { get { return _Minutes; } set { _Minutes = value; } }
        public int Seconds { get { return _Seconds; } set { _Seconds = value; } }
    }
}
