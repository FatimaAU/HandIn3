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
        public int XCoor { get; private set; }
        public int YCoor { get; private set; }
        public int Altitude { get; private set; }

        public void SetPosition(int x, int y, int alt)
        {
            XCoor = x;
            YCoor = y;
            Altitude = alt;
        }
    }
}
