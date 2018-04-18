﻿using System;
using System.Collections.Generic;
using System.Globalization;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class TimestampFormatter : ITimestampFormatter
    {
        public DateTime InDateTime { get; set; }
        public string InPretty { get; set; }
        public string Unformatted { get; set; }

        public void FormatTimestamp(string format = "yyyyMMddHHmmssfff")
        {
            if (Unformatted == null)
                throw new Exception("Exception trhwon in FormatTimestamp: Unformatted is null");

            InDateTime = DateTime.ParseExact(Unformatted, format, CultureInfo.CreateSpecificCulture("en-US"));
            
            InPretty = String.Format(new CultureInfo("en-US"),
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
