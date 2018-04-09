using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    public class Flight
    {
        public void ExtractFlight(List<string> data, 
            out string tag, ref Position pos, ref Timestamp time)
        {
            tag = data[0];
            string x = data[1];
            string y = data[2];
            string al = data[3];
            string timeStamp = data[4];

            pos.SetPosition(x, y, al);
            time.UnformattedTimestamp = timeStamp;
        }
    }
}
