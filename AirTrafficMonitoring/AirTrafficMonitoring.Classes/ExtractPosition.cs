using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    public class ExtractPosition
    {
        public string XCoor { get; private set; }
        public string YCoor { get; private set; }
        public string Altitude { get; private set; }

        public void Position(string x, string y, string alt)
        {
            XCoor = x;
            YCoor = y;
            Altitude = alt;
        }
    }
}
