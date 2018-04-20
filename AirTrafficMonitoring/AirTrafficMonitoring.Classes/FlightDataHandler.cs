using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class FlightDataHandler : IFlightDataHandler
    {
        public void Distribute(List<string> data, out string tag, ref IPosition pos, ref ITimestampFormatter formatter)
        {
            tag = data[0];
            var x = int.Parse(data[1]);
            var y = int.Parse(data[2]);
            var al = int.Parse(data[3]);
            var timestamp = data[4];

            pos.SetPosition(x, y, al);
            formatter.Unformatted = timestamp;

        }
    }
}
