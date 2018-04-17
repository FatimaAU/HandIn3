using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface ITimestampFormatter
    {
        DateTime InDateTime { get; set; }
        string InFormatted { get; set; }
        void FormatTimestamp(string time, string format = "yyyyMMddHHmmssfff");
    }
}
