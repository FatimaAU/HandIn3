using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes
{
    public class MonitoredArea
    {
        private int _coordinateHigher;
        private int _coordinateLower;
        private int _altitudeHigher;
        private int _altitudeLower;

        public MonitoredArea(int cH, int cL, int aH, int aL)
        {
            _coordinateHigher = cH;
            _coordinateLower = cL;
            _altitudeHigher = aH;
            _altitudeLower = aL;
        }

        public bool InsideMonitoredArea(List<string> data)
        {
            return InsideMonitoredYCoor()
        }

        private bool InsideMonitoredCoordinates(string x, string y)
        {
            return InsideMonitoredXCoor(x) && InsideMonitoredYCoor(y);
        }

        private bool InsideMonitoredXCoor(string x)
        {
            return int.Parse(x) <= _coordinateHigher 
                   && int.Parse(x) >= _coordinateLower;
        }

        private bool InsideMonitoredYCoor(string y)
        {
            return int.Parse(y) <= _coordinateHigher
                   && int.Parse(y) >= _coordinateLower;
        }

        private bool InsideMonitoredAltitude(string alLower, string alHigher)
        {
            return int.Parse(alLower) >= _altitudeLower
                   && int.Parse(alHigher) <= _altitudeHigher;
        }

    }
}
