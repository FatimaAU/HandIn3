using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    interface ITimestampFormatter
    {
        string FormatTimestamp(string time, string format = "yyyyMMddHHmmssfff");
        string GetDaySuffix(DateTime dateToCheck);
    }
}
