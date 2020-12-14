using System;
using System.Collections.Generic;

namespace WebService.Repository.Context.DatabaseSQL
{
    public partial class DateDim
    {
        public DateDim()
        {
            FMeasurement = new HashSet<FMeasurement>();
        }

        public int DateDimKey { get; set; }
        public DateTime Date { get; set; }
        public int DayOfWeek { get; set; }
        public string DayOfWeekName { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; }
        public int WeekNumber { get; set; }

        public virtual ICollection<FMeasurement> FMeasurement { get; set; }
    }
}
