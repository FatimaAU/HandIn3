using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    public class ExtractFlight
    {
        public void Flight(List<string> data, 
            out string tag, ref ExtractPosition pos, ref ExtractTimestamp time)
        {
            tag = data[0];
            string x = data[1];
            string y = data[2];
            string al = data[3];
            string timeStamp = data[4];

            pos.Position(x, y, al);
            time.Timestamp = timeStamp;
        }
    }
}
