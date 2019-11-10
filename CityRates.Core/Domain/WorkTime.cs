using System;
using System.Collections.Generic;
using System.Text;

namespace CityRates.Core.Domain
{
    public class WorkTime
    {
        public int WorkStartInMinutes { get; set; }
        public int WorkEndInMinutes { get; set; }
        public string DayName { get; set; }

        public bool isDayOff { get; set; }

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
