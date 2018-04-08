using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes
{
    public class FlightTrackingValidation
    {
        public static bool MonitoredFlightData(List<string> data)
        {
            return MonitoredC
                   && Int32.Parse(data[2]) <= 90000
                   && Int32.Parse(data[2]) >= 10000
                   && Int16.Parse(data[3]) >= 500
                   && Int16.Parse(data[3]) <= 20000;
        }

        public bool MonitoredCoordinates(string x, string y)
        {
            return Int32.Parse(x) <= 90000 && Int32.Parse(y) >= 10000;
        }
    }
}
