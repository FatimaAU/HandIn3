using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class MonitoredArea : IMonitoredArea
    {
        private readonly int _coordinateHigher;
        private readonly int _coordinateLower;
        private readonly int _altitudeHigher;
        private readonly int _altitudeLower;

        public MonitoredArea(int cH, int cL, int aH, int aL)
        {
            _coordinateHigher = cH;
            _coordinateLower = cL;
            _altitudeHigher = aH;
            _altitudeLower = aL;
        }

        public bool InsideMonitoredArea(IPosition position)
        {
            return InsideMonitoredCoordinates(int.Parse(position.XCoor), int.Parse(position.YCoor))
                   && InsideMonitoredAltitude(int.Parse(position.Altitude));
        }

        private bool InsideMonitoredCoordinates(int x, int y)
        {
            return InsideMonitoredXCoor(x) && InsideMonitoredYCoor(y);
        }

        private bool InsideMonitoredXCoor(int x)
        {
            return x <= _coordinateHigher 
                   && x >= _coordinateLower;
        }

        private bool InsideMonitoredYCoor(int y)
        {
            return y <= _coordinateHigher
                   && y >= _coordinateLower;
        }

        private bool InsideMonitoredAltitude(int alt)
        {
            return alt >= _altitudeLower
                   && alt <= _altitudeHigher;
        }

    }
}
