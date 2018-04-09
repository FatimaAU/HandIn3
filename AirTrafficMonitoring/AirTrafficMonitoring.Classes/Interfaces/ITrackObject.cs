using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface ITrackObject
    {
        string Tag { get; }
        string XCoordinate { get; }
        string YCoordinate { get; }
        string Altitude { get; }
        string TimeStamp { get; }
    }
}
