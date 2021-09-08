using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TimesheetPlayground.UI.Models.Enum;
using TimesheetPlayground.UI.Models.DTO;
using System.Globalization;

namespace TimesheetPlayground.UI.Common
{
    public static class WorkDayHandler
    {
        private class WeekDay
        {
            public int WeekNumber { get; set; }

            public DateTime Date { get; set; }
        }

        public static List<DateTime> GetWorkDaysOfMonth(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            List<DateTime> resultList = new List<DateTime>();

            for(DateTime i = firstDay; i <= lastDay; i = i.AddDays(1))
            {
                if(i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                {
                    resultList.Add(i);
                }
            }

            return resultList;
        }

        public static List<WorkedHourDO> GetWorkedHourDOsOfMonth(int timesheetId, List<int> allProjectIds, DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            List<WorkedHourDO> resultList = new List<WorkedHourDO>();

            foreach (var projectId in allProjectIds)
            {
                for (DateTime i = firstDay; i <= lastDay; i = i.AddDays(1))
                {
                    resultList.Add(new WorkedHourDO()
                    {
                        TimesheetId = timesheetId,
                        ProjectId = projectId,
                        Hours = 0,
                        WorkDay = i.Day,
                        IsWeekend = i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday ? true : false,
                    });
                }
            }    

            return resultList.OrderBy(w => w.ProjectId).ToList();
        }

        public static List<WorkedHourDO> GetWorkedHourDOsOfWeek(int timesheetId, List<int> allProjectIds, DateTime date, int weekNumber)
        {
            var weekDays = GetWeekdaysOfMonth(date);

            List<WorkedHourDO> resultList = new List<WorkedHourDO>();
            var firstWeek = weekDays.First().WeekNumber;

            foreach (var projectId in allProjectIds)
            {
                foreach (var weekday in weekDays.Where(w => w.WeekNumber == (firstWeek + weekNumber - 1)).ToList())
                {
                    resultList.Add(new WorkedHourDO()
                    {
                        TimesheetId = timesheetId,
                        ProjectId = projectId,
                        Hours = 0,
                        WorkDay = weekday.Date.Day,
                        IsWeekend = weekday.Date.DayOfWeek == DayOfWeek.Saturday || weekday.Date.DayOfWeek == DayOfWeek.Sunday ? true : false,
                    });
                }
            }

            return resultList.OrderBy(w => w.ProjectId).ToList();
        }

        public static int GetWeekNumberOfMonth(DateTime date)
        {
            var weekDays = GetWeekdaysOfMonth(date);
            var firstWeekNumber = weekDays.First().WeekNumber;
            var lastWeekNumber = weekDays.Last().WeekNumber;

            return lastWeekNumber - firstWeekNumber + 1;
        }

        private static List<WeekDay> GetWeekdaysOfMonth(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            var weekDays = new List<WeekDay>();

            for (DateTime i = firstDay; i <= lastDay; i = i.AddDays(1))
            {
                weekDays.Add(new WeekDay
                {
                    WeekNumber = CultureInfo.CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(i, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday),
                    Date = i,
                });
            }

            return weekDays;
        }
    }
}
