using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class Position : IPosition
    {
        public string XCoor { get; private set; }
        public string YCoor { get; private set; }
        public string Altitude { get; private set; }

        public void SetPosition(string x, string y, string alt)
        {
            XCoor = x;
            YCoor = y;
            Altitude = alt;
        }
    }
}
