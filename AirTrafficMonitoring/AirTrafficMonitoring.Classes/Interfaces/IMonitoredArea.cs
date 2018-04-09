using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    interface IMonitoredArea
    {
        bool InsideMonitoredArea(Position position);
        bool InsideMonitoredCoordinates(string x, string y);
        bool InsideMonitoredXCoor(string x);
        bool InsideMonitoredYCoor(string y);
        bool InsideMonitoredAltitude(string alt);
    }
}
