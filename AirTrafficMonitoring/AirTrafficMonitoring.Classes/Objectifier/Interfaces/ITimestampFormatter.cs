using System;

namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface ITimestampFormatter
    {
        DateTime InDateTime { get; set; }
        string InPretty { get; set; }
        string Unformatted { get; set; }
        void FormatTimestamp(string format = "yyyyMMddHHmmssfff");
    }
}
