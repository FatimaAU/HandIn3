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
        IPosition Position { get; set; }
        string Timestamp { get; }
        int Course { get; set; }
        int Velocity { get; set; }

        DateTime InDateTime { get; set; }
    }
}
