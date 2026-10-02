using System;

namespace Tm.English.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static int GetMonthDiff(this DateTime startDate, DateTime endDate)
        {
            int monthDiff = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthDiff);
        }
    }
}
