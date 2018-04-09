using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface IFlightDataExtractor
    {
        void ExtractFlight(List<string> data,
            out string tag, ref IPosition pos, ref ITimestamp time);
    }
}
