using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    interface IFlight
    {
        void ExtractFlight(List<string> data,
            out string tag, ref Position pos, ref Timestamp time);
    }
}
