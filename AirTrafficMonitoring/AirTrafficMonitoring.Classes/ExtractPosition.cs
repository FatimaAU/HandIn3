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
        public ExtractPosition(List<string> fullFlightInfo, out string x, out string y, out string alt)
        {
            x = fullFlightInfo[1];
            y = fullFlightInfo[2];
            alt = fullFlightInfo[3];
        }
    }
}
