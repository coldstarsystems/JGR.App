using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Essentials;

namespace JGRBuildingServices
{
    public class Helpers
    {
        public Boolean IsDeviceOnline()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Weeks
    {
        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        public static Int32 CountBankHolidays(DateTime startDate, DateTime endDate)
        {
            var bankHolidays = App.BankHolidaysDatabase.GetAllBankHolidays(Convert.ToString(DateTime.Now.Year)).Select(c => c.BankHolidayDate).ToList();

            var bankHolidayDays = 0;

            foreach (var day in EachDay(startDate, endDate))
            {
                if (bankHolidays.Contains(day))
                {
                    bankHolidayDays++;
                }
            }

            return bankHolidayDays;
        }

        public static Double GetBusinessDays(DateTime startDate, DateTime endDate)
        {
            Double calcBusinessDays = 1 + ((endDate - startDate).TotalDays * 5 - (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

            if (endDate.DayOfWeek == DayOfWeek.Saturday)
            {
                calcBusinessDays--;
            }

            if (startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                calcBusinessDays--;
            }

            return calcBusinessDays;
        }

        public static Int32 GetISO8601WeekOfYear(DateTime time)
        {
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);

            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}