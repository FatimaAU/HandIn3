using System;
using System.Collections.Generic;
using System.Globalization;

namespace AirTrafficMonitoring.Classes
{
    public class TimestampFormatter
    {
        //private string format = "yyyyMMddHHmmssfff";
        private string _format;

        public string FormatTimestamp(string time, string format = "yyyyMMddHHmmssfff")
        {
            _format = format;
            DateTime date = DateTime.ParseExact(time, _format, CultureInfo.CreateSpecificCulture("en-US"));
            return String.Format(new CultureInfo("en-US"),
                "{0:MMMM d}{1}{0:, yyyy, 'at' HH:mm:ss 'and' fff 'milliseconds'}", date, GetDaySuffix(date));
        }

        private string GetDaySuffix(DateTime dateToCheck)
        {
            return (dateToCheck.Day % 10 == 1 && dateToCheck.Day != 11) ? "st"
                : (dateToCheck.Day % 10 == 2 && dateToCheck.Day != 12) ? "nd"
                : (dateToCheck.Day % 10 == 3 && dateToCheck.Day != 13) ? "rd"
                : "th";
        }
    }
}
