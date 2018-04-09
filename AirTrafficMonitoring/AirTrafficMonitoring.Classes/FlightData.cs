using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class FlightDataExtractor : IFlightDataExtractor
    {
        public void ExtractFlight(List<string> data, 
            out string tag, ref IPosition pos, ref ITimestamp time)
        {
            tag = data[0];
            var x = data[1];
            var y = data[2];
            var al = data[3];
            var timeStamp = data[4];

            pos.SetPosition(x, y, al);
            time.UnformattedTimestamp = timeStamp;
        }
    }
}
