using System;
using System.Collections.Generic;
using System.Globalization;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class TimestampFormatter : ITimestampFormatter
    {
        public DateTime InDateTime { get; set; }
        public string InFormatted { get; set; }

        public void FormatTimestamp(string time, string format = "yyyyMMddHHmmssfff")
        {
            //_format = format;
            InDateTime = DateTime.ParseExact(time, format, CultureInfo.CreateSpecificCulture("en-US"));
            
            InFormatted = String.Format(new CultureInfo("en-US"),
                "{0:MMMM d}{1}{0:, yyyy, 'at' HH:mm:ss 'and' fff 'milliseconds'}", InDateTime, GetDaySuffix(InDateTime));
        }

        private static string GetDaySuffix(DateTime dateToCheck)
        {
            return (dateToCheck.Day % 10 == 1 && dateToCheck.Day != 11) ? "st"
                : (dateToCheck.Day % 10 == 2 && dateToCheck.Day != 12) ? "nd"
                : (dateToCheck.Day % 10 == 3 && dateToCheck.Day != 13) ? "rd"
                : "th";
        }
    }
}
