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
        int XCoordinate { get; }
        int YCoordinate { get; }
        int Altitude { get; }
        string Timestamp { get; }
        int Course { get; }
        int Velocity { get; set; }
    }
}
