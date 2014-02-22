using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace UI.Utilities
{
    public class DateTimeUtility
    {

        //public static string ToMonthName(this DateTime dateTime)
        //{
        //    return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        //}

        //public static string ToShortMonthName(this DateTime dateTime)
        //{
        //    return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        //}

        public static List<DateTime> getdatesbetweendates(DateTime startdate, DateTime enddate)
        {
            List<DateTime> dates = new List<DateTime>();
            for (DateTime dt = startdate; dt <= enddate; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }
            return dates;
        }

        public static string ToShortMonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
        }

        public static string ToLongMonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        public static List<customweek> GetWeeks(int month,int year)
        {
            var calendar = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            var firstDayOfWeek = DayOfWeek.Monday;//System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = ;
            var weekPeriods =
            Enumerable.Range(1, calendar.GetDaysInMonth(year, month))
                      .Select(d =>
                      {
                          var date = new DateTime(year,month, d);
                          var weekNumInYear = calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, firstDayOfWeek);
                          return new { date, weekNumInYear };
                      })
                      .GroupBy(x => x.weekNumInYear)
                      .Select(x => new {DateFrom = x.First().date, To = x.Last().date })
                      .ToList();
            List<customweek> weektimeset = new List<customweek>();

            for (int i = 0; i < weekPeriods.Count;i++)
            {
                customweek tempweek = new customweek();
                tempweek.weeknumber = i;
                tempweek.startdate = weekPeriods[i].DateFrom;
                tempweek.enddate = weekPeriods[i].To;
                tempweek.year = weekPeriods[i].DateFrom.Year;
                tempweek.month = weekPeriods[i].DateFrom.Month;
                tempweek.monthname = ToShortMonthName(weekPeriods[i].DateFrom.Month);
                tempweek.dayscollection = getdatesbetweendates(weekPeriods[i].DateFrom, weekPeriods[i].To);
                tempweek.days = tempweek.dayscollection.Count;

                if (DateTime.Now.Day >= weekPeriods[i].DateFrom.Day && DateTime.Now.Day <= weekPeriods[i].To.Day)
                {
                    tempweek.isCurrentweek = true;
                }
                
                weektimeset.Add(tempweek);
            }
            return weektimeset;
        }

        
    }

    public class customweek
    {
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int days { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string monthname { get; set; }
        public int weeknumber { get; set; }
        public List<DateTime> dayscollection { get; set; }
        public bool isCurrentweek { get; set; }
    }
}