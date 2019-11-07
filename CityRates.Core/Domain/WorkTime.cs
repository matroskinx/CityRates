using System;
using System.Collections.Generic;
using System.Text;

namespace CityRates.Core.Domain
{
    public class WorkTime
    {
        int WorkStartInMinutes { get; set; }
        int WorkEndInMinutes { get; set; }
        string DayName { get; set; }

        bool isDayOff { get; set; }

        public WorkTime(string dayName, int start, int end)
        {
            this.DayName = dayName;
            this.WorkStartInMinutes = start;
            this.WorkEndInMinutes = end;
            this.isDayOff = false;
        }

        public WorkTime(string dayName, bool isDayOff)
        {
            this.DayName = dayName;
            this.isDayOff = isDayOff;
        }
    }
}
