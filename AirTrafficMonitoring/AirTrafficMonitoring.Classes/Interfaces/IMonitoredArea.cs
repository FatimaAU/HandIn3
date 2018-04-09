using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface IMonitoredArea
    {
        bool InsideMonitoredArea(IPosition position);
        //bool InsideMonitoredCoordinates(string x, string y);
        //bool InsideMonitoredXCoor(string x);
        //bool InsideMonitoredYCoor(string y);
        //bool InsideMonitoredAltitude(string alt);
    }
}
